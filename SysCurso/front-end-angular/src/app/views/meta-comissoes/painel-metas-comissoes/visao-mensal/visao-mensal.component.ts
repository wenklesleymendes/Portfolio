import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import * as Highcharts from 'highcharts';
import { Meses } from 'src/app/utils/variables/meses';

@Component({
  selector: 'app-visao-mensal',
  templateUrl: './visao-mensal.component.html',
  styleUrls: ['./visao-mensal.component.scss']
})
export class VisaoMensalComponent implements OnInit {
  @Input() graficoMensal: any[] = [];
  isLoadingResults: boolean = true;
  xAxis: any[] = [];
  metas: any[] = [];
  realizadas: any[] = [];

  constructor() { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.graficoMensal) return;
    const { currentValue } = changes.graficoMensal;
    this.isLoadingResults = true;
    setTimeout(() => {
      if (currentValue && currentValue?.length > 0) {
        this.xAxis = currentValue.map(elem => {
          if (!elem || elem == '') return ' - ';
          const date = new Date(elem.mes);
          const month = Meses.find(elem => elem.value == date.getUTCMonth()).short;
          const year = date.getUTCFullYear();
          return `${month}/${year}`;
        });
        this.metas = currentValue.map(elem => elem.quantidadeMeta);
        this.realizadas = currentValue.map(elem => elem.quantidadeMatriculasRealizadas);
      }
      setTimeout(() => {
        this.isLoadingResults = false;
        this.createChart();
      }, 50);
    }, 1500);
  }

  ngOnInit() {
  }
  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  createChart() {
    const options: any = {
      title: {
        text: ''
      },

      yAxis: {
        title: {
          text: 'Matrículas'
        }
      },

      credits: {
        enabled: false
      },

      exporting: {
        enabled: true,
        buttons: {
          contextButton: {
            menuItems: ['printChart', 'downloadPNG', 'downloadJPEG', 'downloadPDF', 'exportCSV', 'downloadXLS'],
          },
        },
      },

      xAxis: {
        categories: this.xAxis,
        accessibility: {
          rangeDescription: ''
        }
      },

      legend: {
        layout: 'vertical',
        align: 'center',
        verticalAlign: 'top'
      },

      plotOptions: {
        series: {
          label: {
            connectorAllowed: false
          },
        }
      },

      series: [{
        name: 'Meta',
        data: this.metas
      }, {
        name: 'Realizado',
        data: this.realizadas
      }],

      responsive: {
        rules: [{
          condition: {
            maxWidth: 500
          },
          chartOptions: {
            legend: {
              layout: 'horizontal',
              align: 'center',
              verticalAlign: 'bottom'
            }
          }
        }]
      }
    }

    Highcharts.chart('grafico-visao-mensal', options);
  }
}