import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class NoCacheInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Exception for Header
    if (req.url.match(/viacep/)) return next.handle(req);

    // Clone headers and add new options
    let optHeades = req.headers.set('Cache-Control', 'no-cache, no-store, must-revalidate, post-check=0, pre-check=0');
    optHeades = req.headers.set('Pragma', 'no-cache');
    optHeades = req.headers.set('Expires', '0');

    // Clone request and set new headers 
    const newReq = req.clone({ headers: optHeades });
    return next.handle(newReq);
  }
}
