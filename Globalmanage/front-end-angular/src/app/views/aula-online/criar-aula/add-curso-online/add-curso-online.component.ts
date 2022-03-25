import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { FormService } from 'src/app/services/form.service';
import { AulaOnlineService } from 'src/app/services/aula-online/aula-online.service';

@Component({
  selector: 'app-add-curso-online',
  templateUrl: './add-curso-online.component.html',
  styleUrls: ['./add-curso-online.component.scss']
})
export class AddCursoOnlineComponent implements OnInit, OnDestroy {
  id: number = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  filterCursos: Observable<any[]>;
  curso: FormArray;
  cursosDefault: any[] = null;
  refresh: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<AddCursoOnlineComponent>,
    private fb: FormBuilder,
    private cursoService : CursoService,
    private formService: FormService,
    private aulaOnlineService: AulaOnlineService,
    private animationsService: AnimationsService,
    private deleteService: DeleteService,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    if(this.data?.id) this.id = this.data.id;
    this.isLoadingResults = true;
    this.buildForm();

    Promise.all([
      this.getCursos()
    ])
    .then(() => this.loadData())
    .catch(() => this.isLoadingResults = true);
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.refresh);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [this.id],
      nomeAulaOnline: [null],
      cursoSelect: [null],
      curso: this.fb.array([])
    });

    this.curso = this.form.get('curso') as FormArray;

    this.filterCursos = this.form.get('cursoSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterCursos(elem) : this.cursosDefault.slice())
      );
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.aulaOnlineService.getPorId(this.id)
      .subscribe(val => {
        if (!val || val['status'] === 'error') return this.error = true;

        const { curso } = val;

        const patch: any = {};
        for(let key in val) {
          if (val[key]) patch[key] = val[key];
        }
        this.form.patchValue(patch);

        this.curso.clear();

        if(curso?.length > 0) {
          curso.forEach(elem => {
            const { id, cursoId, nomeCurso, aulaOnlineId } = elem;
            this.curso.push(
              this.fb.group({
                id: [id],
                cursoId: [cursoId],
                nomeCurso: [nomeCurso],
                aulaOnlineId: [aulaOnlineId]
              })
            );
          });
        }
      })
    }
    this.isLoadingResults = false;
  }

  getCursos(): Promise<any> {
    return new Promise((res, rej) => {
      this.cursoService.getCursoComMateria()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else {
            this.cursosDefault = val
            res();
          };  
          this.form.get('cursoSelect').setValue(null);
        });
    });
  }

  loadCursoOptions(id: number): any[] {
    const curso = this.cursosDefault.find(elem => elem.id == id);
    if (!curso) return null;
    if (curso && curso.materia) return curso.materia;
    return null;
  }

  _filterCursos(value: string): any[] {
    const filterValue = value.toLowerCase();
    return this.cursosDefault.filter(elem => elem.descricao.toLowerCase().indexOf(filterValue) === 0);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    // Setting 'curso'
    let cursoForm = this.form.get('curso').value;
    let cursoFormTemp = [];
    cursoForm.forEach(elem => cursoFormTemp.push({ id: elem }));

    const formValue = this.form.value;

    // Removing useless data
    delete formValue.cursoSelect;

    // Unifying data
    const data = {...formValue };
    // Make request
    this.aulaOnlineService.cadastrar(data).subscribe(val => {
      this.refresh = true;
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
      }
    })
  }

  addCurso(): void {
    const { cursoSelect } = this.form.value;
    if (!cursoSelect) return;
    // Check if 'curso' exists
    const curso = this.cursosDefault?.length >= 0 ?  this.cursosDefault.find(elem => elem.descricao == cursoSelect) : null;
    if (!curso) return;
    // Check if isn't alredy selected
    const cursoSelected = this.form.get('curso').value as any[];
    const alredySelected = cursoSelected.find(elem => elem.nomeCurso == cursoSelect);
    if (alredySelected) {
      this.animationsService.showErrorSnackBar('Curso já selecionado');
      return;
    }
    this.curso.push(
      this.fb.group({
        id: [0],
        cursoId: [curso.id],
        nomeCurso: [curso.descricao],
        aulaOnlineId: [this.id]
      })
    );

    this.form.get('cursoSelect').setValue(null);
  }

  removeDoFormArray(controls: any, index: number) {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      controls as FormArray;
      controls.removeAt(index);
    });
  }
}
