import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { TurmaService } from 'src/app/services/gerenciador/turma.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-turma',
  templateUrl: './turma.component.html',
  styleUrls: ['./turma.component.scss']
})
export class TurmaComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'unidade',
    'curso',
    'presencial',
    'ano',
    'semestre',
    'diaSemana',
    'periodo',
    'horario',
    'sala',
    'disponivel',
    'quantidadeVagas',
    'options'
  ];

  dataSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);

  constructor(
    private turmaService: TurmaService,
    private deleteService: DeleteService,
    private router: Router
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    this.turmaService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  ajustarVigencia(vigencia): string {
    if (vigencia == null || vigencia.length == 0) return ' - ';
    if (vigencia == '1') return '1º Semestre';
    if (vigencia == '2') return '2º Semestre';
    if (vigencia == '3') return 'Anual';
    if (vigencia == '4') return 'Bimestral';
    if (vigencia == '5') return 'Trimestral';
    return ' - ';
  }

  ajustarPeriodo(periodo): string {
    if (periodo == null || periodo.length == 0) return ' - ';
    if (periodo == 1) return 'Manhã';
    if (periodo == 2) return 'Tarde';
    if (periodo == 3) return 'Noite';
    return ' - ';
  }
  
  ajustarUnidade(unidades: any[]): string {
    if (unidades == null || unidades.length == 0) return ' - ';
    let text = '';
    unidades.forEach(elem => text += `${elem.nome ? elem.nome : '  '}, `)
    return text.substring(0, text.length - 2);
  }

  ajustarCurso(cursos: any[]): string {
    if (cursos == null || cursos.length == 0) return ' - ';
    let text = '';
    cursos.forEach(elem => text += `${elem.descricao ? elem.descricao : '  '}, `)
    return text.substring(0, text.length - 2);
  }

  ajustarDiaDaSemana(days): string {
    if (days == null || days.length == 0) return ' - ';
    let week = '';
    for(let day in days) if (days[day]) week += `${day}-`;
    if (week.length < 3) return ' - ';
    return week.substring(0, week.length - 1);
  }

  ajustarHorario(inicio: string, fim: string): string {
    if ((inicio == null || inicio.length == 0) && (fim == null || fim.length == 0)) return ' - ';
    inicio = inicio.padStart(4, '0');
    fim = fim.padStart(4, '0');
    return `${inicio.slice(0,2)}:${inicio.slice(2,5)} - ${fim.slice(0,2)}:${fim.slice(2,5)}`;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0', replicar: boolean = false): void {
    this.router.navigateByUrl(`gerenciador/curso-turmas/adicionar/${id}`, { state: { replicar } })
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.turmaService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
