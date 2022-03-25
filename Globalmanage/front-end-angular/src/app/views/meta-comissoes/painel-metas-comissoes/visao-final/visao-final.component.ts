import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import * as Highcharts from 'highcharts';

@Component({
    selector: 'app-visao-final',
    templateUrl: './visao-final.component.html',
    styleUrls: ['./visao-final.component.scss']
})
export class VisaoFinalComponent implements OnInit {
    @Input() graficoFinal: any;
    isLoadingResults: boolean = true;
    meta: number;
    realizado: number;
    constructor() { }

    // --------------------------------------------------------------------------
    //  LIFECYCLE HOOKS
    // --------------------------------------------------------------------------
    ngOnChanges(changes: SimpleChanges): void {
        if (!changes && !changes.graficoFinal) return;
        const { currentValue } = changes.graficoFinal;
        this.isLoadingResults = true;
        setTimeout(() => {
            if (currentValue) {
                this.meta = currentValue?.meta ? currentValue.meta : 0;
                this.realizado = currentValue?.realizado ? currentValue.realizado : 0;
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
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: ''
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b> <br> Valor: <b>{point.y}</b>'
            },
            accessibility: {
                point: {
                    valueSuffix: '%'
                }
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} % <br> Valor: <b>{point.y}</b>'
                    }
                }
            },
            exporting: {
                enabled: true,
                buttons: {
                    contextButton: {
                        menuItems: ['printChart', 'downloadPNG', 'downloadJPEG', 'downloadPDF'],
                    },
                },
            },
            series: [{
                name: 'Percentual',
                colorByPoint: true,
                data: [{
                    name: 'Meta',
                    y: this.meta,
                    sliced: true,
                    selected: true
                }, {
                    name: 'Realizado',
                    y: this.realizado
                }]
            }]
        }
        Highcharts.chart('grafico-visao-final', options);
    }
}