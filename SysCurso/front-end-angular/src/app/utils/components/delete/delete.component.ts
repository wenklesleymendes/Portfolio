import { Component, Inject, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.scss']
})
export class DeleteComponent implements OnDestroy {
  canRemove: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<DeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  remove(remove: boolean): void {
    this.canRemove = remove;
    this.dialogRef.close(this.canRemove);
  }

  ngOnDestroy(): void {
    this.dialogRef.close(this.canRemove);
  }
}
