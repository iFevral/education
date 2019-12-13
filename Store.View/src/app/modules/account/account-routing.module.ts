import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';

import { SignInComponent, SignUpComponent, ResetPasswordComponent } from '.';

const accountRoutes: Routes = [
    {
        path: 'Account',
        children: [
            { path: 'SignIn', component: SignInComponent },
            { path: 'SignUp', component: SignUpComponent },
            { path: 'ResetPassword', component: ResetPasswordComponent }
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
