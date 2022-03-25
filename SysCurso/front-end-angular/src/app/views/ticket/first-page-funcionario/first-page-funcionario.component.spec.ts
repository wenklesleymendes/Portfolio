import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstPageFuncionarioComponent } from './first-page-funcionario.component';

describe('FirstPageFuncionarioComponent', () => {
  let component: FirstPageFuncionarioComponent;
  let fixture: ComponentFixture<FirstPageFuncionarioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FirstPageFuncionarioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstPageFuncionarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
