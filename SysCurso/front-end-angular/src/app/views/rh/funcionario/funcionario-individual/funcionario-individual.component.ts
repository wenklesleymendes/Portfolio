import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { DataProvider } from './data-provider';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { delay } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { MatAccordion } from '@angular/material/expansion';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';

@Component({
  selector: 'app-funcionario-individual',
  templateUrl: './funcionario-individual.component.html',
  styleUrls: ['./funcionario-individual.component.scss']
})
export class FuncionarioIndividualComponent implements OnInit {
  @ViewChild(MatAccordion, { static: false }) accordion!: MatAccordion;

  panelOpenState = false;
  error: boolean = false;
  isLoadingResults: boolean = false;
  funcionarioData: any = null;
  id: number = 0;

  // Children inputs
  dadosPessoaisInput: any;
  dadosContratacaoAllInput: any;

  constructor(
    private dataProvider: DataProvider,
    private router: ActivatedRoute,
    private animationsService: AnimationsService,
    private location: Location,
    private funcionarioService: FuncionarioService
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
    else this.loadData();

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
      const { dadosPessoais, dadosContratacaoAll, salarioUnidade } = val;

      if (!dadosPessoais || !dadosContratacaoAll || !salarioUnidade) {
        this.funcionarioData = null;
      } else {
        this.organizeData(val).subscribe(val => this.funcionarioData = val);
      }
    });
  }

  loadData(): void {
    this.isLoadingResults = true;
    this.error = false;

    this.funcionarioService.getPorId(this.id)
      .pipe(delay(750))
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else {
          this.funcionarioData = val;
          this.organizeDataForChildren(val);
          this.accordion.openAll();
        }
      });
  }

  organizeData(funcionario: any): Observable<any> {
    const { dadosPessoais, dadosContratacaoAll, salarioUnidade } = funcionario;
    const { dadosContratacao, dadosBancario, agenteIntegracao, cursoProfessor, jornadaTrabalho } = dadosContratacaoAll;
    dadosContratacao.tipoRegimeContratacao = parseInt(dadosContratacao.tipoRegimeContratacao);

    return of({
      id: this.id,
      ...dadosPessoais,
      dadosContratacao,
      agenteIntegracao,
      jornadaTrabalho,
      dadosBancario,
      cursoProfessor,
      salarioUnidade
    });
  }

  organizeDataForChildren(data: any): void {
    const {
      id,
      nome,
      cpf,
      rg,
      dataNascimento,
      contato,
      endereco,
      dadosContratacao,
      dadosBancario,
      agenteIntegracao,
      jornadaTrabalho,
      cursoProfessor,
      salarioUnidade,
      isActive
    } = data;

    const dadosPessoais = { id, nome, cpf, rg, dataNascimento, contato, endereco, isActive };
    const dadosContratacaoAll = { dadosContratacao, dadosBancario, agenteIntegracao, jornadaTrabalho, cursoProfessor, salarioUnidade, funcionarioId: id };

    this.dadosPessoaisInput = dadosPessoais;
    this.dadosContratacaoAllInput = dadosContratacaoAll;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {

    this.dataProvider.validateNext(true);

    if (this.funcionarioData) {
      this.animationsService.showProgressBar(true);

      this.funcionarioService.cadastrar(this.funcionarioData).subscribe(val => {
        if (val.length != 0) {
          const message = (this.funcionarioData.id == 0) ? 'Cadastrado com sucesso' : 'Atualizado com sucesso';
          if (val['status'] == 'error') return;
          if (val.id) {
            this.id = val.id;
            this.funcionarioData.id = val.id;
          }
          this.animationsService.showProgressBar(false);
          this.animationsService.showSuccessSnackBar(message);
        }
      });
    } else {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
    }
  }

  voltar(): void {
    this.location.back();
  }
}
