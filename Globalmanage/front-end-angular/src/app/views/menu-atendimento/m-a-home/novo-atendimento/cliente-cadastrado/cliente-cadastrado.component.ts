import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cliente-cadastrado',
  templateUrl: './cliente-cadastrado.component.html',
  styleUrls: ['./cliente-cadastrado.component.scss']
})
export class ClienteCadastradoComponent implements OnInit {

  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = true;
  celular: string = "";
  id: any;

  constructor(
    private formService: FormService,
    private fb: FormBuilder,
    private router: Router,

    public dialogRef: MatDialogRef<ClienteCadastradoComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {

    this.buildForm();

    this.id = this.data.id

    Promise.all([])
      .then(() => {
        this.isLoadingResults = false;
      })
      .catch(() => this.isLoadingResults = false);
  }

  goToEditarResultados(): void {

    let id = this.id;

    this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados/editar-resultados'], { state: { id } }); //, { state: { matriculaId } }
    this.close();
  }

  buildForm(): void {
    this.form = this.fb.group({

    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
