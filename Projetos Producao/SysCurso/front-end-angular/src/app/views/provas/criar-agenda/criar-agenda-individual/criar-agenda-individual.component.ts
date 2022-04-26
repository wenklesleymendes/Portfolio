import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, Validators, FormArray, AbstractControl } from '@angular/forms';
import { Router, Navigation, ActivatedRoute } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { startWith, map } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { DeleteService } from 'src/app/services/delete.service';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { ProvasService } from 'src/app/services/provas/provas.service';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { ColegioAutorizadoService } from 'src/app/services/provas/colegio-autorizado.service';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-criar-agenda-individual',
  templateUrl: './criar-agenda-individual.component.html',
  styleUrls: ['./criar-agenda-individual.component.scss']
})
export class CriarAgendaIndividualComponent implements OnInit {
  id = 0;
  form: FormGroup;
  unidadeParticipanteProva: FormArray;
  error: boolean = false;
  isLoadingResults: boolean = false;  
  filterUnidades: Observable<any[]>;
  unidadesDefault: any[] = null;
  filterColegioAutorizado: Observable<any[]>;
  colegioAutorizadoDefault: any[] = null;
  displayedColumns: string[] = ['unidade', 'horarioSaida', 'localSaida', 'options'];
  dataSource = new BehaviorSubject<FormArray | AbstractControl>(null);
  hourMinute = HourMinuteMask;
  state: any;
  replicar: boolean = false;
  cursos: any = false;
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private provasService: ProvasService,
    private unidadeService: UnidadeService,
    private cursoService: CursoService,
    private deleteService: DeleteService,
    private colegioAutorizadoService: ColegioAutorizadoService,
    private routerActive: ActivatedRoute,
    private router: Router,
    private formService: FormService
  ) {
    // Get id
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
    // Get State
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      this.state = currentNavigation.extras.state;
      this.replicar = this.state.replicar;
    }
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    if(this.id !== 0) this.isLoadingResults = true;

    this.buildForm();
    Promise.all([
      this.getunidades(),
      this.getColegioAutorizado(),
      this.getCursos()
    ])
    .then(() => this.loadData())
    .catch(() => this.isLoadingResults = false)
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      unidadeId: [null],
      unidadeSelect: [null],
      colegioAutorizadoId: [null, [Validators.required]],
      colegioAutorizadoSelect: [null, [Validators.required]],
      inicioInscricao: [null, [Validators.required]],
      terminoInscricao: [null, [Validators.required]],
      dataProva: [null, [Validators.required]],
      enderecoColegioAutorizado: [null],
      quantidadeVagas: [null, [Validators.required]],
      agendaCurso: [null, [Validators.required]],

      unidadeParticipanteProva: this.fb.array([])
    });

    this.unidadeParticipanteProva = this.form.get('unidadeParticipanteProva') as FormArray;

    this.form.setValidators([
      CompareInitialEndDate('inicioInscricao', 'terminoInscricao'),
      CompareInitialEndDate('terminoInscricao', 'dataProva')
    ]);

    this.filterUnidades = this.form.get('unidadeSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
      );

    this.form.get('unidadeSelect').valueChanges.subscribe(val => {
      const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
      if (unidadeId && unidadeId.id) {
        this.form.get('unidadeId').setValue(unidadeId.id);
      }
      else {
        this.form.get('unidadeId').setValue(null);
      }
    });

    this.filterColegioAutorizado = this.form.get('colegioAutorizadoSelect').valueChanges
      .pipe(
        startWith(''),
        map(elem => elem ? this._filterColegioAutorizado(elem) : this.colegioAutorizadoDefault.slice())
      );

    this.form.get('colegioAutorizadoSelect').valueChanges.subscribe(val => {
      const colegioId = this.colegioAutorizadoDefault?.length >= 0 ? this.colegioAutorizadoDefault.find(elem => elem.nomeColegioAutorizado == val) : null;
      if (colegioId && colegioId.id) {
        this.form.get('colegioAutorizadoId').setValue(colegioId.id);
      }
      else {
        this.form.get('colegioAutorizadoId').setValue(null);
      }
    });


    if (!this.replicar) this.form.get('id').setValue(this.id);
  }

  loadData(): void {
    if (this.id != 0) {
      this.isLoadingResults = true;
      this.provasService.getPorId(this.id)
      .subscribe(val => {
        if (!val || val['status'] === 'error') return this.error = true;

        const { unidadeParticipanteProva, colegioAutorizado, agendaCurso } = val;

        const patch: any = {};
        for(let key in val) {
          if (val[key]) patch[key] = val[key];
        }
        this.form.patchValue(patch);

        if(agendaCurso) {
          const cursosIds: number[] = []
          agendaCurso.forEach(elem => cursosIds.push(elem?.cursoId));
          this.form.get('agendaCurso').setValue(cursosIds);
        }

        this.unidadeParticipanteProva.clear();
        if(unidadeParticipanteProva?.length > 0) {
          unidadeParticipanteProva.forEach(elem => {
            const { id, unidadeId, horaSaida, localSaida } = elem;
            this.unidadeParticipanteProva.push(
              this.fb.group({
                id: [id],
                unidadeId: [unidadeId],
                horaSaida: [horaSaida, [Validators.required]],
                localSaida: [localSaida, [Validators.required]],
              })
            );
          });

          if(colegioAutorizado?.nomeColegioAutorizado) this.form.get('colegioAutorizadoSelect').setValue(colegioAutorizado.nomeColegioAutorizado);

          this.dataSource.next(null);
          this.dataSource.next(this.unidadeParticipanteProva);
        }

        this.isLoadingResults = false;

        // Set id
        if (this.replicar) this.form.get('id').setValue(0);
      })
    }
  }

  getunidades(): Promise<any> {
    return new Promise((res, rej) => {
      this.unidadeService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.unidadesDefault = val;
          this.form.get('unidadeSelect').setValue('');
          res();
        });
    });
  }

  getCursos(): Promise<any> {
    return new Promise((res, rej) => {
      this.cursoService.getCursos()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.cursos = val;
          res();
        });
    });
  }

  getColegioAutorizado(): Promise<any> {
    return new Promise((res, rej) => {
      this.colegioAutorizadoService.getAll()
        .subscribe(val => {
          if (val['status'] === 'error') {
            this.error = true;
            rej();
          }
          else this.colegioAutorizadoDefault = val;
          this.form.get('colegioAutorizadoSelect').setValue('');
          res();
        });
    });
  }

  ajustarLabelUnidade(unidadeParticipanteProva: FormArray | AbstractControl, index: number): string {
    const FormArray = unidadeParticipanteProva as FormArray
    const formValue = FormArray.at(index).value;
    const unidadeId = formValue.unidadeId

    const unidade = this.unidadesDefault.find(elem => elem.id === unidadeId);

    return unidade?.nome ? unidade.nome : '';
  }

  _filterUnidades(value: string): any[] {
    if(!value) return;
    const filterValue = value.toLowerCase();
    return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  _filterColegioAutorizado(value: string): any[] {
    if(!value) return;
    const filterValue = value.toLowerCase();
    return this.colegioAutorizadoDefault.filter(elem => elem.nomeColegioAutorizado.toLowerCase().indexOf(filterValue) === 0);
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
    delete formValue.unidadeId;
    delete formValue.unidadeSelect;

    // Validate 'unidadeParticipanteProva'
    if(this.replicar) {
      formValue.unidadeParticipanteProva = formValue.unidadeParticipanteProva.map(elem => {
        elem.id = 0;
        return elem;
      });
    }

    // Validate 'cursos'
    formValue.agendaCurso = formValue.agendaCurso.map(elem => { 
      return {
        cursoId: elem,
        agendaProvaId: this.id
      }
    })

    // Make request
    this.provasService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);

        this.loadData();
      }
    })
  }

  gerar(): void {
    const { unidadeId } = this.form.value;

    if(!unidadeId) {
      this.animationsService.showErrorSnackBar('Preencha a unidade');
      return;
    }

    const selected: any[] = this.unidadeParticipanteProva.value;
    const alredySeleted = selected.find(elem => elem.unidadeId === unidadeId);
    const unidade = this.unidadesDefault.find(elem => elem.id === unidadeId);

    if(alredySeleted) {
      this.animationsService.showErrorSnackBar('Unidade já selecionada');
      return;
    }

    this.unidadeParticipanteProva.push(
      this.fb.group({
        id: [0],
        unidadeId: [unidadeId],
        horaSaida: [null, [Validators.required]],
        localSaida: [`${unidade?.endereco?.rua}, ${unidade?.endereco?.numero}, ${unidade?.endereco?.cidade}, ${unidade?.endereco?.estado}, ${unidade?.endereco?.cep}`, [Validators.required]],
      })
    );

    this.dataSource.next(null);
    this.dataSource.next(this.unidadeParticipanteProva);

    this.form.get('unidadeId').setValue(null);
    this.form.get('unidadeSelect').setValue(null);
  }

  excluirParcela(index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.unidadeParticipanteProva.removeAt(index);
      this.dataSource.next(null);
      this.dataSource.next(this.unidadeParticipanteProva);

      // if(item?.id === 0) {
      // } else {
      //   this.deleteService.openDelete().then(res => {
      //     if (!res) return;
      //     this.contasPagarService.deletarParcela(item.id).subscribe(val => {
      //       if (!val) this.dataSource.next(unidadeParticipanteProva);
      //       else {
      //         this.unidadeParticipanteProva.at(index).get('dataVencimento').disable({emitEvent: false});
      //         this.unidadeParticipanteProva.at(index).get('valorParcela').disable({emitEvent: false});
      //         this.unidadeParticipanteProva.at(index).get('codigoBarras').disable({emitEvent: false});
      //         this.unidadeParticipanteProva.at(index).get('tipoPagamento').disable({emitEvent: false});
      //         this.unidadeParticipanteProva.at(index).get('lancamentoManual').disable({emitEvent: false});
      //         this.unidadeParticipanteProva.at(index).get('statusPagamento').setValue(3);
      //       }
      //     });
      //   });
      // }

    });
  }
}
