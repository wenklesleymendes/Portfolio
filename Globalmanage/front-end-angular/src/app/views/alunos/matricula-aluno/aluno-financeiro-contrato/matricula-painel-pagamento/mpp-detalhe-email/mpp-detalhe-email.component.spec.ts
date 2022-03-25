import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppDetalheEmailComponent } from './mpp-detalhe-email.component';

describe('MppDetalheEmailComponent', () => {
  let component: MppDetalheEmailComponent;
  let fixture: ComponentFixture<MppDetalheEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppDetalheEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppDetalheEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
