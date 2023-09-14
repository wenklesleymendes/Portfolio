import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-documento-unidade',
  templateUrl: './documento-unidade.component.html',
  styleUrls: ['./documento-unidade.component.scss']
})
export class DocumentoUnidadeComponent implements OnInit, OnChanges {
  @Input() documentoUnidadeInput: any;
  displayedColumns: string[] = ['descricao', 'dataAnexo'];
  data = [];
  dataSource = new MatTableDataSource(this.data);
  selection = new SelectionModel<any>(true, []);

  constructor() { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    console.log('DATA', this.documentoUnidadeInput)
    this.dataSource.data = this.documentoUnidadeInput ? this.documentoUnidadeInput : [];;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.documentoUnidadeInput || changes.documentoUnidadeInput.firstChange) return;
    const { currentValue } = changes.documentoUnidadeInput;
    this.dataSource.data = currentValue ? currentValue : [];
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  removeDescricao(index: number): void {
    let data = this.dataSource.data;
    data.splice(index, 1);
    this.dataSource.data = data;
  }
}
