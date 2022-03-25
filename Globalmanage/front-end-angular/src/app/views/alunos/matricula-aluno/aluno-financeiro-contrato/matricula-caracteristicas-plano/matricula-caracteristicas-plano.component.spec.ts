import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaCaracteristicasPlanoComponent } from './matricula-caracteristicas-plano.component';

describe('MatriculaCaracteristicasPlanoComponent', () => {
  let component: MatriculaCaracteristicasPlanoComponent;
  let fixture: ComponentFixture<MatriculaCaracteristicasPlanoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaCaracteristicasPlanoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaCaracteristicasPlanoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
