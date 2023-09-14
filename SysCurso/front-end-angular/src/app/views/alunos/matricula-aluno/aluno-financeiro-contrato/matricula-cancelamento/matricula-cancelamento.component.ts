import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatriculaCancelamentoService } from 'src/app/services/aluno/matricula-cancelamento.service';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatTableDataSource } from '@angular/material/table';
import { MppDetalheEmailComponent } from 'src/app/views/alunos/matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-email/mpp-detalhe-email.component';
import { MppDetalhePagamentoComponent } from 'src/app/views/alunos/matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/mpp-detalhe-pagamento/mpp-detalhe-pagamento.component';
import { SelectionModel } from '@angular/cdk/collections';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { EventEmitterService } from 'src/app/services/EventEmitterService';
import { AuthService } from 'src/app/security/auth.service';
import { TefService } from 'src/app/services/tef/tef.service';
import { DicaTefComponent } from 'src/app/views/alunos/matricula-aluno/aluno-financeiro-contrato/matricula-painel-pagamento/dica-tef/dica-tef.component';
import { MsgCompraComponent } from '../matricula-caracteristicas-plano/cartao-credito/msg-compra/msg-compra.component';
import { CartaoCreditoComponent } from '../matricula-caracteristicas-plano/cartao-credito/cartao-credito.component';
import { MatriculaCancelamentoAutorizacaoComponent } from './matricula-cancelamento-autorizacao/matricula-cancelamento-autorizacao.component';
import { MatriculaEfetuaCancelamentoComponent } from './matricula-efetua-cancelamento/matricula-efetua-cancelamento.component';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { AnexoService } from 'src/app/services/anexo.service';
import printJS from 'print-js';

@Component({
  selector: 'app-matricula-cancelamento',
  templateUrl: './matricula-cancelamento.component.html',
  styleUrls: ['./matricula-cancelamento.component.scss']
})
export class MatriculaCancelamentoComponent implements OnInit {

  @Output() onEfetuadoAcao: EventEmitter<any> = new EventEmitter();
  update = false;
  error = false;
  isLoadingResults = false;
  disableBtnClose = false;
  sending = false;
  isento: boolean = false;
  desabilitarForm = false;
  dentroPrazoCancelamento = true;
  valorMulta = null;
  form: FormGroup;
  pagoTotal = true;
  sendingAnexo: boolean = false;
  atestadoAnexado: boolean = false;
  valorEmAtraso = null;
  defaultColumns: string[] = null;
  selection = new SelectionModel<any>(true, []);
  dataSource = new MatTableDataSource();
  cancelamentoMatricula: any[];
  disableButton = false;
  expandedElement: any | null;
  optionExistente: number[] = Array(7).fill('').map((x, i) => i + 1);
  matriculaId: number = null;
  alunoId: number = null;
  atestadoId: Number = null;
  cartaCancelamentoId: Number = null;
  token: any = null;
  usuarioLogadoId: any = null;
  optionBtn: Array<{ situacao: number, options: number[] }> = [
    { situacao: 1, options: [5] },
    { situacao: 2, options: [1, 2, 4, 6] },
    { situacao: 3, options: [] },
    { situacao: 4, options: [1, 2, 4, 6] },
    { situacao: 5, options: [3, 4, 6] },
    { situacao: 6, options: [4, 6, 7] }
  ];
  dialogRefTEF: MatDialogRef<DicaTefComponent> = null;
  displayedColumns: string[] = [
    'descricao',
    'valor',
    'data',
    'numero',
    'email',
    'situacao',
    'options'
  ];
  constructor(
    private animationsService: AnimationsService,
    private formService: FormService,

    public dialogRef: MatDialogRef<MatriculaCancelamentoComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private fb: FormBuilder,
    private matriculaCancelamentoService: MatriculaCancelamentoService,
    private dialog: MatDialog,
    private authService: AuthService,
    private tefService: TefService,
    private anexoService: AnexoService,
    private alunoFinanceiroService: AlunoFinanceiroService,
  ) { }

  ngOnInit(): void {
    this.getCancelamento(this.data.matriculaId);
    this.buildForm();
    this.token = JSON.parse(window.localStorage.getItem('accessToken'));
    this.usuarioLogadoId = this.token?.user.id;
  }

  buildForm(): void {
    this.form = this.fb.group({
      dataCancelamento: [null],
      motivoCancelamento: [null, [Validators.required]],
      comentario: [null, [Validators.required]],
      isentarCancelamento: [null],
      motivoIsencao: [null],
      justificativa: [null],
      autorizador: [null],
    });
  }

