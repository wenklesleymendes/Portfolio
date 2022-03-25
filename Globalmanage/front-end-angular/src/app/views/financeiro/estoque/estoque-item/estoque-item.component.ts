import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { CNPJMask } from 'src/app/utils/mask/mask';
import { EstoqueService } from 'src/app/services/financeiro/estoque.service';
import { FormService } from 'src/app/services/form.service';
import { validarCNPJ } from 'src/app/utils/form-validation/cnpj.validation';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-estoque-item',
  templateUrl: './estoque-item.component.html',
  styleUrls: ['./estoque-item.component.scss']
})
export class EstoqueItemComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  saving: boolean = false;
  form: FormGroup;
  displayedColumns: string[] = ['dataEntrada', 'nomeFornecedor', 'cnpj', 'numeroNotaFiscal',  'quantidadeEntrada', 'quantidadeSaida', 'options'];
  dataSource = new MatTableDataSource([]);
  nome: string = '';
  produtoId: number = null;
  quantidadeEstoque: number = null;
  onlyNumbers = /^-?(0|[1-9]\d*)?$/;
  cnpjMask = CNPJMask;
  sendingAnexo: boolean = false;
  update: boolean = false;
  selection = new SelectionModel<any>(true, []);

  constructor(
    public dialogRef: MatDialogRef<EstoqueItemComponent>,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private deleteService: DeleteService,
    private formService: FormService,
    private estoqueService: EstoqueService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    const { id, nomeProduto, quantidadeEstoque } = this.data['element']
    this.produtoId = id;
    this.nome = nomeProduto;
    this.quantidadeEstoque = quantidadeEstoque;
    this.buildForm();
    this.getAll();
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
      dataEntrada: [null, [Validators.required]],
      nomeFornecedor: [null, [Validators.required]],
      cnpj: [null, [Validators.required, validarCNPJ]],
      numeroNotaFiscal: [null, [Validators.required]],
      quantidadeEntrada: [null, [Validators.pattern(this.onlyNumbers)]],
      produtoId: [this.produtoId ],
    });
    
  }

  getAll(): void {
    if (!this.produtoId) return;
    this.isLoadingResults = true;
    this.estoqueService.getItens(this.produtoId).subscribe(val => {
      if (val['status'] === 'error') this.error = true;
      else this.dataSource.data = val;
      this.isLoadingResults = false;
    })
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue: any = this.form.value;
    if (formValue.quantidadeSaida > (this.quantidadeEstoque + formValue.quantidadeEntrada)) {
      this.animationsService.showErrorSnackBar('Quantidade indiponível no estoque');
      return;
    }

    const data = { ... formValue};
    // Make request
    this.saving = true;
    this.estoqueService.cadastrarItem(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        this.buildForm();
        this.getAll();
      }
      this.update = true;
      this.saving = false;
    })
  }

  editar(element: any): void {
    this.form.patchValue(element);
    this.formService.untuchAllFields(this.form);
  }

  limparForm(): void {
    this.buildForm();
  }

  remove(id, index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const dataOriginal = this.dataSource.data;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
  
      this.estoqueService.deletarItemPorId(id).subscribe(val => {
        if (!val) this.dataSource.data = dataOriginal;
        this.update = true;
      })
    })
  }
}
