import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { DeleteService } from 'src/app/services/delete.service';
import * as moment from 'moment';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { MsgCompraComponent } from './msg-compra/msg-compra.component';
import { Subscription } from 'rxjs';
import { select, Store } from '@ngrx/store';
import { AlunoStoreSelectors, AlunoStoreState } from 'src/app/_store/aluno-store';
import { AuthService } from 'src/app/security/auth.service';
import { AlunoFinanceiroContratoComponent } from 'src/app/views/alunos/matricula-aluno/aluno-financeiro-contrato/aluno-financeiro-contrato.component'
import { EventEmitterService } from 'src/app/services/EventEmitterService';

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

export const CompareTodosValores = (field1Control: string, valor: number): any => {
  return (formGroup: FormGroup) => {
    const field1: FormArray = formGroup.controls[field1Control] as FormArray;
    let valorInserido: number = 0;
    field1.value.forEach(elem => valorInserido += elem?.valor);
    if (valorInserido > valor) {
      field1.controls.forEach((elem: FormGroup) => elem?.controls['valor'].setErrors({ bigValue: true })); // Set error
    }
  };
}

@Component({
  selector: 'app-cartao-credito',
  templateUrl: './cartao-credito.component.html',
  styleUrls: ['./cartao-credito.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class CartaoCreditoComponent implements OnInit, OnDestroy {
  form: FormGroup = null;
  cartoes: FormArray;
  valorTotal: number = 0;
  opcoesParcelamento: number[] = [];
  today: Date = new Date();
  efetuandoPagamento: boolean = false;
  matricula$: Subscription;
  matriculaId: number = null;
  alunoFinanceiroContratoComponent: AlunoFinanceiroContratoComponent;

  constructor(
    public dialogRef: MatDialogRef<CartaoCreditoComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
    private fb: FormBuilder,
    private deleteService: DeleteService,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private formService: FormService,
    private animationsService: AnimationsService,
    private dialog: MatDialog,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    if(this.matricula$) this.matricula$.unsubscribe();
  }

  async loadData(): Promise<void> {
    await this.getMatricula();
    if(this.data?.solicitacao) {
      this.valorTotal = this.data?.solicitacao?.valor;
      this.opcoesParcelamento = Array(12).fill('').map((x,i) => i+1);
    } else {
      this.valorTotal = (this.data?.pendencia?.valor) ? this.data.pendencia.valor : this.calcularValorTotal(this.data?.pagamento, this.data?.campanha);
      this.opcoesParcelamento = this.validarOpcoesParcelamento(this.valorTotal);
    }
    this.buildForm();
    this.addCartoes(this.valorTotal);
  }

  buildForm(): void {
    this.form = this.fb.group({
      cartoes: this.fb.array([])
    });

    this.form.setValidators([CompareTodosValores('cartoes', this.valorTotal)]);
    this.cartoes = this.form.get('cartoes') as FormArray;
  }

  calcularValorTotal(pagamento: any, campanha: any): number {
    if(!pagamento) return 0;
    if(!campanha) campanha = {};
    const { valorTaxaMatricula, valorTotalInscricaoProva, valorTotalPlano, valorMaterialDidatico } = pagamento;
    const { descontoPlanoPagamento, descontoTaxaInscricaoProvas, descontoTaxaMatricula, descontoTaxaMateriaDidatico } = campanha;

    let valor: number = 0;
    if(valorTotalPlano)           descontoPlanoPagamento      ? (valor += (valorTotalPlano          - (valorTotalPlano          * (descontoPlanoPagamento      / 100)))) : (valor += valorTotalPlano);
    if(valorTaxaMatricula)        descontoTaxaMatricula       ? (valor += (valorTaxaMatricula       - (valorTaxaMatricula       * (descontoTaxaMatricula       / 100)))) : (valor += valorTaxaMatricula);
    if(valorTotalInscricaoProva)  descontoTaxaInscricaoProvas ? (valor += (valorTotalInscricaoProva - (valorTotalInscricaoProva * (descontoTaxaInscricaoProvas / 100)))) : (valor += valorTotalInscricaoProva);
    if(this.data?.incluirApostila && valorMaterialDidatico)     descontoTaxaMateriaDidatico ? (valor += (valorMaterialDidatico    - (valorMaterialDidatico    * (descontoTaxaMateriaDidatico / 100)))) : (valor += valorMaterialDidatico);

    return valor;
  }

  validarOpcoesParcelamento(valor: number): number[] {
    if(valor <= 180) return Array(1).fill('').map((x,i) => i+1);
    else if((valor>180) && (valor<=360)) return Array(2).fill('').map((x,i) => i+1);
    else if((valor>360) && (valor<=540)) return Array(4).fill('').map((x,i) => i+1);
    else if((valor>540) && (valor<=700)) return Array(5).fill('').map((x,i) => i+1);
    else if((valor>700) && (valor<=880)) return Array(6).fill('').map((x,i) => i+1);
    else return Array(12).fill('').map((x,i) => i+1);
  }

  closeDatePicker(dateString: string, dp: any, index: number) {
    const formArray: FormArray = this.form.get('cartoes') as FormArray;
    const control: FormControl = formArray.at(index).get('validade') as FormControl;
    const date: moment.Moment = moment(dateString);
    date.endOf('month');
    control.setValue(date.format());
    dp.close();
  }

  addCartoes(valor:number = null): void {
    this.cartoes.push(
      this.fb.group({
        valor: [{disabled: true, value: valor}, [Validators.required]],
        parcela: [1, [Validators.required]],
        numero: [null, [Validators.required]],
        nome: [null, [Validators.required]],
        validade: [null, [Validators.required]],
        codigo: [null, [Validators.required]]
      })
    );
  }

  removeDoFormArray(controls: any, index: number) {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      controls as FormArray;
      controls.removeAt(index);
    });
  }

  getFormArray(index: number): any {
    const formArray: FormArray = this.form.get('cartoes') as FormArray;
    return formArray.at(index).value;
  }

  hasError(index: number, error: string): boolean {
    const formArray: FormArray = this.form.get('cartoes') as FormArray;
    return formArray.at(index).get('valor').hasError(error);
  }

  efetuarPagamentoCartaoCredito(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formArray: FormArray = this.form.get('cartoes') as FormArray;
    const formGroup: FormGroup = formArray.at(0) as FormGroup;
    const { valor, parcela, numero, nome, validade, codigo } = formGroup.getRawValue();

    const usuario = this.authService.getToken();
    const data = {
      usuarioLogadoId: usuario?.user?.id,
      matriculaId: this.matriculaId,
      quantidadeParcela: parcela,
      valorTotal: valor,
      dadosCartaoAluno: {
        id: 0,
        numeroCartao: numero,
        nomePessoa: nome,
        mesValidade: new Date(validade).getUTCMonth().toString(),
        anoValidade: new Date(validade).getUTCFullYear().toString(),
        codigoSeguranca: codigo
      }
    }

    if(this.data?.solicitacao) data['solicitacaoId'] = this.data?.solicitacao?.id;
    else data['pagamentoIds'] = this.data?.pendencia?.pagamentoIds;

    this.efetuandoPagamento = true;
    this.alunoFinanceiroService.efetuarPagamentoCartaoCredito(data).subscribe(val => {
      this.efetuandoPagamento = false;
      if (val?.status === 'error') return;
      else if(val?.retornoCartao === 2 )  this.openMsg(false, 'Transação não autorizada. Entre em contato com seu banco emissor ou por favor tente outro cartão.');
      else if(val?.retornoCartao === 3 )  this.openMsg(false, 'Cartão expirado.');
      else if(val?.retornoCartao === 4 )  this.openMsg(false, 'Cartão bloqueado.');
      else if(val?.retornoCartao === 5 )  this.openMsg(false, 'Contate administradora do cartão.');
      else if(val?.retornoCartao === 6 )  this.openMsg(false, 'Cartão cancelado.');
      else if(val?.retornoCartao === 7 )  this.openMsg(false, 'Verificar os dados digitados. Tente outro cartão ou entre em contato com seu banco emissor.');
      else if(val?.retornoCartao === 8 )  this.openMsg(false, 'Transação já confirmada.');
      else if(val?.retornoCartao === 9 )  this.openMsg(false, 'Limite excedido, tente outro meio de pagamento.');
      else if(val?.retornoCartao === 10 ) this.openMsg(false, 'Validade do cartão expirada.');
      else if(val?.retornoCartao === 11 ) this.openMsg(false, 'Código segurança inválido.');
      else {
        EventEmitterService.get('refreshPainelFinanceiro').emit(true);
        this.openMsg(true, 'Transação realizada com sucesso');
      }
    })
  }

  openMsg(success, msg): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MsgCompraComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: { success, msg },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {
      if(result) this.dialogRef.close(true);
      else {
        const formArray: FormArray = this.form.get('cartoes') as FormArray;
        const formGroup: FormGroup = formArray.at(0) as FormGroup;
        formGroup.patchValue({
          valor: this.valorTotal,
          parcela: 1,
          numero: null,
          nome: null,
          validade: null,
          codigo: null
        });
      }
    });
  }

  getMatricula(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
        if(val == undefined)  {
          this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
          resolve(val);
        }
        if(val?.id) {
          this.matriculaId = val?.id;
          resolve(val);
        } else reject();
      });
    });
  }
}
