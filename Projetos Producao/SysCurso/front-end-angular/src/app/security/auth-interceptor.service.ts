import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

/** Pass untouched request through to the next request handler. */
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    // Exception for the Authorization Header
    if (req.url.match(/viacep/)) return next.handle(req);

    // Get token
    const accessToken: any = this.authService.getToken();
    const { token } = accessToken;

    // Clone headers and add Authorization
    let optHeades = req.headers;
    optHeades = req.headers.set('content-type', 'application/json; charset=utf-8');
    optHeades = req.headers.set('Authorization', `Bearer ${token}`);

    // Clone request and set new headers
    const authReq = req.clone({ headers: optHeades });

    // Make the request
    return next.handle(authReq);
  }
}
