import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';

import { AccountService } from '../../shared/services';

import { AccountRoutingModule } from './account-routing.module';
import {
    SignInComponent,
    SignUpComponent,
    ResetPasswordComponent
} from '.';
import { ProfileComponent } from './components/profile/profile.component';

@NgModule({
    imports: [
        FormsModule,
        BrowserModule,
        AccountRoutingModule,
        ReactiveFormsModule,
        MaterialElementsModule,
        FontAwesomeModule
    ],
    declarations: [
        SignUpComponent,
        SignInComponent,
        ResetPasswordComponent,
        ProfileComponent
    ],
    bootstrap: [
        SignInComponent
    ],
    exports: [
        SignUpComponent,
        SignInComponent
    ],
    providers: [AccountService]
})
export class AccountModule { }
