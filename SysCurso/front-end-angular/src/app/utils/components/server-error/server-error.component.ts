import { Component } from '@angular/core';

@Component({
  selector: 'app-server-error',
  template: `
  <div class="error">
    <span>Servidor em manutenção</span>
  </div>
  `,
  styles: [
    `.error {
      padding: 15px;
      font-size: 18pt;
      color: #111;
      border-radius: 2rem;
      text-align: center;
      background-color: rgba(255,0,0,0.5);
    }
    `
  ]
})
export class ServerErrorComponent {

  constructor() { }

}
