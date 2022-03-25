import { Component, OnInit, OnDestroy, Input, SimpleChanges, OnChanges, Output, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { FinanceiroStoreActions, FinanceiroStoreState, FinanceiroStoreSelectors } from 'src/app/_store/financeiro-store';
import { CurrencyPipe } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { AlunoFinanceiroService } from 'src/app/services/aluno/aluno-financeiro.service';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { AnimationsService } from 'src/app/services/animations.service';
import { MatDialog } from '@angular/material/dialog';
import { CartaoCreditoComponent } from './cartao-credito/cartao-credito.component';
import { TefService } from 'src/app/services/tef/tef.service';
import { AuthService } from 'src/app/security/auth.service';
import { MsgCompraComponent } from './cartao-credito/msg-compra/msg-compra.component';
import * as moment from 'moment';
import { CompararDatasComponent } from '../matricula-formas-pagamento/mfp-boleto/comparar-datas/comparar-datas.component';

@Component({
  selector: 'app-matricula-caracteristicas-plano',
  templateUrl: './matricula-caracteristicas-plano.component.html',
  styleUrls: ['./matricula-caracteristicas-plano.component.scss']
})
export class MatriculaCaracteristicasPlanoComponent implements OnInit, OnDestroy, OnChanges {
  @Input() incluirApostila: boolean;
  @Output() onEfetuandoPagamento: EventEmitter<any> = new EventEmitter();

  displayedColumns: string[] = ['label', 'value'];
  financeiro$: Subscription = null;
  matricula$: Subscription = null;
  planoPagamento$: Subscription = null;
  dadosSelecionados$: Subscription = null;
  financeiroCadastrado$: Subscription = null;
  financeiroComprovante$: Subscription = null;
  campanha$: Subscription = null;
  dataSource = new MatTableDataSource([]);
  planoSelecionado: any = null;
  dadosSelecionados: any = null;
  matricula: any = null;
  campanha: any = null;
  showApostila: boolean = false;
  showBtnPagamento: boolean = true;
  showApostilaCadastrada: boolean = false;
  nomeBtn: string = 'Gerar pagamento';
  hasComprovante: boolean = false;
  disabledBtn: boolean = false;
  tipoPagamento: number = null;
  token: any = null;
  usuarioLogadoId: any = null;
  openCompararData: boolean = false;

  isLoadingResults: boolean = false;

  constructor(
    private storeFinanceiro: Store<FinanceiroStoreState.Financeiro>,
    private storeAluno: Store<AlunoStoreState.Aluno>,
    private currencyPipe: CurrencyPipe,
    private alunoFinanceiroService: AlunoFinanceiroService,
    private animationsService: AnimationsService,
    private dialog: MatDialog,
    private tefService: TefService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.getFinanceiroCadastrado().then(res => {
      this.showApostilaCadastrada = res?.planoPagamentoAluno?.temApostila;
      this.showBtnPagamento = false
    })
    .finally(() => {
      this.getCampanha();
      this.getFinanceiro();
      this.getMatricula();
      this.getDadosSelecionados();
      this.getFinanceiroComprovante();
      this.token = JSON.parse(window.localStorage.getItem('accessToken'));
      this.usuarioLogadoId = this.token?.user.id;
    })
  }

  ngOnDestroy(): void {
    if(this.financeiro$) this.financeiro$.unsubscribe();
    if(this.planoPagamento$)this.planoPagamento$.unsubscribe();
    if(this.matricula$)this.matricula$.unsubscribe();
    if(this.dadosSelecionados$)this.dadosSelecionados$.unsubscribe();
    if(this.financeiroCadastrado$)this.financeiroCadastrado$.unsubscribe();
    if(this.campanha$) this.campanha$.unsubscribe();
    if(this.financeiroComprovante$) this.financeiroComprovante$.unsubscribe();
    this.storeFinanceiro.dispatch(FinanceiroStoreActions.deleteFinanceiro());
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes?.incluirApostila) return;
    const { currentValue } = changes.incluirApostila;
    this.showApostila = currentValue;
    this.apostilaValidation(currentValue);
  }

  getDadosSelecionados(): void {
    this.dadosSelecionados$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectDadosSelecionadosFinanceiro)).subscribe(val => {
      this.dadosSelecionados = val;

      if(!val) this.dataSource.data = [];
    });
  }

  getFinanceiroCadastrado(): Promise<any> {
    return new Promise((res, rej) => {
      this.financeiroCadastrado$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiroCadastrado)).subscribe(val => {
        if(val?.pagamento?.length > 0) {
          res(val);
        }
        else rej(null);
      });
    })
  }

  getFinanceiroComprovante(): void {
    this.financeiroComprovante$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiroComprovante)).subscribe(val => {
      this.hasComprovante = val?.hasComprovante;
    });
  }

  getMatricula(): void {
    this.matricula$ = this.storeAluno.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => {
      this.matricula = val?.id ? val : null;
    });
  }

  getCampanha(): void {
    this.dadosSelecionados$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectCampanhaFinanceiro)).subscribe(val => {
      this.campanha = val?.id ? val : null;
    });
  }

  getFinanceiro(): void {
    this.financeiro$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectFinanceiro)).subscribe(val => {
      if(val && val?.dadosSelecionados && val?.planoPagamento) {
        this.nomeBtn = val?.planoPagamento[0].tipoPagamento === 3 ? 'Gerar cobrança' : 'Gerar pagamento';
        this.tipoPagamento = val?.planoPagamento[0].tipoPagamento;
        this.getPlano(val);
      }
    });
  }

  getPlano(data: any): void {
    if(this.planoPagamento$) {
      this.planoPagamento$.unsubscribe();
      this.planoPagamento$ = null;
    }

    this.planoPagamento$ = this.storeFinanceiro.pipe(select(FinanceiroStoreSelectors.selectPlanoPagamentoIndividualFinanceiro, { planoPagamentoId: data?.dadosSelecionados?.planoPagamentoId })).pipe(distinctUntilChanged(), debounceTime(100)).subscribe(val => {
      if(!val) return;
      this.adaptTabela(val);
      this.planoSelecionado = val;
    });
  }

  adaptTabela(data): void {
    let dataTable: any [] = [];
    if(!data?.isentarMatricula) {
      dataTable.push({ label: 'Taxa de matrícula', value: this.currencyPipe.transform(data?.valorTaxaMatricula) });
    }
    else {
      dataTable.push({ label: 'Taxa de matrícula', value: 'Grátis', style: 'green' });
    }

    if(data?.isentarMaterialDidatico) {
      dataTable.push({ label: 'Apostila', value: 'Grátis', style: 'green' });
    }

    if(this.showApostila) {
      if(dataTable.filter(elem => elem.label === 'Apostila')) {
        dataTable = dataTable.filter(elem => elem.label !== 'Apostila');
      }
      dataTable.push({ label: 'Apostila', value: this.currencyPipe.transform(data?.valorMaterialDidatico) });
    }

    dataTable.push({ label: 'Número de parcelas', value: `${data?.quantidadeParcela}x  ${this.currencyPipe.transform(data?.valorParcela)}` });

    if((data?.tipoPagamento === 3 || data?.tipoPagamento === 7) && data?.valorTotalInscricaoProva > 0) {
      dataTable.push({ label: 'Taxa de inscrição', value: `2x ${this.currencyPipe.transform((data?.valorTotalInscricaoProva / 2))}` });
    }

    if(data?.porcentagemDescontoPontualidade) {
      dataTable.push({ label: 'Desconto pontualidade', value: `${data.porcentagemDescontoPontualidade} %` });
    }

    this.dataSource.data = dataTable;

    this.getFinanceiroCadastrado().then(res => {
      this.apostilaValidation(res?.planoPagamentoAluno?.temApostila);
      this.showBtnPagamento = false
    })
    .catch(() => {});
  }

  apostilaValidation(show: boolean): void {
    let data: any[] = this.dataSource.data;
    if(show === true) {
      if(data.filter(elem => elem.label === 'Apostila')) {
        data = data.filter(elem => elem.label !== 'Apostila');
      }
      if(!this.planoSelecionado?.isentarMaterialDidatico) data.splice(1, 0, { label: 'Apostila', value: this.currencyPipe.transform(this.planoSelecionado?.valorMaterialDidatico) });
      else data.splice(1, 0, { label: 'Apostila', value: 'Grátis', style: 'green' });
    } else if(show === false && !this.planoSelecionado?.isentarMaterialDidatico) {
      data = data.filter(elem => elem.label !== 'Apostila');
    } else {}
    this.dataSource.data = data;
  }

  openCartaoCredito(): void {
    const dialogRef = this.dialog.open(CartaoCreditoComponent, {
      width: '90vw',
      data: { pagamento: this.planoSelecionado, campanha: this.campanha, incluirApostila: this.incluirApostila },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }

  pagar(): void {
    if (this.openCompararData == false)
    {
      if (this.dadosSelecionados?.primeiroPagamento != null && this.dadosSelecionados?.segundoPagamento != null)
      {
        const PrimeiraData = new Date(this.dadosSelecionados?.primeiroPagamento)
        const SegundaData = new Date(this.dadosSelecionados?.segundoPagamento)
        const PrimeiroMes = PrimeiraData.getMonth()+1;
        const SegundoMes = SegundaData.getMonth()+1;

        if (PrimeiroMes == SegundoMes)
        {
          this.openCompararDatas();
          this.openCompararData = true;
          return;
        }
      }
    }

    if(this.campanha?.exigeComprovante && !this.hasComprovante) {
      this.animationsService.showErrorSnackBar('Insira comprovante obrigatório');
      return;
    }

    if(this.planoSelecionado?.tipoPagamento === 3) {
      if((this.planoSelecionado?.quantidadeParcela == 1) && (!this.dadosSelecionados?.primeiroPagamento)){
        this.animationsService.showErrorSnackBar('Preencha 1ª data de pagamento');
        return;
      }

      if((this.planoSelecionado?.quantidadeParcela > 1) && (!this.dadosSelecionados?.primeiroPagamento || !this.dadosSelecionados?.segundoPagamento)){
        this.animationsService.showErrorSnackBar('Preencha todas datas de pagamento');
        return;
      }
      this.efetuarPagamento();
      return;
    }

    const isAluno: boolean = this.authService.isAluno();
    if(isAluno) this.efetuarPagamento();
    else this.pagamentoTEF();
  }

  efetuarPagamento(): void {
    if(this.planoSelecionado?.tipoPagamento === 1) {
      this.openCartaoCredito();
      return;
    }

    const primeiroPagamento = `${this.dadosSelecionados?.primeiroPagamento.format('YYYY-MM-DD')}T03:00:00Z`;
    const segundoPagamento = this.dadosSelecionados?.segundoPagamento && `${this.dadosSelecionados.segundoPagamento.format('YYYY-MM-DD')}T03:00:00Z`;
    const campanhaId = this.campanha?.id;

    const data: any = {
      matriculaId: this.matricula?.id,
      temApostila: this.showApostila,
      planopagamentoId: this.planoSelecionado?.id,
      campanhaId,
      primeiroPagamento,
      segundoPagamento,
      usuarioLogadoId: this.usuarioLogadoId
    }

    this.disabledBtn = true;
    this.onEfetuandoPagamento.emit({efetuou: false});
    this.alunoFinanceiroService.contratarPlano(data).subscribe(val => {
      this.onEfetuandoPagamento.emit({efetuou: true, success: (val?.status === 'error') ? false : true});
      if (val?.status === 'error')
      {
        this.isLoadingResults = true;
        document.location.reload();
        return;
      }
      if(val?.id)
        this.animationsService.showSuccessSnackBar('Efetuado com sucesso');
    });
  }

  pagamentoTEF(): void {
    const valor: number = this.calcularValorTotal(this.planoSelecionado, this.campanha);

    let valorTotal: number = 0;
    if(valor.toString().indexOf('.') > -1) {
      const casas = valor.toString().split('.');
      valorTotal = parseInt(`${casas[0]}${casas[1].slice(0, 2).padEnd(2, '0')}`);
    } else {
      valorTotal = parseInt(`${valor}00`);
    }

    const data = {
      valorTotal,
      credito: this.tipoPagamento === 1,
      temApostila: this.showApostila,
      matriculaId: this.matricula?.id,
      campanhaId: this.campanha?.id ? this.campanha.id : null,
      planopagamentoId: this.planoSelecionado?.id,
      usuarioLogadoId: this.usuarioLogadoId
    }
    this.onEfetuandoPagamento.emit({efetuou: false});
    this.tefService.transacaoTef(data).subscribe(val => {
      if(val?.codigo > 0) this.openMsg(false, 'Pagamento não aprovado');
      this.onEfetuandoPagamento.emit({efetuou: true, success: (val?.status === 'error') ? false : true});
    });
  }

  calcularValorTotal(pagamento: any, campanha: any): number {
    if(!pagamento) return 0;
    if(!campanha) campanha = {};
    const { valorTaxaMatricula, valorTotalInscricaoProva, valorTotalPlano, valorMaterialDidatico } = pagamento;
    const { descontoPlanoPagamento, descontoTaxaInscricaoProvas, descontoTaxaMatricula, descontoTaxaMateriaDidatico } = campanha;
    let valor: number = 0;
    if(valorTotalPlano)           descontoPlanoPagamento      ? (valor += (valorTotalPlano          - (valorTotalPlano          * (descontoPlanoPagamento      / 100)))) : (valor += valorTotalPlano);
    if(valorTaxaMatricula)        descontoTaxaMatricula       ? (valor += (valorTaxaMatricula       - (valorTaxaMatricula       * (descontoTaxaMatricula       / 100)))) : (valor += valorTaxaMatricula);
    if(valorTotalInscricaoProva)  descontoTaxaInscricaoProvas ? (valor += (valorTotalInscricaoProva - (valorTotalInscricaoProva * (descontoTaxaInscricaoProvas / 100)))) : (valor += valorTotalInscricaoProva);
    if(this.incluirApostila && valorMaterialDidatico)     descontoTaxaMateriaDidatico ? (valor += (valorMaterialDidatico    - (valorMaterialDidatico    * (descontoTaxaMateriaDidatico / 100)))) : (valor += valorMaterialDidatico);
    return valor;
  }

  openMsg(success, msg): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MsgCompraComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: { success, msg },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

  openCompararDatas(): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(CompararDatasComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: { },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

}
