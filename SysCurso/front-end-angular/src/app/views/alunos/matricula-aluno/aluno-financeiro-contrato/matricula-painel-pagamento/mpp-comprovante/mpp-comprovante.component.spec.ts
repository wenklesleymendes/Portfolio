import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MppComprovanteComponent } from './mpp-comprovante.component';

describe('MppComprovanteComponent', () => {
  let component: MppComprovanteComponent;
  let fixture: ComponentFixture<MppComprovanteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MppComprovanteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MppComprovanteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
