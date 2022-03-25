import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmar-dados',
  templateUrl: './confirmar-dados.component.html',
  styleUrls: ['./confirmar-dados.component.scss']
})
export class ConfirmarDadosComponent {

    dadosContato: any;

  constructor(
    public dialogRef: MatDialogRef<ConfirmarDadosComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {
    this.dadosContato = this.data?.dadosAlunoForm;
  }

  alterarCelular(): void {

    this.dialogRef.close();

    var element = document.getElementById("idCelular");
    if(element)
      element.focus();
  }

  alterarEmail(): void {

    this.dialogRef.close();

    var element = document.getElementById("idEmail");
    if(element)
      element.focus();
  }

  close(): void {
      this.dialogRef.close();
  }

  continuarSalvar(): void {
    this.dialogRef.close();

    document.getElementById("btnSalvarData").click();    
  }
}
