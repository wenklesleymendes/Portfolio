import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'title-line',
  template: `
  <div class="flex flex-row">
      <h4 *ngIf="title">{{ title }}</h4>
      <div class="flex flex-fill">
          <mat-divider></mat-divider>
      </div>
  </div>
  `,
  styles: [
    `
    .flex {
      display: flex;

    }
    .flex-row {
      display: flex !important;
      flex-direction: row !important;
      flex-wrap: wrap !important;
      width: 100% !important;
    }
    .flex-fill {
      flex: 1 1 auto !important;
    }
    .mat-divider {
      position: relative !important;
      align-self: center;
      margin: 0 2rem;
      border-top-width: 3px;
    }
    `
  ]
})
export class TitleLineComponent implements OnInit {
  @Input() title;

  constructor() { }

  ngOnInit(): void {
  }

}
