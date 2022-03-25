import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { MatDialog } from '@angular/material/dialog';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { DetalheTicketIndividualComponent } from '../administracao-ticket/detalhe-ticket/detalhe-ticket-individual/detalhe-ticket-individual.component';
import { AssuntoTicketService } from 'src/app/services/ticket/assunto-ticket.service';
import { AuthService } from 'src/app/security/auth.service';
import { SelectionModel } from '@angular/cdk/collections';


@Component({
  selector: 'app-painel-ticket',
  templateUrl: './painel-ticket.component.html',
  styleUrls: ['./painel-ticket.component.scss']
})
export class PainelTicketComponent implements OnInit {
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
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private ticketService: TicketService,
    private unidadeService: UnidadeService,
    private authService: AuthService,
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
    this.buildForm();
    this.getunidades();
    this.getAssuntoTickets();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      unidadeId: [null],
      statusTickets: [null],
      nomeAluno: [null],
      numeroMatricula: [null],
      numeroProtocolo: [null],
      unidadeSelect: [''],
      periodoAberturaInicio: [null],
      periodoAberturaFim: [null],
      nomeResponsavel: [null],
      filtroAvancado: true,
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

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    const formValue = this.form.value;
    delete formValue.unidadeSelect;

    const usuario = this.authService.getToken();
    formValue['usuarioLogadoId'] = usuario?.user?.id;

    if (formValue.statusTickets == null)
      formValue.statusTickets = 1;


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
        else {
          this.unidadesDefault = val;
          if (val.length == 1) {
            this.form.get('unidadeSelect').setValue(val[0].nome);
            this.form.get('unidadeSelect').disable();
          }
          else
            this.form.get('unidadeSelect').setValue('')
        }
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
}
