import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConteudoDigitalComponent } from './conteudo-digital.component';

describe('ConteudoDigitalComponent', () => {
  let component: ConteudoDigitalComponent;
  let fixture: ComponentFixture<ConteudoDigitalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConteudoDigitalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConteudoDigitalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
