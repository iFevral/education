import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderListComponent, OrderListByUserComponent } from '.';

const orderRoutes: Routes = [{
    path: 'Orders',
    children: [
        { path: '', component: OrderListComponent, data: { animation: 'Orders' } },
        { path: 'MyOrders', component: OrderListByUserComponent, data: { animation: 'MyOrders' } }
    ]
}];

export const orderRouting = RouterModule.forChild(orderRoutes);

@NgModule({
    imports: [
        RouterModule.forRoot(orderRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class OrderRoutingModule { }
