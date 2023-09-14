import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AnimationsService } from './animations.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor(
    private animationsService: AnimationsService,
    private router: Router,
  ) { }

  /**
   * @description Handle error
   * @param message Message that will be displayed to the user 
   * @param result Default error
   * @returns Observable<T>
   */
  errorHandler<T>(message: string, result: T): any {
    return (error: any): Observable<any> => {
        this.animationsService.showProgressBar(false);
        // Checks if is Unauthorized
        if(error && error.status === 401) {
          this.animationsService.showErrorSnackBar('Sessão Parou');
          window.localStorage.removeItem('accessToken');
          this.router.navigate(['']);
          return of(null);
        }
        // Console error
        console.error(`${message}: ${error?.message}`);
        // Stop loading bar
        // SnackBar error
        this.animationsService.showErrorSnackBar(message);
        // Return error
        return of({ status: 'error', result});
    };
  }

  /**
   * @description Handle error of file
   * @param message Message that will be displayed to the user 
   * @param result Default error
   * @returns Observable<T>
   */
  errorHandlerAnexo<T>(message: string, result: T): any {
    return (error: any): Observable<any> => {
      this.animationsService.showProgressBar(false);
      // Checks if is Unauthorized
      if(error && error.status === 401) {
        this.animationsService.showErrorSnackBar('Sessão Parou');
        window.localStorage.removeItem('accessToken');
        this.router.navigate(['']);
        return of(null);
      }

      if (error && error['error'] && error['error']['text']){
        const b64Data = error['error']['text'];
        return of(b64Data);
      } else {
        // Console error
        console.error(`${message}: ${error.message}`);
        // Stop loading bar
        // SnackBar error
        this.animationsService.showErrorSnackBar(message);
        // Return error
        return of({ status: 'error', result});
      }
    };
  }
}
