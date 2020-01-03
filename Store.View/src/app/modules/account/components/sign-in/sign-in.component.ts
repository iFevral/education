import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Constants } from 'src/app/shared/constants/constants';
import { AccountService } from 'src/app/shared/services';

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
                Validators.email,
                Validators.minLength(Constants.formValidatorParams.emailMinLength),
                Validators.maxLength(Constants.formValidatorParams.emailMaxLength),
            ]),
            password: new FormControl('', [
                Validators.required,
                Validators.minLength(Constants.formValidatorParams.passwordMinLength),
                Validators.maxLength(Constants.formValidatorParams.passwordMaxLength),
            ]),
            rememberMe: new FormControl('', [])
        });
    }

    public signIn(): void {
        if (!this.signInForm.value.rememberMe) {
            this.signInForm.value.rememberMe = false;
        }

        this.accountService.signIn(this.signInForm.value.email, this.signInForm.value.password, this.signInForm.value.rememberMe);
    }
}
