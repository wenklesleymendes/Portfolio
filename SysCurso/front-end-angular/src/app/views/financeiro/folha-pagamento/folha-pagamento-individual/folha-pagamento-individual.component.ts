import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { CPFMask, HourMinuteMask, WeekMask } from 'src/app/utils/mask/mask';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteService } from 'src/app/services/delete.service';
import { FolhaPagamentoService } from 'src/app/services/financeiro/folha-pagamento.service';
import { combineLatest } from 'rxjs';
import { ControlePontoService } from 'src/app/services/rh/controle-ponto.service';
import * as moment from 'moment';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-folha-pagamento-individual',
  templateUrl: './folha-pagamento-individual.component.html',
  styleUrls: ['./folha-pagamento-individual.component.scss']
})
export class FolhaPagamentoIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cpfMask = CPFMask;
  unidades: any[] = [];
  infoFilter: any[] = [];
  horasExtrasDisplayedColumns: string[] = ['porcentagem', 'horas', 'valor', 'options'];
  horasExtrasDataSource = new MatTableDataSource([]);
  horasExtras: any[] = [];
  valorTotalPagamento: number = null;
  tipoRegimeContratacao: number = null;
  saldoHoraExtrasDefault: string = null;
  saldoHoraExtras: string = null;
  hourMinute = HourMinuteMask;
  weekMask =  WeekMask;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private folhaPagamentoService: FolhaPagamentoService,
    private funcionarioService: FuncionarioService,
    private controlePontoService: ControlePontoService,
    private routerActive: ActivatedRoute,
    private deleteService: DeleteService,
    private formService: FormService
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
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      funcionarioId: [null, [Validators.required]],
      unidadeId: [{disabled: true, value: null}],
      cpf: [null, [Validators.required, validarCPF]],
      salarioBruto: [null],
      quantidadeDias: [{disabled: true, value: null}],
      salarioLiquido: [null, [Validators.required]],
      alimentacao: [null],
      competencia: [null, [Validators.required]],
      transporte: [null],
      comissaoPrimeiraParcelaPaga: [null],
      bonusMetaPeriodo: [null],
      monitoriaProva: [null],
      valorAdicional: [null],
      justificativaValorAdicional: [null],
      valorDiasDSR: [null],
      justificativaDSR: [null],
      valorFerias: [null],
      justificativaFerias: [null],
      valorDecimoTerceiro: [null],
      justificativaDecimoTerceiro: [null],
      valorTotalDesconto: [null],
      justificativaDesconto: [null],
      inicioHoraExtraPaga: [null],
      terminoHoraExtraPaga: [null],
      horaExtraPorcentagem: [null],
      horaExtraQuantidade: [null],
      horaExtraValor: [null]
    })

    this.form.get('unidadeId').valueChanges.subscribe(val => {
      if (!val || !this.unidades) return;
      const valorSalario = this.unidades.find(elem => elem.unidadeId === val);
      if (valorSalario && this.tipoRegimeContratacao !== 2 && this.tipoRegimeContratacao !== 7) {
        this.form.get('salarioBruto').setValue(valorSalario.valorSalario)
      }
    })

    combineLatest(this.form.get('quantidadeDias').valueChanges, this.form.get('salarioBruto').valueChanges)
      .subscribe(val => {
        if (this.tipoRegimeContratacao !== 3 && this.tipoRegimeContratacao !== 5) return
        const quantidadeDias = val[0];
        const salarioBruto = val[1];
        if (!quantidadeDias || !salarioBruto) this.form.get('salarioLiquido').setValue(null);
        else this.form.get('salarioLiquido').setValue(quantidadeDias * salarioBruto);
      })

    combineLatest(this.form.get('inicioHoraExtraPaga').valueChanges, this.form.get('terminoHoraExtraPaga').valueChanges)
      .subscribe(val => {
        if (!val[0] || !val[1]) this.saldoHoraExtras = null;
      })

    this.form.get('id').setValue(this.id);
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.folhaPagamentoService.getPorId(this.id)
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;
        const { funcionario, horaExtra, valorTotalPagamento } = val;
        const { cpf } = funcionario;

        this.horasExtrasDataSource.data = horaExtra;
        this.horasExtras = horaExtra;
        this.valorTotalPagamento = valorTotalPagamento;
        this.form.get('cpf').setValue(cpf);
        this.ajustarDadosFuncionario(funcionario);
        this.form.patchValue(val);
        this.getSaldoHorasExtra(true);
        this.isLoadingResults = false;
      })
    }
  }

  getFuncionario(): void {
    const cpf = this.form.get('cpf').value;
    this.buildForm();
    this.form.get('cpf').setValue(cpf);

    this.funcionarioService.getPorCpf(cpf).subscribe(val => {
      if (val['status'] === 'error') return this.error = true;
      this.ajustarDadosFuncionario(val);
    })
  }

  getSaldoHorasExtra(firstLoad?: boolean): void {
    let { cpf, inicioHoraExtraPaga, terminoHoraExtraPaga } = this.form.value;
    if (!cpf || !inicioHoraExtraPaga || !terminoHoraExtraPaga) {
      if (!firstLoad) {
        this.animationsService.showErrorSnackBar('Preencha CPF, Início de Hora Extra e Término de Hora Extra');
      }
      return;
    }
    const data = {
      cpf,
      inicioHoraExtraPaga: (typeof inicioHoraExtraPaga === 'string') ? inicioHoraExtraPaga : inicioHoraExtraPaga.format(),
      terminoHoraExtraPaga: (typeof terminoHoraExtraPaga === 'string') ? terminoHoraExtraPaga : terminoHoraExtraPaga.format()
    }

    this.controlePontoService.buscarSaldoHorasExtras(data).subscribe(val => {
      if (val['status'] === 'error') return;
      this.saldoHoraExtrasDefault = val.saldo.padStart(5, '0');
      this.calcularHoraExtraRestante();
    })
  }

  ajustarDadosFuncionario(funcionario: any): void {
    const { id, nome, dadosContratacao, dadosBancario, salarioUnidade } = funcionario;
    this.tipoRegimeContratacao = dadosContratacao?.tipoRegimeContratacao;

    this.infoFilter = [
      { label: 'Nome', value: nome },
      { label: 'Regime', value: this.ajustarRegime(dadosContratacao?.tipoRegimeContratacao) },
      { label: 'Banco',  value: dadosBancario?.nomeBanco ? dadosBancario.nomeBanco : ' - ' },
      { label: 'Agência Bancária', value: dadosBancario?.numeroAgencia ? dadosBancario.numeroAgencia : ' - ' },
      { label: 'Conta Corrente', value: dadosBancario?.numeroConta ? dadosBancario.numeroConta : ' - ' },
    ];

    this.form.get('funcionarioId').setValue(id);
    if (dadosContratacao?.tipoRegimeContratacao === 3 || dadosContratacao?.tipoRegimeContratacao === 5) {
      this.formService.enableField(this.form.get('quantidadeDias'));
    }

    if (dadosContratacao?.tipoRegimeContratacao === 3 || dadosContratacao?.tipoRegimeContratacao === 5) {
      this.formService.disableField(this.form.get('salarioLiquido'));
    } else {
      this.formService.enableField(this.form.get('salarioLiquido'));
    }

    if (salarioUnidade) {
      this.unidades = salarioUnidade;
      this.form.get('unidadeId').enable();
      this.form.get('unidadeId').setValidators([Validators.required]);

      if (this.unidades.length === 1) this.form.get('unidadeId').setValue(this.unidades[0].unidadeId);

      if (dadosContratacao?.tipoRegimeContratacao === 1 ||
        dadosContratacao?.tipoRegimeContratacao === 4 ||
        dadosContratacao?.tipoRegimeContratacao === 6) {
          this.form.get('salarioBruto').setValue(salarioUnidade[0].valorSalario);
      }
    }
  }

  ajustarUnidade(unidades: any[]): string {
    if (unidades == null || unidades.length == 0) return ' - ';
    let text = '';
    unidades.forEach(elem => text += `${elem.nomeUnidade ? elem.nomeUnidade : '  '}, `)
    return text.substring(0, text.length - 2);
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

  labelSalario(): string {
    if (this.tipoRegimeContratacao == 1 || this.tipoRegimeContratacao == 6 || this.tipoRegimeContratacao == 4) return 'Salário líquido';
    if (this.tipoRegimeContratacao == 2 || this.tipoRegimeContratacao == 7) return 'Bolsa auxílio';
    if (this.tipoRegimeContratacao == 3) return 'Remuneração total de aulas';
    if (this.tipoRegimeContratacao == 5) return 'Remuneração total de dias';
    return 'Salário líquido';
  }

  ajustarLabelSalarioBruto(): string {
    if (this.tipoRegimeContratacao == 1 || this.tipoRegimeContratacao == 6 || this.tipoRegimeContratacao == 4) return 'Salário Bruto';
    if (this.tipoRegimeContratacao == 3) return 'Valor por aula';
    if (this.tipoRegimeContratacao == 5) return 'Valor por dia';
    return 'Salário bruto';
  }

  closeDatePicker(eventData: any, dp: any, control: string) {
    this.form.get(control).setValue(eventData)
    dp.close();
  }

  calcularHoraExtraRestante(): void {
    if (!this.saldoHoraExtrasDefault) return;
    const { inicioHoraExtraPaga, terminoHoraExtraPaga } = this.form.value;
    if (!inicioHoraExtraPaga || !terminoHoraExtraPaga) return;

    const horas: any[] = [];
    const defaultHours: moment.Moment = moment('2020-01-01').set('hour', 0).set('minute', 0).set('second', 0);

    const saldoHorasRestantes: string = this.saldoHoraExtrasDefault.replace(':', '');
    const horasRestantes: moment.Moment = moment(defaultHours.format());
    const hourRestante = saldoHorasRestantes.substr(0, 2);
    const minuteRestante= saldoHorasRestantes.substr(2, 5);
    horasRestantes.add(hourRestante, 'hour');
    horasRestantes.add(minuteRestante, 'minute');

    this.horasExtrasDataSource.data.forEach(elem => horas.push(elem.quantidadeHoras.padStart(4, '0')));

    const horasSomadas: moment.Moment = moment(defaultHours.format());
    horas.forEach((elem: string) => {
      const hour = elem.substr(0, 2);
      const minute = elem.substr(2, 5);
      horasSomadas.add(hour, 'hour');
      horasSomadas.add(minute, 'minute');
    });

    const diffHour = horasRestantes.diff(horasSomadas, 'hour');
    const diffMinuteFull = horasRestantes.diff(horasSomadas, 'minute');
    const diffMinute = (diffMinuteFull - (diffHour * 60));

    this.saldoHoraExtras = `${diffHour.toString().padStart(2, '0')}:${diffMinute.toString().padStart(2, '0')}`
  }

  checkHour(horaExtraQuantidade: string): boolean {
    if (!horaExtraQuantidade || !this.saldoHoraExtras) return false;
    horaExtraQuantidade.padStart(4, '0');
    const defaultHours: moment.Moment = moment('2020-01-01').set('hour', 0).set('minute', 0).set('second', 0);
    const horasInformada: moment.Moment = moment(defaultHours.format());
    const hourInformada = horaExtraQuantidade.substr(0, 2);
    const minuteInformada = horaExtraQuantidade.substr(2, 5);
    const saldoHorasRestantes: string = this.saldoHoraExtras.replace(':', '');
    const horasRestantes: moment.Moment = moment(defaultHours.format());
    const hourRestante = saldoHorasRestantes.substr(0, 2);
    const minuteRestante= saldoHorasRestantes.substr(2, 5);

    horasInformada.add(hourInformada, 'hour');
    horasInformada.add(minuteInformada, 'minute');
    horasRestantes.add(hourRestante, 'hour');
    horasRestantes.add(minuteRestante, 'minute');

    if (horasInformada > horasRestantes) return false;
    return true
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue: any = this.form.getRawValue();
    // Validating 'Hora extra'
    // if (formValue.inicioHoraExtraPaga || formValue.terminoHoraExtraPaga) {
    //   if (this.saldoHoraExtras !== '00:00') {
    //     this.animationsService.showErrorSnackBar('Quantidade de horas inválida');
    //     return;
    //   }
    // }

    delete formValue.horaExtraPorcentagem;
    delete formValue.horaExtraQuantidade;
    delete formValue.horaExtraValor;

    const data = { ...formValue, horaExtra: this.horasExtrasDataSource.data};
    // Make request
    this.folhaPagamentoService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        if (val['erro']) {
          this.animationsService.showErrorSnackBar('Pagamento anterior não realizado');
          return;
        }
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id, valorTotalPagamento } = val;
        this.id = id;
        this.valorTotalPagamento = valorTotalPagamento;
        this.form.get('id').setValue(id);
      }
    })
  }

  addHoraExtra(): void {
    // Get 'Hora extra' select
    const { horaExtraPorcentagem, horaExtraQuantidade, horaExtraValor } = this.form.value;
    if (!horaExtraPorcentagem || !horaExtraQuantidade || !horaExtraValor) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos');
      return;
    }
    // Check if isn't alredy selected
    const alredySelected = this.horasExtras.find(elem => elem.porcentagem == horaExtraPorcentagem);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Porcentagem já selecionada');
      return;
    }
    // Check if 'quantidadeHoras' is valid
    // const hourValid = this.checkHour(horaExtraQuantidade);

    // if (!hourValid) {
    //   this.animationsService.showErrorSnackBar('Quantidade de horas inválida');
    //   return;
    // }

    // Add 'Hora extra'
    this.horasExtras.push({
      id: 0,
      folhaPagamentoId: this.id,
      porcentagem: horaExtraPorcentagem,
      quantidadeHoras: horaExtraQuantidade,
      valor: horaExtraValor
    });
    this.horasExtrasDataSource.data = this.horasExtras;
    this.calcularHoraExtraRestante();

    // Reset form
    this.form.get('horaExtraQuantidade').setValue(null);
    this.form.get('horaExtraPorcentagem').setValue(null);
    this.form.get('horaExtraValor').setValue(null);
  }

  remove(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.horasExtrasDataSource.data;
      data.splice(index, 1);
      this.horasExtrasDataSource.data = data;
      if (data.length == 0) {
        this.form.get('inicioHoraExtraPaga').setValue(null);
        this.form.get('terminoHoraExtraPaga').setValue(null);
      }
      this.calcularHoraExtraRestante();
    })
  }
}
