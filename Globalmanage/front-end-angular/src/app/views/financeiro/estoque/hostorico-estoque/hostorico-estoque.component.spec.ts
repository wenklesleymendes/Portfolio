import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HostoricoEstoqueComponent } from './hostorico-estoque.component';

describe('HostoricoEstoqueComponent', () => {
  let component: HostoricoEstoqueComponent;
  let fixture: ComponentFixture<HostoricoEstoqueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HostoricoEstoqueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HostoricoEstoqueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
