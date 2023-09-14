import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ContasPagarService } from 'src/app/services/financeiro/contas-pagar.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-imprimir-contas-pagar',
  templateUrl: './imprimir-contas-pagar.component.html',
  styleUrls: ['./imprimir-contas-pagar.component.scss']
})
export class ImprimirContasPagarComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  nome: string = null;
  
  constructor(
    public dialogRef: MatDialogRef<ImprimirContasPagarComponent>,
    private fb: FormBuilder,
    private contasPagarService: ContasPagarService,
    private animationsService: AnimationsService,
    private formService: FormService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form = this.fb.group({
      data: [null, [Validators.required]],
      valor: [null, [Validators.required]],
      descricao: [null, [Validators.required]],
    });

    this.nome = this.data?.nome;
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  imprimir(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue = this.form.value;
    const data = { ...formValue, idDespesa: this.data.id };
    this.contasPagarService.imprimirRecibo(data);
  }
}
