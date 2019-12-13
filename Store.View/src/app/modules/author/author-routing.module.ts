import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorListComponent, } from '.';

const authorRoutes: Routes = [{
    path: 'Author',
    children: [
        { path: 'List', component: AuthorListComponent }
    ]
}];

export const authorRouting = RouterModule.forChild(authorRoutes);

@NgModule({
    imports: [
        RouterModule.forRoot(authorRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AuthorRoutingModule { }
