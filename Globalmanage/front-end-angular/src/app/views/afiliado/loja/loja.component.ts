import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TelMask, CelMask, CPFMask, CepMask } from 'src/app/utils/mask/mask';
import { NavigationEnd, NavigationStart, Router } from "@angular/router";
import { AuthService } from 'src/app/security/auth.service';

@Component({
    selector: 'app-loja',
    templateUrl: './loja.component.html',
    styleUrls: ['./loja.component.scss']
})

export class LojaComponent implements OnInit {
    form: FormGroup;
    maskTelefoneFixoPrincipal = TelMask;
    
    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
    ) {}

    ngOnInit(): void {
        
        this.buildForm();

        // Promise.all([

        // ])
        // .then(() => {
        //   this.isLoadingResults = false;
        //   this.loadData();
        // })
        // .catch(() => this.isLoadingResults = false);
    }

    ngAfterViewChecked(): void {
        
    }

    buildForm(): void {
        this.form = this.fb.group({
            nomecompleto: [null, [Validators.required]],
            email: [null, [Validators.required]],
            telefone: [null, [Validators.required]],
            cursos: [null, [Validators.required]],
        })

        // this.form.get('contato').get('telefoneFixo').valueChanges
        //     .subscribe(val => this.maskTelefoneFixoPrincipal = (val && val.length > 10) ? CelMask : TelMask);
    }

    voltarTopo(): void {
        window.scrollTo(0, 0);
    }

    logout(): void {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}