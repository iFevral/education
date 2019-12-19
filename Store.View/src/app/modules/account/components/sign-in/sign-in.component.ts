import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../../../../shared/services';
import { MatSnackBar } from '@angular/material';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
    private signInForm: FormGroup;

    constructor(private accountService: AccountService, private router: Router, private messageContainer: MatSnackBar) {

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
        localStorage.setItem('isRemeberMeActivated', this.signInForm.value.rememberMe);
        this.signInForm.value.rememberMe = false;
        this.accountService.signIn(this.signInForm.value.email, this.signInForm.value.password, this.signInForm.value.rememberMe)
            .subscribe(result => {
                if (result.errors.length > 0) {
                    this.showDialogMessage(result.errors.toString());
                } else {
                    this.accountService.redirectTo('/');
                }
            });
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
