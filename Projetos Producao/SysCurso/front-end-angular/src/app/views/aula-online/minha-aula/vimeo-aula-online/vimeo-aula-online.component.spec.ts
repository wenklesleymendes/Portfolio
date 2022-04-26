import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VimeoAulaOnlineComponent } from './vimeo-aula-online.component';

describe('VimeoAulaOnlineComponent', () => {
  let component: VimeoAulaOnlineComponent;
  let fixture: ComponentFixture<VimeoAulaOnlineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VimeoAulaOnlineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VimeoAulaOnlineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
