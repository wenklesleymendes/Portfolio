import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroUsuarioIndividualComponent } from './cadastro-usuario-individual.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MaterialModule } from 'src/app/material.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';

describe('CadastroUsuarioIndividualComponent', () => {
  let component: CadastroUsuarioIndividualComponent;
  let fixture: ComponentFixture<CadastroUsuarioIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        FormsModule,
        BrowserAnimationsModule,
        HttpClientTestingModule,
        MaterialModule,
        PipeModule
      ],
      declarations: [ CadastroUsuarioIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroUsuarioIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
