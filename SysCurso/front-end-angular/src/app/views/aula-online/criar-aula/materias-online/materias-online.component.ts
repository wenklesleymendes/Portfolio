import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router, ActivatedRoute } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { MatDialog } from '@angular/material/dialog';
import { AulaOnlineService } from 'src/app/services/aula-online/aula-online.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-materias-online',
  templateUrl: './materias-online.component.html',
  styleUrls: ['./materias-online.component.scss']
})
export class MateriasOnlineComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  aulaOnlineId: number = 0;
  nomeAulaOnline: string = null
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nomeCurso',
    'nomeMateria',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private aulaOnlineService: AulaOnlineService,
    private dialog: MatDialog,
    private routerActive: ActivatedRoute,
    private router: Router
  ) { 
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.aulaOnlineId = id ? parseInt(id) : 0;
  }
  
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
    if(!this.aulaOnlineId) return;

    this.isLoadingResults = true;
    this.error = false;

    this.aulaOnlineService.getMateriasPorAulaId(this.aulaOnlineId)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else {
          this.dataSource.data = val?.materiaOnline;
          this.nomeAulaOnline = val?.nomeAulaOnline;
        };
      });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.router.navigateByUrl('aula-online/criar-aula-online');
  }

  goTelaIndividual(id: string = '0', materia: string = null): void {
    this.router.navigateByUrl(`aula-online/materias-online/${this.aulaOnlineId}/${id}`, { state: { materia } });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.aulaOnlineService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
