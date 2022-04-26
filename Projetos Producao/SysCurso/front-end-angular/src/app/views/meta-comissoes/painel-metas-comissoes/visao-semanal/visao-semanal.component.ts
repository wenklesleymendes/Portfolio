import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import * as Highcharts from 'highcharts';
import { DatePtBrPipe } from 'src/app/utils/pipes/date-pt-br.pipe';

@Component({
  selector: 'app-visao-semanal',
  templateUrl: './visao-semanal.component.html',
  styleUrls: ['./visao-semanal.component.scss']
})
export class VisaoSemanalComponent implements OnInit {
  @Input() graficoDiario: any[] = [];
  isLoadingResults: boolean = true;
  xAxis: any[] = [];
  metas: any[] = [];
  realizadas: any[] = [];

  constructor(private datePtBrPipe: DatePtBrPipe) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.graficoDiario) return;
    const { currentValue } = changes.graficoDiario;
    this.isLoadingResults = true;
    setTimeout(() => {
      if (currentValue && currentValue?.length > 0) {
        this.xAxis = currentValue.map(elem => {
          if (!elem || elem == '') return ' - ';
          return this.datePtBrPipe.transform(elem.dataDiaria);
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

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  ngOnInit() {
  }

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
            menuItems: ['printChart', 'downloadPNG', 'downloadJPEG', 'downloadPDF'],
          },
        },
      },

      xAxis: {
        categories: this.xAxis,
        dateTimeLabelFormats: {
          hour: '%H:%M'
        },
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
            maxWidth: 100
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

    Highcharts.chart('grafico-visao-semanal', options);
  }
}