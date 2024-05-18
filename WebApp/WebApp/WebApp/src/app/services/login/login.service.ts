import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  readonly apiUrl = 'https://localhost:7142/api/';

  constructor(private http: HttpClient) { }

  login(user: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json'
        }
      )
    };

    return this.http.get<any>(this.apiUrl + 'Login/Login' + user);
  }
}
