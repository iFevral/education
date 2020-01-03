import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: '', loadChildren: () => import('src/app/modules/printing-edition/printing-edition.module').then(m => m.PrintingEditionModule) },
    { path: 'Account', loadChildren: () => import('src/app/modules/account/account.module').then(m => m.AccountModule) },
    { path: 'Authors', loadChildren: () => import('src/app/modules/author-manager/author-manager.module').then(m => m.AuthorManagerModule) },
    { path: 'Orders', loadChildren: () => import('src/app/modules/order-manager/order-manager.module').then(m => m.OrderManagerModule) },
    { path: 'PrintingEditions', loadChildren: () => import('src/app/modules/printing-edition-manager/printing-edition-manager.module').then(m => m.PrintingEditionManagerModule) },
    { path: 'Users', loadChildren: () => import('src/app/modules/user-manager/user-manager.module').then(m => m.UserManagerModule) }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
