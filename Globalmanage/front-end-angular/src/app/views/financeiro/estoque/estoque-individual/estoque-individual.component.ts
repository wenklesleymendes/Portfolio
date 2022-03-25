import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { CNPJMask } from 'src/app/utils/mask/mask';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { EstoqueService } from 'src/app/services/financeiro/estoque.service';

@Component({
  selector: 'app-estoque-individual',
  templateUrl: './estoque-individual.component.html',
  styleUrls: ['./estoque-individual.component.scss']
})
export class EstoqueIndividualComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cnpjMask = CNPJMask;
  unidades: any[] = null;

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private estoqueService: EstoqueService,
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
      nomeProduto: [null, [Validators.required]],
      alertaQuantidadeMinima: [null, [Validators.required, Validators.pattern(this.onlyNumbers)]],
      dataEntrada: [null, [Validators.required]],
      unidadeId: [null, [Validators.required]],
      codigoNCM: [null, [Validators.required]],
      codigoInterno: [null, [Validators.required]]
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
      this.estoqueService.getPorId(this.id)
      .subscribe(val => {
        if (!val) return;
        if (val['status'] === 'error') return this.error = true;
        this.form.patchValue(val);
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

  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }
    const formValue: any = this.form.value;
    const data = { ... formValue};
    // Make request
    this.estoqueService.cadastrar(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }
}
