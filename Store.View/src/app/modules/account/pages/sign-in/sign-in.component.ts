import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../../../../shared/services/account.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
    signInForm: FormGroup;
    constructor(private accountService: AccountService, public router: Router) {
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

    signIn() {
        this.accountService.login(this.signInForm.value.email, this.signInForm.value.password)
            .subscribe(result => {
                if (result) {
                    this.router.navigate(['/']);
                }
            });
    }
}
