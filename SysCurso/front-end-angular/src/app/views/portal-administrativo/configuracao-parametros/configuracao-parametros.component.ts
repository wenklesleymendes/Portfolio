import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-configuracao-parametros',
  templateUrl: './configuracao-parametros.component.html',
  styleUrls: ['./configuracao-parametros.component.scss']
})
export class ConfiguracaoParametrosComponent implements OnInit {
  form: FormGroup;
  id = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  cartoes: any[] = [
    { label: 'Cielo', value: '1' },
    { label: 'Rede', value: '2' },
    { label: 'VR', value: '3' },
  ];

  constructor(
    private fb: FormBuilder
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      cartao: ['1']
    });
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  salvarData(): void {
  }
} 
