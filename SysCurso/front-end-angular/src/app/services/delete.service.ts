import { Injectable } from '@angular/core';
import { DeleteComponent } from '../utils/components/delete/delete.component';
import { MatDialog } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root'
})
export class DeleteService {

  constructor(private dialog: MatDialog) { }

  openDelete(): Promise<boolean> {
    return new Promise(res => {
      const isMobileResolution = window.innerWidth < 768 ? true : false;
      const dialogRef = this.dialog.open(DeleteComponent, { width: isMobileResolution ? '98vw' : '30vw' });
      dialogRef.afterClosed().subscribe(result => res(result));
    })
  }
}
