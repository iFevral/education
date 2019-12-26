import { Component } from '@angular/core';
import { OrderModel, OrderFilterModel, OrderModelItem, PaymentModel } from '../../../../shared/models';
import { OrderService } from '../../../../shared/services';
import { OrderStatus } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';
import { ListComponent } from '../../../../shared/components/base';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-list-by-user',
    templateUrl: './list-by-user.component.html',
    styleUrls: ['./list-by-user.component.scss']
})
export class OrderListByUserComponent extends ListComponent<OrderModelItem, OrderModel, OrderFilterModel, OrderService> {
    private allTypes: Array<string>;
    private allStatuses: Array<string>;
    private statuses: Array<OrderStatus>;

    constructor(
        private route: ActivatedRoute,
        private orderService: OrderService
    ) {
        super(new OrderFilterModel(), orderService);

        this.displayedColumns = [
            'id',
            'date',
            'productType',
            'title',
            'quantity',
            'amount',
            'status'
        ];
        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.allStatuses = Constants.enumsAttributes.orderStatuses;
        this.statuses = [
            OrderStatus.Paid,
            OrderStatus.Unpaid
        ];

        const userId = parseInt(this.route.snapshot.paramMap.get('id'), 10);
        this.filterModel.IsAscending = false;
        this.filterModel.statuses = this.statuses;
        this.filterModel.userId = isNaN(userId)
            ? null
            : userId;
    }

    public pay(order: OrderModelItem) {

        const handler = (window as any).StripeCheckout.configure({
            key: 'pk_test_1XvcRG66xztreFUG2M8LKXTx00BVSuXbLs',
            locale: 'auto',
            token: (paymentLog: any) => {

                const paymentModel: PaymentModel = new PaymentModel();
                paymentModel.orderId = order.id;
                paymentModel.transactionId = paymentLog.id;
                this.orderService.addPaymentTransaction(paymentModel).subscribe(data => {
                    if (data.errors.length === 0) {
                        this.applyFilters();
                    }
                });
            }
        });

        handler.open({
            name: 'Add payment',
            description: `Payment for order №${order.id}`,
            amount: order.orderPrice * 100
        });

    }
}
