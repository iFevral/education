import { Component } from '@angular/core';
import { OrderModel, OrderFilterModel, OrderModelItem } from '../../../../shared/models';
import { OrderService } from '../../../../shared/services';
import { OrderStatus } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';
import { ListComponent } from '../../../../shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class OrderListComponent extends ListComponent<OrderModelItem, OrderModel, OrderFilterModel, OrderService> {
    private allTypes: Array<string>;
    private allStatuses: Array<string>;
    private statuses: Array<OrderStatus>;

    constructor(
        orderService: OrderService
    ) {
        super(new OrderFilterModel(), orderService);
        this.displayedColumns = [
            'id',
            'date',
            'userName',
            'email',
            'productType',
            'title',
            'quantity',
            'orderPrice',
            'status'
        ];
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
