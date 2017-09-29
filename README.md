 

# Building a Full-Stack Application with Angular 4, ASP.NET Core 2.0 ,  Authentication and Materiale Design


Frontend

Install Angular Material design <br />
Using material design car <br />
Communicating between multiple components with Output and ViewChild <br />
Creating a data store in our service    <br />                          
Handling errors with try or catch <br />
Navigate to a different view with route and passing and get route date <br />

Broadcast an event with subject

Work Promise 


   import 'rxjs/add/operator/toPromise';<br />
       async ngOnInit() {<br />
           const response = await this._WebService.getMessage(); <br />
           console.log(response);<br />
       }<br />

     getMessage() {<br />
           return this._http.get(`http://localhost:38077/api/messages`).toPromise();<br />
          }<br />
 
Use Reactive Forms <br />


Parent Component Listen to Child Component <br />
 Can do this with angular outOuts <br />
 viewChilds<br />
 childeComponent  Output, EventEmitter } from '@angular/core'<br />
 childeComponent  @Output() onPosted = new EventEmitter();<br />
 ParentComponent onPosted(message) {console.log(message);}<br />
 On add diractive <app-new-message (onPosted)="onPosted($event)"></app-new-message><br />
  To function you want fire event at parent    this.onPosted.emit(this.message);<br />




Push data from parent to child by viewChild<br />
 	import { ViewChild } from '@angular/core';<br />
  @ViewChild(MessagesComponent) messages: MessagesComponent; this object have any parameter or function for child<br />


Create Datastore Shared between component <br />

Create Route <br />
Import router for anular route <br />
import {RouterModule} from '@angular/router';<br />
Create List routes <br />
const routes = [{ path: '', component: HomeComponent }]; <br />

RouterModule.forRoot(routes) at module imports  <br />
Navigate via routerLink="/messages" <br />
Passting Paramter with route  <br />


{ path: 'messages/:name', component: MessagesComponent }

[routerLink]="['/messages',msg.owner]"

Retrieve Route Parameters 

import {ActivatedRoute} from '@angular/router';

Use Observables and Http Get
this._http.get(`${this.BASE_URL}/api/messages${owner}`).subscribe(response => {
       this.messages = response.json();
     }, erro => {
       this.HandleError('Unable to get messages');
     });



Broadcast an event with subject
 Instead of allowing components to access our messages array directly, which might not be safe, we can use an observable,specifically a subject. A subject allows observers to subscribe to it.And in this case, we can send our messages array through itwhenever an update through an HTTP request occurs.

Steps

1.	At service import import { Subject } from 'rxjs/Rx';<br />
2.	Create object will call  messagesSubject = new Subject();<br />
3.  this.messagesSubject.next(this.messages);<br />
4.	this._WebService.messagesSubject.subscribe(messages => {  this.messages = messages; });<br />
                                

Use Observables and the async pipe<br />
Which allow us to access data directly from html to service<br />




                   
Angular Reactive Form with form builder <br />

          Import ReactiveFormsModule at module
          
            form: FormGroup;
 
           constructor(private fb: FormBuilder) {
              this.form = fb.group({
              firstName: ''
             });
            }


 <input mdInput placeholder="Frist Name" formControlName="firstName" >
           
Validate form model with reactive <br />

  this.form = fb.group({<br />
     firstName: ['', Validators.required],<br />
     lastName: '',<br />
     email: ['', Validators.email],<br />
     password: ['', Validators.required],<br />
     confirmPassword: ['', Validators.required],<br />
   });<br />

Validate Password with custome validation<br />
Custome Email Validatior<br />



Reactive forms with FormBuilder<br />
Validate with reactive forms<br />
Custom Validator for confirm password and Email forma<br />


Create Auth Service<br />
Eidt User Profile <br />
 
Backend 

Enable cores

   services.AddCors(option => option.AddPolicy("Cors", builder => {
                builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();

            app.UseCors("Cors");


Generate Token Via System.IdentityModel.Tokens.Jwt

    JwtPacket CreateJwtPacket(User user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var jwt = new JwtSecurityToken(claims: claims);
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JwtPacket { Token = encodeJwt, FirstName = user.FirstName };

        }


Auth middleware with Asp NetCore.Authentication.Jwt Bearer

Get Authentication User At  Controller

User in memory data store for demo and unit test .
