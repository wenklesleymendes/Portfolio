import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompararDatasComponent } from './comparar-datas.component';

describe('CompararDatasComponent', () => {
    let component: CompararDatasComponent;
    let fixture: ComponentFixture<CompararDatasComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
        declarations: [ CompararDatasComponent ]
        })
        .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CompararDatasComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
