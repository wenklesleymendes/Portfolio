import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from '../error-handler.service';
import { AnimationsService } from '../animations.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TefService {
  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService
  ) { }

  transacaoTef(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.urlTef}/EfetuarPagamento`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha no TEF', []))
      );
  }

  imprimirComprovante(data): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.post(`${environment.urlTef}/ImprimirComprovante`, data)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(this.errorHandlerService.errorHandler('Falha no TEF', []))
      );
  }

}
