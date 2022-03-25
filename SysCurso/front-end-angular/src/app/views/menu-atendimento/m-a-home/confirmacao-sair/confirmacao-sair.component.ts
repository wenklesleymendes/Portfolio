import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';

@Component({
  selector: 'app-confirmacao-sair',
  templateUrl: './confirmacao-sair.component.html',
  styleUrls: ['./confirmacao-sair.component.scss']
})

export class ConfirmacaoSairComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router,

    public dialogRef: MatDialogRef<ConfirmacaoSairComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) { }

  ngOnInit(): void {

  }

  logout(): void {
    this.dialogRef.close();
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  close(): void {
    this.dialogRef.close();
  }
}
