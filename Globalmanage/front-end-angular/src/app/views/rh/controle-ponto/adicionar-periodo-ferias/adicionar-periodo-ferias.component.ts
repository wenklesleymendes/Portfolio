import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnexoService } from 'src/app/services/anexo.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteService } from 'src/app/services/delete.service';
import { FormService } from 'src/app/services/form.service';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-adicionar-periodo-ferias',
  templateUrl: './adicionar-periodo-ferias.component.html',
  styleUrls: ['./adicionar-periodo-ferias.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class AdicionarPeriodoFeriasComponent implements OnInit, OnDestroy {
  form: FormGroup;
  sendingAnexo: boolean = false;
  formData: FormData = new FormData();
  error: boolean = false;
  isLoadingResults: boolean = false;
  loadedFile: boolean = false;
  displayedColumns: string[] = ['dataInicio', 'dataFim', 'ausencia', 'options'];
  dataSource = new MatTableDataSource([]);
  expandedElement: any | null;
  update: boolean = false;
  anexoEdit: any = null;
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<AdicionarPeriodoFeriasComponent>,
    private fb: FormBuilder,
    private anexoService: AnexoService,
    private animationService: AnimationsService,
    private deleteService: DeleteService,
    private formService: FormService,
    private funcionarioService: FuncionarioService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getFerias();
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.update);
  }
  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      observacao: [null],
      documento: [null],
      tipoFeriasFolgaFalta: [null, [Validators.required]],
      inicio: [null, [Validators.required]],
      termino: [null, [Validators.required]]
    });
    this.form.setValidators([CompareInitialEndDate('inicio', 'termino')]);

    this.form.get('tipoFeriasFolgaFalta').valueChanges.subscribe(val => {
      if (!val || val == '') return;
      if (val == 1 || val == 2) this.formService.enableField(this.form.get('termino'));
      else this.formService.disableField(this.form.get('termino'));
    })
  }

  getFerias(): void {
    this.isLoadingResults = true;
    this.funcionarioService.getFerias(this.data['funcionarioId']).subscribe(val => {
      this.isLoadingResults = false;
      this.dataSource.data = val;
    })
  }

  resetForm(): void {
    this.form.reset();
    this.form.get('id').setValue(0);
  }
  
  labelAusensia(tipoFeriasFolgaFalta): string {
    if (tipoFeriasFolgaFalta == 1) return 'Folga';
    if (tipoFeriasFolgaFalta == 2) return 'Falta';
    if (tipoFeriasFolgaFalta == 3) return 'Férias Gozadas 30 dias de Descanso';
    if (tipoFeriasFolgaFalta == 4) return 'Férias Vendidas 30 dias';
    if (tipoFeriasFolgaFalta == 5) return 'Férias Gozadas 15 dias de Descanso + 15 dias vendidos';
    if (tipoFeriasFolgaFalta == 6) return 'Férias Gozadas 20 dias de Descanso + 10 dias vendidos';
    return ' - ';
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
      // Set FormData
      this.formData.set('file', file);
      this.formData.set('descricao', '');
      this.formData.set('tipoAnexo', '0');
      if (this.anexoEdit && this.anexoEdit.id) {
        this.formData.set('id', this.anexoEdit.id.toString());
      }
      this.loadedFile = true;
    };
  }

  adicionar(): void {
    if (!this.form.valid) {
      this.formService.validateAllFields(this.form);
      this.animationService.showErrorSnackBar('Preencha todos os dados obrigatórios');
      return;
    }

    const hasFile = this.formData.get('file');
    if (!hasFile && !this.anexoEdit && this.form.get('tipoFeriasFolgaFalta').value != 1 && this.form.get('tipoFeriasFolgaFalta').value != 2) {
      this.animationService.showErrorSnackBar('Adicione o documento');
      return;
    };

    const formValue = this.form.value;
    delete formValue.documento;
    const data = { ...formValue, funcionarioId: this.data['funcionarioId'] }
    this.sendingAnexo = true;

    // Post data
    this.funcionarioService.cadastrarFerias(data).subscribe(valFuncionario => {
      if (!valFuncionario || valFuncionario['status']) return;
      this.formData.set('FeriasFuncionarioId', valFuncionario['id']);
      this.formData.set('descricao', this.form.get('documento').value);
      this.update = true;

      if (this.loadedFile) {
        // Post file
        this.anexoService.upload(this.formData).subscribe(valAnexo => {
          if (valAnexo && valAnexo['status'] && valAnexo['status'] == 'done') {
            this.animationService.showSuccessSnackBar('Salvo com sucesso');
            this.getFerias();
            this.sendingAnexo = false;
            this.loadedFile = false;
            this.anexoEdit = null;
            this.resetForm();
          }
        });
      } else {
        this.sendingAnexo = false;
        this.loadedFile = false;
        this.getFerias();
        this.resetForm();
      }
    })
  }

  editar(element): void {
    this.form.patchValue(element);
    if (element && element.anexo && element.anexo[0]) {
      this.anexoEdit = element.anexo[0];
    }
  }

  removeFerias(index: number, id): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const dataOriginal = this.dataSource.data;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
      this.update = true;
      
      this.funcionarioService.deletarPorFérias(id).subscribe(val => {
        if (!val) this.dataSource.data = dataOriginal;
      })
    })
  }

  download(elem): void {
    const { id, arquivoString, extensao } = elem;

    this.anexoService.download(id, arquivoString, extensao);
  }

}
