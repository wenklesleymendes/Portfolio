import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AnexoService } from 'src/app/services/anexo.service';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Anexos } from 'src/app/utils/variables/anexos';
import { ContasPagarService } from 'src/app/services/financeiro/contas-pagar.service';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-detalhe-comprovantes',
  templateUrl: './detalhe-comprovantes.component.html',
  styleUrls: ['./detalhe-comprovantes.component.scss']
})
export class DetalheComprovantesComponent implements OnInit {
  @Input() id;
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  displayedColumns: string[] = ['descricao', 'tipo', 'dataAnexo', 'options'];
  dataSource = new MatTableDataSource([]);
  nome: string = '';
  sendingAnexo: boolean = false;
  anexos = Anexos;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private fb: FormBuilder,
    private anexoService: AnexoService,
    private contasPagarService : ContasPagarService,
    private animationService: AnimationsService,
    private deleteService: DeleteService,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.form = this.fb.group({
      descricao: [null],
      tipoAnexo: [null]
    });
    this.getAnexo();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAnexo(): void {
    this.isLoadingResults = true;
    this.contasPagarService.getDocumentosDespesa(this.id, false).subscribe(val => {
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

  labelAnexo(id): string {
    let label = this.anexos.find(anexo => anexo.value == id ).label;
    return label ? label : '-';
  }

  /**
   * @description Get file
   * @param {event} event - Input's event
   */
  loadFile(event): void {
    const { descricao, tipoAnexo } = this.form.value;

    if (!descricao || tipoAnexo === null) {
      this.animationService.showErrorSnackBar('Insira uma descrição e tipo do anexo');
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
      formData.append('DespesaId', this.id);

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          let data = this.dataSource.data;
          data.push({ descricao, dataAnexo: new Date(), id: val['response']['id'], tipoAnexo });
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
