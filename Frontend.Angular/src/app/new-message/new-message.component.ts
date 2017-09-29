import { IMessage } from '../models/message';
import { WebService } from './../web.service';
import { AuthService } from './../auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-message',
  templateUrl: './new-message.component.html',
  styleUrls: ['./new-message.component.css']
})
export class NewMessageComponent implements OnInit {

  message: IMessage = {
    text: 'Message please review this documment',
    owner: this._auth.name
  };
  constructor(private _WebService: WebService, private _auth: AuthService) { }

  ngOnInit() {
  }
  SaveMessage() {

    this._WebService.postMessage(this.message);
  }

}
