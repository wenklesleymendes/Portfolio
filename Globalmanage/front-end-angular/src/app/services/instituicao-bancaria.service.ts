import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { environment } from '../../environments/environment';
import { API } from './api';

@Injectable({
  providedIn: 'root'
})
export class InstituicaoBancariaService {

  constructor(
    private http: HttpClient,
    private errorHandlerService: ErrorHandlerService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(`${environment.baseUrl}${API.instituicaoBancaria}`)
      .pipe(
        catchError(this.errorHandlerService.errorHandler('Falha ao obter as Instituições bancárias', []))
      );
  }
}
