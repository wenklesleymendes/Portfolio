import { Component, OnInit, OnDestroy, Output } from '@angular/core';
import { CampanhaService } from 'src/app/services/gerenciador/campanha.service';
import { Subscription } from 'rxjs';
import { Navigation, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { FinanceiroStoreActions, FinanceiroStoreState, FinanceiroStoreSelectors } from 'src/app/_store/financeiro-store';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { AnexoService } from 'src/app/services/anexo.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { DocumentoAlunoService } from 'src/app/services/aluno/documento-aluno.service';
import { EventEmitter } from '@angular/core';
import { ContratoAlunoService } from 'src/app/services/aluno/contrato-aluno.service';
import { AuthService } from 'src/app/security/auth.service';
import * as moment from 'moment';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';

import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CartaoCreditoComponent } from './../aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/cartao-credito.component';
import { SelectionModel } from '@angular/cdk/collections';
import { DicaTefComponent } from './../aluno-financeiro-contrato/matricula-painel-pagamento/dica-tef/dica-tef.component';
import { TefService } from 'src/app/services/tef/tef.service';
import { MsgCompraComponent } from './../aluno-financeiro-contrato/matricula-caracteristicas-plano/cartao-credito/msg-compra/msg-compra.component';
import { MppComprovanteComponent } from './../aluno-financeiro-contrato/matricula-painel-pagamento/mpp-comprovante/mpp-comprovante.component';
import { MppDetalhePagamentoComponent } from './../aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-pagamento/mpp-detalhe-pagamento.component';
import { MppReciboComponent } from './../aluno-financeiro-contrato/matricula-painel-pagamento/mpp-recibo/mpp-recibo.component';
import printJS from 'print-js';
import { EventEmitterService } from 'src/app/services/EventEmitterService';
import { BoletoDigitalMobileComponent } from './boleto-digital-mobile/boleto-digital-mobile.component';
import { MatriculaCancelamentoComponent } from './matricula-cancelamento/matricula-cancelamento.component';


@Component({
  selector: 'app-aluno-financeiro-contrato',
  templateUrl: './aluno-financeiro-contrato.component.html',
  styleUrls: ['./aluno-financeiro-contrato.component.scss']
})
export class AlunoFinanceiroContratoComponent implements OnInit, OnDestroy {
  @Output() onUpdateMatricula: EventEmitter<any> = new EventEmitter();
  @Output() onEfetuadoAcao: EventEmitter<any> = new EventEmitter();
  isLoadingResults: boolean = false;
  error: boolean = false;
  campanhas: any[] = null;
  matricula$: Subscription;
  financeiro$: Subscription;
  planoPagamento$: Subscription = null;
  dadosSelecionados$: Subscription = null;
  dadosSelectionados: any = null;
  unidadeId: number = null;
  cursoId: number = null;
  matriculaId: number = null;
  tipoPagamento: number = null;
  hasDadosSelecionados: boolean = false;
  hasDadosCampanha: boolean = false;
  showIncluirApostila: boolean = false;
  exigeComprovante: boolean = false;
  canDownloadComprovante: boolean = false;
  form: FormGroup = null;
  valorApostila: number = null;
  hasMatricula: boolean = false;
  financeiroCadastrado: any = null;
  hasComprovante: boolean = false;
  gerando: boolean = false;
  formData: FormData = new FormData();
  existePendenciaContrato: boolean = false;
  isAluno: boolean = false;
  inicioContrato: moment.Moment = null;
  terminoContrato: moment.Moment = null;

  selection = new SelectionModel<any>(true, []);
  alunoId: number = null;
  dialogRefTEF: MatDialogRef<DicaTefComponent> = null
  usuarioLogadoId: any = null;
  optionExistente: number[] = Array(7).fill('').map((x, i) => i + 1);
  optionBtn: Array<{ situacao: number, options: number[] }> = [
    { situacao: 1, options: [5, 6] },
    { situacao: 2, options: [1, 2, 4] },
    { situacao: 3, options: [] },
    { situacao: 4, options: [1, 2, 4] },
    { situacao: 5, options: [3, 4] },
    { situacao: 6, options: [4, 7] }
  ];

  logoLocalStorage = "";
  cursoLocalStorage = "";
  unidadeLocalStorage = "";
  telaFinanceiro = 0;
  telaFinanceiroPortal = false;
  refreshEvento: any = null;
  existePendenciaFinanceira: boolean = false;

