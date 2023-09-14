import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DeleteService } from 'src/app/services/delete.service';
import { ActivatedRoute } from '@angular/router';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-curso-individual',
  templateUrl: './curso-individual.component.html',
  styleUrls: ['./curso-individual.component.scss']
})
export class CursoIndividualComponent implements OnInit {

  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['descricao', 'options'];
  dataSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private deleteService: DeleteService,
    private cursoService: CursoService,
    private animationsService: AnimationsService,
    private routerActive: ActivatedRoute,
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
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      descricao: [null, Validators.required],
      duracao: [null, Validators.required],
      matricula: [null],
      nacionatalTec: [null, Validators.required],
    });
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.cursoService.getCursoPorId(this.id).subscribe(val => {
        this.form.patchValue(val)

        const { materia } = val

        this.dataSource.data = materia ? materia : [];
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
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue = this.form.value;
    const data = { ...formValue };
    data.duracao = Number(data.duracao);
    // Make request
    this.cursoService.cadastrarCurso(data).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);

        const matriculas = this.dataSource.data;
        matriculas.forEach(elem => {
          if (elem['id'] === 0) {
            elem['cursoId'] = this.id;
            this.cursoService.cadastrarMateria(elem).subscribe();
          }
        })
      }
    })
  }

  addMatricula(): void {
    const value = this.form.get('matricula').value;
    let data = this.dataSource.data;
    data.push({ nomeMateria: value, id: 0, cursoId: this.id });
    this.dataSource.data = data;
    this.form.get('matricula').reset();
  }

  removeMatricula(index: number, id: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      if (id != 0) {
        this.cursoService.deletarMateria(id).subscribe();
      }
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;
    });
  }

}
