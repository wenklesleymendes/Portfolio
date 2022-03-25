import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-preview-video',
  templateUrl: './preview-video.component.html',
  styleUrls: ['./preview-video.component.scss']
})
export class PreviewVideoComponent implements OnInit {
  nome: string = null;
  url: string = null;
  id: string = null;
  player: any;
  options: any;
  preview: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<PreviewVideoComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.nome = this.data?.nome;
    this.url = this.data?.url;
    this.id = this.data?.id;
    this.preview = this.data?.preview;
  }
}
