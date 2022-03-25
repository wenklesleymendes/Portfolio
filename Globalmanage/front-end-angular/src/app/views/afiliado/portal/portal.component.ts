import { Component, OnInit } from '@angular/core';
import { Navigation, Router } from '@angular/router';

@Component({
    selector: 'app-portal',
    templateUrl: './portal.component.html',
    styleUrls: ['./portal.component.scss']
})

export class PortalComponent implements OnInit {

    constructor(
    private router: Router,
    ) {
    }

    ngOnInit(): void {
    }
    
    goToHome(): void {
        this.redirectTo('/afiliado/home');
    }

    goToMeusDados(): void {
        this.redirectTo('/afiliado/meus-dados');
    }

    goToMinhaLoja(): void {
        this.redirectTo('/afiliado/minha-loja');
    }

    goToDinheiroBolso(): void {
        this.redirectTo('/afiliado/dinheiro-no-bolso');
    }

    redirectTo(uri:string){
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
        this.router.navigate([uri]));
    }
}