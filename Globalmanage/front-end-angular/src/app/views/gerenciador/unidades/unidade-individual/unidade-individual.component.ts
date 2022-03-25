import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { MatAccordion } from '@angular/material/expansion';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { delay } from 'rxjs/operators';
import { DataProvider } from './data-provider';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of } from 'rxjs';
import { DirectiveModule } from 'src/app/utils/directive/directive.module';


@Component({
  selector: 'app-unidade-individual',
  templateUrl: './unidade-individual.component.html',
  styleUrls: ['./unidade-individual.component.scss']
})
export class UnidadeIndividualComponent implements OnInit, OnDestroy {
  @ViewChild(MatAccordion, { static: false }) accordion!: MatAccordion;
  file: any;
  updateImg: boolean = false;
  panelOpenState = false;
  error: boolean = false;
  isLoadingResults: boolean = true;
  unidadeData: any = null;
  id: number = 0;
  imgProfileSrc: string = null;

  // Children inputs
  infomacoesInput: any;
  dadosBancariosInput: any;
  contratoLocacaoInput: any;
  historicoOcorrenciasInput: any;
  documentoUnidadeInput: any;

  constructor(
    private unidadeService: UnidadeService,
    private dataProvider: DataProvider,
    private router: ActivatedRoute,
    private animationsService: AnimationsService,
    private snackBar: MatSnackBar,
    private location: Location,
    private directiveModule : DirectiveModule
  ) {
    const id = this.router.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.dataProvider.initAllSubjects();
    // Beauty animation
    if (!this.id) {
      this.isLoadingResults = true;
      setTimeout(() => {
        this.isLoadingResults = false;
        this.accordion.openAll();
      }, 1000);
    }
    else {
      Promise.all([this.getImg()])
        .then(() => this.loadData())
        .catch(() => { })
        .finally(() => this.isLoadingResults = false)
    }

    this.onFormChanges();
  }

