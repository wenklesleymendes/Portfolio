import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnexoService } from 'src/app/services/anexo.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { DeleteService } from 'src/app/services/delete.service';
import { MatTableDataSource } from '@angular/material/table';
import { FormService } from 'src/app/services/form.service';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { CompareInitialEndHour } from 'src/app/utils/form-validation/initial-end-hour.validation';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-ocorrencia',
  templateUrl: './ocorrencia.component.html',
  styleUrls: ['./ocorrencia.component.scss']
})
export class OcorrenciaComponent implements OnInit, OnDestroy {
  form: FormGroup;
  sendingAnexo: boolean = false;
  hourMinute = HourMinuteMask;
  formData: FormData = new FormData();
  error: boolean = false;
  isLoadingResults: boolean = false;
  loadedFile: boolean = false;
  displayedColumns: string[] = ['descricao', 'dataAnexo', 'options'];
  dataSource = new MatTableDataSource([]);
  entrada1: Date = null;  
  saida1: Date = null;
  entrada2: Date = null;
  saida2: Date = null;
  entrada3: Date = null;
  saida3: Date = null;
  entrada4: Date = null;
  saida4: Date = null;
  updated: boolean = false;
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<OcorrenciaComponent>,
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
    this.loadData();
    this.getAnexo();

