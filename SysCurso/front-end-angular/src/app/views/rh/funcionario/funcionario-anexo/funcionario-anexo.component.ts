import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AnexoService } from 'src/app/services/anexo.service';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Anexos } from 'src/app/utils/variables/anexos';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-funcionario-anexo',
  templateUrl: './funcionario-anexo.component.html',
  styleUrls: ['./funcionario-anexo.component.scss']
})
export class FuncionarioAnexoComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  displayedColumns: string[] = ['descricao', 'tipoAnexo', 'dataAnexo', 'options'];
  dataSource = new MatTableDataSource([]);
  nome: string = '';
  sendingAnexo: boolean = false;
  anexos = Anexos;
  documentosPendentes: any[] = [];
  documentosExcessao: number[] = [1, 2];
  documentosExibir: any[] = [];
  update: boolean = false;
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<FuncionarioAnexoComponent>,
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
      descricao: [''],
      tipoAnexo: [0]
    });

    this.nome = this.data?.nome;

    this.documentosPendentes = this.data?.documentosPendentes;
    this.documentosExibir = this.anexos.filter(elem => this.documentosExcessao.find(excessao => elem.value == excessao) ? null : elem)
    this.getAnexo();
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.update);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAnexo(): void {
    this.isLoadingResults = true;
    this.anexoService.getAnexo({ idFuncionario: this.data['id']}).subscribe(val => {
      this.isLoadingResults = false;
      this.dataSource.data = val;
    })
  }

  labelPendente(id): string {
    let label = this.anexos.find(anexo => anexo.value == id ).label;
    return label ? label : '-';
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
        this.update = true;
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
      formData.append('tipoAnexo', this.form.get('tipoAnexo').value);
      formData.append('FuncionarioId', this.data['id']);

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          let data = this.dataSource.data;
          data.push({ descricao, dataAnexo: new Date(), id: val['response']['id'], tipoAnexo: val['response']['tipoAnexo'] });
          this.dataSource.data = data;
          this.sendingAnexo = false;
          this.update = true;
          this.form.reset();
        } else this.sendingAnexo = false;
        //this.getAnexo();
      });
       this.getAnexo();
    };

  }

  download(id, arquivoString, extensao): void {
    this.anexoService.download(id, arquivoString, extensao);
  }

}