  constructor(
    private tefService: TefService,
    private dialog: MatDialog,
    private fb: FormBuilder,
    private campanhaService: CampanhaService,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private anexoService: AnexoService,
    private animationsService: AnimationsService,
    private documentoAlunoService: DocumentoAlunoService,
    private animationService: AnimationsService,
    private contratoAlunoService: ContratoAlunoService,
    private router: Router,
    private authService: AuthService,
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

  ngOnInit(): void {


    this.isAluno = this.authService.isAluno();

    if (this.matriculaId == null && this.isAluno == true) {
      this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
    }

    this.getMatricula();
    this.buildForm();

    this.refreshEvento = EventEmitterService.get('refreshPainelFinanceiro').subscribe(e => this.getCursoUnidadeIds());

    if (this.isAluno) {
      this.getCursoUnidadeIds();
    }
    else {
      this.loadData()
        .then(res => {
          this.hasMatricula = true;
          this.hasDadosSelecionados = true;
          this.existePendenciaContrato = res.existePendenciaContrato;
          var valorTipoPagamento = 1;
          if (res?.planoPagamentoAluno?.tipoPagamento == undefined) {
            valorTipoPagamento = res?.pagamento[0]?.tipoPagamento;
            this.onChangeTipoPagamento(valorTipoPagamento);
            this.form.get('incluirApostila').setValue(false);
            this.form.get('campanha').setValue(null);
            this.valorApostila = null;
            this.hasDadosSelecionados = false;
            this.showIncluirApostila = false;
            const data = { tipoPagamento: valorTipoPagamento, unidadeId: this.unidadeId, cursoId: this.cursoId }
            this.getCampanha(data);
          }
          else {
            valorTipoPagamento = res?.planoPagamentoAluno?.tipoPagamento;
          }
          const data = { tipoPagamento: valorTipoPagamento, unidadeId: this.unidadeId, cursoId: this.cursoId }
          this.getCampanha(data).then(() => {
            if (res?.planoPagamentoAluno?.tipoPagamento != undefined) {
              if (res?.planoPagamentoAluno?.campanhaId) this.form.get('campanha').setValue(res.planoPagamentoAluno.campanhaId);
              this.form.get('incluirApostila').disable();
              this.form.get('campanha').disable();
              this.exigeComprovante = false;

              const campanhaSelected = this.campanhas.find(elem => elem.id === res?.planoPagamentoAluno?.campanhaId);
              if (campanhaSelected?.exigeComprovante) this.canDownloadComprovante = true;
            }
          });
        })
        .catch(() => {
          this.getFinanceiro();
          this.getDadosSelecionados();
          this.onChangeTipoPagamento(1);
          this.hasMatricula = false;
        });
    }
  }

  async getCursoUnidadeIds(): Promise<void> {
    if (this.matriculaId != null) {
      await this.matriculaAlunoService.buscarPorId(this.matriculaId).subscribe(async val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          this.cursoId = val.cursoId;
          this.unidadeId = val.unidadeId;

          this.loadData()
            .then(res => {
              this.hasMatricula = true;
              this.hasDadosSelecionados = true;
              this.existePendenciaContrato = res.existePendenciaContrato;

              const data = { tipoPagamento: res?.planoPagamentoAluno?.tipoPagamento, unidadeId: this.unidadeId, cursoId: this.cursoId }
              this.getCampanha(data).then(() => {
                if (res?.planoPagamentoAluno?.campanhaId) this.form.get('campanha').setValue(res.planoPagamentoAluno.campanhaId);
                this.form.get('incluirApostila').disable();
                this.form.get('campanha').disable();
                this.exigeComprovante = false;

                const campanhaSelected = this.campanhas.find(elem => elem.id === res?.planoPagamentoAluno?.campanhaId);
                if (campanhaSelected?.exigeComprovante) this.canDownloadComprovante = true;
              });
            })
            .catch(() => {
              this.getFinanceiro();
              this.getDadosSelecionados();
              this.onChangeTipoPagamento(1);
              this.hasMatricula = false;
            });
        }
      });
    }
  }

  ngOnDestroy(): void {
    if (this.matricula$) this.matricula$.unsubscribe();
    if (this.financeiro$) this.financeiro$.unsubscribe();
    if (this.planoPagamento$) this.planoPagamento$.unsubscribe();
    this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteFinanceiro());
  }

  loadData(): Promise<any> {
    return new Promise((res, rej) => {
      this.isLoadingResults = true;
      this.alunoFinanceiroService.consultarPainelFinanceiro(this.matriculaId).subscribe(val => {
        this.isLoadingResults = false;
        if (val?.pagamento?.length > 0) {

          this.financeiroCadastrado = val;
          if (val.pagamento != null) {
            this.existePendenciaFinanceira = val.pagamento.find(x => x.tipoSituacao == 2 ||
              x.tipoSituacao == 4 ||
              x.tipoSituacao == 5 ||
              x.tipoSituacao == 6) ? true : false;
          }
          this.storeFinanceiro.dispatch(FinanceiroStoreActions.updateFinanceiroCadastrado({ payload: val }));
          res(val);
        } else {
          rej(null);
        }
      });
    });
  }

  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if (val?.id) {
        this.matriculaId = val?.id;
        this.unidadeId = val?.unidadeId;
        this.cursoId = val?.cursoId;

        var dataMatricula = new Date(val?.dataMatricula).toDateString();
        var dataTerminoFormatada = new Date(val?.dataTermino).toDateString();;
        this.inicioContrato = moment(dataMatricula);
        this.terminoContrato = moment(dataTerminoFormatada)
      }
    });
  }

  getFinanceiro(): void {
    this.financeiro$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiro)).subscribe(val => {
      if (val?.planoPagamento && val?.dadosSelecionados) {
        this.hasDadosSelecionados = true;
        this.getPlano(val);
      }
    });
  }

  getDadosSelecionados(): void {
    this.dadosSelecionados$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectDadosSelecionadosFinanceiro)).subscribe(val => {
      this.dadosSelectionados = val;
      this.form.get('incluirApostila').setValue(false);
      this.form.get('campanha').setValue(null);
    });
  }

  getCampanha(data: any): Promise<any> {
    return new Promise((res, rej) => {
      this.campanhaService.getVigente(data)
        .subscribe(val => {
          if (val?.status === 'error') {
            this.error = true;
            rej();
          }
          else this.campanhas = val;
          res(val);
        });
    });
  }

  buildForm(): void {
    this.form = this.fb.group({
      campanha: [null],
      incluirApostila: [false]
    });

    this.form.get('campanha').valueChanges.subscribe(val => {
      if (!(this.campanhas?.length > 0)) return;
      this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteFinanceiroComprovante());
      this.hasComprovante = false;
      this.formData.delete('file');
      this.formData.delete('descricao');
      this.formData.delete('tipoAnexo');
      this.formData.delete('MatriculaId');

      const campanha = this.campanhas.find(elem => elem.id === val);
      if (campanha) {
        this.storeFinanceiro.dispatch(FinanceiroStoreActions.updateCampanha({ payload: campanha }));
        this.hasDadosCampanha = true;
        this.exigeComprovante = campanha?.exigeComprovante;
      } else {
        this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteCampanha());
        this.hasDadosCampanha = false;
        this.exigeComprovante = false;
      }
    });
  }

  onChangeTipoPagamento(tipoPagamento): void {
    this.form.get('incluirApostila').setValue(false);
    this.form.get('campanha').setValue(null);
    this.valorApostila = null;
    this.hasDadosSelecionados = false;
    this.showIncluirApostila = false;
    this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteFinanceiro());
    const data = { tipoPagamento, unidadeId: this.unidadeId, cursoId: this.cursoId }
    this.getCampanha(data);
  }

  onEfetuadoPagamento(success: boolean = false): void {
    this.gerando = false;
    if (this.hasComprovante === false) {
      if (success) this.animationsService.showSuccessSnackBar('Salvo com sucesso');
      this.onUpdateMatricula.emit(new Date());
      this.ngOnInit();
      return;
    }
    this.anexoService.upload(this.formData).subscribe(val => {
      if (val && val['status'] && val['status'] == 'done') {
        this.animationsService.showSuccessSnackBar('Salvo com sucesso');
        this.onUpdateMatricula.emit(new Date());
        this.ngOnInit();
      }
    });
  }

  onEfetuandoPagamento(e: any): void {
    if (e?.efetuou) {
      this.onEfetuadoPagamento(e?.success);
    } else {
      this.gerando = true;
    }
  }

  loadingGerando(e: boolean): void {
    this.gerando = e;
  }

  getPlano(data: any): void {
    this.planoPagamento$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectPlanoPagamentoIndividualFinanceiro, { planoPagamentoId: data?.dadosSelecionados?.planoPagamentoId })).subscribe(val => {
      this.showApostila(val);
    });
    if (this.planoPagamento$) {
      this.planoPagamento$.unsubscribe();
      this.planoPagamento$ = null;
    }
  }

  showApostila(val): void {
    if (!val?.isentarMaterialDidatico) {
      this.showIncluirApostila = true;
      this.valorApostila = val?.valorMaterialDidatico;
    } else {
      this.showIncluirApostila = false;
      this.valorApostila = null;
    }
  }

  /**
   * @description Get file
   * @param {event} event - Input's event
   */
  loadFile(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      // Set FormData
      this.formData.append('file', file);
      this.formData.append('descricao', '');
      this.formData.append('tipoAnexo', '32');
      this.formData.append('MatriculaAlunoId', this.matriculaId.toString());
      const data = { hasComprovante: true };
      this.hasComprovante = true;
      this.storeFinanceiro.dispatch(FinanceiroStoreActions.updateFinanceiroComprovante({ payload: data }));
    };
  }

  downloadComprovante(): void {
    this.documentoAlunoService.downloadComprovante(this.matriculaId);
  }

  uploadContrato(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      if (file.type != 'application/pdf') {
        this.animationService.showErrorSnackBar('Insira somente arquivo PDF.');
        return;
      }

      const formData: FormData = new FormData();
      // Set FormData
      formData.append('file', file);
      formData.append('TipoAnexo', '33');
      formData.append('MatriculaAlunoId', this.matriculaId.toString());

      this.gerando = true;
      this.contratoAlunoService.uploadContrato(formData).subscribe(val => {

        if (val && val['status'] && val['status'] == 'done') {
          console.log('VAL', val)
          this.gerando = false;
          this.onUpdateMatricula.emit(new Date());
          if (val.response.id > 0) {
            this.existePendenciaContrato = false
          }
        }
      });
    };
  }

  gerarContrato(): void {
    const token = JSON.parse(window.localStorage.getItem('accessToken'));
    const usuarioLogadoId = token?.user.id;

    const data = {
      usuarioLogadoId,
      matriculaId: this.matriculaId
    };

    this.contratoAlunoService.gerarContrato(data);
  }

  downloadContrato(): void {
    this.contratoAlunoService.download(this.matriculaId);
  }

  imprimirContrato(): void {
    this.contratoAlunoService.imprimir(this.matriculaId);
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

  mudarPainelFinanceiro(tela: number): void {
    this.telaFinanceiro = tela;
  }

  private b64toBlob = (b64Data, contentType = '', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
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

  openDetalhePagamento(element): void {
    const dialogRef = this.dialog.open(MppDetalhePagamentoComponent, {
      width: '50vw',
      autoFocus: false,
      data: { element }
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }

  openDetalheCancelamento(): void {
    const dialogRef = this.dialog.open(MatriculaCancelamentoComponent, {
      width: '90vw',
      autoFocus: false,
      data: { matriculaId: this.matriculaId }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result)
        this.ngOnInit();
        this.onUpdateMatricula.emit(new Date());
    });
  }

  openCartaoCredito(pendencia: any): void {
    const dialogRef = this.dialog.open(CartaoCreditoComponent, {
      width: '75vw',
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

  validateOptions(row: any, option: number): boolean {
    if (row === 0 && this.selection.selected.length === 0) return true;

    if (this.selection.selected.length === 0) {
      const { tipoSituacao } = row;
      const tipoAcaoPermitida = this.optionBtn.find(elem => elem.situacao === tipoSituacao)?.options;
      if (!(tipoAcaoPermitida && tipoAcaoPermitida.length > 0)) return true;
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

  selectRow(row: any): void {
    this.selection.toggle(row);
    let allOptions: number[] = this.optionExistente;
    this.selection.selected.forEach((selected: any) => {
      const { tipoSituacao } = selected;
      const tipoAcaoPermitida = this.optionBtn.find(elem => elem.situacao === tipoSituacao)?.options;
      allOptions = allOptions.filter(elem => tipoAcaoPermitida.find((elemD2: any) => elem === elemD2))
    });

    const qtdIsento = this.selection.selected.filter(elem => elem?.tipoSituacao === 3).length;
    if (!(allOptions.length > 0) && (qtdIsento !== this.selection.selected.length)) this.animationsService.showErrorSnackBar('Atenção: Selecione SITUAÇÕES iguais para prosseguir!');
  }

  pagarViaCartao(row, credito): void {
    if (!(this.selection.selected.length > 0)) {
      const valor: number = (row?.tipoSituacao === 2) ? row?.valorVencimento : row?.valor;
      const data = { valor, pagamentoIds: [row?.id], alunoId: this.alunoId };

      const isAluno: boolean = this.authService.isAluno();
      if (isAluno) this.openCartaoCredito(data);
      else this.pagamentoTEF(data, credito);

    } else {
      let pagamentoIds: number[] = [];
      let valor: number = 0;
      this.selection.selected.forEach(elem => {
        pagamentoIds.push(elem.id);
        valor += (elem?.tipoSituacao === 2) ? elem?.valorVencimento : elem?.valor;
      });
      const data = { valor, pagamentoIds: pagamentoIds, alunoId: this.alunoId };

      const isAluno: boolean = this.authService.isAluno();
      if (!isAluno) this.openCartaoCredito(data);
      else this.pagamentoTEF(data, credito);
    }
  }

  pagamentoTEF(pendencia, credito): void {
    const valor: number = pendencia?.valor;

    let valorTotal: number = 0;
    if (valor.toString().indexOf('.') > -1) {
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
    if (credito) this.openDicaTef();
    this.tefService.transacaoTef(data).subscribe(val => {
      this.onEfetuadoAcao.emit(new Date);
      this.dialogRefTEF.close();
      if (val?.codigo > 0) this.openMsg(false, 'Pagamento não aprovado');
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

  enviarBoletoPorEmailOuRecalcular(tipoAcao, row, pdf?: boolean): void {
    if (!(this.selection.selected.length > 0)) {
      const data = { tipoAcao, pdf, pagamentoIds: [row?.id], alunoId: this.alunoId };
      this.alunoFinanceiroService.enviarBoletoPorEmailOuRecalcular(data).subscribe((val: any[]) => {
        if (!(val?.length > 0)) return;
        if (pdf) {
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
          printJS({ printable: val, type: 'raw-html' })
        }
        this.onEfetuadoAcao.emit(new Date);

        if (tipoAcao == 3) {
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
      const data = { tipoAcao, pdf, pagamentoIds: pagamentoIds, alunoId: this.alunoId };
      this.alunoFinanceiroService.enviarBoletoPorEmailOuRecalcular(data).subscribe((val: any[]) => {
        if (!(val?.length > 0)) return;
        if (pdf) {
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
          printJS({ printable: val, type: 'raw-html' });
        }

        this.onEfetuadoAcao.emit(new Date);

        if (tipoAcao == 3) {
          EventEmitterService.get('refreshPainelFinanceiro').emit(true);
        }

      });
    }
  }

  goToBoletoDigital(fin): void {

    if (fin.tipoSituacao == 6) {
      this.isLoadingResults = true;
      var pdf = true;
      if (!(this.selection.selected.length > 0)) {
        const data = { pdf, pagamentoIds: [fin?.id], alunoId: this.alunoId };
        this.alunoFinanceiroService.gerarBoletoResidual(data).subscribe((val: any[]) => {
          this.onEfetuadoAcao.emit(new Date);

          this.getCursoUnidadeIds();
          this.isLoadingResults = false;
          this.abrirModalBoletoDigital(val[0]);
        });
      } else {
        let pagamentoIds: number[] = [];
        let pagamentoNames: string[] = [];
        this.selection.selected.forEach(elem => {
          pagamentoIds.push(elem.id);
          pagamentoNames.push(elem.descricao);
        });
        const data = { pdf, pagamentoIds: pagamentoIds, alunoId: this.alunoId };
        this.alunoFinanceiroService.gerarBoletoResidual(data).subscribe((val: any[]) => {
          this.onEfetuadoAcao.emit(new Date);

          this.getCursoUnidadeIds();
          this.isLoadingResults = false;
          this.abrirModalBoletoDigital(val[0]);
        });
      }
    }
    else {
      this.abrirModalBoletoDigital(fin);
    }
  }

  abrirModalBoletoDigital(fin): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRef = this.dialog.open(BoletoDigitalMobileComponent, {
      width: isMobileResolution ? '95vw' : '90vw',
      autoFocus: false,
      data: { fin }
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
