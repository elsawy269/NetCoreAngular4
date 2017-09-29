import { Component, OnInit } from '@angular/core';
import { WebService } from './../web.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  model = {
    firstName: '',
    lastName: ''

  };
  constructor(private _webSRV: WebService) { }

  ngOnInit() {
    this._webSRV.getUser().subscribe(res => {
      this.model.firstName = res.firstName;
      this.model.lastName = res.lastName;
    });
  }
  save(user) {
    this._webSRV.saveUser(user).subscribe();
  }

}
