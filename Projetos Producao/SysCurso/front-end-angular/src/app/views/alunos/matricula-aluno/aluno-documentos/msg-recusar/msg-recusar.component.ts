import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AnexoService } from 'src/app/services/anexo.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-msg-recusar',
  templateUrl: './msg-recusar.component.html',
  styleUrls: ['./msg-recusar.component.scss']
})
export class MsgRecusarComponent implements OnInit {
  form: FormGroup = new FormGroup({
    tipoRecusa: new FormControl(null, [Validators.required]),
    mensagem: new FormControl({disabled: true, value:null})
  });
  enviando: boolean = false;
  update: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<MsgRecusarComponent>,
    private anexoService: AnexoService,
    private animationService: AnimationsService,
    private formService: FormService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form.get('tipoRecusa').valueChanges.subscribe(_ => this.formService.enableField(this.form.get('mensagem')));
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  save(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formData = this.form.value;
    const data = {
      anexoId: this.data.element.id,
      ...formData
    };
    this.enviando = true;
    this.update = true;
    this.anexoService.recusarDocumento(data).subscribe(val => {
      this.enviando = false;
      if(val?.status === 'error') return;
      this.animationService.showSuccessSnackBar('Recusado com sucesso');
      this.close();
    });
  }
  
  close(): void {
    this.dialogRef.close(this.update);
  }
}
