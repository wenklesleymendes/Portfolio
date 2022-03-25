import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { HourMinuteMask, CPFMask, TelMask, CelMask } from 'src/app/utils/mask/mask';
import { Observable } from 'rxjs';
import { UnidadeService } from 'src/app/services/gerenciador/unidade.service';
import { startWith, map } from 'rxjs/operators';
import { CursoService } from 'src/app/services/gerenciador/curso.service';
import { CompareInitialEndHour } from 'src/app/utils/form-validation/initial-end-hour.validation';
import { CompareInitialEndDate } from 'src/app/utils/form-validation/initial-end-dates.validation';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { AuthService } from 'src/app/security/auth.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-pesquisar',
    templateUrl: './pesquisar.component.html',
    styleUrls: ['./pesquisar.component.scss']
})

export class PesquisarComponent implements OnInit {
    form: FormGroup;
    hourMinute = HourMinuteMask;
    error: boolean = false;
    visualizarTodasUnidades: boolean;
    isLoadingResults: boolean = false;
    cpfMask: string = CPFMask;
    maskCelular = TelMask;
    cursos: any[] = null;
    filterUnidades: Observable<any[]>;
    unidadesDefault = null;
    salas: number[] = Array(20).fill('').map((x,i) => i+1);
    years: number[] = null;
    anos = Array(2).fill('').map((x,i) => (new Date().getUTCFullYear() + i).toString());
    startYear: Date = new Date('2020-01-01');
    maskTelefoneFixoPrincipal = TelMask;

  
    constructor(
        private fb: FormBuilder,
        private animationService: AnimationsService,
        private cursoService: CursoService,
        private unidadeService: UnidadeService,
        private authService: AuthService,
        private formService: FormService,
        private router: Router,

        @Inject(MAT_DIALOG_DATA) public data,
    ) { }
  
    // --------------------------------------------------------------------------
    //  LIFECYCLE HOOKS
    // --------------------------------------------------------------------------
    ngOnInit(): void {
        this.buildForm();
        // this.getunidades();
        // this.getCursos();
        // this.setYears();
        // this.setUnidadeDefault();
    }
  
    // --------------------------------------------------------------------------
    //  BUSSINES RULE
    // --------------------------------------------------------------------------
    buildForm(): void {
        this.form = this.fb.group({
            NomedoAluno: [null],
            DatadoAtendimento: [null],
            CanaldeAtendimento: [null],
            Celular: [null],
            TelefoneFixo: [null],
            StatusdoAluno: [null],
            PesquisarDataInicial: [null],
            PesquisarDataFinal: [null],
            NomedoAtendente: [null],
        });
  
        // this.form.setValidators([
        //     CompareInitialEndHour('horaInicio', 'horaTermino')
        // ]);
  
        // this.form.get('celular').valueChanges
        //     .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);

        // this.form.get('TelefoneFixo').valueChanges
        // .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);
  
        // this.filterUnidades = this.form.get('unidadeSelect').valueChanges
        // .pipe(
        //     startWith(''),
        //     map(elem => elem ? this._filterUnidades(elem) : this.unidadesDefault.slice())
        // );
    
        
    
        // this.form.get('unidadeSelect').valueChanges.subscribe(val => {
        //     const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
        //     if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
        //     else this.form.get('unidadeId').setValue(null);
        // })
      
    }
  
  
    setUnidadeDefault(): void {
        //const unidadeId = this.unidadesDefault?.length >= 0 ? this.unidadesDefault.find(elem => elem.nome == val) : null;
        //if (unidadeId && unidadeId.id) this.form.get('unidadeId').setValue(unidadeId.id);
        //else this.form.get('unidadeId').setValue(null);
        //this.form.get('unidadeId').setValue(unidadeId.id)
    }
  
    getunidades(): void {
        this.unidadeService.getAll()
            .subscribe(val => {
            if (val['status'] === 'error') {this.error = true; }
                else { 
        
                    this.unidadesDefault = val;
                    this.unidadesDefault?.length === 1 ? this.form.get('unidadeSelect').setValue(this.unidadesDefault[0].nome) : this.form.get('unidadeSelect').setValue('');
                }
            });
    }
  
    getCursos(): void {
        this.cursoService.getCursos().subscribe(val => {
            if (val['status'] === 'error') this.error = true;
                else this.cursos = val;
        })
    }
  
  
    setYears(): void {
        const thisYear: number = new Date().getUTCFullYear();
        const year0: number = this.startYear.getUTCFullYear();
    
        const diff: number = (thisYear + 1) - year0;
        this.years = Array(diff).fill(year0).map((x, i) => x + i);
    }
  
    _filterUnidades(value: string): any[] {
        const filterValue = value.toLowerCase();
        return this.unidadesDefault.filter(elem => elem.nome.toLowerCase().indexOf(filterValue) === 0);
    }
  
    // --------------------------------------------------------------------------
    //  BTN EVENTS
    // --------------------------------------------------------------------------
    pesquisar(): void {
        const formValue = this.form.value;
        delete formValue.unidadeSelect;
        console.log('FILTRO', formValue)
        console.log(JSON.stringify(formValue))
    
        if(this.form.value.dataInicioMatricula != null)
        formValue.dataInicioMatricula = new Date(this.form.value.dataInicioMatricula).toLocaleDateString();
    
        if(this.form.value.dataFimMatricula != null)
        formValue.dataFimMatricula = new Date(this.form.value.dataFimMatricula).toLocaleDateString();
    
        (<HTMLInputElement>document.getElementById('verificarFiltraUnidade')).value = "1";
    }

    goToHome(): void {
        this.router.navigate(['menu-atendimento/m-a-home']); //, { state: { matriculaId } }
    }

    goToNovoAtendimento(): void{
        this.router.navigate(['menu-atendimento/m-a-home/novo-atendimento']); //, { state: { matriculaId } }
    }

    goToPesquisar(): void{
        this.router.navigate(['menu-atendimento/m-a-home/pesquisar']); //, { state: { matriculaId } }
    }

    goToOutbound(): void{
        this.router.navigate(['menu-atendimento/m-a-home/outbound']); //, { state: { matriculaId } }
    }

    goToAgendados(): void{
        this.router.navigate(['menu-atendimento/m-a-home/agendados']); //, { state: { matriculaId } }
    }

    goToContatosPrioritarios(): void{
        this.router.navigate(['menu-atendimento/m-a-home/contatos-prioritarios']); //, { state: { matriculaId } }
    }

    goToResultados(): void {
        this.router.navigate(['menu-atendimento/m-a-home/pesquisar/resultados']); //, { state: { matriculaId } }
    }
}