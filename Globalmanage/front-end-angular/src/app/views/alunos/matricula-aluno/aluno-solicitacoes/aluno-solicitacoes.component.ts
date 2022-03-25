import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { AlunoStoreSelectors, AlunoStoreState } from 'src/app/_store/aluno-store';
import { from, Subscription } from 'rxjs';
import { AnimationsService } from 'src/app/services/animations.service';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { CartaoCreditoComponent } from '../aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/cartao-credito.component';
import { AuthService } from 'src/app/security/auth.service';
import { MsgCompraComponent } from '../aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/msg-compra/msg-compra.component';
import { TefService } from 'src/app/services/tef/tef.service';
import { Navigation, Router } from '@angular/router';
import { AsOpcoesComponent } from './as-opcoes/as-opcoes.component';
import { SolicitacoesService } from 'src/app/services/gerenciador/solicitacoes.service';
import { AnexoService } from 'src/app/services/anexo.service';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { FlexLayoutModule } from '@angular/flex-layout'
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { EventEmitterService  } from 'src/app/services/EventEmitterService';
import printJS from 'print-js';
import { SolicitacoesGeradasComponent } from './solicitacoes-geradas/solicitacoes-geradas.component';


@Component({
  selector: 'app-aluno-solicitacoes',
  templateUrl: './aluno-solicitacoes.component.html',
  styleUrls: ['./aluno-solicitacoes.component.scss']
})
export class AlunoSolicitacoesComponent implements OnInit, OnDestroy {
  @Input() id;
  @Output() onContinue: EventEmitter<any> = new EventEmitter();
  @Output() onUpdateMatricula: EventEmitter<any> = new EventEmitter();
  @Output() onEfetuadoAcao: EventEmitter<any> = new EventEmitter();
  error: boolean = false;
  isLoadingResults: boolean = false;
  info: any[] = null;
  cursoId: number = null;
  turmaId: number = null;
  cadastroAluno: any;
  aluno$: Subscription;
  matricula$: Subscription;
  hasTurma: boolean = false;
  novaMatricula: boolean = false;
  enableSalvar: boolean = true;
  selectionSolicitacao = new SelectionModel<any>(true, []);
  selectionHistorico = new SelectionModel<any>(true, []);
  realizarColumns: string[] = ['descricao', 'valor', 'options'];
  realizarSource = new MatTableDataSource();
  historicoColumns: string[] = ['data', 'descricao', 'pagamento', 'valor', 'options'];
  historicoColumnsalunos:  string[] = ['data', 'descricao', 'pagamento', 'valor'];
  historicoSource = new MatTableDataSource([
    { data: '2020-10-10', descricao: '', valor: null },
    { data: '2020-10-10', descricao: '', valor: null },
    { data: '2020-10-10', descricao: '', valor: null }
  ]);
  uploadSolicitacaoId: number = 0;

  matriculaId: number = null;
  logoLocalStorage = "";
  cursoLocalStorage = "";
  unidadeLocalStorage = "";

  isAluno: boolean = false;
  telaSolicitacoes = 0;
  solicitacoesRealizarPortalAluno: any[] = null;

  selection = new SelectionModel<any>(true, []);
  alunoId: number = null;

  constructor(
    private dialog: MatDialog,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private animationsService: AnimationsService,
    private solicitacoesService: SolicitacoesService,
    private authService: AuthService,
    private tefService: TefService,
    private router: Router,
    private anexoService: AnexoService,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private matriculaAlunoService: MatriculaAlunoService
  ) {
    // Get State
    this.logoLocalStorage = window.localStorage.getItem('logoLocalStorage');
    this.cursoLocalStorage = window.localStorage.getItem('cursoLocalStorage');
    this.unidadeLocalStorage = window.localStorage.getItem('unidadeLocalStorage');
    this.alunoId = Number(window.localStorage.getItem('alunoIdLocalStorage'));
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.matriculaId = state.matriculaId;
    }
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {

    this.isAluno = this.authService.isAluno();

    if(this.matriculaId == null && this.isAluno == true)
    {
      this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
    }

    this.loadData();
  }

