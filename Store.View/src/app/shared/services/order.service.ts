import { Injectable } from '@angular/core';
import { BaseService } from './base/base.service';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../constants/constants';
import { Observable, BehaviorSubject } from 'rxjs';
import { OrderModel, BaseModel, PaymentModel, CartModel, CartModelItem, OrderModelItem, OrderItemModel, PrintingEditionModelItem } from '../models';

@Injectable({
    providedIn: 'root'
})
export class OrderService extends BaseService<OrderModel> {

    private cart: BehaviorSubject<CartModel> = new BehaviorSubject<CartModel>(JSON.parse(localStorage.getItem('cart')));

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.orderControllerUrl);
        let cartModel: CartModel = JSON.parse(localStorage.getItem('cart'));
        if (!cartModel) {
            cartModel = new CartModel();
            cartModel.items = new Array<CartModelItem>();
        }

        this.cart = new BehaviorSubject<CartModel>(cartModel);
    }

    public addPaymentTransaction(paymentModel: PaymentModel): Observable<BaseModel> {
        return this.http.patch<PaymentModel>(Constants.apiUrls.orderControllerUrl, paymentModel);
    }

    public getProductsInCart(): BehaviorSubject<CartModel> {
        return this.cart;
    }

    public addProductToCart(newCartItem: CartModelItem) {
        const newСart: CartModel = this.cart.value;
        let isNewItemUpdated = false;

        newСart.items.forEach((element: CartModelItem) => {
            if (element.productId === newCartItem.productId && !isNewItemUpdated) {
                element.quantity += newCartItem.quantity;
                isNewItemUpdated = true;
            }
        });

        if (!isNewItemUpdated) {
            newСart.items.push(newCartItem);
        }

        this.cart.next(newСart);
        localStorage.setItem('cart', JSON.stringify(newСart));
    }

    public updateProductinCart(cartItem: CartModelItem) {
        const newСart: CartModel = this.cart.value;

        newСart.items.forEach((element: CartModelItem) => {
            if (element.productId === cartItem.productId) {
                element.quantity = cartItem.quantity;
            }
        });

        this.cart.next(newСart);
        localStorage.setItem('cart', JSON.stringify(newСart));
    }

    public removeProductFromCart(index: number) {
        const newСart: CartModel = this.cart.value;
        newСart.items.splice(index, 1);

        this.cart.next(newСart);
        localStorage.setItem('cart', JSON.stringify(newСart));
    }


    public applyPurchasing() {
        const order = new OrderModelItem();
        order.orderItems = new Array<OrderItemModel>();

        this.cart.value.items.forEach((element: CartModelItem) => {
            const orderItem = new OrderItemModel();
            orderItem.printingEdition = new PrintingEditionModelItem();
            orderItem.printingEdition.id = element.productId;
            orderItem.amount = element.quantity;
            order.orderItems.push(orderItem);
        });
        console.log(order);

        this.create(order).subscribe(data => {
            console.log(data);
        });

        const cartModel = new CartModel();
        cartModel.items = new Array<CartModelItem>();
        this.cart.next(cartModel);
        localStorage.removeItem('cart');
    }
}
