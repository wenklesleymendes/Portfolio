import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServerErrorComponent } from './server-error/server-error.component';
import { DeleteComponent } from './delete/delete.component';
import { MaterialModule } from 'src/app/material.module';
import { TitleLineComponent } from './title-line/title-line.component';

@NgModule({
  declarations: [
    ServerErrorComponent,
    DeleteComponent,
    TitleLineComponent
  ],
  imports: [ CommonModule, MaterialModule ],
  exports: [ ServerErrorComponent, TitleLineComponent ]
})
export class UtilsComponentModule { }
