import { Component, OnInit } from '@angular/core';
import { Estados } from 'src/app/utils/variables/locations';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { Router } from '@angular/router';
import { validarCPF } from 'src/app/utils/form-validation/cpf.validation';
import { AuthService } from 'src/app/security/auth.service';


@Component({
    selector: 'app-afiliacao',
    templateUrl: './afiliacao.component.html',
    styleUrls: ['./afiliacao.component.scss']
})

export class AfiliacaoComponent implements OnInit {
    form: FormGroup;
    estados: string[] = Estados;
    cpfMask = CPFMask;
    maskCelular = TelMask;
    maskCep = CepMask;

    constructor(
        private fb: FormBuilder,
        private formService: FormService,
        private authService: AuthService,
        private router: Router,
    ) {
    }

    ngOnInit(): void {
        this.buildForm();
    }

    buildForm(): void {
        this.form = this.fb.group({
            nome: [null, [Validators.required]],
            sobrenome: [null, [Validators.required]],
            email: [null, [Validators.required]],
            dataNascimento: [null, [Validators.required]],
            estado: [null, [Validators.required]],
            cidade: [null, [Validators.required]],
            telefone: [null, [Validators.required]],
            cpf: [null, [Validators.required, validarCPF]]
        })

        this.form.get('telefone').valueChanges
        .subscribe(val => this.maskCelular = (val && val.length > 10) ? CelMask : TelMask);
    }

    criarCadastro(): void {
        document.getElementById('cadastro').scrollIntoView({
            behavior: 'smooth'
        });
    }

    logout(): void {
        this.authService.logout();
        this.router.navigate(['/login']);
    }

    duvidasFrequentes(): void{
        document.getElementById('duvidas').scrollIntoView({
            behavior: 'smooth'
        });
    }
}