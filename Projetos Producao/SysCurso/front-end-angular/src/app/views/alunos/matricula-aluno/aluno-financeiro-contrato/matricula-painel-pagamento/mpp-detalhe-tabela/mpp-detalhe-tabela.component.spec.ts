import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppDetalheTabelaComponent } from './mpp-detalhe-tabela.component';

describe('MppDetalheTabelaComponent', () => {
  let component: MppDetalheTabelaComponent;
  let fixture: ComponentFixture<MppDetalheTabelaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppDetalheTabelaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppDetalheTabelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
