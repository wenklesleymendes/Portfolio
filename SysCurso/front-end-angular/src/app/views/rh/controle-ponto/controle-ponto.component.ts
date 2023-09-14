import { Component, OnInit } from '@angular/core';
import { CPFMask } from 'src/app/utils/mask/mask';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { MatDialog } from '@angular/material/dialog';
import { AdicionarPeriodoFeriasComponent } from './adicionar-periodo-ferias/adicionar-periodo-ferias.component';
import { OcorrenciaComponent } from './ocorrencia/ocorrencia.component';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { DeleteService } from 'src/app/services/delete.service';
import { ControlePontoService } from 'src/app/services/rh/controle-ponto.service';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-controle-ponto',
  templateUrl: './controle-ponto.component.html',
  styleUrls: ['./controle-ponto.component.scss']
})
export class ControlePontoComponent implements OnInit {
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  cpfMask = CPFMask;
  pesquisou: boolean = false;
  controlePontoHorarios: any[] = [];
  funcionarioId: any;
  statusFerias: string;
  saldoDevedorTotal: string;
  feriasId: number;
  displayedColumns: string[] = [
    'data',
    'entrada1',
    'saida1',
    'entrada2',
    'saida2',
    'entrada3',
    'saida3',
    'entrada4',
    'saida4',
    'saldo',
    'horaExtra',
    'regime',
    'ocorrencias',
  ];
  dataSource = new MatTableDataSource();
  infoFilter: any[] = [];
  selection = new SelectionModel<any>(true, []);

  teste:any;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private funcionarioService: FuncionarioService,
    private formService: FormService,
    private animationsService: AnimationsService,
    private deleteService: DeleteService,
    private controlePontoService: ControlePontoService,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      dataInicio: [null],
      dataFim: [null],
      cpf: [null, [Validators.required, validarCPF]]
    });

    this.form.setValidators([
      CompareInitialEndDate('dataInicio', 'dataFim')
    ]);
  }

  setColor(val: string): string {
    if (!val || val == '') return 'bg-blue';
    if (val.match(/Férias/gim)) return 'bg-yellow';
    if (val.match(/Folga/gim)) return 'bg-orange';
    if (val.match(/00:00/gim)) return 'bg-blue';
    if (val.match(/-/gim)) return 'bg-red';
    return 'bg-green';
  }

  setColorStatusFerias(statusFerias: string): string {
    if (!statusFerias || statusFerias == '') return 'bg-blue';
    if (statusFerias.match(/Vencida/gim)) return 'bg-red';
    return 'bg-blue';
  }

  getPontoEletronico(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    this.error = false;
    const cpf = this.form.get('cpf').value;
    const dataInicioForm  = this.form.get('dataInicio').value ? new Date(this.form.get('dataInicio').value) : null;
    const dataFimForm     = this.form.get('dataFim').value ? new Date(this.form.get('dataFim').value) : null;
    const dataInicio  = dataInicioForm ? `${dataInicioForm.getMonth() + 1}/${dataInicioForm.getDate()}/${dataInicioForm.getFullYear()}` : null;
    const dataFim     = dataFimForm ? `${dataFimForm.getMonth() + 1}/${dataFimForm.getDate()}/${dataFimForm.getFullYear()}` : null;

    this.funcionarioService.getPontoEletronico({ cpf, dataInicio, dataFim})
    .subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      const { nomeColaborador, matricula, nomeUnidade, regimeContratacao, controlePontoHorarios, funcionarioId, statusFerias, saldoDevedorTotal, feriasId } = val;
      this.infoFilter = [
        { label: 'Nome',      value: nomeColaborador },
        { label: 'Matricula', value: matricula },
        { label: 'Unidade',   value: nomeUnidade },
        { label: 'Regime',    value: this.ajustarRegime(regimeContratacao) }
      ];

      this.teste = val

      this.funcionarioId = funcionarioId;
      this.feriasId = feriasId;
      this.statusFerias = statusFerias;
      this.saldoDevedorTotal = saldoDevedorTotal;
      this.controlePontoHorarios = controlePontoHorarios;
      this.dataSource.data = controlePontoHorarios;
      this.pesquisou = true;
    });
  }

  ajustarHora(value: Date): string {
    if (!value) return '-';
    const date = new Date(value);
    if (date.getFullYear() < 1950) return ' - ';
    const hour = date.getHours().toString().padStart(2, '0');
    const minute = date.getMinutes().toString().padStart(2, '0');
    return `${hour}:${minute}`;
  }

  ajustarRegime(regimeContratacao): string {
    if (!regimeContratacao || regimeContratacao.length == 0) return null;
    let text = '';
    if (regimeContratacao === 1) text = 'CLT Seg a Sex';
    if (regimeContratacao === 2) text = 'Estágio Seg a Sex';
    if (regimeContratacao === 3) text = 'Professor Autônomo';
    if (regimeContratacao === 4) text = 'Professor CLT';
    if (regimeContratacao === 5) text = 'Profissional Autônomo';
    if (regimeContratacao === 6) text = 'CLT Seg a Sab';
    if (regimeContratacao === 7) text = 'Estágio Seg a Sab';
    if (regimeContratacao === 8) text = 'Autônomo Pré CLT Seg a Sex';
    if (regimeContratacao === 9) text = 'Autônomo Pré CLT Seg a Sab';
    if (regimeContratacao === 10) text = 'Autônomo Pré Estágio Seg a Sex';
    if (regimeContratacao === 11) text = 'Autônomo Pré Estágio Seg a Sab';
    return text;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  openFerias(): void {
    const dialogRef = this.dialog.open(AdicionarPeriodoFeriasComponent, {
      width: '90vw',
      data: { funcionarioId: this.funcionarioId, feriasId: this.feriasId  },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getPontoEletronico();
    });
  }

  openOcorrencias(data: any): void {
    const dialogRef = this.dialog.open(OcorrenciaComponent, {
      width: '90vw',
      data: { data },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getPontoEletronico();
    });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.controlePontoService.deletarPontoEletronico(id).subscribe(val => this.getPontoEletronico());
    })
  }
}
