import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent, } from '.';

const userRoutes: Routes = [{
    path: 'Users',
    children: [
        { path: '', component: UserListComponent }
    ]
}];

export const userRouting = RouterModule.forChild(userRoutes);

@NgModule({
    imports: [
        RouterModule.forRoot(userRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class UserRoutingModule { }
