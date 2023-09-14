import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AnexoService } from 'src/app/services/anexo.service';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { TicketService } from 'src/app/services/ticket/ticket.service';
import { FormService } from 'src/app/services/form.service';
import { SelectionModel } from '@angular/cdk/collections';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { AssuntoTicketService } from 'src/app/services/ticket/assunto-ticket.service';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-ticket-individual',
  templateUrl: './ticket-individual.component.html',
  styleUrls: ['./ticket-individual.component.scss']
})
export class TicketIndividualComponent implements OnInit {
  id: number = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  formData: FormData = new FormData();
  form: FormGroup;
  colaboradorColumns: string[] = ['select', 'nome'];
  colaboradorSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);
  nome: string = '';
  sendingAnexo: boolean = false;
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  departamentos: any[] = null;
  showColaborador: boolean = false;
  anexoEdit: any = null;
  loadedFile: boolean = false;
  assuntosTicket: any[] = null;
  idUnidade: any;
  idDepartamento: any;
  idAssuntoTicket: number = null;
  isGetAssunto: boolean = false;
  disableButton: boolean = false;
  funcionarioAssuntoTicket: any[] = [];
  constructor(
    private location: Location,
    private fb: FormBuilder,
    private anexoService: AnexoService,
    private ticketService: TicketService,
    private unidadeService: UnidadeService,
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private assuntoTicketService: AssuntoTicketService,
    private animationsService: AnimationsService,
    private formService: FormService,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    Promise.all([
      this.getunidades(),
      this.getAssuntoTickets()
    ])
      .then(() => this.loadData())
      .catch(() => this.isLoadingResults = false)
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      unidadeId: [null, [Validators.required]],
      departamentoId: [null],
      assuntoTicketId: [null, [Validators.required]],
      mensagem: [null, [Validators.required]],

      unidadeSelect: [null, [Validators.required]]
    });

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      this.showColaborador = false;
      const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome === val) : null;
      if (unidadeId && unidadeId.id) {
        this.idUnidade = unidadeId.id;
        this.form.get('unidadeId').setValue(unidadeId.id);
        this.getDepartamentos(unidadeId.id);
        if (!this.isGetAssunto) {
          this.idAssuntoTicket = null;
          this.idDepartamento = null;
          this.form.get('assuntoTicketId').setValue('');
          this.form.get('departamentoId').setValue('');
          this.getAssuntoTickets();
        }
      }
      else {
        this.form.get('unidadeId').setValue(null);
        this.formService.disableField(this.form.get('departamentoId'));
      }
    })

    this.form.get('departamentoId').valueChanges.subscribe(val => {
      if (!val) { return; }
      this.getColaboradores(val);
      this.idDepartamento = val;
      if (!this.isGetAssunto) {
        this.idAssuntoTicket = null;
        this.form.get('assuntoTicketId').setValue('');
        this.getAssuntoTickets();
      }
    });

    this.form.get('assuntoTicketId').valueChanges.subscribe(val => {
      if (!val) { return; }
      this.idAssuntoTicket = val;
      const id = this.idAssuntoTicket;
      this.idDepartamento = null;
      this.form.get('unidadeSelect').setValue('', { emitEvent: false });
      this.form.get('departamentoId').setValue('', { emitEvent: false });
      this.form.get('unidadeId').setValue('', { emitEvent: false });
      this.assuntoTicketService.getPorId(id)
        .subscribe(val => {
          if (val.status === 'error') {
            this.error = true;
          }
          else {

            if (val.unidadeId) {
              this.isGetAssunto = true;
              this.idUnidade = val.unidadeId;
              const departamentos = this.unidadesDefault.find(elem => elem.id === val.unidadeId);
              this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];

              this.form.get('unidadeSelect').setValue(val.unidade.nome, { emitEvent: false });
              this.form.get('unidadeId').setValue(val.unidadeId, { emitEvent: false });
              this.getDepartamentos(val.unidadeId);
              if (val.centroCustoId) {

                this.idDepartamento = val.centroCustoId;
                this.form.get('departamentoId').setValue(val.centroCustoId, { emitEvent: false });
                this.getColaboradores(val.centroCustoId);

                this.funcionarioAssuntoTicket = val.funcionarioAssuntoTicket;

                if (val.funcionarioAssuntoTicket.lenght > 0) {
                  this.colaboradorSource.data.forEach(elem => this.funcionarioAssuntoTicket.find(elem2 => {
                    if (elem2?.usuarioId === elem['id']) {
                      this.selection.select(elem['id']);
                    }
                  }));
                }
              }

              this.showColaborador = true;
              this.idAssuntoTicket = id;
              this.isGetAssunto = false;
            }
          }
        });
    });
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getUnidadesTicket()
        .subscribe(val => {
          if (val.status === 'error') {
            this.error = true;
            rej();
          }
          else { this.unidadesDefault = val; }
          this.form.get('unidadeSelect').setValue('');
          res(val);
        });
    });
  }

  getDepartamentos(idUnidade) {
    const departamentos = this.unidadesDefault.find(elem => elem.id === idUnidade);
    this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];
    this.formService.enableField(this.form.get('departamentoId'));
    this.formService.notMandatoryFields(this.form.get('departamentoId'));
    if (!this.isGetAssunto) {
      this.getAssuntoTickets();
    }
  }

  getAssuntoTickets(): void {

    this.assuntoTicketService.getPorUnidadeDepartamento(this.idUnidade, this.idDepartamento)
      .subscribe(val => {
        if (val.status === 'error') {
          this.error = true;
        }
        else {
          this.assuntosTicket = val;
        }
      });
  }

  getColaboradores(departamentoId): Promise<any> {
    return new Promise((resolve, reject) => {
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
            this.selection.clear();
            this.colaboradorSource.data.forEach(elem => this.funcionarioAssuntoTicket.find(elem2 => {
              if (elem2?.usuarioId === elem['id']) {
                this.selection.select(elem);
              }
            }));

            this.showColaborador = true;
            resolve(data);
          }
        });
    });
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.ticketService.getPorId(this.id)
        .subscribe(val => {
          if (!val || val['status'] === 'error') return this.error = true;

          const patch: any = {};
          for (let key in val) {
            if (val[key]) patch[key] = val[key];
          }

          this.form.patchValue(patch);
          this.isLoadingResults = false;
        })
    }
  }

  resetForm(): void {
    this.form.reset();
    this.form.get('id').setValue(0);
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
      this.colaboradorSource.data.forEach((row: any) => {
        if (row?.funcionario?.id) { this.selection.select(row); }
      });
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    status
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  voltar(): void {
    this.location.back();
  }

  salvarData(): void {
    // Validating form
    this.disableButton = true;
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      this.disableButton = false;
      return;
    }

    const { id, assuntoTicketId, unidadeId, departamentoId, mensagem } = this.form.value;
    const usuarioDestinarioTicket: any[] = [];

    const usuario = this.authService.getToken();

    if (!usuario) {
      this.animationsService.showErrorSnackBar('Favor logar no sistema');
      this.disableButton = false;
      return;
    }

    this.selection.selected.forEach(elem => {
      usuarioDestinarioTicket.push({
        id: 0,
        funcionarioId: elem.id
      })
    })

    const destinatarioTicket = { id: 0, usuarioLogadoId: usuario?.user?.id, unidadeId, departamentoId, mensagem, usuarioDestinarioTicket };

    const data = {
      id,
      assuntoTicketId,
      usuarioLogadoId: usuario?.user?.id,
      // unidadeId,
      // departamentoId,
      // mensagem,
      destinatarioTicket
    };

    // Make request
    this.ticketService.cadastrar(data).subscribe(val => {
      if (!val || val?.status === 'error') return this.error = true;
      this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');

      const { id } = val;
      this.id = id;
      this.form.get('id').setValue(id);

      if (this.loadedFile) {
        // Post file
        this.formData.set('DestinatarioTicketId', val?.destinatarioTicket[0]?.id);

        this.anexoService.upload(this.formData).subscribe(valAnexo => {
          if (valAnexo && valAnexo['status'] && valAnexo['status'] == 'done') {
            this.animationsService.showSuccessSnackBar('Salvo com sucesso');
            this.sendingAnexo = false;
            this.loadedFile = false;
            this.anexoEdit = null;
            this.disableButton = false;
            this.voltar();
          }
        });
      } else {
        this.sendingAnexo = false;
        this.loadedFile = false;
        this.disableButton = false;
        this.voltar();
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
      if (this.anexoEdit && this.anexoEdit.id) {
        this.formData.set('id', this.anexoEdit.id.toString());
      }
      this.loadedFile = true;
    };
  }

  download(id, arquivoString, extensao): void {
    this.anexoService.download(id, arquivoString, extensao);
  }
}
