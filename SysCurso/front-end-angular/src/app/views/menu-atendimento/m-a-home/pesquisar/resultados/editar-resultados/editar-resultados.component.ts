import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { DadosEditadosComponent } from './dados-editados/dados-editados.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';


@Component({
  selector: 'app-editar-resultados',
  templateUrl: './editar-resultados.component.html',
  styleUrls: ['./editar-resultados.component.scss']
})

export class EditarResultadosComponent {

  isLoadingResults: boolean = true;
  formDadosCliente: FormGroup;
  id = 0;
  error: boolean = false;
  maskTelefoneFixoPrincipal = TelMask;
  maskCelular = TelMask;

  constructor(
    private router: Router,
    private location: Location,
    private dialog: MatDialog,
    private formService: FormService,
    private fb: FormBuilder,
    private alunoService: AlunoService,
    private animationsService: AnimationsService,
  ) {}

  ngOnInit(): void {
    this.buildFormDadosCliente();

    Promise.all([
    ])
    .then(() => {
    this.isLoadingResults = false;
    this.loadData();
    })
    .catch(() => this.isLoadingResults = false);
  }

  loadData(): void {

    if (this.id != 0) {
        this.isLoadingResults = true;
        this.alunoService.getPorId(this.id).subscribe(val => {
          if (!val) return;
          if (val?.status === 'error') return this.error = true;

          this.isLoadingResults = false;
        })
    }
  }

  buildFormDadosCliente(): void {
    this.formDadosCliente = this.fb.group({
    id: [0],
    DatadeCriacaoDadosCliente: [null, [Validators.required]],
    AtendenteDadosCliente: [null, [Validators.required]],
    NomedoClienteDadosCliente: [null, [Validators.required]],
    ComonosConheceuDadosCliente: [null, [Validators.required]],
    CelularDadosCliente: [null, [Validators.required]],
    TelefoneFixoDadosCliente: [null, [Validators.required]],
    EmailDadosCliente: [null, [Validators.required]],
    CursoDadosCliente: [null, [Validators.required]],
    PeriodoDadosCliente: [null, [Validators.required]],
    CanaldeAtendimentoDadosCliente: [null, [Validators.required]],
    AgendamentodaMatriculaDadosCliente: [null, [Validators.required]],
    MotivodeInteressenoCursoDadosCliente: [null, [Validators.required]],
    MotivodoNaoAgendamentoDadosCliente: [null, [Validators.required]],
    StatusDadosCliente: [null, [Validators.required]],
    })

    // Change mask of all contact numbers
    this.formDadosCliente.get('TelefoneFixoDadosCliente').valueChanges
    .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);
    this.formDadosCliente.get('CelularDadosCliente').valueChanges
    .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
  }

  async salvarDataFormDadosCliente(): Promise<void> {

    this.formService.validateAllFields(this.formDadosCliente);
    if (!this.formDadosCliente.valid) {
        this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
    return;
    }

    // Validating form
    const formValue: any = this.formDadosCliente.value;

  }

  voltar(): void {
    this.location.back();
  }

  goToHome(): void {
    this.router.navigate(['menu-atendimento/m-a-home']); //, { state: { matriculaId } }
  }

  goToNovoAtendimento(): void{
    this.router.navigate(['menu-atendimento/m-a-home/novo-atendimento']); //, { state: { matriculaId } }
  }

  goToPesquisar(): void{
    this.router.navigate(['menu-atendimento/m-a-home/pesquisar']); //, { state: { matriculaId } }
  }

  goToOutbound(): void{
    this.router.navigate(['menu-atendimento/m-a-home/outbound']); //, { state: { matriculaId } }
  }

  goToAgendados(): void{
    this.router.navigate(['menu-atendimento/m-a-home/agendados']); //, { state: { matriculaId } }
  }

  goToContatosPrioritarios(): void{
    this.router.navigate(['menu-atendimento/m-a-home/contatos-prioritarios']); //, { state: { matriculaId } }
  }

  goToResultados(): void {
    this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados']); //, { state: { matriculaId } }
  }

  editarDados(): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRefMsg = this.dialog.open(DadosEditadosComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: {  },
      autoFocus: true
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

}
