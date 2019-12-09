import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';

const accountRoutes: Routes = [
    {
        path: 'Account',
        children: [
            { path: 'SignIn', component: SignInComponent },
            { path: 'SignUp', component: SignUpComponent }
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
