import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { HourMinuteMask, CPFMask, TelMask } from 'src/app/utils/mask/mask';
import { Observable } from 'rxjs';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { AuthService } from 'src/app/security/auth.service';
import { FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { ColegioAutorizadoService } from 'src/app/services/provas/colegio-autorizado.service';
import { HistoricoProvasService } from 'src/app/services/provas/historico-provas.service';
import * as XLSX from 'xlsx';
import * as JsonToXML from "src/js2xmlparser";

@Component({
  selector: 'app-historico-prova',
  templateUrl: './historico-prova.component.html',
  styleUrls: ['./historico-prova.component.scss']
})
export class HistoricoProvaComponent implements OnInit {
  form: FormGroup;
  hourMinute = HourMinuteMask;
  error: boolean = false;
  visualizarTodasUnidades: boolean;
  isLoadingResults: boolean = false;
  cpfMask: string = CPFMask;
  maskCelular = TelMask;
  cursos: any[] = null;
  unidades: any[] = null;
  filterUnidades: Observable<any[]>;
  unidadesDefault = null;
  salas: number[] = Array(20).fill('').map((x,i) => i+1);
  years: number[] = null;
  anos = Array(2).fill('').map((x,i) => (new Date().getUTCFullYear() + i).toString());
  startYear: Date = new Date('2020-01-01');
  toppings = new FormControl();
  toppingList: number[] = [1, 2, 3, 4, 5, 6];
  colegiosDefault = null;

  historicoProvas: any[] = null;
  listaGeralDeInscritosParaProva: any[] = null;
  listaDeChamadaOnibus = null;
  filtro: any = {};
  tipoProvaModel = 0;
  unidadeModel = [];
  numeroOnibus = null;
  listaDeChamadaOnibusCabecalho = null;

  constructor(
    private fb: FormBuilder,
    private animationService: AnimationsService,
    private cursoService: CursoService,
    private unidadeService: UnidadeService,
    private authService: AuthService,
    private formService: FormService,
    private colegioAutorizadoService: ColegioAutorizadoService,
    private historicoProvasService: HistoricoProvasService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  ngOnInit(): void {

    this.getColegioAutorizado();

    this.buildForm();

    this.getunidades();

  }

  buildForm(): void {
    this.form = this.fb.group({
      unidadeSelect: [''],
      colegioSelect: [''],
      tipoProva: [''],
      dataInicioMatricula: [''],
      dataFimMatricula: [''],
      statusProva: [''],
      onibus: ['']
    });
  }

  async getColegioAutorizado(): Promise<any> {
    return new Promise<void>((res, rej) => {
      this.colegioAutorizadoService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.colegiosDefault = val;
          res();
        });
    });
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nomeColegioAutorizado.toLowerCase().indexOf(filterValue) === 0);
  }

  getunidades(): void {
    this.isLoadingResults = true;
    this.unidadeService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') {this.error = true; }
        else {

          this.unidades = val;
         // this.unidadesDefault?.length === 1 ? this.form.get('unidadeSelect').setValue(this.unidadesDefault[0].nome) : this.form.get('unidadeSelect').setValue('');
        }
      });
  }

  getCursos(): void {
    this.cursoService.getCursos().subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.cursos = val;
    })
  }

  getLocalProva(): void {
    this.cursoService.getCursos().subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.cursos = val;
    })
  }

  getNumeroOnibus(): void {
    this.listaDeChamadaOnibus = null;
    this.listaDeChamadaOnibusCabecalho = null;
    var unidadeIDSelecionada = this.form.value.unidadeSelect == '' ? null : this.form.value.unidadeSelect;
    var dataInicioMatricula = this.form.value.dataInicioMatricula == '' ? null : this.form.value.dataInicioMatricula;
    var dataFimMatricula = this.form.value.dataFimMatricula == '' ? null : this.form.value.dataFimMatricula;
    var tipoProva = this.form.value.tipoProva == '' ? null : this.form.value.tipoProva;

    if(unidadeIDSelecionada != null)
    {
      if(unidadeIDSelecionada.length == 1)
      {
        if(dataInicioMatricula != null && dataFimMatricula != null && tipoProva != null)
        {
          this.isLoadingResults = true;
          this.historicoProvasService.getNumeroOnibus(unidadeIDSelecionada[0], dataInicioMatricula, dataFimMatricula)
            .subscribe(val => {
            if (val['status'] === 'error') { this.error = true; }
            else { this.numeroOnibus = val; }
            this.isLoadingResults = false;
          });
        }
      }
    }
  }

  ListaColegioAutorizadoExcel(): void {
    this.isLoadingResults = true;
    this.filtrar();
    this.historicoProvasService.ListaColegioAutorizadoExcel(this.filtro)
      .subscribe(val => {
      this.isLoadingResults = false;
      if (val['status'] === 'error') {
        this.error = true;
      }
      else {
        this.historicoProvas = val;

        // Gerar XML
        if(val.length > 0)
        {
          var element = document.createElement('a');
          var blob = new Blob([JsonToXML.parse("aluno", this.historicoProvas, "alunos")], {
            type: 'text/xml'
          });
          var url = URL.createObjectURL(blob);
          element.href = url;
          element.setAttribute('download', 'arquivo.xml');
          document.body.appendChild(element);
          element.click();
        }
      }
    });
  }

  exportexcel(tabela: string, lista: string): void
  {

    let element = document.getElementById(tabela);

    if((this.listaDeChamadaOnibus != null && tabela == "excel-table3") ||
       (this.listaGeralDeInscritosParaProva != null && tabela == "excel-table2") ||
       (this.historicoProvas != null && tabela == "excel-table"))
    {
      if(tabela == "excel-table3")
      {
        var unidadeText = document.getElementById("unidadeID");
        var onibusText = document.getElementById("onibusID");
        lista = unidadeText.textContent + " " + onibusText.textContent + ".xlsx";
      }

      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element, {raw:true});

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

      /* save to file */
      XLSX.writeFile(wb, lista);
    }
  }

  ListaGeralDeInscritosParaProvaExcel(): void {
    this.isLoadingResults = true;
    this.filtrar();
    this.historicoProvasService.ListaGeralDeInscritosParaProvaExcel(this.filtro)
      .subscribe(val => {
      this.isLoadingResults = false;
      if (val['status'] === 'error') {
        this.error = true;
      }
      else {
        this.listaGeralDeInscritosParaProva = val;
      }
    });
  }

  ListaDeChamadaOnibusExcel(): void {
    this.filtrar();
    if(this.filtro.onibus != null)
    {
      this.isLoadingResults = true;
      this.historicoProvasService.ListaDeChamadaOnibusExcel(this.filtro)
        .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          this.listaDeChamadaOnibusCabecalho = val;
          this.listaDeChamadaOnibus = val.historicoProvas;
        }
      });
    }
  }

  filtrar(): void
  {

    this.filtro.unidadeSelect = this.form.value.unidadeSelect == '' ? null : this.form.value.unidadeSelect;
    this.filtro.colegioSelect = this.form.value.colegioSelect == '' ? null : this.form.value.colegioSelect;
    this.filtro.tipoProva = this.form.value.tipoProva == '' ? null : this.form.value.tipoProva;
    this.filtro.dataInicioMatricula = this.form.value.dataInicioMatricula == '' ? null : this.form.value.dataInicioMatricula;
    this.filtro.dataFimMatricula = this.form.value.dataFimMatricula == '' ? null : this.form.value.dataFimMatricula;
    this.filtro.statusProva = this.form.value.statusProva == '' ? null : this.form.value.statusProva;
    this.filtro.onibus = this.form.value.onibus == '' ? null : this.form.value.onibus;

  }

}
