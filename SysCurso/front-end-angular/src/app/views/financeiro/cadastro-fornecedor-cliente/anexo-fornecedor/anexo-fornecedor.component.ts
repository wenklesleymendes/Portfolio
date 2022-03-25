import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AnexoService } from 'src/app/services/anexo.service';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-anexo-fornecedor',
  templateUrl: './anexo-fornecedor.component.html',
  styleUrls: ['./anexo-fornecedor.component.scss']
})
export class AnexoFornecedorComponent implements OnInit {
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  displayedColumns: string[] = ['descricao', 'dataAnexo', 'options'];
  dataSource = new MatTableDataSource([]);
  nome: string = '';
  sendingAnexo: boolean = false;
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<AnexoFornecedorComponent>,
    private fb: FormBuilder,
    private anexoService: AnexoService,
    private animationService: AnimationsService,
    private deleteService: DeleteService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form = this.fb.group({
      descricao: ['']
    });

    this.nome = this.data['nome'];
    this.getAnexo();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAnexo(): void {
    this.isLoadingResults = true;
    this.anexoService.getAnexo({ FornecedorId: this.data['id']}).subscribe(val => {
      this.isLoadingResults = false;
      this.dataSource.data = val;
    })
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  removeAnexo(id, index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const dataOriginal = this.dataSource.data;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
  
      this.anexoService.deletarAnexo(id).subscribe(val => {
        if (!val) this.dataSource.data = dataOriginal;
      })
    })
  }

  /**
   * @description Get file
   * @param {event} event - Input's event
   */
  loadFile(event): void {
    const { descricao } = this.form.value;

    if (!descricao) {
      this.animationService.showErrorSnackBar('Insira uma descrição');
      return;
    }

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
      formData.append('descricao', this.form.get('descricao').value);
      formData.append('tipoAnexo', '0');
      formData.append('FornecedorId', this.data['id']);

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          let data = this.dataSource.data;
          data.push({ descricao, dataAnexo: new Date(), id: val['response']['id'] });
          this.dataSource.data = data;
          this.sendingAnexo = false;
          this.form.reset();
        }
        this.getAnexo();
      });
    };
  }

  download(id, arquivoString, extensao): void {
    this.anexoService.download(id, arquivoString, extensao);
  }

}
