import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCursoOnlineComponent } from './add-curso-online.component';

describe('AddCursoOnlineComponent', () => {
  let component: AddCursoOnlineComponent;
  let fixture: ComponentFixture<AddCursoOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCursoOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCursoOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
