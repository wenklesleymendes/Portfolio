import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

@Component({
    selector: 'app-dinheiro-no-bolso',
    templateUrl: './dinheiro-no-bolso.component.html',
    styleUrls: ['./dinheiro-no-bolso.component.scss']
})

export class DinheiroNoBolsoComponent implements OnInit {

    form: FormGroup;
    today: Date = new Date();

    constructor(
        private fb: FormBuilder,
        private router: Router,
    ) {
    }

    ngOnInit(): void {
        this.buildForm();
    }

    buildForm(): void {
        this.form = this.fb.group({
            unidade: [null, [Validators.required]],
            nomeUsuario: [null, [Validators.required]],
            dataInicio: [null, [Validators.required]],
            dataFinal: [null, [Validators.required]],
        })
    }

    goToHome(): void {
        this.redirectTo('/afiliado/home');
    }

    redirectTo(uri:string){
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
        this.router.navigate([uri]));
    }

}