import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../../../../shared/services';
import { UserModelItem } from '../../../../shared/models';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';

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
                Validators.minLength(3),
                Validators.maxLength(40),
                Validators.required,
            ]),
            lastName: new FormControl('', [
                Validators.minLength(3),
                Validators.maxLength(40),
                Validators.required
            ]),
            email: new FormControl('', [
                Validators.minLength(5),
                Validators.maxLength(40),
                Validators.required,
                Validators.email
            ]),
            password: new FormControl('', [
                Validators.minLength(6),
                Validators.maxLength(40),
                Validators.required
            ]),
            confirmPassword: new FormControl('', [
                Validators.required
            ])
        });
    }

    public signUp() {
        console.log(this.signUpForm);
        if (this.signUpForm.value.password === this.signUpForm.value.confirmPassword) {

            this.userModel.firstName = this.signUpForm.value.firstName;
            this.userModel.lastName = this.signUpForm.value.lastName;
            this.userModel.email = this.signUpForm.value.email;
            this.userModel.password = this.signUpForm.value.password;

            this.accountService.signUp(this.userModel).subscribe(result => {
                if (result.errors.length > 0) {
                    this.showDialogMessage(result.errors.toString());
                } else {
                    this.showDialogMessage(result.message);
                    this.router.navigate(['/']);
                }
            });
        }
    }

    public showDialogMessage(message: string) {
        if (message === '') {
            return;
        }

        this.messageContainer.open(message, 'X', {
            duration: 5000,
            verticalPosition: 'top'
        });
    }
}
