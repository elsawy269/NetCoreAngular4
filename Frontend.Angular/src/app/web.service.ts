import { AuthService } from './auth.service';
import { IMessage } from './models/message';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { MdSnackBar } from '@angular/material';
import 'rxjs/add/operator/toPromise';
import { Subject } from 'rxjs/Rx';
@Injectable()
export class WebService {
  BASE_URL = 'http://localhost:38077';
  private messagesStore: IMessage[] = [];
  private messagesSubject = new Subject();
  messages = this.messagesSubject.asObservable();
  constructor(private _http: Http, private sb: MdSnackBar, private _auth: AuthService) {
  }
  getMessage(owner) {
    owner = (owner) ? '/' + owner : '';
    this._http.get(`${this.BASE_URL}/api/messages${owner}`).subscribe(response => {
      this.messagesStore = response.json();
      this.messagesSubject.next(this.messagesStore);
    }, erro => {
      this.HandleError('Unable to get messages');
    });
  }
  async postMessage(message: IMessage) {
    try {
      const response = await this._http.post(`${this.BASE_URL}/api/messages`, message).toPromise();
      this.messagesStore.push(response.json());
    } catch (error) {
      this.HandleError('Unable to post messages');
    }

  }
  getUser() {
    return this._http.get(`${this.BASE_URL}/users/me`, this._auth.tokenHeader).map(res => res.json());
  }
  saveUser(user) {
    return this._http.post(`${this.BASE_URL}/users/me`, user, this._auth.tokenHeader).map(res => res.json());

  }
  private HandleError(msg) {
    console.error(msg);
    this.sb.open(msg, 'close', { duration: 2000 });
  }
}
