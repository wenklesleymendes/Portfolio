import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AnimationsService {
  private progressBar  = new BehaviorSubject(false);

  constructor(private snackBar: MatSnackBar) { }

  completeAllSubjects(): void {
    this.progressBar.complete();
  }

  getProgressBar(): Observable<boolean> {
    return this.progressBar;
  }

  showProgressBar(state: boolean): void { 
    this.progressBar.next(state);
  }

  showErrorSnackBar(message): void {
    this.snackBar.open(message, 'ok', { 
      duration: 15000,
      horizontalPosition: 'end',
      panelClass: ['snackbar-error']
    });
  }

  showSuccessSnackBar(message): void {
    this.snackBar.open(message, 'ok', { 
      duration: 15000,
      horizontalPosition: 'end',
      panelClass: ['snackbar-success']
    });
  }
}
