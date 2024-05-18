import { HTTP_INTERCEPTORS, HttpEvent, HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { TokenStorageService } from '../services/tokenstorage/tokenstorage.service';
import { Observable } from 'rxjs';


const TOKEN_HEADER_KEY = 'Authorization';     

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  readonly apiUrl = 'https://localhost:7142/api/';

  constructor(private token: TokenStorageService, private http: HttpClient) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authReq = req;
    const token = this.token.getToken();
    if (token != null) {
      authReq = req.clone({ headers: req.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token) });
    }
    return next.handle(authReq);
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];
