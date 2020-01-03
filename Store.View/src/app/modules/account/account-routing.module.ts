import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';

import {
    SignInComponent,
    SignUpComponent,
    ResetPasswordComponent,
    ProfileComponent,
    EmailConfirmationComponent
} from 'src/app/modules/account/index';

const accountRoutes: Routes = [
    { path: 'SignIn', component: SignInComponent, data: { animation: 'SignIn' } },
    { path: 'SignUp', component: SignUpComponent, data: { animation: 'SignUp' } },
    { path: 'ResetPassword', component: ResetPasswordComponent, data: { animation: 'ResetPassword' } },
    { path: 'Profile', component: ProfileComponent, data: { animation: 'Profile' } },
    { path: 'ConfirmEmail', component: EmailConfirmationComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(accountRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AccountRoutingModule { }
