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
            'email': new FormControl('', [
                Validators.required,
                Validators.email
            ]),
            'password': new FormControl('', [
                Validators.required
            ])
        });
    }
}
