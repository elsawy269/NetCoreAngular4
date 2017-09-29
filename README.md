 

# Building a Full-Stack Application with Angular 4, ASP.NET Core 2.0 ,  Authentication and Materiale Design


Frontend

Install Angular Material design
Using material design car
Communicating between multiple components with Output and ViewChild
Creating a data store in our service                             
Handling errors with try or catch
Navigate to a different view with route and passing and get route date

Broadcast an event with subject

Work Promise 


   import 'rxjs/add/operator/toPromise';
       async ngOnInit() {
           const response = await this._WebService.getMessage();
           console.log(response);
       }

     getMessage() {
           return this._http.get(`http://localhost:38077/api/messages`).toPromise();
          }
 
Use Reactive Forms 


Parent Component Listen to Child Component 
 Can do this with angular outOuts 
 viewChilds
 childeComponent  Output, EventEmitter } from '@angular/core'
 childeComponent  @Output() onPosted = new EventEmitter();
 ParentComponent onPosted(message) {console.log(message);}
 On add diractive <app-new-message (onPosted)="onPosted($event)"></app-new-message>
  To function you want fire event at parent    this.onPosted.emit(this.message);




Push data from parent to child by viewChild
 	import { ViewChild } from '@angular/core';
  @ViewChild(MessagesComponent) messages: MessagesComponent; this object have any parameter or function for child


Create Datastore Shared between component 

Create Route 
Import router for anular route 
import {RouterModule} from '@angular/router';
Create List routes
const routes = [{ path: '', component: HomeComponent }];

RouterModule.forRoot(routes) at module imports 
Navigate via routerLink="/messages"
Passting Paramter with route  


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
1.	At service import import { Subject } from 'rxjs/Rx';
2.	Create object will call  messagesSubject = new Subject();
3.  this.messagesSubject.next(this.messages);
4.	this._WebService.messagesSubject.subscribe(messages => {  this.messages = messages; });
                                

Use Observables and the async pipe
Which allow us to access data directly from html to service




                   
Angular Reactive Form with form builder 

          Import ReactiveFormsModule at module
          
            form: FormGroup;
 
           constructor(private fb: FormBuilder) {
              this.form = fb.group({
              firstName: ''
             });
            }


 <input mdInput placeholder="Frist Name" formControlName="firstName" >
           
Validate form model with reactive 

  this.form = fb.group({
     firstName: ['', Validators.required],
     lastName: '',
     email: ['', Validators.email],
     password: ['', Validators.required],
     confirmPassword: ['', Validators.required],
   });

Validate Password with custome validation
Custome Email Validatior



Reactive forms with FormBuilder
Validate with reactive forms
Custom Validator for confirm password and Email forma


Create Auth Service
Eidt User Profile 
 
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
