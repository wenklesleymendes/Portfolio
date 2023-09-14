import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { DataProvider } from '../data-provider';
import { DeleteService } from 'src/app/services/delete.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-historico-ocorrencias',
  templateUrl: './historico-ocorrencias.component.html',
  styleUrls: ['./historico-ocorrencias.component.scss']
})
export class HistoricoOcorrenciasComponent implements OnInit {
  @Input() historicoOcorrenciasInput: any;
  form: FormGroup;
  displayedColumns: string[] = ['descricao', 'data', 'options'];
  dataSource = new MatTableDataSource();
  btnAddRegistroHidden: boolean = true;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private fb: FormBuilder,
    private dataProvider: DataProvider,
    private deleteService: DeleteService
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnChanges(changes: SimpleChanges): void {
    if (!changes && !changes.historicoOcorrenciasInput || changes.historicoOcorrenciasInput.firstChange) return;
    const { currentValue } = changes.historicoOcorrenciasInput;
    this.dataSource.data = currentValue ? currentValue : [];
    this.dataProvider.historicoOcorrenciaNext(this.dataSource.data);
  }
  
  ngOnInit(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required]]
    })

    this.dataSource.data = this.historicoOcorrenciasInput ? this.historicoOcorrenciasInput : [];
    this.dataProvider.historicoOcorrenciaNext(this.dataSource.data);
    this.onFormChanges();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  onFormChanges(): void {
    // Descricao
    this.form.get('descricao').valueChanges.subscribe(val => {
      this.btnAddRegistroHidden = (val == '') ? true : false;
    })
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  addRegistro(): void {
    const { descricao} = this.form.value;
    if (!descricao) return;
    let data = this.dataSource.data;
    data.push({ descricao, id: 0 });
    this.dataSource.data = data;
    this.dataProvider.historicoOcorrenciaNext(data);
    this.form.reset();
  }

  removeRegistro(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
      this.dataProvider.historicoOcorrenciaNext(data);
    });
  }
}
