import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatriculaDetalheCampanhaComponent } from './matricula-detalhe-campanha.component';

describe('MatriculaDetalheCampanhaComponent', () => {
  let component: MatriculaDetalheCampanhaComponent;
  let fixture: ComponentFixture<MatriculaDetalheCampanhaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatriculaDetalheCampanhaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatriculaDetalheCampanhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
