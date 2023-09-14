import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EstoqueItemComponent } from './estoque-item.component';

describe('EstoqueItemComponent', () => {
  let component: EstoqueItemComponent;
  let fixture: ComponentFixture<EstoqueItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EstoqueItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EstoqueItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
