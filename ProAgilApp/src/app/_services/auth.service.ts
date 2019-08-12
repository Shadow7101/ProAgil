import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { Login } from '../_models/Login';
import { Router } from '@angular/router';
import { Token } from '../_models/Token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/user';
  jwtHelper = new JwtHelperService();
  user: any;
  decodeToken: Token;


  constructor(private http: HttpClient) { }

  Register(model: User): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, model);
  }

  Login(model: Login): Observable<Login> {
    return this.http.post(`${this.baseUrl}/login`, model).pipe(
      map((response: any) => {
        this.user = response;
        if (this.user) {
          localStorage.setItem('token', this.user.token);
          this.decodeToken = this.jwtHelper.decodeToken(this.user.token);
          localStorage.setItem('userName', this.decodeToken.unique_name);
          return this.user.user;
        }
        return null;
      })
    );
  }

  Logado(): boolean {
    const token = localStorage.getItem('token');
    // return this.jwtHelper.isTokenExpired(token);
    return (token !== null);
  }

  UserName(): string {
    if (!this.Logado()) { return null; }
    return localStorage.getItem('userName');
  }
}
