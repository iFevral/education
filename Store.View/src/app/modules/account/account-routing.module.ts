import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';

import {
    SignInComponent,
    SignUpComponent,
    ResetPasswordComponent
} from '.';

const accountRoutes: Routes = [
    {
        path: 'Account',
        children: [
            { path: 'SignIn', component: SignInComponent, data: { animation: 'SignIn' } },
            { path: 'SignUp', component: SignUpComponent, data: { animation: 'SignUp' } },
            { path: 'ResetPassword', component: ResetPasswordComponent, data: { animation: 'ResetPassword' } }
        ]
    }
];

export const UsersRouting = RouterModule.forChild(accountRoutes);

@NgModule({
    imports: [
        RouterModule.forRoot(accountRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AccountRoutingModule { }
