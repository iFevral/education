import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from 'src/app/modules/order-manager/order-manager-routing.module';
import { MaterialElementsModule } from 'src/app/shared/modules';

import { OrderService, CartService } from 'src/app/shared/services';

import { CartComponent } from 'src/app/modules/order-manager/components/cart/cart.component';
import { OrderListComponent, OrderListByUserComponent } from 'src/app/modules/order-manager/index';

@NgModule({
    declarations: [OrderListComponent, OrderListByUserComponent, CartComponent],
    imports: [
        CommonModule,
        FormsModule,
        MaterialElementsModule,
        OrderRoutingModule
    ],
    exports: [
        CartComponent
    ],
    providers: [OrderService, CartService]
})
export class OrderManagerModule { }
