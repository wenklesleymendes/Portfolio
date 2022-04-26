import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteService } from 'src/app/services/delete.service';
import { Router } from '@angular/router';
import { FuncionarioService } from 'src/app/services/rh/funcionario.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { MatDialog } from '@angular/material/dialog';
import { FuncionarioAnexoComponent } from './funcionario-anexo/funcionario-anexo.component';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { FuncionarioDetalhamentoComponent } from './funcionario-detalhamento/funcionario-detalhamento.component';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-funcionario',
  templateUrl: './funcionario.component.html',
  styleUrls: ['./funcionario.component.scss']
})
export class FuncionarioComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  form: FormGroup;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  filterUnidades: Observable<any[]>;
  unidadesDefault = [];
  displayedColumns: string[] = [
    'unidade',
    'nomeColaborador',
    'cpf',
    'regimeContratacao',
    'dataContratacao',
    'dataRecisao',
    'feriasVencido',
    'documentos',
    'isActive',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private funcionarioService: FuncionarioService,
    private unidadeService: UnidadeService,
    private fb: FormBuilder,
    private dialog: MatDialog,
    private router: Router
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.buildForm();
    this.getAll();
    this.getunidades();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------

  buildForm(): void {
    this.form = this.fb.group({
      nome: [null],
      cpf: [null, [validarCPF]],
      unidadeId: [null],
      unidadeSelect: [''],
      ativo: [null],
      dataFimTerminoContrato: [null],
      dataInicioTerminoContrato: [null]
    });

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
      else this.form.get('unidadeId').setValue(null);
    })
  }

  getAll() {
    this.isLoadingResults = true;
    this.error = false;
    const formValue = this.form.value;
    delete formValue['unidadeSelect'];
    this.funcionarioService.getAllFiltro(formValue)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  getunidades(): void {
    this.unidadeService.getAll()
      .subscribe(val => {
        if (val['status'] === 'error') this.error = true;
        else this.unidadesDefault = val;
        this.form.get('unidadeSelect').setValue('');
      });
  }

  ajustarUnidade(unidades: any[]): string {
    if (!unidades || unidades.length == 0) return ' - ';
    let text = '';
    unidades.forEach(elem => text += `${elem}, `)
    return text.substring(0, text.length - 2);
  }

  ajustarStatusDocumento(statusDocumento: number[]): { value: string, valid: boolean } {
    return statusDocumento?.length > 0 ? { value: 'Doc pendente', valid: false } : { value: 'Doc ok', valid: true };
  }

  ajustaFeriasVencida(feriasVencida: any) {
    if (!feriasVencida) return { value: '-', valid: false };
    const dias = feriasVencida.dias ? feriasVencida.dias : '0';
    const meses = feriasVencida.meses ? feriasVencida.meses : '0';
    const anos = feriasVencida.anos ? feriasVencida.anos : '0';
    return { value: `Anos ${anos} - Meses ${meses} - Dias ${dias}`, valid: (anos != '0') };
  }

  ajustarRegime(regimeContratacao): string {
    if (!regimeContratacao || regimeContratacao.length == 0) return null;
    let text = '';
    if (regimeContratacao === 1) text = 'CLT Seg a Sex';
    if (regimeContratacao === 2) text = 'Estágio Seg a Sex';
    if (regimeContratacao === 3) text = 'Professor Autônomo';
    if (regimeContratacao === 4) text = 'Professor CLT';
    if (regimeContratacao === 5) text = 'Profissional Autônomo';
    if (regimeContratacao === 6) text = 'CLT Seg a Sab';
    if (regimeContratacao === 7) text = 'Estágio Seg a Sab';
    if (regimeContratacao === 8) text = 'Autônomo Pré CLT Seg a Sex';
    if (regimeContratacao === 9) text = 'Autônomo Pré CLT Seg a Sab';
    if (regimeContratacao === 10) text = 'Autônomo Pré Estágio Seg a Sex';
    if (regimeContratacao === 11) text = 'Autônomo Pré Estágio Seg a Sab';
    return text;
  }

  _filterUnidades(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`rh/cadastro-funcionario/adicionar/${id}`);
  }

  openDetlhamento(id): void {
    const dialogRef = this.dialog.open(FuncionarioDetalhamentoComponent, {
      width: '90vw',
      data: { id },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  openAnexo(id: string = '0', nome: string = '', documentosPendentes: string[]): void {
    const dialogRef = this.dialog.open(FuncionarioAnexoComponent, {
      width: '90vw',
      data: { id, nome, documentosPendentes },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll();
    });
  }

  mudarStatus(id: string = '0'): void {
    this.funcionarioService.desativarAtivar(id).subscribe(val => this.getAll());
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.funcionarioService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
