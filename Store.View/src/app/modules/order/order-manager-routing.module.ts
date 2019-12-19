import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderListComponent, OrderListByUserComponent } from '.';

const orderRoutes: Routes = [{
    path: 'Orders',
    children: [
        { path: '', component: OrderListComponent, data: { animation: 'Orders' } },
        { path: ':id', component: OrderListByUserComponent, data: { animation: 'Orders' } }
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
