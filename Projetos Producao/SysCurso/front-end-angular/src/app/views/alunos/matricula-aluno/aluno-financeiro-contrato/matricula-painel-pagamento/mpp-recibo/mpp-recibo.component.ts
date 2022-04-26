import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/security/auth.service';
import { CelMask } from 'src/app/utils/mask/mask';
import { AlunoStoreSelectors, AlunoStoreState } from 'src/app/_store/aluno-store';

@Component({
  selector: 'app-mpp-recibo',
  templateUrl: './mpp-recibo.component.html',
  styleUrls: ['./mpp-recibo.component.scss']
})
export class MppReciboComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  aluno$: Subscription = null;
  email: string = null;
  whatsapp: string = null;
  maskCelular: string = CelMask;
  isAluno: boolean = null;

  constructor(
    public dialogRef: MatDialogRef<MppReciboComponent>,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private auth: AuthService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.getAluno();
    this.isAluno = this.auth.isAluno();
  }

  ngOnDestroy(): void {
    if(this.aluno$) this.aluno$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAluno(): void {
    this.aluno$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectCadastroAluno)).subscribe(val => {
      if(val?.id) {
        this.email = val?.contato?.email;
        this.whatsapp = val?.contato?.celular;
      }
    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  imprimir(): void {
    if(!this.data.recibo) return;

    const b64Data = this.data.recibo;
    const blob = this.b64toBlob(b64Data, 'application/pdf');
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = 'Recibo.pdf';
    link.click();
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
}
