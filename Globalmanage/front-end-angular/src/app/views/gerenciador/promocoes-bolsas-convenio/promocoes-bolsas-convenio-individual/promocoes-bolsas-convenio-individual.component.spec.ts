import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PromocoesBolsasConvenioIndividualComponent } from './promocoes-bolsas-convenio-individual.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PromocoesBolsasConvenioIndividualComponent', () => {
  let component: PromocoesBolsasConvenioIndividualComponent;
  let fixture: ComponentFixture<PromocoesBolsasConvenioIndividualComponent>;

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
      declarations: [ PromocoesBolsasConvenioIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PromocoesBolsasConvenioIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
