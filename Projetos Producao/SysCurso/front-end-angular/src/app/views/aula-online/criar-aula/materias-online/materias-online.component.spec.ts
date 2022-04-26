import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MateriasOnlineComponent } from './materias-online.component';

describe('MateriasOnlineComponent', () => {
  let component: MateriasOnlineComponent;
  let fixture: ComponentFixture<MateriasOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MateriasOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MateriasOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
