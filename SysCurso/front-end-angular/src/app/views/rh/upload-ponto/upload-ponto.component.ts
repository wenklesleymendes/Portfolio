import { Component, OnInit, ViewChild } from '@angular/core';
import { ControlePontoService } from 'src/app/services/rh/controle-ponto.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DeleteService } from 'src/app/services/delete.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-upload-ponto',
  templateUrl: './upload-ponto.component.html',
  styleUrls: ['./upload-ponto.component.scss']
})
export class UploadPontoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  
  sendingAnexo: boolean = false;
  isLoadingResults: boolean = false;
  error: boolean = false;
  displayedColumns: string[] = [
    'nomeArquivo',
    'dataCadastro',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private controlePontoService: ControlePontoService,
    private deleteService: DeleteService,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
    this.getAll();
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    this.controlePontoService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  /**
   * @description Get file
   * @param {event} event - Input's event
   */
  loadFile(event): void {

    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      this.sendingAnexo = true;
      const formData: FormData = new FormData();
      // Set FormData
      formData.append('file', file);

      this.controlePontoService.subirPonto(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          this.sendingAnexo = false;
          this.getAll();
        }
      });
    };
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.controlePontoService.deletarPorId(id).subscribe(val => this.getAll());
    })
  }
}
