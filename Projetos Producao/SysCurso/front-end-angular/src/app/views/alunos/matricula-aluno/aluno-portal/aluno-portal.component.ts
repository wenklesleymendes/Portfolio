import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
//import { AlunoCursoTurmaSelectComponent } from './aluno-curso-turma-select/aluno-curso-turma-select.component';
//import { AlunoPortalComponent } from './aluno-portal/aluno-portal.component';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreActions, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { Subscription } from 'rxjs';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { TurmaService } from 'src/app/services/gerenciador/turma.service';
import { AuthService } from 'src/app/security/auth.service';
import { Navigation, Router } from '@angular/router';
import { AulaOnlineService } from 'src/app/services/aula-online/aula-online.service';

@Component({
  selector: 'app-aluno-portal',
  templateUrl: './aluno-portal.component.html',
  styleUrls: ['./aluno-portal.component.scss']
})
export class AlunoPortalComponent implements OnInit, OnDestroy {
  @Input() id;
  @Output() onContinue: EventEmitter<any> = new EventEmitter();
  @Output() onUpdateMatricula: EventEmitter<any> = new EventEmitter();
  error: boolean = false;
  isLoadingResults: boolean = false;
  info: any[] = null;
  cursoId: number = null;
  turmaId: number = null;
  matriculaId: number = null;
  cadastroAluno: any;
  aluno$: Subscription;
  matricula$: Subscription;
  hasTurma: boolean = false;
  novaMatricula: boolean = false;
  enableSalvar: boolean = true;
  turmaResult: any = null;
  isAluno: boolean = false;
  nacionalTech: boolean = true;
  selectedTurma: boolean = false;
  isLoadingVideo: boolean = false;
  vimeoUrl: string = null;
  vimeoProgress: number = null;
  videoId: string = null;
  materias: any[] = [];
  nomeAulaOnline: string = null;
  semAula: boolean = false;

  logoLocalStorage = "";
  cursoLocalStorage = "";
  unidadeLocalStorage = "";
  cursoEjaEnccejaEAD = "";

  constructor(
    private aulaOnlineService: AulaOnlineService,
    private dialog: MatDialog,
    private store: Store<AlunoStoreState.Aluno>,
    private matriculaAlunoService: MatriculaAlunoService,
    private alunoService: AlunoService,
    private turmaService: TurmaService,
    private animationsService: AnimationsService,
    private router: Router,
    private authService: AuthService
  ) {
      this.logoLocalStorage = window.localStorage.getItem('logoLocalStorage');
      this.cursoLocalStorage = window.localStorage.getItem('cursoLocalStorage');
      this.unidadeLocalStorage = window.localStorage.getItem('unidadeLocalStorage');
      const currentNavigation: Navigation = this.router.getCurrentNavigation();
      if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
        const state = currentNavigation.extras.state;
        this.matriculaId = state.matriculaId;
      }

