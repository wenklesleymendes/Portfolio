import { Component, OnInit, ViewChild, ChangeDetectorRef } from "@angular/core";
import { AuthService } from 'src/app/security/auth.service';
import { NavigationEnd, NavigationStart, Router } from "@angular/router";
import { MatSidenav } from '@angular/material/sidenav';
import { AnimationsService } from 'src/app/services/animations.service';

@Component({
    selector: 'app-afiliado',
    templateUrl: './afiliado.component.html',
    styleUrls: ['./afiliado.component.scss']
})

export class AfiliadoComponent implements OnInit {
    @ViewChild('menuList', { static: true }) menuList: MatSidenav;
    
    menuIsChanging: boolean = false;
    sidebar: HTMLElement;
    nav: boolean = false;
    showBar: boolean = false;
    showFiller = false;
    sidebarMode: string = 'over';

    constructor(
        private router: Router,
        private authService: AuthService,
        private animationsService: AnimationsService,
        private _changeDetectionRef : ChangeDetectorRef,
    ) {
    }

    ngOnInit(): void {

        this.sidebar = document.getElementById("hamburgerMenu");
        this.animationsService.getProgressBar().subscribe(val => {
        this.showBar = val;
        this._changeDetectionRef.detectChanges();

    });
        this.menuList._animationStarted.subscribe(_ => this.menuIsChanging = true);
        this.menuList._animationEnd.subscribe(_ => this.menuIsChanging = false);
    }

    logout(): void {
        this.authService.logout();
        this.router.navigate(['/login']);
      }

    menuClick(): void {
        if(!this.menuIsChanging) this.menuList.toggle();
    }

    openNav() {
        /* Troca a animação do icone no menu */
        this.sidebar.classList.add("change")
        /* Marca que o menu está aberto */
        this.nav = true;
      }
    
      closeNav() {
        /* Troca a animação do icone no menu */
        this.sidebar.classList.remove("change")
        /* Marca que o menu está fechado */
        this.nav = false;
      }

    toggleNav() {
        var menuHamburguer = (<HTMLInputElement>document.getElementById('hamburgerMenu'));
        if(menuHamburguer != null)
        {
          menuHamburguer.classList.contains("change") ? this.closeNav(): this.openNav();
        }
    }
}