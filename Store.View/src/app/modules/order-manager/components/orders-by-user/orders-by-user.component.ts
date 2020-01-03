import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Constants } from 'src/app/shared/constants/constants';
import { OrderStatus } from 'src/app/shared/enums';
import { OrderModel, OrderFilterModel, OrderModelItem, PaymentModel, BaseModel } from 'src/app/shared/models';

import { OrderService } from 'src/app/shared/services';
import { ListComponent } from 'src/app/shared/components/base';

@Component({
    selector: 'app-list-by-user',
    templateUrl: './orders-by-user.component.html',
    styleUrls: ['./orders-by-user.component.scss']
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

        this.displayedColumns = Constants.displayedColumns.ordersByUser;

        this.allTypes = Constants.enumsKeys.printingEditionTypes;
        this.allStatuses = Constants.enumsKeys.orderStatuses;
        this.statuses = Constants.enumsValues.orderStatuses;

        const userId = parseInt(this.route.snapshot.paramMap.get('id'), 10);
        this.filterModel.IsAscending = false;
        this.filterModel.statuses = this.statuses;
        this.filterModel.userId = isNaN(userId)
            ? null
            : userId;
    }

    public pay(order: OrderModelItem): void {

        const handler = (window as any).StripeCheckout.configure({
            key: Constants.stripeConfig.key,
            locale: Constants.stripeConfig.locale,
            token: (paymentLog: any) => {

                const paymentModel: PaymentModel = new PaymentModel();
                paymentModel.orderId = order.id;
                paymentModel.transactionId = paymentLog.id;

                this.orderService.addPaymentTransaction(paymentModel).subscribe((resultModel: BaseModel) => {
                    if (resultModel.errors.length === 0) {
                        this.applyFilters();
                    }
                });
            }
        });

        handler.open({
            name: Constants.stripeConfig.title,
            description: `Payment for order №${order.id}`,
            amount: order.orderPrice * Constants.stripeConfig.amount
        });

    }
}