      var cursoId = Number(window.localStorage.getItem('cursoIdLocalStorage'));
      if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
        this.cursoEjaEnccejaEAD = "EAD";
      else
        this.cursoEjaEnccejaEAD = "Eja Encceja";
    }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {

    this.isLoadingResults = true;

    this.isAluno = this.authService.isAluno();

    if(this.isAluno)
    {
      if(this.matriculaId == null)
      {
        this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
      }

      this.aluno$ = this.store.pipe(select(AlunoStoreSelectors.selectCadastroAluno)).subscribe(val => this.cadastroAluno = val);

      this.matricula$ = this.store.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
        if(val?.turma?.id) {
          this.getTurma(val.turma.id);
          this.cursoId = val?.cursoId;
          this.novaMatricula = false;
          this.matriculaId = val.id;
          this.nacionalTech = val?.curso?.nacionatalTec;
        }
        else {
          if(this.cadastroAluno?.id) this.novaMatricula = true;
        }
      });

      if(this.router.url != "/alunos/matricula-aluno/aluno-curso-turma")
        this.getAulas();
    }

    this.isLoadingResults = false;
  }

  ngOnDestroy(): void {
    if(this.aluno$ != null)
      this.aluno$.unsubscribe();
    if(this.matricula$ != null)
      this.matricula$.unsubscribe();
  }
  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getTurma(turmaId): void {
    this.isLoadingResults = true;
    this.turmaService.getPorId(turmaId).subscribe(val => {
      this.isLoadingResults = false;
      this.hasTurma = true;
      this.store.dispatch(AlunoStoreActions.updateCursoAluno({ payload: val }))

      let data = { ...val };
      data.curso = this.ajustarCurso(data?.curso, this.cursoId);
      data.presencial = data.presencial ? 'Presencial' : 'Distância';
      data.semestre = this.ajustarVigencia(data.semestre);
      data.periodo = this.ajustarPeriodo(data.periodo);
      data['horario'] = this.ajustarHorario(data.horarioInicio, data.horarioTermino)
      data['diaSemana'] = this. ajustarDiaDaSemana({
        Seg: data.segunda,
        Ter: data.terca,
        Qua: data.quarta,
        Qui: data.quinta,
        Sex: data.sexta,
        Sab: data.sabado,
        Dom: data.domingo
      });
      const { curso, presencial, ano, semestre, diaSemana, periodo, horario, sala, quantidadeVagas } = data;
      this.info = [
        { label: 'Curso', value: this.isAluno ? (this.nacionalTech ? curso : 'Supletivo Preparatório') : curso},
        { label: 'Modalidade', value: presencial },
        { label: 'Ano', value: ano },
        { label: 'Semestre', value: semestre },
        { label: 'Dia da Semana', value: diaSemana },
        { label: 'Período', value: periodo },
        { label: 'Horário', value: horario },
        { label: 'Sala', value: sala },
      ];
      if(!this.isAluno) this.info.push({ label: 'Vagas disponíveis', value: quantidadeVagas });
    })
  }

  ajustarVigencia(vigencia): string {
    if (vigencia == null || vigencia.length == 0) return ' - ';
    if (vigencia == '1') return '1º Semestre';
    if (vigencia == '2') return '2º Semestre';
    if (vigencia == '3') return 'Anual';
    if (vigencia == '4') return 'Bimestral';
    if (vigencia == '5') return 'Trimestral';
    return ' - ';
  }

  ajustarPeriodo(periodo): string {
    if (periodo == null || periodo.length == 0) return ' - ';
    if (periodo == 1) return 'Manhã';
    if (periodo == 2) return 'Tarde';
    if (periodo == 3) return 'Noite';
    return ' - ';
  }

  ajustarUnidade(unidades: any[]): string {
    if (unidades == null || unidades.length == 0) return ' - ';
    let text = '';
    unidades.forEach(elem => text += `${elem.nome ? elem.nome : '  '}, `)
    return text.substring(0, text.length - 2);
  }

  ajustarCurso(cursosSelected: any[], cursoId: number): string {
    if (cursosSelected == null || cursosSelected.length == 0) return ' - ';
    return cursosSelected.find(elem => elem.id === cursoId)?.descricao;
  }

  ajustarDiaDaSemana(days): string {
    if (days == null || days.length == 0) return ' - ';
    let week = '';
    for(let day in days) if (days[day]) week += `${day}-`;
    if (week.length < 3) return ' - ';
    return week.substring(0, week.length - 1);
  }

  ajustarHorario(inicio: string, fim: string): string {
    if ((inicio == null || inicio.length == 0) && (fim == null || fim.length == 0)) return ' - ';
    inicio = inicio.padStart(4, '0');
    fim = fim.padStart(4, '0');
    return `${inicio.slice(0,2)}:${inicio.slice(2,5)} - ${fim.slice(0,2)}:${fim.slice(2,5)}`;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openSelectTurma(changeTurma: boolean = false): void {
    const dialogRef = this.dialog.open(AlunoPortalComponent, {
      width: '90vw',
      autoFocus: false,
      data: { }
    });
    dialogRef.afterClosed().subscribe(turmaResult => {
      if(!turmaResult) return;
      const { id, cursoId, curso, presencial, ano, semestre, diaSemana, periodo, horario, sala, quantidadeVagas } = turmaResult;
      this.cursoId = cursoId;
      this.turmaId = id;

      this.info = [
        { label: 'Curso', value: this.isAluno ? (this.nacionalTech ? curso : 'Supletivo Preparatório') : curso },
        { label: 'Modalidade', value: presencial },
        { label: 'Ano', value: ano },
        { label: 'Semestre', value: semestre },
        { label: 'Dia da Semana', value: diaSemana },
        { label: 'Período', value: periodo },
        { label: 'Horário', value: horario },
        { label: 'Sala', value: sala },
        { label: 'Vagas disponíveis', value: quantidadeVagas }
      ];
      this.turmaResult = turmaResult;

      if(changeTurma) this.changeTurma(turmaResult);
    });
  }

  changeTurma({ id, cursoId }): void {
    const data = {
      turmaId: id,
      matriculaId: this.matriculaId,
      cursoId: cursoId
    };

    this.turmaService.transferirDeTurma(data).subscribe(val => {
      if(val?.error === 'error') return;
      this.onUpdateMatricula.emit(new Date());
      this.animationsService.showSuccessSnackBar('Mudado com sucesso');
    });
  }

  next(): void {
    this.store.dispatch(AlunoStoreActions.updateCursoAluno({ payload: this.turmaResult }));

    this.alunoService.cadastrar(this.cadastroAluno).subscribe(val => {
      if (val?.status) return;
      this.animationsService.showSuccessSnackBar(this.cadastroAluno.id == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
      this.store.dispatch(AlunoStoreActions.updateCadastroAluno({ payload: val }));

      const token = JSON.parse(window.localStorage.getItem('accessToken'));
      const usuarioLogadoId = token?.user.id;
      const unidadeId = token?.user?.unidade?.id;
      const data = {
        usuarioLogadoId,
        unidadeId,
        id: 0,
        alunoId: val?.id,
        cursoId: this.cursoId,
        turmaId: this.turmaId
      }
      this.matriculaAlunoService.cadastrar(data).subscribe(valMatricula => {
        if(!valMatricula){
          this.animationsService.showErrorSnackBar('O aluno já matrículado em curso supletivo para esta unidade.');
          return;
        }

        if (valMatricula?.status === 'error') return;
        this.enableSalvar = false;
        this.store.dispatch(AlunoStoreActions.updateMatriculaAluno({ payload: valMatricula }));
        this.onContinue.emit(valMatricula);
      })
    })
  }

  salvar(): void {
    const token = JSON.parse(window.localStorage.getItem('accessToken'));
    const usuarioLogadoId = token?.user.id;
    const unidadeId = token?.user?.unidade?.id;
    const data = {
      usuarioLogadoId,
      unidadeId,
      id: 0,
      alunoId: this.cadastroAluno?.id,
      cursoId: this.cursoId,
      turmaId: this.turmaId
    }
    this.matriculaAlunoService.cadastrar(data).subscribe(valMatricula => {
      if(!valMatricula){
        this.animationsService.showErrorSnackBar('O aluno já matrículado em curso supletivo para esta unidade.');
        return;
      }

      if (valMatricula?.status === 'error') return;
      this.enableSalvar = false;
      this.store.dispatch(AlunoStoreActions.updateMatriculaAluno({ payload: valMatricula }));
      this.onContinue.emit(valMatricula);
    })
  }

  gotoMinhasAulas(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-curso-turma'], { state: { matriculaId } });
  }

  goToFinaceiro(matriculaId: number): void {
    // window.localStorage.setItem('telaFinanceiroLocalStorage', "0");
    // this.router.navigate(['/alunos/matricula-aluno/aluno-financeiro-contrato'], { state: { matriculaId } });
    this.redirectTo('/alunos/matricula-aluno/aluno-financeiro-contrato');
  }

  goToSolicitacoes(matriculaId: number): void {
    // this.router.navigate(['/alunos/matricula-aluno/aluno-solicitacoes'], { state: { matriculaId } });
    this.redirectTo('/alunos/matricula-aluno/aluno-solicitacoes');
  }

  goToDocumentos(matriculaId: number): void {
    // this.router.navigate(['/alunos/matricula-aluno/aluno-documentos'], { state: { matriculaId } });
    this.redirectTo('/alunos/matricula-aluno/aluno-documentos');
  }

  goToEja(matriculaId: number): void {
    this.router.navigate(['/alunos/eja-encceja'], { state: { matriculaId } });
  }

  goToAulasOnline(matriculaId: number): void {
    this.router.navigate(['/aula-online/minhas-aulas'], { state: { matriculaId } });
  }

  redirectTo(uri:string){
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this.router.navigate([uri]));
  }

  changeVideo(aula): void {
    this.isLoadingVideo = true;

    setTimeout(() => {
      const { id, urlVideo } = aula;
      this.vimeoUrl = urlVideo;
      this.videoId = id;
      this.vimeoProgress = 0;
      this.isLoadingVideo = false;
    }, 0);
  }

  openPanel(materia): boolean {

    if(materia?.id === null || materia?.id === undefined) return;
    const id = materia?.id;
    const hasMateria = this.materias.find(materia => {
      const videoAulas: any[] = materia?.videoAula;
      if(videoAulas) {
        const achouMateria = videoAulas.find(videoAula => videoAula.id === this.videoId);
        if(achouMateria) return materia;
      }
    });
    const idFound = hasMateria?.id;
    if(idFound === null || idFound === undefined) return;
    return id === idFound;
  }

  getAulas(): Promise<any> {

    return new Promise((res, rej) => {
      this.aulaOnlineService.minhasAulasOnline(this.matriculaId).subscribe(val => {
        if (val?.status === 'error') {
          this.error = true;
          rej();
          return;
        }
        this.materias = val?.materiaOnline;
        this.nomeAulaOnline = val?.nomeAulaOnline;
        res(val);
        return;
      });
    });
  }
}
