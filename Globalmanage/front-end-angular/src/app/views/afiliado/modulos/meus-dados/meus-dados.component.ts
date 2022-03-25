import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { AnimationsService } from "src/app/services/animations.service";
import { FormService } from "src/app/services/form.service";
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { debounceTime, distinctUntilChanged, pairwise } from 'rxjs/operators';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { LocationService } from "src/app/services/location.service";
import { Location } from '@angular/common';
import { Estados } from "src/app/utils/variables/locations";

@Component({
    selector: 'app-meus-dados',
    templateUrl: './meus-dados.component.html',
    styleUrls: ['./meus-dados.component.scss']
})

export class MeusDadosComponent implements OnInit {

    form: FormGroup;
    id = 0;
    cpfMask = CPFMask;
    maskTelefoneFixoPrincipal = TelMask;
    maskCep = CepMask;
    alunoJaCadastrado: boolean = false;
    estados: string[] = Estados;
    today: Date = new Date();

    constructor(
        private location: Location,
        private fb: FormBuilder,
        private animationsService: AnimationsService,
        private router: Router,
        private routerActive: ActivatedRoute,
        private formService: FormService,
        private alunoService: AlunoService,
        private locationService: LocationService,
    ) {
        // Get id
        const id = this.routerActive.snapshot.paramMap.get('id');
        this.id = id ? parseInt(id) : 0;
    }

    ngOnInit(): void {
        this.buildForm();
    }

    buildForm(): void {
        this.form = this.fb.group({
            id: [0],
            nome: [null, [Validators.required]],
            email: [null, [Validators.required]],
            cpf: [null, [Validators.required, validarCPF]],
            telefoneFixo: [null],
            sexo: [null, [Validators.required]],
            dataNascimento: [null, [Validators.required]],
            pix: this.fb.group({
                id: [0],
                tipoChavePix: [null, [Validators.required]],
                chavePix: [null, [Validators.required]],
            }),
            endereco: this.fb.group({
              id: [0],
              cep: [null, [Validators.required]],
              rua: [null, [Validators.required]],
              numero: [null, [Validators.required]],
              complemento: [null],
              bairro: [null, [Validators.required]],
              cidade: [null, [Validators.required]],
              estado: [null, [Validators.required]]
            }),
        })
        
        // Cep
        this.form.get('endereco').get('cep').valueChanges
        .pipe(debounceTime(500))
        .subscribe(val => this.getLocation(val));

        // Change mask of all contact numbers
        this.form.get('telefoneFixo').valueChanges
            .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);

        this.form.get('cpf').valueChanges.pipe(debounceTime(250), distinctUntilChanged(),pairwise()).subscribe(([oldValue, cpf]) => {
            if(this.form.get('cpf').hasError('cpfInvalido')) return;
            this.alunoJaCadastrado = false;
            this.alunoService.getPorCpf(cpf).subscribe(res => {
                if(res?.id) {
        
                const { unidade, nacionalidade, naturalidade } = res;
        
                const patch: any = {};
                for(let key in res) {
                    if (res[key]) patch[key] = res[key];
                }
        
                const token = JSON.parse(window.localStorage.getItem('accessToken'));
                const unidadeUsuarioLogado = token?.user?.unidade;
        
                this.form.patchValue(patch);
        
                if(!token?.user.perfilUsuario.verTodasUnidades)
                {
                    this.form.get('unidadeSelect').setValue(unidadeUsuarioLogado?.nome ? unidadeUsuarioLogado.nome : null);
                    this.form.get('unidadeId').setValue(unidadeUsuarioLogado?.id ? unidadeUsuarioLogado.id : null);
                }
        
                this.form.get('nacionalidade').setValue(nacionalidade?.descricao ? nacionalidade.descricao : null);
                this.form.get('naturalidade').setValue(naturalidade?.descricao ? naturalidade.descricao : null);
                this.alunoJaCadastrado = true;
        
                }
            })
        })
        
        if(this.id === 0) this.form.get('cpf').setValue(null);
    }

    getLocation(cep: string): void {
        this.locationService.getLocation(cep).subscribe(val => {
          const { bairro, localidade, logradouro, uf } = val;
    
          if (bairro) this.form.get('endereco').get('bairro').setValue(bairro);
          if (logradouro) this.form.get('endereco').get('rua').setValue(logradouro);
          if (localidade) this.form.get('endereco').get('cidade').setValue(localidade);
          if (uf) this.form.get('endereco').get('estado').setValue(uf);
        })
    }

    voltar(): void {
        this.location.back();
    }

    goToHome(): void {
        this.redirectTo('/afiliado/home');
    }

    redirectTo(uri:string){
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
        this.router.navigate([uri]));
    }

}