import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DeleteService } from 'src/app/services/delete.service';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { Router } from '@angular/router';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-curso',
  templateUrl: './curso.component.html',
  styleUrls: ['./curso.component.scss']
})
export class CursoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource([]);
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = ['descricao', 'options'];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private cursoService: CursoService,
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

    this.cursoService.getCursos().subscribe(val => {
      if (!val) return;
      if (val['status'] === 'error') this.error = true;
      else this.dataSource.data = val;
      this.isLoadingResults = false;
    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`gerenciador/curso-turmas/adicionar-curso/${id}`)
  }

  delete(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.cursoService.deletarCurso(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
