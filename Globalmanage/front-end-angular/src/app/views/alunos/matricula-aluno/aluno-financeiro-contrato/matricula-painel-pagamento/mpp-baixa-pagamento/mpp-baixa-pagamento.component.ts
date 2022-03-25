import { CurrencyPipe } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { DatePtBrPipe } from 'src/app/utils/pipes/date-pt-br.pipe';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import internal from 'events';
import { FormService } from 'src/app/services/form.service';
import * as moment from 'moment';

@Component({
  selector: 'app-mpp-baixa-pagamento',
  templateUrl: './mpp-baixa-pagamento.component.html',
  styleUrls: ['./mpp-baixa-pagamento.component.scss']
})
export class MppBaixaPagamentoComponent implements OnInit {
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['label', 'value'];
  dataSource = new MatTableDataSource([]);
  cartao: boolean = false;
  today: Date = new Date();
  valorTotal: number = 0;
  multiplasParcelas: boolean = false;
  pagamentoIds:any;
  valorParcela:number=0;
  constructor(
    public dialogRef: MatDialogRef<MppBaixaPagamentoComponent>,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private animationsService: AnimationsService,
    private currencyPipe: CurrencyPipe,
    private datePtBrPipe: DatePtBrPipe,
    private formService: FormService,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }


  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getData();
    this.buildForm();
    if (this.data?.pendencia?.pagamentoIds.length > 1)
      this.multiplasParcelas = true;
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      tipoPagamento: ["0", [Validators.required]],
      valorPago: [null, [Validators.required]],
      dataPagamento: [null, [Validators.required]],
      bandeiraCartao: [null],
      codigoAutorizacao: [null],
      quantidadeParcela: [null],
      valorParcela: [null],
      numeroCartao: [null],
      acquirer: [null],
    });

    this.form.get('valorPago').setValue(this.valorTotal);
    this.form.get('valorPago').disable();
    this.form.get('valorParcela').disable();
    this.form.get('tipoPagamento').valueChanges.subscribe(async val => {
      this.cartao = false;
          this.form.get('tipoPagamento').setValidators(Validators.required);
      switch (val) {
        case 1:
        case 2:
          this.cartao = true;
          this.form.get('bandeiraCartao').setValidators(Validators.required);
          this.form.get('codigoAutorizacao').setValidators(Validators.required);
          this.form.get('quantidadeParcela').setValidators(Validators.required);
          this.form.get('valorParcela').setValidators(Validators.required);
          this.form.get('numeroCartao').setValidators(Validators.required);
          this.form.get('acquirer').setValidators(Validators.required);
          break;
        case 3:
          this.cartao = false;
          this.form.get('bandeiraCartao').clearValidators();
          this.form.get('codigoAutorizacao').clearValidators();
          this.form.get('quantidadeParcela').clearValidators();
          this.form.get('valorParcela').clearValidators();
          this.form.get('numeroCartao').clearValidators();
          this.form.get('acquirer').clearValidators();
        default:
          break;
      }
    });

    this.form.get('quantidadeParcela').valueChanges.subscribe(async val => {
      this.valorParcela = this.valorTotal / val;
      this.form.get('valorParcela').setValue(this.valorParcela);

    });

    // this.form.get('mesValidade').subscribe(async val => {
    //   if(val > 12)
    //   this.form.get('mesValidade').setValue('');

    // });

    // this.form.get('anoValidade').valueChanges.subscribe(async val => {
    //   const ano = moment(new Date()).format('YY')
    //   // if(val > 12)
    //   // this.form.get('mesValidade').setValue('');

    // });
  }

  getData(): void {
    this.pagamentoIds =this.data?.pendencia?.pagamentoIds;
    this.valorTotal = (this.data?.pendencia?.valor);
  }


  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  salvar() {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    this.form.value["valorPago"] = this.valorTotal;
    this.form.value["valorParcela"] = this.valorParcela;
    this.form.value["pagamentoIds"] =this.pagamentoIds;
    this.alunoFinanceiroService.baixaManual(this.form.value).subscribe(val => {
      if (val?.status !== 'error') {
       this.animationsService.showSuccessSnackBar('Enviado com sucesso');
       this.dialogRef.close();
      }
    });
  }
}
