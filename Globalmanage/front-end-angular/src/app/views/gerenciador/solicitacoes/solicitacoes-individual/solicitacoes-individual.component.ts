import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { SolicitacoesService } from 'src/app/services/gerenciador/solicitacoes.service';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { map, startWith } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AuthService } from 'src/app/security/auth.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { MatChipInputEvent, MatChipList } from '@angular/material/chips';
import { AbstractControl, ValidatorFn } from '@angular/forms';

@Component({
  selector: 'app-solicitacoes-individual',
  templateUrl: './solicitacoes-individual.component.html',
  styleUrls: ['./solicitacoes-individual.component.scss']
})
export class SolicitacoesIndividualComponent implements OnInit {
  @ViewChild('chipList', { static: true }) chipList: MatChipList;
  error: boolean = false;
  isLoadingResults: boolean = false;
  sending: boolean = false;
  form: FormGroup;
  update: boolean = false;
  id: number = 0;
  cursos = [];
  parcelas = Array(12).fill('').map((x,i) => i+1);
  colaboradorColumns: string[] = ['select', 'nome'];
  colaboradorSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  unidades: any[] = null;
  departamentos: any[] = null;
  showColaborador: boolean = false;
  solicitacaoFuncionarioTicket: any[] = [];
  firstLoad: boolean = true;
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  emailDestinatarios: string[] = [];
  imgProfileSrc: string = null;
  file: any;
  updateImg: boolean = false;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private solicitacoesService: SolicitacoesService,
    private animationsService: AnimationsService,
    private cursoService: CursoService,
    private formService: FormService,
    private routerActive: ActivatedRoute,
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private unidadeService: UnidadeService,
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();

