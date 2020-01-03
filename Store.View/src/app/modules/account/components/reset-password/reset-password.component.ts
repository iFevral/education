import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';

import { AccountService } from 'src/app/shared/services';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent {

    private resetPasswordForm: FormGroup;

    constructor(private accountService: AccountService, private router: Router, private messageContainer: MatSnackBar) {
        this.resetPasswordForm = new FormGroup({
            email: new FormControl('', [
                Validators.required,
                Validators.email
            ])
        });
    }

    public redirect(): void {
        const email = this.resetPasswordForm.value.email;
        this.accountService.resetPassword(email);
    }

    public showDialogMessage(message: string): void {
        if (message === '') {
            return;
        }

        this.messageContainer.open(message, 'X', {
            duration: 5000,
            verticalPosition: 'top'
        });
    }
}
