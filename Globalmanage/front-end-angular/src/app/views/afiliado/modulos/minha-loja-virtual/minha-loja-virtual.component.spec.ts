import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MinhaLojaVirtualComponent } from './minha-loja-virtual.component';

describe('MinhaLojaVirtualComponent', () => {
  let component: MinhaLojaVirtualComponent;
  let fixture: ComponentFixture<MinhaLojaVirtualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MinhaLojaVirtualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MinhaLojaVirtualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});