import { WebService } from './../web.service';
import { IMessage } from '../models/message';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  constructor(private _WebService: WebService, private route: ActivatedRoute) { }
  async ngOnInit() {
    const name = this.route.snapshot.params.name;
    this._WebService.getMessage(name);
    this._WebService.getUser().subscribe();
  }
}
