import { map } from 'rxjs/operators';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { MatriculaAlunoService } from './../../../../../services/aluno/matricula-aluno.service';
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { ProvaAlunoService } from 'src/app/services/provas/provaAluno.service';
import { T } from '@angular/cdk/keycodes';
import { AnimationsService } from 'src/app/services/animations.service';
import { AxisDateTimeLabelFormatsOptions } from 'highcharts';


@Component({
  selector: 'app-aluno-provas-certificados-materias',
  templateUrl: './aluno-provas-certificados-materias.component.html',
  styleUrls: ['./aluno-provas-certificados-materias.component.scss']
})
export class AlunoProvasCertificadosMateriasComponent implements OnInit, OnDestroy {
  id: any;
  statusProva: any;
  cursoId: any;
  error: boolean = false;
  isLoadingResults: boolean = false;
  loadingTurma: boolean = false;
  form: FormGroup;
  dataResultado: any;
  prova: any;
  matriculaAluno: any;
  desabilitarForm: boolean = false;
  provasRealizadas: any[];
  provaMateriaAluno: any[];
  displayedColumns: string[] = [
    'tabNomeMateria',
    'tabStatus',
  ];
  constructor(
    public dialogRef: MatDialogRef<AlunoProvasCertificadosMateriasComponent>,
    private location: Location,
    private fb: FormBuilder,
    private provaAlunoService: ProvaAlunoService,
    private cursoService: CursoService,
    private matriculaAlunoService: MatriculaAlunoService,
    private animationsService: AnimationsService,
    @Inject(MAT_DIALOG_DATA) public data,
    private router: Router
  ) { }
  ngOnDestroy(): void {
  }

  ngOnInit() {
    this.id = this.data?.provaId;
    this.statusProva = this.data?.statusProva;
    this.cursoId = this.data?.cursoId;
    this.buildForm();
    Promise.all([
    ])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);
  }

  buildForm(): void {
    this.form = this.fb.group({
      observacao: [null],
    });
  }
  loadData(): void {
    this.provaAlunoService.getPorId(this.id).subscribe(val => {
      if (!val || val.id === 0) { return; }
      if (val?.status === 'error') { return this.error = true; }
      this.prova = val;
      this.dataResultado = val.UpdatedAt;

      if (this.statusProva > 0 && this.prova.provaMateriaAluno.length == 0 ) {
        this.cursoService.getCursoPorId(this.cursoId).subscribe(mat => {
          if (!mat || mat.id === 0) { return; }
          if (mat?.status === 'error') { return this.error = true; }

          const statusProva = this.statusProva;
          // tslint:disable-next-line: only-arrow-functions
          this.provaMateriaAluno = mat.materia.map(function (mate) {
            return {
              nomeMateria: mate.nomeMateria,
              aprovado: statusProva === 3
            };
          });
        });
        this.dataResultado = "Novo";
      } else {
        this.provaMateriaAluno = this.prova.provaMateriaAluno;
        this.form.get('observacao').setValue(val.observacao);
        this.dataResultado = this.provaMateriaAluno[0].updatedAt;
        this.statusProva = this.prova.statusProva;
      }
    });
  }

  openMaterias(id) {
    console.log('Teste');
  }

  salvarData() {
    this.prova.observacao = this.form?.value.observacao;
    this.prova.statusProva = this.statusProva;
    this.prova.provaMateriaAluno = this.provaMateriaAluno;
    this.provaAlunoService.AtualizarStatusProva(this.prova).subscribe(val => {
      if (!val || val?.status === 'error') { return this.error = true; }
      this.animationsService.showSuccessSnackBar('Salvo com sucesso');

      this.dialogRef.close(true);

    });

  }
}
