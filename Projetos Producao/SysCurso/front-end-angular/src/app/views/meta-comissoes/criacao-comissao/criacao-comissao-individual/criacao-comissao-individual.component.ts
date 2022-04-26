import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { DeleteService } from 'src/app/services/delete.service';
import { ComissoesService } from 'src/app/services/metas-comissoes/comissoes.service';

@Component({
  selector: 'app-criacao-comissao-individual',
  templateUrl: './criacao-comissao-individual.component.html',
  styleUrls: ['./criacao-comissao-individual.component.scss']
})
export class CriacaoComissaoIndividualComponent implements OnInit {
  form: FormGroup;
  parcelasForm: FormArray;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;  
  cpfMask = CPFMask;
  unidades: any[] = null;
  parcelas = Array(12).fill('').map((x,i) => i+1);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private comissoesService: ComissoesService,
    private deleteService: DeleteService,
    private routerActive: ActivatedRoute,
    private unidadeService: UnidadeService,
    private formService: FormService
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getunidades();
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      unidadeId: [null, [Validators.required]],
      dataInicioVirgencia: [null, [Validators.required]],
      dataFimVirgencia: [null, [Validators.required]],
      periodoIndeterminado: [false, [Validators.required]],
      tipoPagamento: [null],
      quantidadeParcelas: [12],
      tipoComissao: [{ disabled: true, value: false}],
      parcelasForm: this.fb.array([
        this.fb.group({
          id: [0],
          valor: [null]
        })
      ])
    })
    this.parcelasForm = this.form.get('parcelasForm') as FormArray;

    this.form.setValidators([
      CompareInitialEndDate('dataInicioVirgencia', 'dataFimVirgencia')
    ]);

    this.form.get('periodoIndeterminado').valueChanges.subscribe(val => {
      if (val){
        this.formService.disableField(this.form.get('dataInicioVirgencia'))
        this.formService.disableField(this.form.get('dataFimVirgencia'))
      } else {
        this.formService.enableField(this.form.get('dataInicioVirgencia'))
        this.formService.enableField(this.form.get('dataFimVirgencia'))
      }
    })
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else {
          this.unidades = val;
        }
      });
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.comissoesService.getPorId(this.id)
      // .pipe(delay(1000))
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;

        const { comissaoParcelas } = val;
        this.form.patchValue(val);

        if (comissaoParcelas) {
          this.parcelasForm.removeAt(0);
          comissaoParcelas.forEach(elem => this.addParcelas(elem));
        }
        this.isLoadingResults = false;
      })
    }
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  addParcelas(elem?: any): void {
    const id = elem?.id ? elem.id : 0;
    const valor = elem?.valor ? elem.valor : null;
    this.parcelasForm.push(
      this.fb.group({
        id: id,
        valor: valor
      })
    );
  }

  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue: any = this.form.value;
    const parcelas = this.form.get('parcelasForm').value;

    let comissaoParcelas: any[] = [];
    parcelas.forEach((elem, index) => {
      comissaoParcelas.push({
        "id": elem.id,
        "numeroParcela": (index + 1),
        "valor": elem.valor
      });
    });

    delete formValue['parcelasForm'];
    formValue['quantidadeParcelas'] = comissaoParcelas.length;
    const data = { comissaoParcelas, ... formValue}
    // Make request
    this.comissoesService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }

  removeDoFormArray(controls: any, index: number) {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      controls as FormArray;
      controls.removeAt(index);
    });
  }
}