  getCancelamento(matriculaId) {
    this.matriculaCancelamentoService.buscarPorMatriculaId(matriculaId).subscribe(val => {
      if (!val || val['status'] === 'error') {
        this.error = true
        return;
      }
      this.form.get('dataCancelamento').setValue(val.dataCancelamento);
      this.form.get('dataCancelamento').disable();
      this.cancelamentoMatricula = val;
      this.cancelamentoMatricula["usuarioLogadoId"] = this.usuarioLogadoId;
      this.dentroPrazoCancelamento = val.dentroPrazoCancelamento;
      this.valorMulta = val.valorMultaCancelamento;
      this.valorEmAtraso = val.valorEmAtraso;
      this.dataSource.data = val?.parcelasEmAtraso;
      this.pagoTotal = val?.pagoTotal;
      this.matriculaId = val?.matriculaAlunoId;
      this.alunoId = val?.matriculaAluno?.alunoId;
      this.isento = val.isentarCancelamento == true;
      this.atestadoId = val?.anexoAtestadoMedicoId;
      this.cartaCancelamentoId = val?.anexoCartaCancelamentoId;
      if (val.id > 0) {
        this.form.get('motivoCancelamento').setValue(val.motivoCancelamento);
        this.form.get('comentario').setValue(val.comentario);
        this.form.get('isentarCancelamento').setValue(val.isentarCancelamento);
        this.form.get('motivoIsencao').setValue(val.motivoIsencao);
        this.form.get('isentarCancelamento').disable();
        this.form.get('motivoIsencao').disable();
        this.form.get('justificativa').disable();
        this.form.get('autorizador').disable();

        if (val.cancelamentoIsencao) {
          this.form.get('justificativa').setValue(val.cancelamentoIsencao.justificativa);
          this.form.get('autorizador').setValue(val.cancelamentoIsencao.nomeUsuario);
        }
        if (val.statusCancelamento > 2) {
          this.form.get('comentario').disable();
          this.form.get('motivoCancelamento').disable();
        }
      }
    });
  }

  efetuarCancelamento() {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    this.disableButton = true;

    const data = this.MontarModelCancelamento();
    if (this.cancelamentoMatricula["anexoCartaCancelamentoId"] > 0) {
      this.cancelamentoMatricula["validar"] = true;
      this.matriculaCancelamentoService.efetuarCancelamento(data).subscribe(val => {
        if (!val || val['status'] === 'error') {
          this.error = true
          this.disableButton = false;
          return;
        }
        this.animationsService.showSuccessSnackBar('Cancelamento efetuado.');
        EventEmitterService.get('refreshPainelAluno').emit(this.cancelamentoMatricula["matriculaAlunoId"]);
        EventEmitterService.get('refreshPainelFinanceiro').emit(true);
        this.closeModal()

      });
    }
    else {
      const token = JSON.parse(window.localStorage.getItem('accessToken'));
      const matriculaAlunoId = data.matriculaAlunoId;
      const motivoCancelamento = data.motivoCancelamento;
      this.matriculaCancelamentoService.gerarCartaCancelamento(matriculaAlunoId, token?.user.id, motivoCancelamento);
      this.abritModalEfetuarCancelamento(data);
    }
  }

