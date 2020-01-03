import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { MaterialElementsModule } from 'src/app/shared/modules';
import { AccountRoutingModule } from 'src/app/modules/account/account-routing.module';

import {
    SignInComponent,
    SignUpComponent,
    ResetPasswordComponent,
    ProfileComponent,
    EmailConfirmationComponent
} from '.';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MaterialElementsModule,
        AccountRoutingModule
    ],
    declarations: [
        SignUpComponent,
        SignInComponent,
        ResetPasswordComponent,
        ProfileComponent,
        EmailConfirmationComponent
    ],
    bootstrap: [
        SignInComponent
    ],
    exports: [
        SignUpComponent,
        SignInComponent
    ]
})
export class AccountModule { }
