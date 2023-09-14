import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MatTableDataSource } from '@angular/material/table';
// import { ActivatedRoute } from '@angular/router';

import { AssuntoTicketService } from 'src/app/services/ticket/assunto-ticket.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { AuthService } from 'src/app/security/auth.service';


@Component({
  selector: 'app-criacao-assuntos-individual',
  templateUrl: './criacao-assuntos-individual.component.html',
  styleUrls: ['./criacao-assuntos-individual.component.scss']
})
export class CriacaoAssuntosIndividualComponent implements OnInit, OnDestroy {

  error = false;
  isLoadingResults = false;
  sending = false;
  form: FormGroup;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  update = false;
  id = 0;
  selection = new SelectionModel<any>(true, []);
  formData: FormData = new FormData();
  filterUnidades: Observable<any[]>;
  showColaborador = false;
  departamentos: any[] = null;
  unidadesDefault = null;
  colaboradorSource = new MatTableDataSource();
  colaboradorColumns: string[] = ['select', 'nome'];
  funcionarioAssuntoTicket: any[] = [];
  firstLoad = true;
  constructor(
    public dialogRef: MatDialogRef<CriacaoAssuntosIndividualComponent>,
    private fb: FormBuilder,
    private assuntoTicketService: AssuntoTicketService,
    private animationsService: AnimationsService,
    private formService: FormService,
    private unidadeService: UnidadeService,
    private usuarioService: UsuarioService,
    private authService: AuthService,
    // private routerActive: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public data,
  ) {
    // Get id

    this.id = data.id ? data.id : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    /*this.assunto = this.data?.assunto;*/
    this.buildForm();
    this.isLoadingResults = true;
    Promise.all([this.getunidades()])
      .then(() => {
        if (this.id) {

          this.firstLoad = true;
          this.loadData(this.id); }
        else {
          this.isLoadingResults = false;
          this.firstLoad = false;
        }
      })
      .catch(
        () => this.isLoadingResults = false
      );
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.update);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildForm(): void {
    this.form = this.fb.group({
      descricao: [null, [Validators.required]],
      tempoEmDias: [null, [Validators.required]],
      unidadeId: [null],
      departamentoId: [null],
      unidadeSelect: [null],
    });
    this.id = this.data.id;
    if (this.data.element) {
      const { descricao, tempoEmDias } = this.data.element;
      this.form.get('descricao').setValue(descricao);
      this.form.get('tempoEmDias').setValue(tempoEmDias);
    }

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      this.showColaborador = false;
      const unidadeId = this.unidadesDefault?.length > 0 ? this.unidadesDefault.find(elem => elem.nome === val) : null;
      if (unidadeId && unidadeId.id) {
        this.form.get('unidadeId').setValue(unidadeId.id);
        const departamentos = this.unidadesDefault.find(elem => elem.id === unidadeId.id);
        this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];
        this.formService.enableField(this.form.get('departamentoId'));
        this.formService.notMandatoryFields(this.form.get('departamentoId'));
      }
      else {
        this.form.get('unidadeId').setValue(null);
        this.formService.disableField(this.form.get('departamentoId'));
      }
    });
    this.form.get('departamentoId').valueChanges.subscribe(async val => {
      if (!val) { return; }
      const colaboradores = await this.getColaboradores(val);
      if(this.firstLoad) {
        this.firstLoad = false;
        colaboradores.forEach(elem => this.funcionarioAssuntoTicket.find(elem2 => {
          if(elem2?.id === elem?.id) { this.selection.select(elem?.funcionario.id); }
        }));
      }
    });
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getAll()
        .subscribe(val => {
          if (val.status === 'error') {
            this.error = true;
            rej();
          }
          else { this.unidadesDefault = val; }
          this.form.get('unidadeSelect').setValue('');
          res();
        });
    });
  }

  getColaboradores(centroCustoId): Promise<any> {
    return new Promise((resolve, reject) => {
      const { unidadeId } = this.form.value;
      this.usuarioService.getFiltrar({ unidadeId, centroCustoId })
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            reject();
          }
          else {
            const usuario = this.authService.getToken();
            const data = val.filter(elem => elem?.id !== usuario?.user?.id);
            this.colaboradorSource.data = data;
            this.showColaborador = true;
            resolve(data);
          }
        });
    });
  }


  loadData(id): void {

    this.isLoadingResults = true;
    this.assuntoTicketService.getPorId(id)
      .subscribe(async val => {
        if (!val || val.status === 'error') {
          this.error = true;
          return;
        }
        const { unidadeId, centroCustoId, usuarios } = val;

        if (unidadeId) {
          const unidade = this.unidadesDefault.find(elem => elem.id === unidadeId);
          if (unidade?.nome) {
            this.form.get('unidadeSelect').setValue(unidade.nome);
            this.form.get('unidadeId').setValue(unidade.id);
          }

          if (!this.departamentos) {
            const departamentos = this.unidadesDefault.find(elem => elem.id === unidadeId);
            this.departamentos = departamentos?.centroCusto ? departamentos.centroCusto : [];
          }
        }

        if (usuarios && usuarios.length > 0) {
          this.funcionarioAssuntoTicket = usuarios;
        }

        if (centroCustoId) {
          const unidade = this.unidadesDefault.find(elem => elem.id === unidadeId);
          if (!unidade?.centroCusto) {
            // this.isLoadingResults = false;
            return;
          }
          const departamento = unidade.centroCusto.find(elem => elem.id === centroCustoId);
          if (departamento?.id) { this.form.get('departamentoId').setValue(departamento.id); }

          // const colaboradores = await this.getColaboradores(centroCustoId);
          // colaboradores.forEach(elem => this.funcionarioAssuntoTicket.find(elem2 => {
          //   if (elem2?.id === elem?.id) {
          //     this.selection.select(elem?.id);
          //   }
          // }));
        }

        this.isLoadingResults = false;
      });
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  /** Whether the number of selected elements matches the total number of rows. */
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
        if(row?.funcionario?.id) this.selection.select(row?.funcionario?.id);
      });
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {status
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
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
    };
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvar(): void {
    const formValue = this.form.value;
    const funcionarioIds: number[] = [];
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    for (let index = 0; index < this.selection.selected.length; index++) {
      const element = this.selection.selected[index];
      if (element.id) {
        funcionarioIds.push(element.id);
      } else {
        funcionarioIds.push(element);
      }
    }
    formValue.funcionarioIds = funcionarioIds;
    formValue.CentroCustoId = formValue.departamentoId;
    this.sending = true;
    const data = { id: this.id, ...formValue };

    this.assuntoTicketService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.id === 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
      }

      this.sending = false;
      this.update = true;
    });
  }
}
