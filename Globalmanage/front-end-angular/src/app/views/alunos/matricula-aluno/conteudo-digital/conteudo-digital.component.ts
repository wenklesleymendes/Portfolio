import { Component, OnInit } from '@angular/core';
import { Navigation, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { AulaOnlineService } from 'src/app/services/aula-online/aula-online.service';
import { ApostilaOnlineService } from 'src/app/services/aluno/apostila-online.service';
import { AuthService } from 'src/app/security/auth.service';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { Store, select } from '@ngrx/store';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { fileURLToPath } from 'url';

@Component({
  selector: 'app-conteudo-digital',
  templateUrl: './conteudo-digital.component.html',
  styleUrls: ['./conteudo-digital.component.scss']
})
export class ConteudoDigitalComponent implements OnInit {

  isAluno: boolean = false;
  isLoadingResults: boolean = false;
  error: boolean = false;
  materias: any[] = [];
  matriculaId: number = null;
  cursoId: number = null;
  matricula$: Subscription;
  matricula: any;
  materialLiberado: boolean = false;
  constructor(private router: Router,
    private aulaOnlineService: AulaOnlineService,
    private apostilaOnlineService: ApostilaOnlineService,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private matriculaAlunoService: MatriculaAlunoService,
    private authService: AuthService) {
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.matriculaId = state.matriculaId;
    }
  }

  ngOnInit(): void {

    this.isAluno = this.authService.isAluno();
    this.isLoadingResults = true;
    if (this.isAluno == true) {
      if (this.matriculaId == null) {
        this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
      }
      this.getMatricula();
    }
  }

  goToApostilaOnline(material): void {
      if (material?.url)
        window.open(material?.url, '_blank');
  }

  getMatricula(): Promise<any> {
    return new Promise((res, rej) => {
      this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
        if (val?.id) {
          this.matricula = val;
          this.matriculaId = val?.id;
          this.cursoId = val?.cursoId;
          this.materialLiberado = val?.materialLiberado;

          if (val?.materialLiberado)
            this.getMateriais();
          else
            this.isLoadingResults = false;

          rej();
          return;
        }
        else {
          this.matriculaAlunoService.buscarPorId(this.matriculaId).subscribe(mat => {
            if (mat?.id) {
              this.matricula = mat;
              this.materialLiberado = mat?.materialLiberado;

              if (mat?.materialLiberado)
                this.getMateriais();

              else
                this.isLoadingResults = false;
              res(val);
              return;
            }
          });
        }
      });
    });
  }

  getMateriais(): Promise<any> {
    this.cursoId = Number(window.localStorage.getItem('cursoIdLocalStorage'));

    return new Promise((res, rej) => {
      this.apostilaOnlineService.getApostilaPorCursoId(this.cursoId).subscribe(val => {
        if (val?.status === 'error') {
          this.error = true;
          rej();
          this.isLoadingResults = false;
          return;
        }
        this.materias = val;
        res(val);
        this.isLoadingResults = false;
        return;
      });
    });
  }
}
