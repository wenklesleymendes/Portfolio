import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerguntasOnlineIndividualComponent } from './perguntas-online-individual.component';

describe('PerguntasOnlineIndividualComponent', () => {
  let component: PerguntasOnlineIndividualComponent;
  let fixture: ComponentFixture<PerguntasOnlineIndividualComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerguntasOnlineIndividualComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerguntasOnlineIndividualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
