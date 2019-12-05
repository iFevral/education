import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
    signInForm: FormGroup;
    constructor() {
        this.signInForm = new FormGroup({
            'firstname': new FormControl('', [
                Validators.required,
            ]),
            'lastname': new FormControl('', [
                Validators.required
            ]),
            'email': new FormControl('', [
                Validators.required,
                Validators.email
            ]),
            'password': new FormControl('', [
                Validators.required
            ]),
            'confirmPassword': new FormControl('', [
                Validators.required
            ])
        });
    }
}