  ngOnDestroy(): void {
    this.dataProvider.completeAllSubjects();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  onFormChanges() {
    this.dataProvider.getAll().subscribe(val => {
      const { infomacoes, dadosBancario, contratoLocacao, historicoOcorrencias } = val;

      if (!infomacoes || !dadosBancario || !contratoLocacao || !historicoOcorrencias) {
        this.unidadeData = null;
      } else {
        this.organizeData(val).subscribe(val => this.unidadeData = val);
      }
    });
  }

  loadData(): void {
    this.isLoadingResults = true;
    this.error = false;

    this.unidadeService.getPorId(this.id)
      .pipe(delay(750))
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else {
          this.unidadeData = val;
          this.organizeDataForChildren(val);
          this.accordion.openAll();
        }
      });
  }

  getImg(): Promise<any> {
    return new Promise((res, rej) => {
      if (this.id === 0) return rej();

      this.unidadeService.getImg(this.id)
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

  organizeData(unidade: any): Observable<any> {
    const { infomacoes, dadosBancario, contratoLocacao, historicoOcorrencias } = unidade;
    const { nome, cnpj, razaoSocial, nomeFantasia, vigenciaInicioAVCB, vigenciaTerminoAVCB, vigenciaInicioAlvara, vigenciaTerminoAlvara, endereco, contato, horarioFuncionamento, sigla } = infomacoes;
    const { comAulaSemanaInicio, comAulaSemanaTermino, comAulaSabadoInicio, comAulaSabadoTermino, semAulaSemanaInicio, semAulaSemanaTermino, semAulaSabadoInicio, semAulaSabadoTermino } = horarioFuncionamento;
    const { unidadeDespesas, nomeProprietario, telefoneProprietario, nomeImobiliaria, telefoneFixo, celular, email, vigenciaInicio, vigenciaTermino, valorAluguel, valorCondominio, valorIPTU } = contratoLocacao;
    const idContratoLocacao = contratoLocacao.id;

    return of({
      // Infomacoes
      id: this.id,
      nome,
      cnpj,
      razaoSocial,
      nomeFantasia,
      horarioFuncionamento: [
        {
          finalSemana: true,
          comAula: true,
          semanaInicio: comAulaSemanaInicio,
          semanaTermino: comAulaSemanaTermino,
          sabadoInicio: comAulaSabadoInicio,
          sabadoTermino: comAulaSabadoTermino
        },
        {
          finalSemana: false,
          comAula: false,
          semanaInicio: semAulaSemanaInicio,
          semanaTermino: semAulaSemanaTermino,
          sabadoInicio: semAulaSabadoInicio,
          sabadoTermino: semAulaSabadoTermino
        }
      ],
      vigenciaInicioAVCB,
      vigenciaTerminoAVCB,
      vigenciaInicioAlvara,
      vigenciaTerminoAlvara,
      endereco,
      contato,
      sigla,
      // Dados Bancarios
      dadosBancario,
      // Contrato Locação
      unidadeDespesas,
      contratoLocacao: {
        id: idContratoLocacao,
        nomeProprietario,
        telefoneProprietario,
        nomeImobiliaria,
        telefoneFixo,
        celular,
        email,
        vigenciaInicio,
        vigenciaTermino,
        valorAluguel,
        valorCondominio,
        valorIPTU
      },
      // Historico de ocorrências
      historicoOcorrencias,
    });
  }

  organizeDataForChildren(data: any): void {
    const {
      // Informações
      nome,
      cnpj,
      razaoSocial,
      nomeFantasia,
      horarioFuncionamento,
      vigenciaInicioAVCB,
      vigenciaTerminoAVCB,
      vigenciaInicioAlvara,
      vigenciaTerminoAlvara,
      endereco,
      contato,
      sigla,
      // Dados Bancarios
      dadosBancario,
      // Documentos da unidade
      anexo,
      // Contrato Locação
      unidadeDespesas,
      contratoLocacao,
      // Historico Ocorrencias
      historicoOcorrencias,

      // ID's
    } = data;

    const infomacoesInput = { nome, cnpj, razaoSocial, nomeFantasia, vigenciaInicioAVCB, vigenciaTerminoAVCB, vigenciaInicioAlvara, vigenciaTerminoAlvara, endereco, contato, horarioFuncionamento, sigla };
    const dadosBancariosInput = { ...dadosBancario }
    const contratoLocacaoInput = { ...contratoLocacao, unidadeDespesas };
    const documentoUnidadeInput = [...anexo];
    const historicoOcorrenciasInput = [...historicoOcorrencias];

    this.infomacoesInput = infomacoesInput;
    this.dadosBancariosInput = dadosBancariosInput;
    this.contratoLocacaoInput = contratoLocacaoInput;
    this.documentoUnidadeInput = documentoUnidadeInput;
    this.historicoOcorrenciasInput = historicoOcorrenciasInput;
  }

  saveImg(unidadeId): void {
    if (!this.updateImg) return;
    const extensao = this.imgProfileSrc.split(';base64,')[0].replace('data:', '');
    const foto = this.imgProfileSrc.split(';base64,')[1];
    const data = { foto, extensao, unidadeId }
    this.unidadeService.uploadFoto(data).subscribe(val => {

    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {
    this.dataProvider.validateNext(true);

    if (this.unidadeData) {
      this.animationsService.showProgressBar(true);

      this.unidadeService.postUnidadeForm(this.unidadeData).subscribe(val => {
        if (val.length != 0) {
          if (val['status'] == 'error') return;
          const message = (this.unidadeData.id == 0) ? 'Incluido com sucesso' : 'Salvo com sucesso';
          this.animationsService.showSuccessSnackBar(message);
          if (val.id) {
            this.id = val.id;
            this.unidadeData.id = val.id;
            this.saveImg(val.id);
          }
          this.organizeDataForChildren(val);
          this.animationsService.showProgressBar(false);
        }
      });
    } else {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
    }
  }

  voltar(): void {
    this.location.back();
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
}
