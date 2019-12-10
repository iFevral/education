import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../../../../shared/services';
import { MatSnackBar } from '@angular/material';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss'],
    providers: [AccountService]
})
export class SignInComponent {
    private signInForm: FormGroup;

    constructor(private accountService: AccountService, private router: Router, private errorContainer: MatSnackBar) {
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

    public signIn() {
        this.accountService.signIn(this.signInForm.value.email, this.signInForm.value.password)
            .subscribe(result => {
                if (result) {
                    this.router.navigate(['/']);
                }
                this.errorContainer.open('Wrong email or password', 'X', {
                    duration: 3000,
                    verticalPosition: 'top',
                });
            });
    }
}
