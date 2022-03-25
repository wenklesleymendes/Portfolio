import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarAulaComponent } from './criar-aula.component';

describe('CriarAulaComponent', () => {
  let component: CriarAulaComponent;
  let fixture: ComponentFixture<CriarAulaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CriarAulaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriarAulaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
