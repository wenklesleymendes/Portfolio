import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MppDetalheEmailComponent } from './mpp-detalhe-email/mpp-detalhe-email.component';
import { MppDetalhePagamentoComponent } from './mpp-detalhe-pagamento/mpp-detalhe-pagamento.component';
import { MatTableDataSource } from '@angular/material/table';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { AnimationsService } from 'src/app/services/animations.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import printJS from 'print-js';
import { CartaoCreditoComponent } from '../matricula-caracteristicas-plano/cartao-credito/cartao-credito.component';
import { TefService } from 'src/app/services/tef/tef.service';
import { MsgCompraComponent } from '../matricula-caracteristicas-plano/cartao-credito/msg-compra/msg-compra.component';
import { AuthService } from 'src/app/security/auth.service';
import { DocumentoAlunoService } from 'src/app/services/aluno/documento-aluno.service';
import { MppReciboComponent } from './mpp-recibo/mpp-recibo.component';
import { DicaTefComponent } from './dica-tef/dica-tef.component';
import { MppComprovanteComponent } from './mpp-comprovante/mpp-comprovante.component';
import { MppBaixaPagamentoComponent } from './mpp-baixa-pagamento/mpp-baixa-pagamento.component';

@Component({
  selector: 'app-matricula-painel-pagamento',
  templateUrl: './matricula-painel-pagamento.component.html',
  styleUrls: ['./matricula-painel-pagamento.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class MatriculaPainelPagamentoComponent implements OnInit, OnDestroy {
  @Input() financeiroCadastrado: any;
  @Output() onEfetuadoAcao: EventEmitter<any> = new EventEmitter();
  @Output() loadingGerando: EventEmitter<any> = new EventEmitter();
  defaultColumns: string[] = null;
  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);
  aluno$: Subscription = null;
  alunoId: number = null;
  total: number = null;
  desconto: number = null;
  devido: number = null;
  expandedElement: any | null;
  matriculaId: number = null;
  matricula$: Subscription;
  dialogRefTEF: MatDialogRef<DicaTefComponent> = null
  optionExistente: number[] = Array(7).fill('').map((x,i) => i+1);
  token: any = null;
  usuarioLogadoId: any = null;
  isAluno: boolean = false;
  baixaManual: boolean = false;
  optionBtn: Array<{situacao: number, options: number[]}> = [
    { situacao: 1, options: [5,6] },
    { situacao: 2, options: [1,2,4] },
    { situacao: 3, options: [] },
    { situacao: 4, options: [1,2,4] },
    { situacao: 5, options: [3,4] },
    { situacao: 6, options: [4,7] }
 ];

  constructor(
    private dialog: MatDialog,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private animationsService: AnimationsService,
    private tefService: TefService,
    private authService: AuthService,
    private documentoAlunoService: DocumentoAlunoService
  ) {
    this.alunoId = Number(window.localStorage.getItem('alunoIdLocalStorage'));
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.isAluno = this.authService.isAluno();
    if(this.financeiroCadastrado) {
      this.dataSource.data = this.financeiroCadastrado?.pagamento;
      this.total = this.financeiroCadastrado?.total;
      this.desconto = this.financeiroCadastrado?.desconto;
      this.devido = this.financeiroCadastrado?.devido;
      this.getAluno();
      this.getMatricula();
      this.displayedColumns = this.validateColumns(this.financeiroCadastrado?.pagamento);

      this.token = JSON.parse(window.localStorage.getItem('accessToken'));
      this.usuarioLogadoId = this.token?.user.id;
      this.baixaManual = this.token?.user?.perfilUsuario?.baixaManual;
    }

    if(this.isAluno)
      this.defaultColumns = ['descricao', 'dataPagamento', 'valor', 'valorVencimento', 'data', 'situacao'];
    else
      this.defaultColumns = ['descricao', 'valor', 'desconto', 'promocao', 'tarifa', 'valorVencimento', 'data', 'numero', 'email', 'situacao', 'options'];
  }

  ngOnDestroy(): void {
    if(this.aluno$) this.aluno$.unsubscribe();
    if(this.matricula$) this.matricula$.unsubscribe();
  }

  corLinhaParcela(row): string {
    if(this.selection.isSelected(row)) {
      return 'bg-light-grey';
    }
    else {
      if(row?.tipoSituacao === 1 || row?.tipoSituacao === 3) return 'bg-light-green ';
      else if(row?.tipoSituacao === 4 || row?.tipoSituacao === 5) return 'bg-light-red ';
      else if(row?.tipoSituacao === 6) return 'bg-light-orange';
      else return '';
    }
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAluno(): void {
    this.aluno$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectCadastroAluno)).subscribe(val => {
      if(val?.id) {
        this.alunoId = val?.id;
      }
    });
  }

  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if(val?.id) {
        this.matriculaId = val?.id;
      }
    });
  }

  validateOptions(row: any, option: number): boolean {
    if(row === 0 && this.selection.selected.length === 0) return true;

    if(this.selection.selected.length === 0) {
      const { tipoSituacao } = row;
      const tipoAcaoPermitida = this.optionBtn.find(elem => elem.situacao === tipoSituacao)?.options;
      if(!(tipoAcaoPermitida && tipoAcaoPermitida.length > 0)) return true;
      return !tipoAcaoPermitida.find((elem: number) => elem === option);
    }

    let allOptions: number[] = this.optionExistente;
    this.selection.selected.forEach((selected: any) => {
      const { tipoSituacao } = selected;
      const tipoAcaoPermitida = this.optionBtn.find(elem => elem.situacao === tipoSituacao)?.options;
      allOptions = allOptions.filter(elem => tipoAcaoPermitida.find((elemD2: any) => elem === elemD2))
    });

    return !(allOptions.find((elem: number) => elem === option));
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    let numRows = 0;
    this.dataSource.data.forEach((elem: any) => {
      if(elem?.tipoSituacao === 2) numRows++;
    });
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
      this.isAllSelected() ?
      this.selection.clear() :
      this.selectAllAberto()
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {status
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  selectAllAberto(): void {
    this.selection.clear()
    this.dataSource.data.forEach((row: any) => {
      const { tipoSituacao } = row;
      if(tipoSituacao === 2) this.selection.select(row);
    });
  }

  selectRow(row: any): void {
    this.selection.toggle(row);
    let allOptions: number[] = this.optionExistente;
    this.selection.selected.forEach((selected: any) => {
      const { tipoSituacao } = selected;
      const tipoAcaoPermitida = this.optionBtn.find(elem => elem.situacao === tipoSituacao)?.options;
      allOptions = allOptions.filter(elem => tipoAcaoPermitida.find((elemD2: any) => elem === elemD2))
    });

    const qtdIsento = this.selection.selected.filter(elem => elem?.tipoSituacao === 3).length;
    if(!(allOptions.length > 0) && (qtdIsento !== this.selection.selected.length)) this.animationsService.showErrorSnackBar('Atenção: Selecione SITUAÇÕES iguais para prosseguir!');
  }

  colorPago(element): string {
    if(!element?.valorPago || !element?.valorVencimento || !element?.valor || !element?.dataVencimento || !element?.dataPagamento) return '';

    const { valorPago, valorVencimento, valor, tarifaBanco } = element;
    const dataVencimento = new Date(element.dataVencimento);
    const dataPagamento = new Date(element.dataPagamento);

    const tarifa = tarifaBanco ? tarifaBanco : 0;
    if(element.tipoSituacao == 1) return 'green';
    else if((dataPagamento <= dataVencimento) && ((valorPago + tarifa) >= valorVencimento)) return 'green';
    else if((dataPagamento > dataVencimento) && ((valorPago + tarifa) >= valor)) return 'green';
    else return 'red' ;
  }

  validateColumns(pagamentos?: any[]): string[] {
    if(!(pagamentos?.length > 0)) return this.defaultColumns;
    // const columns: string[] = ['descricao', 'valor', 'data'];
    var columns: string[] = null;

    if(this.isAluno)
      columns = ['descricao', 'data', 'valor', 'dataPagamento'];
    else
      columns = ['descricao', 'valor', 'data'];

    let hasPromocao: boolean = false;
    let hasDesconto: boolean = false;
    let hasTarifa: boolean = false;
    let hasValorVencimento: boolean = false;

    pagamentos.forEach(elem => {
      if(elem?.promocaoBolsaConvenio) hasPromocao = true;
      if(elem?.desconto) hasDesconto = true;
      if((elem?.tarifaBanco || elem?.formaPagamento === 1)) hasTarifa = true;
      if(elem?.valorVencimento) hasValorVencimento = true;
    });

    if(this.isAluno){

    }
    else {
      if(hasPromocao)columns.push('promocao');
      if(hasDesconto)columns.push('desconto');
      if(hasTarifa)columns.push('tarifa');
      if(hasValorVencimento)columns.push('valorVencimento');
    }

    if(this.isAluno)
    columns.push('situacao');
    else
    columns.push('numero', 'email', 'situacao', 'options');
    return columns;
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openDetalheEmail(id): void {
    const dialogRef = this.dialog.open(MppDetalheEmailComponent, {
      width: '50vw',
      autoFocus: false,
      data: { id }
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }

  openDetalhePagamento(element): void {
    const dialogRef = this.dialog.open(MppDetalhePagamentoComponent, {
      // width: '50vw',
      autoFocus: false,
      data: { element }
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }



  openBaixaPagamento(element, cartao): void {
    const dialogRef = this.dialog.open(MppBaixaPagamentoComponent, {
      width: '35vw',
      autoFocus: false,
      data: { element }
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }

  openCartaoCredito(pendencia: any): void {
    const dialogRef = this.dialog.open(CartaoCreditoComponent, {
      width: '90vw',
      data: { pendencia },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.onEfetuadoAcao.emit(new Date);
    });
  }

  openRecibo(recibo: any): void {
    const dialogRef = this.dialog.open(MppReciboComponent, {
      width: '50vw',
      data: { recibo },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDicaTef(): void {
    this.dialogRefTEF = this.dialog.open(DicaTefComponent, {
      width: '50vw',
      autoFocus: false
    });
    this.dialogRefTEF.afterClosed().subscribe(result => {
    });
  }

  pagarViaCartao(row, credito): void {
    if(!(this.selection.selected.length > 0)) {
      const valor: number = (row?.tipoSituacao === 2) ? row?.valorVencimento : row?.valor;
      const data = { valor, pagamentoIds: [row?.id], alunoId: this.alunoId };

      const isAluno: boolean = this.authService.isAluno();
      if(isAluno) this.openCartaoCredito(data);
      else this.pagamentoTEF(data, credito);

    } else {
      let pagamentoIds: number[] = [];
      let valor: number = 0;
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
        valor += (elem?.tipoSituacao === 2) ? elem?.valorVencimento : elem?.valor;
      });
      const data  = { valor, pagamentoIds: pagamentoIds, alunoId: this.alunoId };

      const isAluno: boolean = this.authService.isAluno();
      if(isAluno) this.openCartaoCredito(data);
      else this.pagamentoTEF(data, credito);
    }
  }

  reciboPagamentoMensalidade(row): void {
    if(!(this.selection.selected.length > 0)) {
      const data = { usuarioLogadoId: this.usuarioLogadoId, pagamentoIds: [row?.id], matriculaId: this.matriculaId };
      this.loadingGerando.emit(true);
      this.documentoAlunoService.reciboPagamentoMensalidade(data).subscribe(val => {
        this.loadingGerando.emit(false);
        if(val?.status === 'error') return;
        this.openRecibo(val);
      });
    } else {
      let pagamentoIds: number[] = [];
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
      });
      const data = { usuarioLogadoId: this.usuarioLogadoId, pagamentoIds: pagamentoIds, matriculaId: this.matriculaId };
      this.loadingGerando.emit(true);
      this.documentoAlunoService.reciboPagamentoMensalidade(data).subscribe(val => {
        this.loadingGerando.emit(false);
        if(val?.status === 'error') return;
        this.openRecibo(val);
      });
    }
  }

  enviarBoletoPorEmail(tipoAcao, row): void {
    if(!(this.selection.selected.length > 0)) {
      const data  = { tipoAcao, pagamentoIds: [row?.id], alunoId: this.alunoId };
      this.alunoFinanceiroService.enviarBoletoPorEmail(data).subscribe(val => {
        if(val?.status === 'error') return;
        this.animationsService.showSuccessSnackBar('Enviado com sucesso');
        this.onEfetuadoAcao.emit(new Date);
      });
    } else {
      let pagamentoIds: number[] = [];
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
      });
      const data  = { tipoAcao, pagamentoIds: pagamentoIds, alunoId: this.alunoId };
      this.alunoFinanceiroService.enviarBoletoPorEmail(data).subscribe(val => {
        if(val?.status === 'error') return;
        this.animationsService.showSuccessSnackBar('Enviado com sucesso');
        this.onEfetuadoAcao.emit(new Date);
      });
    }
  }

  gerarBoletoResidual(row, pdf?: boolean): void {
    if(!(this.selection.selected.length > 0)) {
      const data  = { pdf,pagamentoIds: [row?.id], alunoId: this.alunoId };
      this.alunoFinanceiroService.gerarBoletoResidual(data).subscribe((val: any[]) => {
        this.onEfetuadoAcao.emit(new Date);
      });
    } else {
      let pagamentoIds: number[] = [];
      let pagamentoNames: string[] = [];
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
        pagamentoNames.push(elem.descricao);
      });
      const data  = { pdf, pagamentoIds: pagamentoIds, alunoId: this.alunoId };
      this.alunoFinanceiroService.gerarBoletoResidual(data).subscribe((val: any[]) => {
        this.onEfetuadoAcao.emit(new Date);
      });
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

  pagamentoTEF(pendencia, credito): void {
    const valor: number = pendencia?.valor;

    let valorTotal: number = 0;
    if(valor.toString().indexOf('.') > -1) {
      const casas = valor.toString().split('.');
      valorTotal = parseInt(`${casas[0]}${casas[1].slice(0, 2).padEnd(2, '0')}`);
    } else {
      valorTotal = parseInt(`${valor}00`);
    }

    const data = {
      valorTotal,
      credito,
      pagamentoIds: pendencia?.pagamentoIds,
      matriculaId: this.matriculaId,
      usuarioLogadoId: this.usuarioLogadoId
    }
    if(credito) this.openDicaTef();
    this.tefService.transacaoTef(data).subscribe(val => {
      this.onEfetuadoAcao.emit(new Date);
      this.dialogRefTEF.close();
      if(val?.codigo > 0) this.openMsg(false, 'Pagamento não aprovado');
    });
  }

  openMsg(success, msg): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MsgCompraComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: { success, msg },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

  gerarComprovanteCartao(row: any): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MppComprovanteComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { row },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }
}