    this.isLoadingResults = true;
    Promise.all([this.getCursos(), this.getunidades(), this.getImg()])
      .then(() => {
        if (this.id) this.loadData(this.id);
        else {
          this.isLoadingResults = false;
          this.firstLoad = false;
        }
      })
      .catch(() => this.isLoadingResults = false)
  }

  validarEmail(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      return (control.value === null && this.emailDestinatarios?.length === 0) ? { required: true } : null;
    };
  }

  buildForm(): void {
    this.form = this.fb.group({
      descricao: [null, [Validators.required]],
      valor: [null, [Validators.required]],
      solicitacaoCursoIds: [null, [Validators.required]],
      quantidadeParcelaPaga: [null, [Validators.required]],
      isBalcao: [false, [Validators.required]],
      tipoSolicitacao: [false, [Validators.required]],
      enviaTicket: [false, [Validators.required]],
      enviaTicketPosPgto: [false],
      enviaEmail: [false, [Validators.required]],
      enviaEmailPosPgto: [false],
      isPendenciaDocumental: [false, [Validators.required]],
      isAnexo: [false, [Validators.required]],
      emailDestinatarios: [null],
      emailTitulo: [null],
      emailConteudo: [null],
      unidadeId: [null],
      centroCustoId: [null],
      statusCertificados: [null],
      statusProvas: [null],

      unidadeSelect: [null],
    });

    this.form.get('tipoSolicitacao').valueChanges.subscribe(val => {
      if(val) this.formService.disableField(this.form.get('valor'))
      else this.formService.enableField(this.form.get('valor'))
    });

    this.form.get('enviaEmail').valueChanges.subscribe(val => {
      if(val) {
        this.form.get('emailDestinatarios').setValidators([this.validarEmail()]);
        this.form.get('emailDestinatarios').updateValueAndValidity();
        this.form.get('emailDestinatarios').enable();
        this.formService.enableField(this.form.get('emailTitulo'));
        this.formService.enableField(this.form.get('emailConteudo'));
        this.formService.enableField(this.form.get('enviaEmailPosPgto'));
      } else {
        this.formService.disableField(this.form.get('emailDestinatarios'));
        this.formService.disableField(this.form.get('emailTitulo'));
        this.formService.disableField(this.form.get('emailConteudo'));
        this.formService.disableField(this.form.get('enviaEmailPosPgto'));
        this.emailDestinatarios = [];
      }
    })

    this.form.get('enviaTicket').valueChanges.subscribe(val => {
      if(val) {
        this.formService.enableField(this.form.get('unidadeSelect'));
        this.formService.enableField(this.form.get('unidadeId'));
        this.formService.enableField(this.form.get('centroCustoId'));
        this.formService.enableField(this.form.get('enviaTicketPosPgto'));
      } else {
        this.formService.disableField(this.form.get('unidadeSelect'));
        this.formService.disableField(this.form.get('unidadeId'));
        this.formService.disableField(this.form.get('centroCustoId'));
        this.formService.disableField(this.form.get('enviaTicketPosPgto'));
        this.selection.clear();
        this.colaboradorSource.data = [];
      }
    })

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
        this.formService.enableField(this.form.get('centroCustoId'));
        this.formService.notMandatoryFields(this.form.get('centroCustoId'));
      }
      else {
        this.form.get('unidadeId').setValue(null);
        this.formService.disableField(this.form.get('centroCustoId'));
      }
    })

    this.form.get('centroCustoId').valueChanges.subscribe(async val => {
      if (!val) { return; }
      const colaboradores = await this.getColaboradores(val);
      if(this.firstLoad) {
        this.firstLoad = false;
        colaboradores.forEach(elem => this.solicitacaoFuncionarioTicket.find(elem2 => {
          if(elem2?.funcionarioId === elem?.funcionario?.id) { this.selection.select(elem?.funcionario.id); }
        }));
      }
    });

    this.form.get('emailDestinatarios').statusChanges.subscribe( status => {
      if(!this.chipList) { return; }
      this.chipList.errorState = status === 'INVALID'
    });
  }

  loadData(id): void {
    this.solicitacoesService.getPorId(id).subscribe(val => {
      this.isLoadingResults = false;

      if(val?.status === 'error') return;
      this.form.patchValue(val);

      const { isInscritoProva, isAprovadoProva, unidadeId, centroCustoId, statusCertificado, statusProvaEnum,
        solicitacaoFuncionarioTicket, emailDestinatario, isPreDefinida, descricao } = val;

      const cursosIds = val?.solicitacaoCurso ? [ ...new Set(val.solicitacaoCurso.map(elem => elem.cursoId))] : [];
      this.form.get('solicitacaoCursoIds').setValue(cursosIds);
      this.form.get('tipoSolicitacao').setValue(val?.tipoSolicitacao === 1 ? false : true);

      if(unidadeId) {
        const unidade = this.unidadesDefault.find(elem => elem.id == unidadeId);
        if (unidade?.nome) { this.form.get('unidadeSelect').setValue(unidade.nome); }
      }

      if(centroCustoId) {
        const unidade = this.unidadesDefault.find(elem => elem.id === unidadeId);
        if(!unidade?.centroCusto) {
          this.isLoadingResults = false;
          return;
        }
        const departamento = unidade.centroCusto.find(elem => elem.id === centroCustoId);
        if (departamento?.id) this.form.get('centroCustoId').setValue(departamento.id);
      }
      this.solicitacaoFuncionarioTicket = solicitacaoFuncionarioTicket;

      if(emailDestinatario) this.emailDestinatarios = emailDestinatario.map(elem => elem?.email);
      this.form.get('emailDestinatarios').setValue(null);
      if(statusCertificado) this.form.get('statusCertificados').setValue([...statusCertificado.map(elem => elem?.statusCertificadoEnum)]);
      if(statusProvaEnum) this.form.get('statusProvas').setValue([...statusProvaEnum.map(elem => elem?.statusProvaEnum)])

      if(isPreDefinida) this.formService.disableField(this.form.get('descricao'), descricao);

    });
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getAll()
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

  getCursos(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.cursoService.getCursos().subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
          reject();
        }
        else {
          this.cursos = val;
          resolve();
        }
      })
    });
  }

  getImg(): Promise<any> {
    return new Promise((res, rej) => {
      if(this.id === 0) return rej();

      this.solicitacoesService.getImg(this.id)
        .subscribe(val => {
          if (val?.status === 'error') {
            this.error = true;
            return rej();
          }
          this.imgProfileSrc = val;
          return res();
        });
    });
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

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;
    if ((value || '').trim()) this.emailDestinatarios.push(value.trim());
    if (input) input.value = '';
  }

  remove(emailDestinatarios: string): void {
    const index = this.emailDestinatarios.indexOf(emailDestinatarios);
    if (index >= 0) this.emailDestinatarios.splice(index, 1);
    if(this.emailDestinatarios?.length === 0) this.form.get('emailDestinatarios').setValue(null);
  }

  saveImg(solicitacaoId): void {
    if(!this.updateImg) return;
    const extensao = this.imgProfileSrc.split(';base64,')[0].replace('data:', '');
    const foto = this.imgProfileSrc.split(';base64,')[1];
    const data = { foto, extensao, solicitacaoId }
    this.solicitacoesService.uploadFoto(data).subscribe(val => {

    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  salvar(): void {
    this.formService.validateAllFields(this.form);

    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    this.sending = true;
    const formValue = this.form.value
    formValue.tipoSolicitacao = formValue.tipoSolicitacao ? 2 : 1;
    formValue.emailDestinatarios = (this.emailDestinatarios?.length === 0) ? formValue.emailDestinatarios : this.emailDestinatarios;
    formValue['funcionarioIds'] = this.selection.selected;
    delete formValue.unidadeSelect;

    const data = { id: this.id, ...formValue }

    this.solicitacoesService.cadastrar(data).subscribe(val => {
      if (!val?.status) {
        this.animationsService.showSuccessSnackBar(this.id == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
      }

      this.saveImg(val.id);
      this.sending = false;
      this.update = true;
    })
  }

  loadImgProfile(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    this.file = event.target.files[0];
    // Render file
    reader.readAsDataURL(file);

    reader.onloadend = e => {
      this.imgProfileSrc = reader.result as string;
      this.updateImg = true;
    };
  }

  removeImg(): void {
    this.imgProfileSrc = null;
    this.updateImg = false;
    if(this.id) this.solicitacoesService.removerFoto(this.id).subscribe();
  }
}
