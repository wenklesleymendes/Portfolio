import { Component, OnInit } from '@angular/core';
import { Router, Navigation } from '@angular/router';

@Component({
  selector: 'app-cursos-turmas',
  templateUrl: './cursos-turmas.component.html',
  styleUrls: ['./cursos-turmas.component.scss']
})
export class CursosTurmasComponent implements OnInit {
  selectedIndex: number = 0;

  constructor(private router: Router) {
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.selectedIndex = state.tabPage ? state.tabPage : 0;
    }
  }

  ngOnInit(): void {

  }
}
