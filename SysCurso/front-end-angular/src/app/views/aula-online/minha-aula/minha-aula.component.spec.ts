import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MinhaAulaComponent } from './minha-aula.component';

describe('MinhaAulaComponent', () => {
  let component: MinhaAulaComponent;
  let fixture: ComponentFixture<MinhaAulaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MinhaAulaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MinhaAulaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
