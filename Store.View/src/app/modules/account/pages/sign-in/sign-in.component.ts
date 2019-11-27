import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
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
