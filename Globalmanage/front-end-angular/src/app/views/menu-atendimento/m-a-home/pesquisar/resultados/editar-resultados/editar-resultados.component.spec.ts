import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarResultadosComponent } from './editar-resultados.component';

describe('EditarResultadosComponent', () => {
  let component: EditarResultadosComponent;
  let fixture: ComponentFixture<EditarResultadosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarResultadosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarResultadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
