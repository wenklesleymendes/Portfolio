import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router, ActivatedRoute } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { PerguntaService } from 'src/app/services/aula-online/pergunta.service';
import { Location } from '@angular/common';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-perguntas-online',
  templateUrl: './perguntas-online.component.html',
  styleUrls: ['./perguntas-online.component.scss']
})
export class PerguntasOnlineComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  aulaOnlineId: number = 0;
  materiaId: number = 0;
  videoId: number = 0;
  nomeAula: string = null;
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nome',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private deleteService: DeleteService,
    private perguntaService: PerguntaService,
    private routerActive: ActivatedRoute,
    private router: Router
  ) { 
    // Get id
    const aulaOnlineId = this.routerActive.snapshot.paramMap.get('aulaOnlineId');
    this.aulaOnlineId = aulaOnlineId ? parseInt(aulaOnlineId) : 0;
    const materiaId = this.routerActive.snapshot.paramMap.get('materiaId');
    this.materiaId = materiaId ? parseInt(materiaId) : 0;
    const videoId = this.routerActive.snapshot.paramMap.get('videoId');
    this.videoId = videoId ? parseInt(videoId) : 0;
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
    this.isLoadingResults = true;
    this.error = false;

    this.perguntaService.buscarPorVideoAula(this.videoId)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else {
          const { titulo, lista } = val;
          this.nomeAula = titulo;
          this.dataSource.data = lista;
        }
      });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }


  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`aula-online/materias-online/${this.aulaOnlineId}/${this.materiaId}/${this.videoId}/${id}`);
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.perguntaService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
