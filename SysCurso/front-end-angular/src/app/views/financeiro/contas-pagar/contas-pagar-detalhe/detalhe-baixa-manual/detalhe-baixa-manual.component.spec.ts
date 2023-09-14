import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheBaixaManualComponent } from './detalhe-baixa-manual.component';

describe('DetalheBaixaManualComponent', () => {
  let component: DetalheBaixaManualComponent;
  let fixture: ComponentFixture<DetalheBaixaManualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalheBaixaManualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalheBaixaManualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