  abritModalEfetuarCancelamento(data) {
    const dialogRef = this.dialog.open(MatriculaEfetuaCancelamentoComponent, {
      width: '45vw',
      disableClose: true,
      autoFocus: false,
      data: { cancelamentoMatricula: data }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result > 0) {
        this.update = true;
        this.cartaCancelamentoId = result;
        this.cancelamentoMatricula["anexoCartaCancelamentoId"] = result;
        this.isLoadingResults = false;
        this.update = true;
        this.closeModal();
      }
    });
  }

  MontarModelCancelamento() {
    const { motivoCancelamento, comentario, isentarCancelamento, motivoIsencao } = this.form.value;
    const matriculaAluno = this.cancelamentoMatricula["matriculaAluno"]
    const data = {
      "id": this.cancelamentoMatricula["id"],
      "matriculaAlunoId": this.cancelamentoMatricula["matriculaAlunoId"],
      "alunoId": matriculaAluno["alunoId"],
      "dataCancelamento": this.cancelamentoMatricula["dataCancelamento"],
      "motivoCancelamento": motivoCancelamento,
      "comentario": comentario,
      "valorMultaCancelamento": this.cancelamentoMatricula["valorMultaCancelamento"],
      "valorEmAtraso": this.cancelamentoMatricula["valorEmAtraso"],
      "isentarCancelamento":this.cancelamentoMatricula["isentarCancelamento"],
      "motivoIsencao": this.cancelamentoMatricula["motivoIsencao"],
      "usuarioIsencaoId": this.cancelamentoMatricula["usuarioIsencaoId"],
      "usuarioLogadoId": this.cancelamentoMatricula["usuarioLogadoId"],
      "anexoAtestadoMedicoId": this.cancelamentoMatricula["anexoAtestadoMedicoId"],
      "anexoCartaCancelamentoId": this.cancelamentoMatricula["anexoCartaCancelamentoId"],
      "pagoTotal": this.cancelamentoMatricula["pagoTotal"],
      "dentroPrazoCancelamento": this.cancelamentoMatricula["dentroPrazoCancelamento"]
    }
    return data;
  }

  download(tipo): void {

    let nameDownloadFile;
    let anexoId;
    switch (tipo) {
      case 1:
        nameDownloadFile = `${"CartaCancelamento"}-${this.cancelamentoMatricula["numeroMatricula"]}.pdf`;
        anexoId = this.cartaCancelamentoId;
        break;
      case 2:
        nameDownloadFile = `${"AtestadoMedicoMatricula"}-${this.cancelamentoMatricula["numeroMatricula"]}.pdf`;
        anexoId = this.atestadoId;
        break;
    }

    this.anexoService.download(anexoId, nameDownloadFile, "application/pdf");
  }

  uploadAtestado(event): void {
    let reader = new FileReader();
    // Select file
    let file: File = event.target.files[0];

    const maxSize: number = 50 * 1024 * 1024;
    if (file?.size > maxSize) {
      this.animationsService.showErrorSnackBar('Arquivo ultrapassa 50mb');
      return;
    }

    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      this.sendingAnexo = true;
      const formData: FormData = new FormData();

      if (file.type != 'application/pdf') {
        this.animationsService.showErrorSnackBar('Insira somente arquivo PDF.');
        return;
      }

      // Set FormData
      formData.append('file', file);
      formData.append('tipoAnexo', "37");
      formData.append('MatriculaAlunoId', this.cancelamentoMatricula["matriculaAlunoId"]);

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          this.sendingAnexo = false;
          this.atestadoAnexado = true;
          this.cancelamentoMatricula["anexoAtestadoMedicoId"] = val?.response?.id;
          this.atestadoId = val?.response?.id;

          const data = this.MontarModelCancelamento();
          this.cancelamentoMatricula["validar"] = false;
          this.matriculaCancelamentoService.efetuarCancelamento(data).subscribe(val => {
            if (!val || val['status'] === 'error') {
              this.error = true
              this.disableButton = false;
              return;
            }
          });

          if (val?.status == 'error') this.sendingAnexo = false;
        }
      });
    };
  }
  //#region Métodos Importados do Painel Financeiro
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.selectAllPendente()
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    let numRows = 0;
    this.dataSource.data.forEach((elem: any) => {
      if (elem?.tipoSituacao !== 1 && elem?.tipoSituacao !== 3) numRows++;
    });
    return numSelected === numRows;
  }

  selectAllPendente(): void {
    this.selection.clear()
    this.dataSource.data.forEach((row: any) => {
      const { tipoSituacao } = row;
      if (tipoSituacao !== 1 && tipoSituacao !== 3) this.selection.select(row);
    });
  }

  checkboxLabel(row?: any): string {
    status
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  closeModal() {
    this.dialogRef.close(this.update);
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
    if (!(allOptions.length > 0) && (qtdIsento !== this.selection.selected.length + 1)) this.animationsService.showErrorSnackBar('Atenção: Selecione SITUAÇÕES iguais para prosseguir!');
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

  pagarViaCartao(pagamentoIds, valor, credito): void {

    const data = { valor, pagamentoIds: pagamentoIds, alunoId: this.alunoId };

    const isAluno: boolean = this.authService.isAluno();
    if (isAluno) this.openCartaoCredito(data);
    else this.pagamentoTEF(data, credito);
  }

  openCartaoCredito(pendencia: any): void {
    const dialogRef = this.dialog.open(CartaoCreditoComponent, {
      width: '90vw',
      data: { pendencia },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.onEfetuadoAcao.emit(new Date);
      this.update = true;
    });
  }

  autorizarIsencao(row) {

    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    else {
      const { motivoCancelamento, comentario } = this.form.value;
      this.cancelamentoMatricula["motivoCancelamento"] = motivoCancelamento;
      this.cancelamentoMatricula["comentario"] = comentario;

      let parcelas: any[] = [];
      if (this.selection.selected.length > 0) {
        parcelas = this.selection.selected
      } else {
        parcelas.push(row);
      }
      const dialogRef = this.dialog.open(MatriculaCancelamentoAutorizacaoComponent, {
        width: '45vw',
        autoFocus: false,
        data: { cancelamentoMatricula: this.cancelamentoMatricula, parcelas }
      });
      dialogRef.afterClosed().subscribe(result => {
        if (result != false) {
          this.getCancelamento(result);
          this.update = true;
          this.dialogRef.disableClose = true;
          this.disableBtnClose = true;
        }
      });
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
      else { this.update = true; }

    });
  }

  openDicaTef(): void {
    this.dialogRefTEF = this.dialog.open(DicaTefComponent, {
      width: '50vw',
      autoFocus: false
    });
    this.dialogRefTEF.afterClosed().subscribe(result => {
      this.update = true;
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

  acoesFinanceiras(tipoAcao, row, pdf?: boolean) {
    let pagamentoIds: number[] = [];
    let pagamentoNomes: string[] = [];
    let multaNova: boolean = false;
    let valor: number = 0;

    if (this.selection.selected.length > 0) {
      this.selection.selected.forEach(elem => {
        if (elem.id > 0) {
          pagamentoIds.push(elem.id);
          pagamentoNomes.push(elem.descricao);
          valor += elem?.valor;
        }
        else
          multaNova = true;
      });
    } else {
      if (row?.id > 0) {
        pagamentoIds.push(row?.id);
        pagamentoNomes.push(row?.descricao)
        valor = row?.valor;
      } else {
        multaNova = true;
      }
    }
    if (multaNova && tipoAcao !== 6) {
      const data = this.MontarModelCancelamento();
      this.matriculaCancelamentoService.gerarMultaCancelamento(data).subscribe(val => {
        if (!val || val['status'] === 'error') {
          this.error = true
          this.disableButton = false;
          this.animationsService.showErrorSnackBar('Ocorreu um erro ao registrar a multa.');
        }

        this.dialogRef.disableClose = true;
        this.disableBtnClose = true;
        pagamentoIds.push(val?.id);
        this.gereciadorAcoesFinanceiraS(tipoAcao, pagamentoIds, pagamentoNomes, pdf, valor);
      });
    }
    else {
      this.gereciadorAcoesFinanceiraS(tipoAcao, pagamentoIds, pagamentoNomes, pdf, valor)
    }
  }

  gereciadorAcoesFinanceiraS(tipoAcao, pagamentoIds: Number[], pagamentoNomes: string[], pdf?: boolean, valor?: Number) {

    switch (tipoAcao) {
      //Enviar por e-mail
      case 1:
        this.enviarBoletoPorEmail(tipoAcao, pagamentoIds);
        break;
      //Imprimir/Download boleto
      case 2:
      //Recalcular boleto
      case 3:
        this.enviarBoletoPorEmailOuRecalcular(tipoAcao, pagamentoIds, pagamentoNomes, pdf);
        break;
      //Pagar via crédito
      case 4:
      //Pagar via débido
      case 5:
        this.pagarViaCartao(pagamentoIds, valor, tipoAcao === 4);
        break;
      case 7:
        this.gerarBoletoResidual(pagamentoIds, pdf);
        break;

      default:
        break;
    }
  }

  enviarBoletoPorEmail(tipoAcao, pagamentoIds: Number[]): void {

    const data = { tipoAcao, pagamentoIds: pagamentoIds, alunoId: this.alunoId };

    this.alunoFinanceiroService.enviarBoletoPorEmail(data).subscribe(val => {
      if (val?.status === 'error') return;
      this.animationsService.showSuccessSnackBar('Enviado com sucesso');
      this.onEfetuadoAcao.emit(new Date);
      this.update = true;
    });
  }

  gerarBoletoResidual(pagamentoIds, pdf?: boolean): void {
    const data = { pdf, pagamentoIds: pagamentoIds, alunoId: this.alunoId };
    this.alunoFinanceiroService.gerarBoletoResidual(data).subscribe((val: any[]) => {
      this.onEfetuadoAcao.emit(new Date);
      this.update = true;
    });
  }

  enviarBoletoPorEmailOuRecalcular(tipoAcao, pagamentoIds: Number[], pagamentoNames: string[], pdf?: boolean): void {

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
        });
      } else {
        printJS({ printable: val, type: 'raw-html' });
      }
      this.onEfetuadoAcao.emit(new Date);
      this.update = true;
    });
  }
  //#endregion

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
}
