import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderListComponent } from '.';
import { OrderService } from '../../shared/services';
import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';
import { OrderRoutingModule } from './order-manager-routing.module';

@NgModule({
    declarations: [OrderListComponent],
    imports: [
        CommonModule,
        FormsModule,
        MaterialElementsModule,
        OrderRoutingModule
    ],
    providers: [OrderService]
})
export class OrderManagerModule { }
