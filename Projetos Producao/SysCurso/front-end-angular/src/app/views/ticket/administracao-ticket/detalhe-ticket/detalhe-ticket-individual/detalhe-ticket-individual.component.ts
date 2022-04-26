import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnexoService } from 'src/app/services/anexo.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { MatTableDataSource } from '@angular/material/table';
import { FormService } from 'src/app/services/form.service';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { startWith, map } from 'rxjs/operators';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-detalhe-ticket-individual',
  templateUrl: './detalhe-ticket-individual.component.html',
  styleUrls: ['./detalhe-ticket-individual.component.scss']
})
export class DetalheTicketIndividualComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  formData: FormData = new FormData();
  form: FormGroup;
  nome: string = '';
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  departamentos: any[] = null;
  showColaborador: boolean = false;
  anexoEdit: any = null;
  sendingAnexo: boolean = false;
  hourMinute = HourMinuteMask;
  loadedFile: boolean = false;
  updated: boolean = false;
  unidades: any[] = null;
  colaboradorColumns: string[] = ['select', 'nome'];
  colaboradorSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);
  mensagens: any[] = [];
  statusTicket: number;
  aberturaChamado: Date | string;
  assunto: string = null;

  constructor(
    public dialogRef: MatDialogRef<DetalheTicketIndividualComponent>,
    private fb: FormBuilder,
    private anexoService: AnexoService,
    private animationsService: AnimationsService,
    private ticketService: TicketService,
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private unidadeService: UnidadeService,
    private formService: FormService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.assunto = this.data?.assunto;
    this.buildForm();
    this.isLoadingResults = true;
    Promise.all([
      this.getunidades()
    ])
      .then(() => this.loadData())
      .catch(() => this.isLoadingResults = false)
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.updated);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildForm(): void {
    this.form = this.fb.group({
      unidadeId: [null, [Validators.required]],
      departamentoId: [null],
      mensagem: [null, [Validators.required]],
      statusTicket: [null, [Validators.required]],

      unidadeSelect: [null, [Validators.required]],
    });

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      this.showColaborador = false;
      const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) {
        this.form.get('unidadeId').setValue(unidadeId.id);
        let departamentos = this.unidadesDefault.find(elem => elem.id == unidadeId.id);
        this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];
        this.formService.enableField(this.form.get('departamentoId'));
        this.formService.notMandatoryFields(this.form.get('departamentoId'));
      }
      else {
        this.form.get('unidadeId').setValue(null);
        this.formService.disableField(this.form.get('departamentoId'));
      }
    })

    this.form.get('departamentoId').valueChanges.subscribe(val => {
      if (!val) return;
      this.getColaboradores(val);
    });
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getUnidadesTicket()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.unidadesDefault = val;
          this.form.get('unidadeSelect').setValue('');
          res();
        });
    });
  }

  getColaboradores(departamentoId): void {
    const { unidadeId } = this.form.value;
    this.usuarioService.getFiltrar({ unidadeId, centroCustoId: departamentoId })
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          const usuario = this.authService.getToken();
          const data = val.filter(elem => elem?.id !== usuario?.user?.id);
          this.colaboradorSource.data = data;
          this.showColaborador = true;
        }
      });
  }

  loadData(): void {
    if (this.data.id != 0) {
      this.isLoadingResults = true;
      this.ticketService.getTimeline(this.data.id)
        .subscribe(val => {
          if (!val || val['status'] === 'error') {
            this.error = true
            return;
          }
          const { statusTicket, mensagens, usuarioResponsavel } = val;
          this.statusTicket = statusTicket;
          this.mensagens = mensagens;

          if(usuarioResponsavel?.unidadeId) {
            const unidade = this.unidadesDefault.find(elem => elem.id == usuarioResponsavel.unidadeId);
            if (unidade?.nome) this.form.get('unidadeSelect').setValue(unidade.nome);
          }

          if(usuarioResponsavel?.departamentoId) {
            const unidade = this.unidadesDefault.find(elem => elem.id === usuarioResponsavel.unidadeId);
            if(!unidade?.centroCusto) {
              this.isLoadingResults = false;
              return;
            }

            const departamento = unidade.centroCusto.find(elem => elem.id === usuarioResponsavel?.departamentoId);
            if (departamento?.id) this.form.get('departamentoId').setValue(departamento.id);
          }

          if(usuarioResponsavel?.statusTicket) {
            this.form.get('statusTicket').setValue(usuarioResponsavel.statusTicket);
          }

          if(mensagens.length > 0) this.aberturaChamado = mensagens[0].data;

          this.isLoadingResults = false;
        })
    }
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.colaboradorSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.colaboradorSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {status
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const { statusTicket, unidadeId, departamentoId, mensagem } = this.form.value;
    const usuarioDestinarioTicket: any[] = [];

    const usuario = this.authService.getToken();

    if(!usuario) {
      this.animationsService.showErrorSnackBar('Favor logar no sistema');
      return;
    }

    this.selection.selected.forEach(elem => {
      usuarioDestinarioTicket.push({
        id: 0,
        funcionarioId: elem.id
      })
    })

    const data = {
      statusTicket,
      ticketId: this.data.id,
      usuarioLogadoId: usuario?.user?.id,
      mensagem,
      usuarioDestinarioTicket,
      unidadeId,
      departamentoId,
    };

    // Make request
    this.ticketService.responderTicket(data).subscribe(val => {
      if (!val || val?.status === 'error') return this.error = true;
      this.animationsService.showSuccessSnackBar('Salvo com sucesso');
      this.updated = true;
      this.form.reset();
      if (this.loadedFile) {
        // Post file
        this.formData.set('DestinatarioTicketId', val?.id);

        this.anexoService.upload(this.formData).subscribe(valAnexo => {
          if (valAnexo && valAnexo['status'] && valAnexo['status'] == 'done') {
            this.animationsService.showSuccessSnackBar('Salvo com sucesso');
            this.sendingAnexo = false;
            this.loadedFile = false;
            this.anexoEdit = null;
            this.loadData();
          }
        });
      } else {
        this.sendingAnexo = false;
        this.loadedFile = false;
        this.loadData();
      }
    })
  }

  loadFile(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);
    reader.onloadend = () => {
      // Set FormData
      this.formData.set('file', file);
      this.formData.set('descricao', '');
      this.formData.set('tipoAnexo', '0');
      if (this.anexoEdit?.id) {
        this.formData.set('id', this.anexoEdit.id.toString());
      }

      this.loadedFile = true;
    };
  }

  download(id, arquivoString, extensao): void {
    this.anexoService.download(id, arquivoString, extensao);
  }

}
