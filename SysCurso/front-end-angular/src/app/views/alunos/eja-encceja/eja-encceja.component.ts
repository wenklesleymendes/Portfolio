import { Component, OnDestroy, OnInit } from '@angular/core';
import { Navigation, Router } from '@angular/router';

@Component({
    selector: 'eja-encceja',
    templateUrl: './eja-encceja.component.html',
    styleUrls: ['./eja-encceja.component.scss']
  })
  export class EjaEnccejaComponent implements OnInit, OnDestroy {
    matriculaId: number = null;
    cursoEjaEnccejaEAD = "";

    constructor(
      private router: Router,
    ) {
      var cursoId = Number(window.localStorage.getItem('cursoIdLocalStorage'));
      if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
        this.cursoEjaEnccejaEAD = "EAD";
      else
        this.cursoEjaEnccejaEAD = "Eja Encceja";
    }

    ngOnDestroy(): void {

    }
    ngOnInit(): void {

    }

    gotoMinhasAulas(matriculaId: number): void {
      this.router.navigate(['/aula-online/minhas-aulas'], { state: { matriculaId } });
    }

    goToFinaceiro(matriculaId: number): void {
      this.router.navigate(['/alunos/matricula-aluno/aluno-financeiro-contrato'], { state: { matriculaId } });
    }

    goToSolicitacoes(matriculaId: number): void {
      this.router.navigate(['/alunos/matricula-aluno/aluno-solicitacoes'], { state: { matriculaId } });
    }

    goToDocumentos(matriculaId: number): void {
      this.router.navigate(['/alunos/matricula-aluno/aluno-documentos'], { state: { matriculaId } });
    }

    goToEja(matriculaId: number): void {
      this.router.navigate(['/alunos/eja-encceja'], { state: { matriculaId } });
    }
}
