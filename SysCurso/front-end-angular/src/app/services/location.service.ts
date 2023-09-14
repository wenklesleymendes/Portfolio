import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { API } from '../services/api';
import { AnimationsService } from './animations.service';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(
    private http: HttpClient,
    private animationsService: AnimationsService,
  ) { }

  getLocation(cep: string): Observable<any> {
    this.animationsService.showProgressBar(true);
    return this.http.get(`${API.endereco}/${cep}/json`)
      .pipe(
        tap(() => this.animationsService.showProgressBar(false)),
        catchError(() => {
          this.animationsService.showProgressBar(false);
          return of([]);
        })
      );
  }
}
