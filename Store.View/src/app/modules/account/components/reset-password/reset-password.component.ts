import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';

import { AccountService } from '../../../../shared/services';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent {

    private resetPasswordForm: FormGroup;

    constructor(private accountService: AccountService, private router: Router, private messageContainer: MatSnackBar) {
        this.resetPasswordForm = new FormGroup({
            'email': new FormControl('', [
                Validators.required,
                Validators.email
            ])
        });
    }

    public redirect() {
        const email = this.resetPasswordForm.value.email;
        this.accountService.resetPassword(email)
            .subscribe(result => {
                if (result.errors.length > 0) {
                    this.messageContainer.open(result.errors.toString(), 'X', {
                        duration: 3000,
                        verticalPosition: 'top',
                    });
                } else {
                    this.messageContainer.open('We have sent instructions to ' + email, 'X', {
                        duration: 3000,
                        verticalPosition: 'top',
                    });
                    this.router.navigate(['/']);
                }
            });
    }
}
