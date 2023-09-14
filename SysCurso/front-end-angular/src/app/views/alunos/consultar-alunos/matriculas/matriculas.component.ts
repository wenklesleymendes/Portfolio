import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-matriculas',
  templateUrl: './matriculas.component.html',
  styleUrls: ['./matriculas.component.scss']
})
export class MatriculasComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = [
    'matricula',
    'unidade',
    'statusMatricula',
    'curso',
    'ano',
    'semestre',
    'financeiro',
    'options'];
  dataSource = new MatTableDataSource([{}]);
  nome: string = '';

  constructor(
    public dialogRef: MatDialogRef<MatriculasComponent>,
    private matriculaAlunoService: MatriculaAlunoService,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.nome = this.data?.nome;
    this.getData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getData(): void {
    this.isLoadingResults = true;
    const token = JSON.parse(window.localStorage.getItem('accessToken'));
    const usuarioLogadoId = token?.user.id;
    this.matriculaAlunoService.buscarMinhasMatriculas(this.data?.id, usuarioLogadoId).subscribe(val => {
      this.isLoadingResults = false;
      this.dataSource.data = val;
    })
  }

  goToMatricula(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno'], { state: { matriculaId } });
    this.dialogRef.close();
  }

  adicionar(): void {
    this.router.navigate(['/alunos/matricula-aluno']);
    this.dialogRef.close();
  }

  delete(id): void {
    this.matriculaAlunoService.deletar(id).subscribe();
  }
}
