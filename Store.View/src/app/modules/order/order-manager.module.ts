import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderListComponent, OrderListByUserComponent } from '.';
import { OrderService } from '../../shared/services';
import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';
import { OrderRoutingModule } from './order-manager-routing.module';
import { CartComponent } from './components/cart/cart.component';

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
    providers: [OrderService]
})
export class OrderManagerModule { }
