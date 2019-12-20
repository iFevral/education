import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../../../../shared/services';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
    private signInForm: FormGroup;

    constructor(private accountService: AccountService) {

        this.signInForm = new FormGroup({
            email: new FormControl('', [
                Validators.required,
                Validators.email
            ]),
            password: new FormControl('', [
                Validators.required
            ]),
            rememberMe: new FormControl('', [])
        });
    }

    public signIn() {
        this.signInForm.value.rememberMe = this.signInForm.value.rememberMe
            ? this.signInForm.value.rememberMe
            : false;

        this.accountService.signIn(this.signInForm.value.email, this.signInForm.value.password, this.signInForm.value.rememberMe);
    }
}
