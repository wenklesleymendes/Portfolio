import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreActions, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { Subscription } from 'rxjs';
import { Navigation, Router } from '@angular/router';
import { EventEmitterService } from 'src/app/services/EventEmitterService';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { DatePtBrPipe } from 'src/app/utils/pipes/date-pt-br.pipe';
import { AuthService } from 'src/app/security/auth.service';
import { ConsultarAlunosComponent } from '../consultar-alunos/consultar-alunos.component';
// import { fontawesome } from 'src/assets';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CadastrarAlunosComponent } from '../consultar-alunos/cadastrar-alunos/cadastrar-alunos.component';


@Component({
  selector: 'app-matricula-aluno',
  templateUrl: './matricula-aluno.component.html',
  styleUrls: ['./matricula-aluno.component.scss']
})
export class MatriculaAlunoComponent implements OnInit, OnDestroy {
  isLoadingResults: boolean = false;
  error: boolean = false;
  selectedIndex: number = 0;
  hasCurso: boolean = false;
  hasFinanceiro: boolean = false;
  curso$: Subscription;
  aluno$: Subscription;
  matricula$: Subscription;
  aluno: any;
  numeroMatricula: number = null;
  matriculaId: number = null;
  alredyUpdateMatricula: boolean = false;
  quantidadeDocumentosPendentes: number = null;
  existePendenciaSolicitacaoAnexo: number = null;
  existePendenciaFinanceira: boolean = false;
  existePendenciaContrato: boolean = false;
  isAluno: boolean = false;
  refreshEvento: any = null;

  info: any[] = [
    { label: 'Nome', value: '' },
    { label: 'Matrícula', value: '' },
    { label: 'Data da Matrícula', value: '' },
    { label: 'Status da Matrícula', value: '' }
  ];

  logoLocalStorage = "";
  cursoLocalStorage = "";
  unidadeLocalStorage = "";

  constructor(
    private store: Store<AlunoStoreState.Aluno>,
    private router: Router,
    private matriculaAlunoService: MatriculaAlunoService,
    private authService: AuthService,
    private datePtBrPipe: DatePtBrPipe,
    private dialog: MatDialog,
  ) {
    // Get State
    this.logoLocalStorage = window.localStorage.getItem('logoLocalStorage');
    this.cursoLocalStorage = window.localStorage.getItem('cursoLocalStorage');
    this.unidadeLocalStorage = window.localStorage.getItem('unidadeLocalStorage');
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.matriculaId = state.matriculaId;
    }
  }

  ngOnInit(): void {
    this.isAluno = this.authService.isAluno();

    if (this.matriculaId == null && this.isAluno == true) {
      this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
    }
    this.refreshEvento = EventEmitterService.get('refreshPainelAluno').subscribe(e => this.reflexMatricula(e));

    this.aluno$ = this.store.pipe(select(AlunoStoreSelectors.selectCadastroAluno)).subscribe(val => {
      if (!this.isAluno) {
        if (!val) {
          this.router.navigate(['alunos/consultar-alunos']);
        }
      }
      this.aluno = val;

      window.localStorage.setItem('CPFAlunoLocalStorage', val.cpf);

    });
    this.curso$ = this.store.pipe(select(AlunoStoreSelectors.selectCursoAluno)).subscribe(val => {
      if (val?.id) { this.hasCurso = true; }
    });

    this.matricula$ = this.store.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if (val?.id) {
        this.matriculaId = val.id;

        if ((<HTMLInputElement>document.getElementById('idMatriculaUnidadeCarregada')) != null)
          (<HTMLInputElement>document.getElementById('idMatriculaUnidadeCarregada')).value = val.unidade.id;

        this.numeroMatricula = val?.numeroMatricula;
        this.getMatricula();
      }

      this.quantidadeDocumentosPendentes = val?.quantidadeDocumentosPendentes;
      this.existePendenciaSolicitacaoAnexo = val?.existePendenciaSolicitacaoAnexo;
    });

    if (this.matriculaId) this.getMatricula();
    else this.showInfo();
  }

  ngOnDestroy(): void {
    this.curso$.unsubscribe();
    this.aluno$.unsubscribe();
    this.matricula$.unsubscribe();
    this.store.dispatch(AlunoStoreActions.deleteAluno());
  }

  changeTab(tab: number): void {
    this.selectedIndex = tab;
  }

  reflexMatricula(matriculaId) {
    if (this.matriculaId == null && matriculaId != null)
      this.matriculaId = matriculaId;

    this.getMatricula(false, true)
  }

  getMatricula(fromtab?: boolean, updateMatricula?: boolean): void {
    if (updateMatricula) this.alredyUpdateMatricula = false;

    if (this.alredyUpdateMatricula && !fromtab) return;
    this.matriculaAlunoService.buscarPorId(this.matriculaId).subscribe(val => {
      var dataMatricula = val?.dataMatricula != null ? new Date(val?.dataMatricula).toLocaleDateString() : "";
      this.info = [
        { label: 'Unidade', value: val?.unidade?.nome },
        { label: 'Nome', value: val?.aluno?.nome },
        { label: 'Matrícula', value: val?.numeroMatricula },
        { label: 'Data da Matrícula', value: dataMatricula },
        //{ label: 'Data da Matrícula', value: this.datePtBrPipe.transform(val?.dataMatricula) },
        { label: 'Status da Matrícula', value: val?.status ? 'ATIVADO' : 'CANCELADO' }
      ];

      window.localStorage.setItem('UnidadeNomeMatriculaAlunoLocalStorage', val?.unidade?.nome);
      window.localStorage.setItem('UnidadeIDMatriculaAlunoLocalStorage', val.unidadeId);

      this.alredyUpdateMatricula = true;
      this.existePendenciaContrato = val?.existePendenciaContrato;
      this.existePendenciaFinanceira = val?.existePendenciaFinanceira;
      this.store.dispatch(AlunoStoreActions.updateMatriculaAluno({ payload: val }));
    });
  }

  showInfo(): void {
    this.info = [
      { label: 'Nome', value: this.aluno?.nome }
    ];
  }

  gotoMinhasAulas(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-curso-turma'], { state: { matriculaId } });
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

  abrirModalConsultarAlunos(): void{
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    window.localStorage.setItem('CliqueVerMatriculaAlunoLocalStorage', '1');

    const dialogRef = this.dialog.open(CadastrarAlunosComponent, {
      width: isMobileResolution ? '95vw' : '90vw',
      autoFocus: false,
      data: {  }
    });
    dialogRef.afterClosed().subscribe(result => {
      window.localStorage.setItem('CliqueVerMatriculaAlunoLocalStorage', '2');
    });
  }
}
