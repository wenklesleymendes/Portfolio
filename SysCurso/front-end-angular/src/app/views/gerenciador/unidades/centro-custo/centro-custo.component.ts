import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { CentroCustoService } from 'src/app/services/gerenciador/centro-custo.service';
import { DeleteService } from 'src/app/services/delete.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-centro-custo',
  templateUrl: './centro-custo.component.html',
  styleUrls: ['./centro-custo.component.scss']
})
export class CentroCustoComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  displayedColumns: string[] = ['nome', 'options'];
  dataSource = new MatTableDataSource([]);
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<CentroCustoComponent>,
    private fb: FormBuilder,
    private centroCustoService: CentroCustoService,
    private deleteService: DeleteService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form = this.fb.group({
      nome: ['']
    });

    this.getCentroCusto();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getCentroCusto(): void {
    this.isLoadingResults = true;
    this.centroCustoService.getCentroCustoPorUnidade(this.data['id']).subscribe(val => {
      this.dataSource.data = val;
      this.isLoadingResults = false;
    })
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  addCentro(): void {
    const { nome } = this.form.value;
    if (!nome) return;
    
    this.centroCustoService.cadastrarCentroCusto({nome, unidadeId: this.data['id']}).subscribe(val => {
      let data = this.dataSource.data;
      data.push({ nome, id: val['id'] });
      this.dataSource.data = data;  
    })
  }

  removeCentro(id, index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const dataOriginal = this.dataSource.data;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;

      this.centroCustoService.deletarCentroCusto(id).subscribe(val => {
        if (!val) this.dataSource.data = dataOriginal;
       })
    });

  }
}
