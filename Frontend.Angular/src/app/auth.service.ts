import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  BASE_URL = 'http://localhost:38077/Auth';
  name_Key = 'firstName';
  token_key = 'token';



  constructor(private _http: Http, private router: Router) { }


  get name() {
    return localStorage.getItem(this.name_Key);
  }
  get isAuthenticated() {
    return !!localStorage.getItem(this.name_Key);
  }

  get tokenHeader() {
    const header = new Headers({  'Authorization': 'Bearer ' + localStorage.getItem(this.token_key) });
    return new RequestOptions({ headers: header });
  }

  login(loginData) {
    this._http.post(`${this.BASE_URL}/login`, loginData).subscribe(res => {
      this.authenticate(res);
    });
  }

  register(user) {
    delete user.confirmPassword;
    this._http.post(`${this.BASE_URL}/register`, user).subscribe(res => {
      this.authenticate(res);
    });
  }
  logout() {
    localStorage.removeItem(this.token_key);
    localStorage.removeItem(this.name_Key);
  }

  authenticate(res) {
    const authResponse = res.json();

    // tslint:disable-next-line:curly
    if (!authResponse.token)
      return;
    localStorage.setItem(this.token_key, authResponse.token);
    localStorage.setItem(this.name_Key, authResponse.firstName);
    this.router.navigate(['/']);
  }
}
