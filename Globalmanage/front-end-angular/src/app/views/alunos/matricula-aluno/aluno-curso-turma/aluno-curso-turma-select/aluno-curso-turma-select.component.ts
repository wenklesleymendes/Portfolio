import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { TurmaService } from 'src/app/services/gerenciador/turma.service';
import { MatTableDataSource } from '@angular/material/table';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { Subscription } from 'rxjs';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-aluno-curso-turma-select',
  templateUrl: './aluno-curso-turma-select.component.html',
  styleUrls: ['./aluno-curso-turma-select.component.scss']
})
export class AlunoCursoTurmaSelectComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  loadingTurma: boolean = false;
  form: FormGroup;
  nome: string = '';
  cursos: any[] = null;
  aluno$: Subscription;
  dataSource = new MatTableDataSource();
  cadastroAluno: any;
  displayedColumns: string[] = [
    'unidade',
    'curso',
    'presencial',
    'ano',
    'semestre',
    'diaSemana',
    'periodo',
    'horario',
    'sala',
    'disponivel',
    'quantidadeVagas',
    'options'
  ];
  selection = new SelectionModel<MatTableDataSource<any>>(true, []);

  constructor(
    public dialogRef: MatDialogRef<AlunoCursoTurmaSelectComponent>,
    private fb: FormBuilder,
    private store: Store<AlunoStoreState.Aluno>,
    private cursoService: CursoService,
    private turmaService: TurmaService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.aluno$ = this.store.pipe(select(AlunoStoreSelectors.selectCadastroAluno)).subscribe(val => this.cadastroAluno = val);
    // this.getCursos();
    this.getCursosPorUnidade();
  }

  ngOnDestroy(): void {
    this.aluno$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      cursoId: [null, [Validators.required]]
    });
    this.form.get('cursoId').valueChanges.subscribe(val => {
      if(!val) return;
      this.getTurmas(val);
    });
  }

  getCursos(): void {    
    this.cursoService.getCursos().subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.cursos = val;
    })
  }

  getCursosPorUnidade(): void {
    const token = JSON.parse(window.localStorage.getItem('accessToken'));
    const usuarioLogadoId = token?.user.id;

    var idUnidade = (<HTMLInputElement>document.getElementById('idMatriculaUnidadeCarregada')).value;
    if(idUnidade == "")
      idUnidade = this.cadastroAluno?.unidadeId;

    const unidadeId = idUnidade;
    this.cursoService.getCursosPorUnidade({unidadeId, usuarioLogadoId}).subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.cursos = val;
    })
  }

  getTurmas(cursoId): void {
    this.loadingTurma = true;
    const token = JSON.parse(window.localStorage.getItem('accessToken'));
    const usuarioLogadoId = token?.user.id;
    var idUnidade = (<HTMLInputElement>document.getElementById('idMatriculaUnidadeCarregada')).value;
    if(idUnidade == "")
      idUnidade = this.cadastroAluno?.unidadeId;
    const unidadeId = idUnidade;
    if(unidadeId === null || unidadeId === undefined) return;
    this.turmaService.getTurmasDisponiveis({cursoId, unidadeId, usuarioLogadoId}).subscribe(val => {
      this.dataSource.data = val;
      this.loadingTurma = false;
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

  ajustarCurso(cursosSelected: any[]): string {
    if (cursosSelected == null || cursosSelected.length == 0) return ' - ';
    let text = '';
    cursosSelected.forEach(elem => text += `${elem.descricao ? elem.descricao : '  '}, `);
    return text.substring(0, text.length - 2);
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

  close(element): void {
    if(!element) return;
    const formValue = this.form.value;
    let data = { ...element, ...formValue };
    data.curso = this.ajustarCurso(data.curso);
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
    
    this.dialogRef.close(data);
  }
}
