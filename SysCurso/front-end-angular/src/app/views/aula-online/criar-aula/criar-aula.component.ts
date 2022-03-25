import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { MatDialog } from '@angular/material/dialog';
import { AulaOnlineService } from 'src/app/services/aula-online/aula-online.service';
import { AddCursoOnlineComponent } from './add-curso-online/add-curso-online.component';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-criar-aula',
  templateUrl: './criar-aula.component.html',
  styleUrls: ['./criar-aula.component.scss']
})
export class CriarAulaComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

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
    private deleteService: DeleteService,
    private aulaOnlineService: AulaOnlineService,
    private dialog: MatDialog,
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

    this.aulaOnlineService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`aula-online/materias-online/${id}`);
  }

  openAddCurso(id: number = 0): void {
    const dialogRef = this.dialog.open(AddCursoOnlineComponent, {
      width: '90vw',
      autoFocus: false,
      data: {
        id
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
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
