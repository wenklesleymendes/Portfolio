import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpEventType } from '@angular/common/http';

import { AnimationsService } from '../animations.service';
import { ErrorHandlerService } from '../error-handler.service';
import { API } from '../api';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { catchError,map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApostilaOnlineService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
    private errorHandlerService: ErrorHandlerService) { }

    getApostilaPorIdMateria(materiaId): Observable<any> {

    const params: HttpParams = new HttpParams().set('materiaId', materiaId);
      return this.http.get(`${environment.baseUrl}${API.apostilaOnline.bucarApostilaPorIdMateria}`, { params })
        .pipe(
          catchError(this.errorHandlerService.errorHandler('Falha ao obter apostila', []))
        );
    }

    getApostilaPorCursoId(cursoId): Observable<any> {

    const params: HttpParams = new HttpParams().set('cursoId', cursoId);
      return this.http.get(`${environment.baseUrl}${API.apostilaOnline.bucarApostilaPorCursoId}`, { params })
        .pipe(
          catchError(this.errorHandlerService.errorHandler('Falha ao obter apostila', []))
        );
    }

    // downloadPDF(url): any {
    //   const options = { responseType: ResponseContentType.Blob  };
    //   return this.http.get(url, options).pipe(map(
    //     (res) => {
    //         return new Blob([res], { type: 'application/pdf' });
    //     }));
    // }
}
