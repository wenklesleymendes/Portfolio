import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-cancelar-pagamento',
  templateUrl: './cancelar-pagamento.component.html',
  styleUrls: ['./cancelar-pagamento.component.scss']
})
export class CancelarPagamentoComponent implements OnInit, OnDestroy {
  form: FormGroup;
  canRemove: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<CancelarPagamentoComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form = this.fb.group({
      despesaParcelaId: [this.data.id, [Validators.required]],
      observacao: [null, [Validators.required]],
      usuario: [null],
    });
  }

  ngOnDestroy(): void {
    this.dialogRef.close({remove: this.canRemove, data: this.form.value});
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  remove(remove: boolean): void {
    this.canRemove = remove;
    this.dialogRef.close({remove: this.canRemove, data: this.form.value});
  }
}
