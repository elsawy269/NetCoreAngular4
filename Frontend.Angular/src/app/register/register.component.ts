import { AuthService } from './../auth.service';
import { validate, Validator } from 'codelyzer/walkerFactory/walkerFn';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;
  constructor(private fb: FormBuilder, private _auth: AuthService) {
    this.form = fb.group({
      firstName: ['', Validators.required],
      lastName: '',
      email: ['', Validators.email],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],


    }, { validator: matchingFilds('password', 'confirmPassword') });
  }

  ngOnInit() {
  }
  onSubmit() {
    console.log(this.form.errors);
    // if (this.form.valid)
     {
      this._auth.register(this.form.value);
    }
  }

}




function matchingFilds(field1, field2) {
  return form => {
    if (form.controls[field1].value !== form.controls[field2].value) {
      return { misMatchField: true };
    }
    return { misMatchField: false };
  };
}
