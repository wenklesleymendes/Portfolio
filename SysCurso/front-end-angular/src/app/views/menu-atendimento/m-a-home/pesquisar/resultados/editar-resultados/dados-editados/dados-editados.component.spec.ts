import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DadosEditadosComponent } from './dados-editados.component';

describe('DadosEditadosComponent', () => {
  let component: DadosEditadosComponent;
  let fixture: ComponentFixture<DadosEditadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DadosEditadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DadosEditadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
