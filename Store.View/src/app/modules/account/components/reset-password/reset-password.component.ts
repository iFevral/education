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
                    this.showDialogMessage(result.errors.toString());
                } else {
                    this.showDialogMessage('We have sent instructions to ' + email);
                    this.router.navigate(['/']);
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
