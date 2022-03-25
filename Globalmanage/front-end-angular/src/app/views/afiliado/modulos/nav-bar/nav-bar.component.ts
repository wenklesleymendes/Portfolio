import { Component, HostBinding, Input, OnInit } from "@angular/core";
import { NavItem } from "../../../../interfaces/nav-item.interface";
import { NavigationStart, Router } from "@angular/router";
import { NavService } from "./nav.service";
import {
  animate,
  state,
  style,
  transition,
  trigger
} from "@angular/animations";

@Component({
  selector: "app-nav-bar",
  templateUrl: "./nav-bar.component.html",
  styleUrls: ["./nav-bar.component.scss"],
  animations: [
    trigger("indicatorRotate", [
      state("collapsed", style({ transform: "rotate(0deg)" })),
      state("expanded", style({ transform: "rotate(180deg)" })),
      transition(
        "expanded <=> collapsed",
        animate("225ms cubic-bezier(0.4,0.0,0.2,1)")
      )
    ])
  ]
})
export class NavBarComponent implements OnInit {
  expanded: boolean = false;
  @HostBinding("attr.aria-expanded") ariaExpanded = this.expanded;
  @Input() item: NavItem;
  @Input() depth: number;

  constructor(public navService: NavService, public router: Router) {
    if (this.depth === undefined) {
      this.depth = 0;
    }
  }

  ngOnInit() {
    this.navService.currentUrl.subscribe((url: string) => {
      // this.expanded = false;
      // this.ariaExpanded = false;
    });
  }

  onItemSelected(item: NavItem) {

    var cursoId = Number(window.localStorage.getItem('cursoIdLocalStorage'));
      if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
      {
        if(item.displayName == "YouTube")
        {
          window.open("https://www.youtube.com/channel/UCfgpZT7ibTWvyGNTnWTheBw"); //canal do youtube da nacional
        }
      }
      else
      {
        if(item.displayName == "YouTube")
        {
          window.open("https://www.youtube.com/c/SupletivoPreparat%C3%B3rio"); //canal do youtube da supletivo
        }
      }

      if(cursoId != 1 && cursoId != 2 && cursoId !=3 && cursoId != 4)
      {
        if(item.displayName == "Telegram")
        {
          window.open("https://t.me/cursosnacionaltec"); //telegram da nacional
        }
      }
      else
      {
        if(item.displayName == "Telegram")
        {
          window.open("https://t.me/supletivopreparatorioejaencceja"); //telegram do supletivo
        }
      }

    if (!item.children || !item.children.length) {
      this.router.navigate([item.route]);
      this.navService.closeNav();
      if((<HTMLInputElement>document.getElementById('hamburgerMenu')) != null)
      {
        (<HTMLInputElement>document.getElementById('hamburgerMenu')).classList.remove("change");
      }
    }
    if (item.children && item.children.length) {
      this.expanded = !this.expanded;
    }
  }
}
