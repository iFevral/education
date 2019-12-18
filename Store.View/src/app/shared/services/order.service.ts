import { Injectable } from '@angular/core';
import { BaseService } from './base/base.service';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../constants/constants';
import { Observable } from 'rxjs';
import { OrderModel, BaseModel, PaymentModel } from '../models';

@Injectable({
    providedIn: 'root'
})
export class OrderService extends BaseService<OrderModel> {

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.orderControllerUrl);
    }

    public addPaymentTransaction(paymentModel: PaymentModel): Observable<BaseModel> {
        return this.http.patch<PaymentModel>(Constants.apiUrls.orderControllerUrl, paymentModel);
    }
}
