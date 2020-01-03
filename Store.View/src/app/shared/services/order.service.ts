import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base/base.service';
import { HttpClient } from '@angular/common/http';
import { Constants } from 'src/app/shared/constants/constants';
import { Observable } from 'rxjs';
import {
    OrderModel,
    BaseModel,
    PaymentModel,
    CartModel,
    CartModelItem,
    OrderModelItem,
    OrderItemModel,
    PrintingEditionModelItem
} from 'src/app/shared/models';
import { MatSnackBar } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class OrderService extends BaseService<OrderModel> {

    constructor(
        http: HttpClient,
        messageContainer: MatSnackBar
    ) {
        super(http, Constants.apiUrls.orderControllerUrl, messageContainer);
    }

    public addPaymentTransaction(paymentModel: PaymentModel): Observable<BaseModel> {
        return this.http.patch<PaymentModel>(Constants.apiUrls.orderControllerUrl, paymentModel);
    }

    public createOrder(cartModel: CartModel): void {
        const order = new OrderModelItem();
        order.orderItems = new Array<OrderItemModel>();

        cartModel.items.forEach((element: CartModelItem) => {
            const orderItem = new OrderItemModel();

            orderItem.printingEdition = new PrintingEditionModelItem();
            orderItem.printingEdition.id = element.productId;
            orderItem.amount = element.quantity;

            order.orderItems.push(orderItem);
        });

        this.create(order).subscribe((resultModel: BaseModel) => {
            if (resultModel.errors.length > 0) {
                this.showDialogMessage(resultModel.errors.toString());
            }
        });
    }
}
