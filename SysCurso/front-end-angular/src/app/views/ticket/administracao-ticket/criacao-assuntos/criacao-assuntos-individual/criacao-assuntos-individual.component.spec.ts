import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriacaoAssuntosIndividualComponent } from './criacao-assuntos-individual.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MaterialModule } from 'src/app/material.module';

describe('CriacaoAssuntosIndividualComponent', () => {
  let component: CriacaoAssuntosIndividualComponent;
  let fixture: ComponentFixture<CriacaoAssuntosIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        FormsModule,
        BrowserAnimationsModule,
        HttpClientTestingModule,
        MaterialModule
      ],
      declarations: [ CriacaoAssuntosIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriacaoAssuntosIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
