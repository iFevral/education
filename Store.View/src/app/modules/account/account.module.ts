import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';
import { AccountRoutingModule } from './account-routing.module';

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
        FontAwesomeModule,
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
