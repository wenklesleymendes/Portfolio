import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit, ViewEncapsulation, Compiler } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { Observable, Subscription } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { Navigation, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import * as moment from 'moment';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { StatusProva } from '../../../../utils/variables/statusProva';
import { ProvaAlunoService } from '../../../../services/provas/provaAluno.service';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { TransporteProvaService } from '../../../../services/provas/transporteProva.service';
import { AlunoProvasCertificadosConfirmacaoComponent } from './aluno-provas-certificados-confirmacao/aluno-provas-certificados-confirmacao';
//import printJS from 'print-js';
import printJS from 'src/app/utils/custom-printjs/src/index'; // printJS modificado para esperar carregamento completo do iframe
import { AuthService } from 'src/app/security/auth.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
// tslint:disable-next-line: max-line-length
import { AlunoProvasCertificadosMateriasComponent } from './aluno-provas-certificados-materias/aluno-provas-certificados-materias.component';
import { BoletoAviso2Component } from '../aluno-financeiro-contrato/matricula-formas-pagamento/mfp-boleto/boleto-aviso2/boleto-aviso2.component';

@Component({
  selector: 'app-aluno-provas-certificados',
  templateUrl: './aluno-provas-certificados.component.html',
  styleUrls: ['./aluno-provas-certificados.component.scss']
})
export class AlunoProvasCertificadosComponent implements OnInit {
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  id = 0;
  statusProva = StatusProva;
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  colegiosDefault = null;
  tipoProva = 1;
  matriculaAluno: any;
  matriculaId: number = null;
  unidadeId: any;
  matriculaAluno$: Subscription;
  cursoId: any;
  alunoId: any;
  datasProvas: any;
  today = moment();
  dataDefinida: any;
  podeInscrever = false;
  podeAprovar = false;
  inscrito = false;
  dadosTransporte = null;
  statusProvaAtual: number;
  agendaProvaId = null;
  dataProva: any;
  inicioSenha: any;
  provasRealizadas: any[];
  desabilitarForm: boolean = false;
  displayedColumns: string[] = [
    'tabDataProva',
    'tabTipoProva',
    'tabLocalProva',
    'tabStatusProva',
    'tabNumeroOnibus',
    'tabHoraSaida',
    'tabMaterias'
  ];

  constructor(
    private dialog: MatDialog,
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private formService: FormService,
    private router: Router,
    private provaAlunoService: ProvaAlunoService,
    private transporteProvaService: TransporteProvaService,
    private store: Store<AlunoStoreState.Aluno>,
    private authService: AuthService,
  ) { }


  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.matriculaAluno$ = this.store.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      this.matriculaAluno = val;
      this.cursoId = val.cursoId;
      this.unidadeId = val.unidadeId;
      this.alunoId = val.alunoId;
    });
    this.buildForm();

    Promise.all([
    ])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);

  }

  ngOnDestroy(): void {
    this.matriculaAluno$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildForm(): void {
    this.form = this.fb.group({
      colegioId: [null],

      statusProva: [null, [Validators.required]],
      colegioSelect: [null, [Validators.required]],
      localProva: [null],
      tipoProva: [null, [Validators.required]],
      dataProva: [null],
      dataInscricao: [null],
      tipoTransporte: [null],
      identificacaoUsuario: [null],
      senhaProva: [null],
    });

    this.form.get('statusProva').setValue(1);

    this.form.get('colegioSelect').disable();
    this.form.get('tipoProva').disable();
    this.form.get('dataProva').disable();
    this.form.get('tipoTransporte').disable();

    this.filterUnidades = this.form.get('colegioSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.colegiosDefault.slice())
      );

    this.form.get('statusProva').valueChanges
      .subscribe(status => {
        if (!this.inscrito) {
          if (status == '1' && this.desabilitarForm) {
            this.form.get('colegioSelect').disable();
            this.form.get('tipoProva').disable();
            this.form.get('dataProva').disable();
            this.form.get('dataInscricao').disable();
            this.form.get('tipoTransporte').disable();
            this.form.get('identificacaoUsuario').disable();
            this.form.get('senhaProva').disable();
          }
          else {
            this.formService.mandatoryFields(this.form.get('colegioSelect'));
            this.form.get('colegioSelect').enable();
            this.form.get('tipoProva').enable();
            this.form.get('dataProva').enable();
            this.form.get('dataInscricao').enable();
            this.form.get('tipoTransporte').enable();
            this.form.get('identificacaoUsuario').enable();
            this.form.get('senhaProva').enable();
          }
        }
      });

    this.form.get('colegioSelect').valueChanges.subscribe(val => {
      const colegioId = this.colegiosDefault?.length >= 0 ? this.colegiosDefault.find(elem => elem.nomeColegioAutorizado == val) : null;
      if (colegioId && colegioId.id) {
        this.form.get('colegioId').setValue(colegioId.id);
        this.getDatas(colegioId.id);

        this.form.get('dataProva').reset();
        this.form.get('tipoTransporte').reset();
        this.form.get('tipoProva').reset();
      }
      else { this.form.get('colegioId').setValue(null); }
    });

    this.form.get('tipoProva').valueChanges.subscribe(val => {
      if (val != null) {
        this.tipoProva = val;
        if (val == '1' || val == 'Presencial') { //Presencial
          this.formService.mandatoryFields(this.form.get('dataProva'));
          this.formService.mandatoryFields(this.form.get('tipoTransporte'));

          this.form.get('dataProva').reset();
          this.form.get('tipoTransporte').reset();
          this.form.get('identificacaoUsuario').reset();
          this.form.get('senhaProva').reset();
          this.dataDefinida = true;
        }
        else { //Online
          this.formService.notMandatoryFields(this.form.get('dataProva'));
          this.formService.notMandatoryFields(this.form.get('tipoTransporte'));

          this.form.get('dataInscricao').setValue(this.today.format('DD/MM/YYYY'));
          this.form.get('dataProva').reset();
          this.form.get('tipoTransporte').reset();
          this.form.get('identificacaoUsuario').reset();
          this.form.get('senhaProva').reset();
          this.dataDefinida = false;

          this.form.get('senhaProva').setValue(this.inicioSenha);
        }
      }
    });

    this.form.get('dataProva').valueChanges.subscribe(val => {
      if (val == null) { return; }
      this.dataDefinida = this.datasProvas?.find(value => {
        return moment(value.dataProva).format('YYYY-MM-DD') == val.format('YYYY-MM-DD');
      });
      this.dataProva = val.format('DD/MM/YYYY')

      if (this.dataDefinida) {
        this.agendaProvaId = this.dataDefinida.id;
        this.form.get('tipoTransporte').reset('');
      }
      else {
        this.form.get('tipoTransporte').setValue('2');
      }
    });

    this.form.get('tipoTransporte').valueChanges.subscribe(val => {
      if (val == null || val != 1) {
        this.dadosTransporte = null;
        return;
      }
      this.getProximoOnibus(this.agendaProvaId);
    });

    this.form.get('identificacaoUsuario').valueChanges.subscribe(val => {
      if (val == null || val == '') { return; }
      this.form.get('identificacaoUsuario').setValue(val.replace(/\D/, ''), { emitEvent: false });
    });

    this.form.get('senhaProva').valueChanges.subscribe(val => {
      if (val == null || val == '') { return; }
      this.form.get('senhaProva').setValue(val.replace(/\D/, ''), { emitEvent: false });
    });
  }

  loadData(): void {

    this.isLoadingResults = true;

    this.provaAlunoService.informacaoCadastro(this.matriculaAluno?.id).subscribe(val => {

      if (!val) { return; }
      if (val?.status === 'error') { return this.error = true; }

      this.inicioSenha = val.alunoRG?.replace(/\D/, '').substring(0, 3);
      this.unidadesDefault = val.colegiosAutorizados;
      this.colegiosDefault = val.colegiosAutorizados;
      this.form.get('colegioSelect').setValue('');

      this.podeInscrever = !val.pendenciaPagamento && !val.pendenciaDocumental && val.dentroPrazo;

      console.log("teste: "+this.podeInscrever);

      const patch: any = {};
      for (let key in val) {
        if (val[key]) { patch[key] = val[key]; }
      }

      this.form.patchValue(patch);

      this.isLoadingResults = false;

      this.getProvasRealizadas(this.matriculaAluno?.id);
      this.provaAlunoService.buscarPorMatriculaId(this.matriculaAluno?.id).subscribe(val => {
        if (!val || val.id == 0) { return; }
        if (val?.status === 'error') { return this.error = true; }

        if (val.id && val.id != 0) {
          this.inscrito = true;

          if (val.tipoProva == 1 && moment(val.dataProva) < moment()) {
            this.podeAprovar = true;
          }
          else if (val.tipoProva === 2 && val.identificacaoUsuario && val.senhaProva) {
            this.podeAprovar = true;
          }

          this.statusProvaAtual = val.statusProva;
          if (val.statusProva === 3) {
            this.desabilitarForm = true;
          }
          else {
            this.desabilitarForm = false
          }
        }

        this.id = val.id;
        this.agendaProvaId = val.agendaProvaId;

        this.form.get('statusProva').setValue(val.statusProva);
        this.form.get('colegioSelect').setValue(val.colegioAutorizado?.nomeColegioAutorizado);
        this.form.get('tipoProva').setValue(val.tipoProva);
        this.form.get('colegioSelect').disable({ emitEvent: false });
        this.form.get('tipoProva').disable({ emitEvent: false });
        if (val.tipoProva == 1) {
          this.form.get('dataProva').setValue(val.dataProva);
          this.form.get('tipoTransporte').setValue(val.tipoTransporte);
          this.dataProva = moment(val.dataProva).format('DD/MM/YYYY');
          if (val.tipoTransporte == 1) {
            this.getOnibus(val.unidadeTransporteProvaId);
          }
          this.form.get('dataProva').disable({ emitEvent: false });
          this.form.get('tipoTransporte').disable({ emitEvent: false });
        }

        if (val.tipoProva == 2) {
          let dataInscricao;
          if (val.dataInscricao) {
            dataInscricao = moment(val.dataInscricao).format('DD/MM/YYYY');
          } else {
            dataInscricao = moment(this.today).format('DD/MM/YYYY');
          }

          this.form.get('dataInscricao').setValue(dataInscricao);
          this.form.get('identificacaoUsuario').setValue(val.identificacaoUsuario);
          this.form.get('senhaProva').setValue(val.senhaProva);
        }
      });
    });

    if (this.desabilitarForm) {
      this.form.controls['dataInscricao'].disable();
      this.form.controls['identificacaoUsuario'].disable();
      this.form.controls['senhaProva'].disable();
      this.form.disable();
    }
  }

  datePickerClass() {
    return (date: any): MatCalendarCellCssClasses => {
      if (this.datasProvas.find(data => data.dataProva.split('T')[0] == date.format('YYYY-MM-DD'))) {
        return 'highlight-date';
      } else {
        return '';
      }
    };
  }

  getDatas(colegioId): void {
    this.provaAlunoService.BuscarProvasDisponiveis(colegioId, this.unidadeId, this.cursoId)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') { this.error = true; }
        else { this.datasProvas = val; }

      });
  }

  getProximoOnibus(agendaProvaId): void {
    this.transporteProvaService.buscarProximoOnibus(agendaProvaId, this.unidadeId)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') { this.error = true; }
        else {
          this.dadosTransporte = val;
        }
      });
  }

  getOnibus(UnidadeTransporteProvaId): void {
    this.transporteProvaService.buscarOnibus(UnidadeTransporteProvaId)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') { this.error = true; }
        else {
          this.dadosTransporte = val;
        }
      });
  }
  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nomeColegioAutorizado.toLowerCase().indexOf(filterValue) === 0);
  }

  getDescricaoStatusProva(statusId): string {
    let descricaoStatus: string;
    switch (statusId) {
      case 1:
        descricaoStatus = 'Não Inscrito';
        break;
      case 2:
        descricaoStatus = 'Inscrito para Prova';
        break;
      case 3:
        descricaoStatus = 'Aprovado';
        break;
      case 4:
        descricaoStatus = 'Reprovado';
        break;
      case 5:
        descricaoStatus = 'Faltou / Reprovado';
        break;
      default:
        descricaoStatus = '';
        break;
    }
    return descricaoStatus;

  }

  getDescricaoTipoProva(tipoProva): string {
    let descricaoTipoProva: string;

    switch (tipoProva) {
      case 1:
        descricaoTipoProva = "Presencial"
        break;
      case 2:
        descricaoTipoProva = "On-line"
        break;

      default:
        break;
    }
    return descricaoTipoProva;
  }
  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  async salvarData(): Promise<void> {
    this.formService.validateAllFields(this.form);

    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    if (this.form.value.statusProva === 2) {
      this.confirmarDados();
    } else {
      this.openMaterias(this.id, this.form.value.statusProva);
    }
  }

  async salvarCredenciais(): Promise<void> {
    const formValue: any = this.form.value;
    if (formValue['identificacaoUsuario'].length < 6) {
      this.animationsService.showErrorSnackBar('Identificação do Usuário deve conter 6 dígitos');
      return;
    }

    if (formValue['senhaProva'].length < 6) {
      this.animationsService.showErrorSnackBar('Senha da Prova deve conter 6 dígitos');
      return;
    }

    let data = {
      id: this.id,
      identificacaoUsuario: formValue['identificacaoUsuario'],
      senhaProva: formValue['senhaProva']
    };

    this.provaAlunoService.cadastrarCredenciais(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar('Cadastrado com sucesso');
      }
    });
  }

  confirmarDados() {
    const dialogRef = this.dialog.open(AlunoProvasCertificadosConfirmacaoComponent, {
      width: '50vw',
      data: { id: this.alunoId },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Validating form
        const formValue: any = this.form.value;

        formValue['colegioAutorizadoId'] = this.unidadesDefault.find(val => val.nomeColegioAutorizado == formValue['colegioSelect']).id;
        formValue['localProva'] = formValue['colegioSelect'];
        formValue['matriculaAlunoId'] = this.matriculaAluno?.id;
        formValue['AgendaProvaId'] = this.agendaProvaId ?? undefined;
        formValue['dataInscricao'] = `${this.today.format('YYYY-MM-DD')}T03:00:00Z`;

        const usuario = this.authService.getToken();
        formValue["UsuarioLogadoId"] = usuario?.user?.id;

        let data = formValue;
        data.id = this.id;
        this.transporteProvaService.buscarProximoOnibus
        data.unidadeTransporteProva = this.dadosTransporte;

        this.provaAlunoService.cadastrar(data).subscribe(val => {
          if (val && !val['status']) {
            this.animationsService.showSuccessSnackBar('Salvo com sucesso');
            if (val.inscricaoProvaDocumento) {
              printJS({ printable: val.inscricaoProvaDocumento, type: 'raw-html' });
              this.loadData();
            }
          }
        });
      }
    });
  }
  getProvasRealizadas(matriculaId) {
    this.provaAlunoService.buscarProvasRealizadas(matriculaId).subscribe(val => {
      this.isLoadingResults = false;
      if (val.status === 'error') { this.error = true; }
      else {
        this.provasRealizadas = val;

        this.provasRealizadas.forEach(element => {
          element.statusProva = this.getDescricaoStatusProva(element.statusProva);
          element.tipoProva = this.getDescricaoTipoProva(element.tipoProva);
        });
      }
    });
  }
  async cancelarInscricao(): Promise<void> {
    this.provaAlunoService.cancelarInscricao(this.id).subscribe(val => {
      if (val && !val.status) {
        this.animationsService.showSuccessSnackBar('Inscrição cancelada com sucesso');
        // this.form.reset();
        this.id = 0;
        this.inscrito = false;
        this.tipoProva = 1;

        this.dataDefinida = false;
        this.dadosTransporte = null;
        this.buildForm();

        Promise.all([
        ])
          .then(() => {
            this.isLoadingResults = false;
            this.loadData();
          })
          .catch(() => this.isLoadingResults = false);
      }
    });
  }

  async imprimirFormulario(): Promise<void> {
    this.provaAlunoService.imprimirFormulario(this.id).subscribe(val => {
      if (val && !val.status) {
        printJS({ printable: val.inscricaoProvaDocumento, type: 'raw-html' });
      }
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

  openMaterias(provaId, statusProva) {
    const cursoId = this.cursoId;
    const dialogRef = this.dialog.open(AlunoProvasCertificadosMateriasComponent, {
      width: '50vw',
      data: {
        provaId,
        statusProva,
        cursoId
      },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      this.id = 0;
      this.inscrito = false;
      this.tipoProva = 1;
      this.dataDefinida = false;
      this.dadosTransporte = null;
      this.inscrito = false;
      this.podeInscrever = false;
      this.podeAprovar = false;
      this.desabilitarForm = false;
      this.dadosTransporte = null;
      this.form.reset();
      this.buildForm();
      this.loadData();
    });
  }
}
