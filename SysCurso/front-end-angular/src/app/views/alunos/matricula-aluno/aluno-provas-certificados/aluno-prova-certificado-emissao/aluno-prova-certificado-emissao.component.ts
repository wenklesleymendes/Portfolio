import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { FormService } from 'src/app/services/form.service';
import { AnexoService } from 'src/app/services/anexo.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import * as moment from 'moment';
import { CertificadoProvaService } from './../../../../../services/provas/certificado-prova.service';
import { ProvaAlunoService } from './../../../../../services/provas/provaAluno.service';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { analyzeAndValidateNgModules } from '@angular/compiler';

@Component({
  selector: 'app-aluno-prova-certificado-emissao',
  templateUrl: './aluno-prova-certificado-emissao.component.html',
  styleUrls: ['./aluno-prova-certificado-emissao.component.scss']
})
export class AlunoProvaCertificadoEmissaoComponent implements OnInit {
  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = false;
  datasPedido: any;
  today = moment();
  sendingAnexo: boolean = false;
  dataSource = new MatTableDataSource([]);
  matriculaAluno: any;
  matriculaAluno$: Subscription;
  statusCertificado: any;
  certificado: any;
  anexoId: any;
  id: any;
  certificadosEmitidos: any[];
  desabilitarForm: boolean = false;
  displayedColumns: string[] = [
    'tabDataSolicitacao',
    'tabDataEmissao',
    'tabgdae',
    'tabDownload',
  ];
  constructor(
    private fb: FormBuilder,
    private formService: FormService,
    private anexoService: AnexoService,
    private animationService: AnimationsService,
    private provaAlunoService: ProvaAlunoService,
    private certificadoProvaService: CertificadoProvaService,
    private store: Store<AlunoStoreState.Aluno>,
  ) { }

  ngOnInit(): void {
    this.matriculaAluno$ = this.store.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => this.matriculaAluno = val);
    this.buildForm();
    Promise.all([])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);
  }

  buildForm(): void {
    this.form = this.fb.group({
      statusCertificado: [null, [Validators.required]],
      dataRecebimentoSuporte: [null, [Validators.required]],
      dataEntregaAluno: [null],
      gdae: [null],

    })
    this.form.get('statusCertificado').valueChanges.subscribe(val => {
      this.statusCertificado = val;
      switch (val) {
        case 2:
          this.form.get('dataRecebimentoSuporte').setValidators(Validators.required);
          this.form.get('dataEntregaAluno').disable();
          this.form.get('dataRecebimentoSuporte').enable();
          this.form.get('gdae').setValidators(Validators.required);
          this.form.get('gdae').enable();
          if (!this.form.value.dataRecebimentoSuporte)
            this.form.get('dataRecebimentoSuporte').setValue(this.today);
          break;
        case 3:
          this.form.get('dataEntregaAluno').setValidators(Validators.required);
          this.form.get('dataRecebimentoSuporte').disable();
          this.form.get('gdae').disable();
          this.form.get('dataEntregaAluno').enable();
          if (!this.form.value.dataEntregaAluno)
            this.form.get('dataEntregaAluno').setValue(this.today);
        default:
          break;
      }
    });
  }

  loadData(): void {

    this.form.reset();
    this.id = 0;
    this.statusCertificado = 1;
    this.certificadoProvaService.buscarSolicitacaoAtual(this.matriculaAluno.id).subscribe(val => {
      if (!val) {
        this.form.get('statusCertificado').setValue(1);
        return;
      }
      if (val?.status === 'error') { return this.error = true; }

      this.id = val.id;
      this.certificado = val;
      this.statusCertificado = val.statusCertificado;
      this.form.get('statusCertificado').setValue(val.statusCertificado);
      this.form.get('dataRecebimentoSuporte').setValue(val.dataRecebimentoSuporte);
      this.form.get('dataEntregaAluno').setValue(val.dataEntregaAluno);
      this.form.get('gdae').setValue(val.gdae);
    });
  }

  download(id: any): void {
    this.anexoService.download(id, 'ReciboCertificado.pdf', 'application/pdf');
  }
  /**
 * @description Get file
 * @param {event} event - Input's event
 */
  loadFile(event: { target: { files: File[]; }; }): void {
    let reader = new FileReader();
    // Select file
    let file: File = event.target.files[0];

    const maxSize: number = 50 * 1024 * 1024;
    if (file?.size > maxSize) {
      this.animationService.showErrorSnackBar('Arquivo ultrapassa 50mb');
      return;
    }

    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      this.sendingAnexo = true;
      const formData: FormData = new FormData();
      // Set FormData
      formData.append('file', file);
      formData.append('tipoAnexo', '37');
      formData.append('MatriculaAlunoId', this.matriculaAluno?.id);

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          console.log('VAL', val)
          this.sendingAnexo = false;
          this.anexoId = val['response']['id'];
          let data = this.dataSource.data;
          data.push({ dataAnexo: new Date(), id: this.anexoId, tipoAnexo: val['response']['tipoAnexo'] });
          this.dataSource.data = data;
          this.salvarData(true);
        }
        if (val?.status == 'error') this.sendingAnexo = false;

      });
    };
  }

  salvarData(ignorarValidacao) {

    this.formService.validateAllFields(this.form);

    if (!this.form.valid && !ignorarValidacao) {
      this.animationService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue: any = this.form.value;
    formValue['id'] = this.id;
    formValue['MatriculaAlunoId'] = this.matriculaAluno?.id;
    formValue['statusCertificado'] = this.statusCertificado;
    formValue['anexoId'] = this.anexoId;

    switch (this.statusCertificado) {
      case 3:
        formValue['dataRecebimentoSuporte'] = this.certificado.dataRecebimentoSuporte;
        formValue['gdae'] = this.certificado.gdae;
        break;
      default:
        break;
    }

    this.certificadoProvaService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationService.showSuccessSnackBar('Salvo com sucesso');
          this.buildForm();
          this.loadData();
      }
    });
  }
}
