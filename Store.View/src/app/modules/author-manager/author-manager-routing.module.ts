import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorListComponent, } from 'src/app/modules/author-manager/index';

const authorRoutes: Routes = [
    { path: '', component: AuthorListComponent, data: { animation: 'Authors' } }
];

@NgModule({
    imports: [
        RouterModule.forChild(authorRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AuthorRoutingModule { }
