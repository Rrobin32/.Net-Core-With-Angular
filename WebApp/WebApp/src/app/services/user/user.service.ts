import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly apiUrl = 'https://localhost:7142/api/';

  constructor(private http: HttpClient) { }

  getUserList(queryString :string): Observable<any[]> {
    const httpOptions = {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json',
        }
      )
    };
    return this.http.get<any[]>(this.apiUrl + 'Users/GetUserInfo' + queryString);
  }

  addUser(user: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json'
        }
      )
    };
    return this.http.post<any>(this.apiUrl + 'Users/AddUserInfo/', user, httpOptions);
  }

  updateUser(user: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl + 'Users/UpdateUserInfo/', user, httpOptions);
  }

  deleteUser(user: any): Observable<any> {
    const httpOptions =
    {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl + 'Users/DeleteUserInfo/', user, httpOptions);
  }
}
