import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppDetalheEmailIndividualComponent } from './mpp-detalhe-email-individual.component';

describe('MppDetalheEmailIndividualComponent', () => {
  let component: MppDetalheEmailIndividualComponent;
  let fixture: ComponentFixture<MppDetalheEmailIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppDetalheEmailIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppDetalheEmailIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