  ngOnDestroy(): void {
    if(this.matricula$) this.matricula$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  async loadData(): Promise<void> {

    if(this.isAluno)
    {
      if(this.matriculaId != null)
      {
        await this.matriculaAlunoService.buscarPorId(this.matriculaId).subscribe(async val => {
          if (val['status'] === 'error')
          {
            this.error = true;
          }
          else
          {
            this.cursoId = val.cursoId;

            this.realizarSource.data = await this.getSolicitacoes();
            this.solicitacoesRealizarPortalAluno = this.realizarSource.data;
            this.historicoSource.data = await this.historicoSolicitacao();
          }
        });
      }
    }
    else
    {
      await this.getMatricula();
      this.realizarSource.data = await this.getSolicitacoes();
      this.historicoSource.data = await this.historicoSolicitacao();
    }
  }

  getMatricula(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
        if(val?.id) {
          this.cursoId = val?.cursoId;
          this.matriculaId = val?.id;
          resolve(val);
        } else reject();
      });
    });
  }

  getSolicitacoes(): Promise<any> {

    const usuario = this.authService.getToken();

    return this.solicitacoesService.buscarPorCursoId(this.cursoId, this.matriculaId, usuario?.user?.id).toPromise();
  }

  historicoSolicitacao(): Promise<any> {

    return this.solicitacoesService.historicoSolicitacao(this.matriculaId).toPromise();
  }

  openCartaoCredito(element?: any): void {
    const solicitacao = { valor: element?.valor, id: element?.id, matriculaId: this.matriculaId };
    const dialogRef = this.dialog.open(CartaoCreditoComponent, {
      width: '90vw',
      data: { solicitacao },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.onUpdateMatricula.emit(new Date());
     this.loadData();
    });
  }

  openMsg(success, onlyMsg, msg): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MsgCompraComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: {
        success,
        msg,
        onlyMsg
      },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

  pagar(element: any): void {

    if(element?.existePendencia) {
      this.openMsg(false, true, element?.mensagem);
      return;
    }

    if(!element?.valor) {
      const usuario = this.authService.getToken();
      const data = {
        usuarioLogadoId: usuario?.user?.id,
        solicitacaoId: element?.id,
        matriculaId: this.matriculaId
      };
      this.efetuarPagamento(data);
      return;
    }

    const isAluno: boolean = this.authService.isAluno();
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(AsOpcoesComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { isAluno },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {
      if(result === 1 || result === 2 ) {
        if(isAluno) this.openCartaoCredito(element);
        else this.pagamentoTEF(result, element);
      }

      if( result === 3) {
        const usuario = this.authService.getToken();
        const data = {
          usuarioLogadoId: usuario?.user?.id,
          solicitacaoId: element?.id,
          matriculaId: this.matriculaId,
          tipoPagamento: 3,
          valor: element?.valor
        };
        this.efetuarPagamento(data);
        return;
      }
    });
  }

  efetuarPagamento(data: any): void {
    this.solicitacoesService.efetuarSolicitacao(data).subscribe(val => {
      this.onUpdateMatricula.emit(new Date());
      this.loadData();
      if (val?.status === 'error') return;
      if (val?.id) this.animationsService.showSuccessSnackBar('Efetuado com sucesso');
    });
  }

  pagamentoTEF(result, element: any): void {
    let valorTotal: number = 0;
    if(element?.valor.toString().indexOf('.') > -1) {
      const casas = element?.valor.toString().split('.');
      valorTotal = parseInt(`${casas[0]}${casas[1].slice(0, 2).padEnd(2, '0')}`);
    } else {
      valorTotal = parseInt(`${element?.valor}00`);
    }

    const usuario = this.authService.getToken();
    const data = {
      valorTotal,
      usuarioLogadoId: usuario?.user?.id,
      credito: result === 1,
      matriculaId: this.matriculaId,
      solicitacaoId: element?.id
    }

    this.tefService.transacaoTef(data).subscribe(val => {
      if(val?.codigo > 0) this.openMsg(false, false,'Pagamento não aprovado');
      this.onUpdateMatricula.emit(new Date());
      this.loadData();
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selectionSolicitacao.selected.length;
    const numRows = this.realizarSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selectionSolicitacao.clear() :
      this.realizarSource.data.forEach(row => this.selectionSolicitacao.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {status
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionSolicitacao.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  desabilitarBtnVisualizar(element: any): boolean {
    if(element?.solicitacao?.isPreDefinida) {
      if(element?.statusPagamento !== 1) {
        return false
      } else {
        return true;
      }
    } else {
      return true;
    }
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  visualizar(element: any): void {
    const usuario = this.authService.getToken();
    const params = {
      solicitacaoId: element?.id,
      usuarioLogadoId: usuario?.user?.id,
      matriculaId: this.matriculaId
     };
    this.solicitacoesService.gerarSolicitacao(params);
  }

  loadFile(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      if(file.type != 'application/pdf') {
        this.animationsService.showErrorSnackBar('Insira somente arquivo PDF.');
        return;
      }
      this.animationsService.showProgressBar(true);
      const formData: FormData = new FormData();
      // Set FormData
      formData.append('file', file);
      formData.append('tipoAnexo', '0');
      formData.append('IsRecusado', 'false');
      formData.append('SolicitacaoAlunoId', this.uploadSolicitacaoId.toString());

      const data = {
        tipoAnexo: '0',
        SolicitacaoAlunoId: this.uploadSolicitacaoId.toString()
      }
      // console.log(data)

      this.anexoService.upload(formData).subscribe(val => {
        if (val?.status == 'done') this.animationsService.showProgressBar(false);
        this.onUpdateMatricula.emit(new Date());
        this.loadData();
      });
    };
  }

  download(id, descricao,): void {
    this.anexoService.download(id, `${descricao}.pdf`, 'application/pdf');
  }

  gotoMinhasAulas(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-curso-turma'], { state: { matriculaId } });
  }

  goToFinaceiro(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-financeiro-contrato'], { state: { matriculaId } });
  }

  goToSolicitacoes(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-solicitacoes'], { state: { matriculaId } });
  }

  goToDocumentos(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-documentos'], { state: { matriculaId } });
  }

  goToEja(matriculaId: number): void {
    this.router.navigate(['/alunos/eja-encceja'], { state: { matriculaId } });
  }

  mudarPainelSolicitacoes(tela: number): void {
    this.telaSolicitacoes = tela;
  }

  pagarsoli(soli: any): void {
    if(soli.existePendencia) {
      this.openMsg(false, true, soli.mensagem);
      return;
    }

    if(!soli.valor) {
      const usuario = this.authService.getToken();
      const data = {
        usuarioLogadoId: usuario?.user?.id,
        solicitacaoId: soli.id,
        matriculaId: this.matriculaId
      };
      this.efetuarPagamento(data);
      return;
    }

    const isAluno: boolean = this.authService.isAluno();
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(CartaoCreditoComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { isAluno },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {
      if(result === 1 || result === 2 ) {
        if(isAluno) this.openCartaoCreditosoli(soli);
        else this.pagamentoTEF(result, soli);
      }

      if( result === 3) {
        const usuario = this.authService.getToken();
        const data = {
          usuarioLogadoId: usuario?.user?.id,
          solicitacaoId: soli.id,
          matriculaId: this.matriculaId,
          tipoPagamento: 3,
          valor: soli.valor
        };
        this.efetuarPagamento(data);
        return;
      }
    });
  }

  pagarViaCartao(row, credito): void {
    if(!(this.selection.selected.length > 0)) {
      const valor: number = (row?.tipoSituacao === 2) ? row?.valorVencimento : row?.valor;
      const data = { valor, pagamentoIds: [row?.id], alunoId: this.alunoId };

      const isAluno: boolean = this.authService.isAluno();
      if(isAluno) this.openCartaoCreditosoli(data);
      else this.pagamentoTEFsoli(data, credito);

    } else {
      let pagamentoIds: number[] = [];
      let valor: number = 0;
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
        valor += (elem?.tipoSituacao === 2) ? elem?.valorVencimento : elem?.valor;
      });
      const data  = { valor, pagamentoIds: pagamentoIds, alunoId: this.alunoId };

      const isAluno: boolean = this.authService.isAluno();
      if(isAluno) this.openCartaoCreditosoli(data);
      else this.pagamentoTEFsoli(data, credito);
    }
  }

  enviarBoletoPorEmailOuRecalcular(tipoAcao, row, pdf?: boolean): void {

    if(!(this.selection.selected.length > 0)) {
      const data  = { tipoAcao, pdf,pagamentoIds: [row?.id], alunoId: this.alunoId };
      this.alunoFinanceiroService.enviarBoletoPorEmailOuRecalcular(data).subscribe((val: any[]) => {
        if(!(val?.length > 0)) return;
        if(pdf) {
          val.forEach(elem => {
            const b64Data = elem;
            const blob: any = this.b64toBlob(b64Data, 'application/pdf');
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            link.download = `${row?.descricao}.pdf`;
            link.click();
            this.onEfetuadoAcao.emit(new Date);
          })
        } else {
          printJS({ printable: val, type: 'raw-html'})
        }
        this.onEfetuadoAcao.emit(new Date);

        if(tipoAcao == 3){
          EventEmitterService.get('refreshPainelFinanceiro').emit(true);
        }

      });
    } else {
      let pagamentoIds: number[] = [];
      let pagamentoNames: string[] = [];
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
        pagamentoNames.push(elem.descricao);
      });
      const data  = { tipoAcao, pdf, pagamentoIds: pagamentoIds, alunoId: this.alunoId };
      this.alunoFinanceiroService.enviarBoletoPorEmailOuRecalcular(data).subscribe((val: any[]) => {
        if(!(val?.length > 0)) return;
        if(pdf) {
          val.forEach((elem, index) => {
            const b64Data = elem;
            const blob = this.b64toBlob(b64Data, 'application/pdf');
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            link.download = `${pagamentoNames[index]}.pdf`;
            link.click();
            // document.body.removeChild(link);
          });
        } else {
          printJS({ printable: val, type: 'raw-html'});
        }

        this.onEfetuadoAcao.emit(new Date);

        if(tipoAcao == 3){
          EventEmitterService.get('refreshPainelFinanceiro').emit(true);
        }

      });
    }
  }

  private b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize ) {
      const slice = byteCharacters.slice(offset, offset + sliceSize);

      const byteNumbers = new Array(slice.length);
      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }

      const byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
  }

  openCartaoCreditosoli(soli: any): void {
    const solicitacao = { valor: soli.valor, id: soli.id, matriculaId: this.matriculaId };
    const dialogRef = this.dialog.open(CartaoCreditoComponent, {
      width: '90vw',
      data: { solicitacao },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.onUpdateMatricula.emit(new Date());
     this.loadData();
    });
  }

  pagamentoTEFsoli(result, soli: any): void {
    let valorTotal: number = 0;
    if(soli.valor.toString().indexOf('.') > -1) {
      const casas = soli.valor.toString().split('.');
      valorTotal = parseInt(`${casas[0]}${casas[1].slice(0, 2).padEnd(2, '0')}`);
    } else {
      valorTotal = parseInt(`${soli.valor}00`);
    }

    const usuario = this.authService.getToken();
    const data = {
      valorTotal,
      usuarioLogadoId: usuario?.user?.id,
      credito: result === 1,
      matriculaId: this.matriculaId,
      solicitacaoId: soli.id
    }

    this.tefService.transacaoTef(data).subscribe(val => {
      if(val?.codigo > 0) this.openMsg(false, false,'Pagamento não aprovado');
      this.onUpdateMatricula.emit(new Date());
      this.loadData();
    });
  }

  pagarComBoleto(soli): void
  {

    const usuario = this.authService.getToken();
    const data = {
      usuarioLogadoId: usuario?.user?.id,
      solicitacaoId: soli?.id,
      matriculaId: this.matriculaId,
      tipoPagamento: 3,
      valor: soli?.valor
    };
    this.efetuarPagamento(data);

    const isAluno: boolean = this.authService.isAluno();
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(SolicitacoesGeradasComponent, {
      width: isMobileResolution ? '98vw' : '80vw',
      data: { isAluno },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {
      if(result === 1) {
        this.redirectTo('/alunos/matricula-aluno/aluno-financeiro-contrato');
      }
      if(result === 2) {
        return;
      }
    });
  }

  redirectTo(uri:string){
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this.router.navigate([uri]));
  }
}
