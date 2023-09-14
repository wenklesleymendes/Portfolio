import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlunoPortalComponent } from './aluno-portal.component';

describe('AlunoPortalComponent', () => {
  let component: AlunoPortalComponent;
  let fixture: ComponentFixture<AlunoPortalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlunoPortalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlunoPortalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
