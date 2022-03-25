import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MateriasOnlineInitComponent } from './materias-online-init.component';

describe('MateriasOnlineInitComponent', () => {
  let component: MateriasOnlineInitComponent;
  let fixture: ComponentFixture<MateriasOnlineInitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MateriasOnlineInitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MateriasOnlineInitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
