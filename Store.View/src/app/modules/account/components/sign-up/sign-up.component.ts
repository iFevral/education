import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { AccountService } from 'src/app/shared/services';
import { UserModelItem } from 'src/app/shared/models';
import { Constants } from 'src/app/shared/constants/constants';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {

    private userModel: UserModelItem = new UserModelItem();

    private signUpForm: FormGroup;

    constructor(private accountService: AccountService, private router: Router, private messageContainer: MatSnackBar) {
        this.signUpForm = new FormGroup({
            firstName: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.nameMinLength),
                Validators.maxLength(Constants.formValidatorParams.nameMaxLength),
                Validators.required,
            ]),
            lastName: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.nameMinLength),
                Validators.maxLength(Constants.formValidatorParams.nameMaxLength),
                Validators.required
            ]),
            email: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.emailMinLength),
                Validators.maxLength(Constants.formValidatorParams.emailMaxLength),
                Validators.required,
                Validators.email
            ]),
            password: new FormControl('', [
                Validators.minLength(Constants.formValidatorParams.passwordMinLength),
                Validators.maxLength(Constants.formValidatorParams.passwordMaxLength),
                Validators.required
            ]),
            confirmPassword: new FormControl('', [
                Validators.required
            ])
        });
    }

    public signUp(): void {
        if (this.signUpForm.value.password === this.signUpForm.value.confirmPassword) {

            this.userModel.firstName = this.signUpForm.value.firstName;
            this.userModel.lastName = this.signUpForm.value.lastName;
            this.userModel.email = this.signUpForm.value.email;
            this.userModel.password = this.signUpForm.value.password;

            this.accountService.signUp(this.userModel);
        }
    }
}
