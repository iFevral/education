import { Component } from '@angular/core';

import { Constants } from 'src/app/shared/constants/constants';
import { OrderService } from 'src/app/shared/services';
import { OrderStatus } from 'src/app/shared/enums';

import { OrderModel, OrderFilterModel, OrderModelItem } from 'src/app/shared/models';
import { ListComponent } from 'src/app/shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.scss']
})
export class OrderListComponent extends ListComponent<OrderModelItem, OrderModel, OrderFilterModel, OrderService> {
    private allTypes: Array<string>;
    private allStatuses: Array<string>;
    private statuses: Array<OrderStatus>;

    constructor(
        orderService: OrderService
    ) {
        super(new OrderFilterModel(), orderService);
        
        this.displayedColumns = Constants.displayedColumns.orders;

        this.allStatuses = Constants.enumsAttributes.orderStatuses;
        
        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.statuses = [
            OrderStatus.Paid,
            OrderStatus.Unpaid
        ];
        this.filterModel.IsAscending = false;
        this.filterModel.statuses = this.statuses;
    }
}
