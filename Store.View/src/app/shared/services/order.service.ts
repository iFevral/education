import { Injectable } from '@angular/core';
import { BaseService } from './base/base.service';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../constants/constants';
import { Observable } from 'rxjs';
import { OrderModel, OrderFilterModel, OrderModelItem } from '../models';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class OrderService extends BaseService {

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.orderControllerUrl);
    }

    public getAll(filterModel: OrderFilterModel): Observable<OrderModel> {
        return this.http.post<OrderModel>(this.url, filterModel)
            .pipe(map((resultModel: OrderModel) => {
                resultModel.items.forEach((element: OrderModelItem) => {
                    const date = new Date(element.date);
                    element.date = date.toDateString();
                });
                return resultModel;
            }));
    }
}