    this.isLoadingResults = true
    setTimeout(() => this.isLoadingResults = false, 500)
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.updated);
  }
  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      entrada1: [null],
      saida1: [null],
      entrada2: [null],
      saida2: [null],
      entrada3: [null],
      saida3: [null],
      entrada4: [null],
      saida4: [null],
      tipoOcorrenciaPonto: [null, Validators.required],
      regimeContratacao: [null, Validators.required],
      observacao: [null, Validators.required],
      documento: [null]
    });

    this.form.setValidators([
      CompareInitialEndHour('entrada1', 'saida1'),
      CompareInitialEndHour('saida1', 'entrada2'),
      CompareInitialEndHour('entrada2', 'saida2'),
      CompareInitialEndHour('saida2', 'entrada3'),
      CompareInitialEndHour('entrada3', 'saida3'),
      CompareInitialEndHour('saida3', 'entrada4'),
      CompareInitialEndHour('entrada4', 'saida4')
    ]);

    this.form.get('entrada1').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.entrada1 = null;
      if (!this.entrada1) this.entrada1 = this.getDate();
      this.entrada1.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('saida1').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.saida1 = null;
      if (!this.saida1) this.saida1 = this.getDate();
      this.saida1.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('entrada2').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.entrada2 = null;
      if (!this.entrada2) this.entrada2 = this.getDate();
      this.entrada2.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('saida2').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.saida2 = null;
      if (!this.saida2) this.saida2 = this.getDate();
      this.saida2.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('entrada3').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.entrada3 = null;
      if (!this.entrada3) this.entrada3 = this.getDate();
      this.entrada3.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('saida3').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.saida3 = null;
      if (!this.saida3) this.saida3 = this.getDate();
      this.saida3.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('entrada4').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.entrada4 = null;
      if (!this.entrada4) this.entrada4 = this.getDate();
      this.entrada4.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
    this.form.get('saida4').valueChanges.subscribe((val: string) => {
      if (!val || val == '') return this.saida4 = null;
      if (!this.saida4) this.saida4 = this.getDate();
      this.saida4.setUTCHours(this.updateHour(val).hour, this.updateHour(val).minute);
    });
  }

  loadData(): void {
    const {
      entrada1,
      saida1,
      entrada2,
      saida2,
      entrada3,
      saida3,
      entrada4,
      saida4,
      observacao,
      tipoOcorrenciaPonto,
      regimeContratacao
    } = this.data['data'];

    if (entrada1) this.entrada1 = new Date(entrada1);
    else this.entrada1 = this.getDate();
    if (saida1) this.saida1 = new Date(saida1);
    else this.saida1 = this.getDate();
    if (entrada2) this.entrada2 = new Date(entrada2);
    else this.entrada2 = this.getDate();
    if (saida2) this.saida2 = new Date(saida2);
    else this.saida2 = this.getDate();
    if (entrada3) this.entrada3 = new Date(entrada3);
    else this.entrada3 = this.getDate();
    if (saida3) this.saida3 = new Date(saida3);
    else this.saida3 = this.getDate();
    if (entrada4) this.entrada4 = new Date(entrada4);
    else this.entrada4 = this.getDate();
    if (saida4) this.saida4 = new Date(saida4);
    else this.saida4 = this.getDate();

    if (tipoOcorrenciaPonto && tipoOcorrenciaPonto > 0) this.form.get('tipoOcorrenciaPonto').setValue(tipoOcorrenciaPonto);
    this.form.get('observacao').setValue(observacao);
    this.form.get('regimeContratacao').setValue(regimeContratacao);
    this.form.get('entrada1').setValue(this.getHourFromDate(this.entrada1));
    this.form.get('saida1').setValue(this.getHourFromDate(this.saida1));
    this.form.get('entrada2').setValue(this.getHourFromDate(this.entrada2));
    this.form.get('saida2').setValue(this.getHourFromDate(this.saida2));
    this.form.get('entrada3').setValue(this.getHourFromDate(this.entrada3));
    this.form.get('saida3').setValue(this.getHourFromDate(this.saida3));
    this.form.get('entrada4').setValue(this.getHourFromDate(this.entrada4));
    this.form.get('saida4').setValue(this.getHourFromDate(this.saida4));
  }

  getAnexo(): void {
    this.isLoadingResults = true;
    this.anexoService.getAnexo({ idPontoEletronico: this.data['data']['id']}).subscribe(val => {
      this.isLoadingResults = false;
      this.dataSource.data = val;
    })
  }

  updateHour(hour: string): {hour: number, minute: number} {
    if (!hour || hour == '') return null;
    hour = hour.padStart(4, '0');
    const _hour = hour.slice(0,2);
    const _minute = hour.slice(2,5);
    return { hour: parseInt(_hour), minute: parseInt(_minute)}
  }

  getDate(): Date {
    const dataCadastrado = this.data['data']['dataCadastrado'] as String;
    const day = dataCadastrado.split('/')[0];
    const month = dataCadastrado.split('/')[1];
    const year = dataCadastrado.split('/')[2];
    const date: Date = new Date(`${year}-${month}-${day} 00:00`);
    date.setHours(0,0,0,0);
    return date;
  }

  getHourFromDate(date: Date) {
    if (!date) return null;
    const _hour = date.getHours().toString().padStart(2, '0');
    const _minute = date.getMinutes().toString().padStart(2, '0');
    if (_hour === '00' && _minute === '00') return null;
    return `${_hour}${_minute}`;
  }

  validateHour(date: Date): Date {
    if (!date) return null;
    const _hour = date.getHours().toString().padStart(2, '0');
    const _minute = date.getMinutes().toString().padStart(2, '0');
    if (_hour === '00' && _minute === '00') return null;
    return date;
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
      this.loadedFile = true;
    };
  }

  adicionar(): void {
    if (!this.form.valid) {
      this.formService.validateAllFields(this.form);
      this.animationService.showErrorSnackBar('Preencha todos os dados obrigatórios');
      return;
    }

    if (this.form.get('tipoOcorrenciaPonto').value == 2 || this.form.get('tipoOcorrenciaPonto').value == 3) {
      if (!this.formData.get('file')) {
        this.animationService.showErrorSnackBar('Insira um documento');
        return;
      }
    }

    const { tipoOcorrenciaPonto, regimeContratacao, observacao } = this.form.value;
    const hours = {
      entrada1: (tipoOcorrenciaPonto == 1 || tipoOcorrenciaPonto == 7) ? this.getDate() : this.validateHour(this.entrada1),
      saida1: (tipoOcorrenciaPonto == 1 || tipoOcorrenciaPonto == 7) ? this.getDate() : this.validateHour(this.saida1),
      entrada2: this.validateHour(this.entrada2),
      saida2: this.validateHour(this.saida2),
      entrada3: this.validateHour(this.entrada3),
      saida3: this.validateHour(this.saida3),
      entrada4: this.validateHour(this.entrada4),
      saida4: this.validateHour(this.saida4)
    };
    const data = {
      ...hours,
      observacao,
      tipoOcorrenciaPonto,
      regimeContratacao,
      id: this.data['data']['id'],
      numeroPIS: this.data['data']['numeroPIS'],
      funcionarioId: this.data['data']['funcionarioId'],
    }

    // Post data
    this.sendingAnexo = true;
    this.funcionarioService.atualizarPontoEletronico(data).subscribe(valFuncionario => {
      this.updated = true;
      if (!valFuncionario || valFuncionario['status']) return;
      this.animationService.showSuccessSnackBar('Salvo com sucesso')
      this.formData.set('PontoEletronicoId', this.data['data']['id']);
      this.formData.set('descricao', this.form.get('documento').value);

      // Post file
      if (this.formData.get('file')) {
        this.anexoService.upload(this.formData).subscribe(valAnexo => {
          if (valAnexo && valAnexo['status'] && valAnexo['status'] == 'done') {
            this.sendingAnexo = false;
          } else this.sendingAnexo = false;
        });
      } else this.sendingAnexo = false;
    })
  }

  removeAnexo(id, index: number): void {
    this.deleteService.openDelete().then(res => {
      this.updated = true;
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

  download(id, arquivoString, extensao): void {
    this.anexoService.download(id, arquivoString, extensao);
  }

}
