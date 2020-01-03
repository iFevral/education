import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OrderListComponent, OrderListByUserComponent } from 'src/app/modules/order-manager/index';

const orderRoutes: Routes = [
    { path: '', component: OrderListComponent, data: { animation: 'Orders' } },
    { path: ':id', component: OrderListByUserComponent, data: { animation: 'Orders' } }
];

@NgModule({
    imports: [
        RouterModule.forChild(orderRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class OrderRoutingModule { }
