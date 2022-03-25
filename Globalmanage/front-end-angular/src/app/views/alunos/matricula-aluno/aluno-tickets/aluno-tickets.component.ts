import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { MatDialog } from '@angular/material/dialog';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { AssuntoTicketService } from 'src/app/services/ticket/assunto-ticket.service';
import { AuthService } from 'src/app/security/auth.service';
import { DetalheTicketIndividualComponent } from 'src/app/views/ticket/administracao-ticket/detalhe-ticket/detalhe-ticket-individual/detalhe-ticket-individual.component';
import { AlunoStoreSelectors, AlunoStoreState } from 'src/app/_store/aluno-store';
import { select, Store } from '@ngrx/store';
import { AlunoTicketNovoComponent } from 'src/app/views/alunos/matricula-aluno/aluno-tickets/aluno-ticket-novo/aluno-ticket-novo.component';
import { SelectionModel } from '@angular/cdk/collections';
import { AlunoOcorrenciaNovoComponent } from "src/app/views/alunos/matricula-aluno/aluno-tickets/aluno-ocorrencia-novo/aluno-ocorrencia-novo.component";
import { AlunoOcorrenciaDetalheComponent } from "src/app/views/alunos/matricula-aluno/aluno-tickets/aluno-ocorrencia-detalhe/aluno-ocorrencia-detalhe.component";

@Component({
  selector: 'app-aluno-tickets',
  templateUrl: './aluno-tickets.component.html',
  styleUrls: ['./aluno-tickets.component.scss']
})
export class AlunoTicketsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  filterUnidades: Observable<any[]>;
  unidadesDefault = [];
  displayedColumns: string[] = [
    'protocolo',
    'assunto',
    'unidade',
    'rm',
    'aluno',
    'dataAbertura',
    'dataAtendimento',
    'sla',
    'status',
    'responsavel',
    'atendente',
    'options'
  ];
  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  assuntosTicket: any[] = null;
  matricula$: Subscription;
  matriculaId: number = null;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private ticketService: TicketService,
    private unidadeService: UnidadeService,
    private authService: AuthService,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private assuntoTicketService: AssuntoTicketService,
    private dialog: MatDialog,
    private fb: FormBuilder,
    private router: Router
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getMatricula();
    this.buildForm();
    this.getunidades();
    this.getAssuntoTickets();
    this.getAll();
  }

  ngOnDestroy(): void {
    this.matricula$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      unidadeId: [null],
      numeroProtocolo: [null],
      unidadeSelect: [''],
      periodoAberturaInicio: [null],
      periodoAberturaFim: [null],
      nomeResponsavel: [null],
      assuntoTicketId: [null]
    });

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
      else this.form.get('unidadeId').setValue(null);
    })
  }

  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      if (val?.id) {
        this.matriculaId = val?.id;
      }
    });
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    const formValue = this.form.value;
    delete formValue.unidadeSelect;

    const usuario = this.authService.getToken();
    formValue['usuarioLogadoId'] = usuario?.user?.id,
      formValue['matriculaId'] = this.matriculaId


    this.ticketService.filtrarTicket(formValue)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;
        this.form.get('unidadeSelect').setValue('');
      });
  }

  getAssuntoTickets(): void {
    this.assuntoTicketService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else this.assuntosTicket = val;
      });
  }

  ajustarStatus(status): any {
    if (!status) return { label: ' - ', style: '' };
    else if (status === 1) return { label: 'Aberto', style: 'bg-orange' };
    else if (status === 2) return { label: 'Devolvido', style: 'bg-yellow' };
    else if (status === 3) return { label: 'Em atendimento', style: 'bg-blue' };
    else if (status === 4) return { label: 'Finalizado', style: 'bg-green' };
    else if (status === 6) return { label: 'Ocorrência', style: 'bg-red' };
    else return { label: ' - ', style: '' };
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`ticket/meus-ticket/adicionar/${id}`)
  }

  openDetalhe(id: number = 0, assunto: string = ''): void {
    const dialogRef = this.dialog.open(DetalheTicketIndividualComponent, {
      width: '90vw',
      data: { id, assunto },
      autoFocus: false,
      panelClass: 'full-width-dialog'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.ticketService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }

  // openTicket(id: string = '0', nome: string = '', elem?: any): void {
  openTicket(): void {
    const dialogRef = this.dialog.open(AlunoTicketNovoComponent, {
      width: '90vw',
      data: { id: this.matriculaId },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  openOcorrencia(id): void {
    const dialogRef = this.dialog.open(AlunoOcorrenciaNovoComponent, {
      width: '90vw',
      data: { id: id },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  openDetalheOcorrencia(id, assunto) {
    const dialogRef = this.dialog.open(AlunoOcorrenciaDetalheComponent, {
      width: '90vw',
      data: { id: id, assunto: assunto },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
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
}
