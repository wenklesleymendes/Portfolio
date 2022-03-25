import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhePendenteComponent } from './detalhe-pendente.component';

describe('DetalhePendenteComponent', () => {
  let component: DetalhePendenteComponent;
  let fixture: ComponentFixture<DetalhePendenteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalhePendenteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalhePendenteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
