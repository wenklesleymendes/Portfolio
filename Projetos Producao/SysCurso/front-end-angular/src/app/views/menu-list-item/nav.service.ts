import { Injectable } from '@angular/core';
import { Event, NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class NavService {
  menuList: any;
  currentUrl = new BehaviorSubject<string>('');

  constructor(private router: Router) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        this.currentUrl.next(event.urlAfterRedirects);
      }
    });
  }

  closeNav() {
    this.menuList.close();
  }

  openNav() {
    this.menuList.open();
  }
}
