import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartModel, CartModelItem } from 'src/app/shared/models';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    private cart: BehaviorSubject<CartModel>;

    constructor() {
        let cartModel: CartModel = JSON.parse(localStorage.getItem('cart'));
        if (!cartModel) {
            cartModel = new CartModel();
            cartModel.items = new Array<CartModelItem>();
        }

        this.cart = new BehaviorSubject<CartModel>(cartModel);
    }

    public getProductsInCart(): Observable<CartModel> {
        return this.cart.asObservable();
    }

    public add(newCartItem: CartModelItem): void {
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

    public update(cartItem: CartModelItem): void {
        const newСart: CartModel = this.cart.value;

        newСart.items.forEach((element: CartModelItem) => {
            if (element.productId === cartItem.productId) {
                element.quantity = cartItem.quantity;
            }
        });

        this.cart.next(newСart);
        localStorage.setItem('cart', JSON.stringify(newСart));
    }

    public remove(index: number): void {
        const newСart: CartModel = this.cart.value;
        newСart.items.splice(index, 1);

        this.cart.next(newСart);
        localStorage.setItem('cart', JSON.stringify(newСart));
    }

    public clear(): void {
        const newCart = new CartModel();
        newCart.items = new Array<CartModelItem>();
        this.cart.next(newCart);
        localStorage.setItem('cart', JSON.stringify(newCart));
    }
}
