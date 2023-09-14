import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContatosPrioritatiosComponent } from './contatos-prioritarios.component';

describe('ContatosPrioritatiosComponent', () => {
  let component: ContatosPrioritatiosComponent;
  let fixture: ComponentFixture<ContatosPrioritatiosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContatosPrioritatiosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContatosPrioritatiosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
