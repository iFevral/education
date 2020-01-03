import { Component } from '@angular/core';
import { MatDialogRef, MatTableDataSource } from '@angular/material';

import { OrderService, CartService } from '../../../../shared/services';

import { CartModel, CartModelItem } from '../../../../shared/models';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent {
    private title: string;
    private cart: CartModel;
    private total: number;
    private displayedColumns: string[];

    private dataSource: MatTableDataSource<CartModelItem>;

    constructor(
        private dialogRef: MatDialogRef<CartComponent>,
        private orderService: OrderService,
        private cartService: CartService
    ) {
        this.displayedColumns = [
            'title',
            'price',
            'quantity',
            'sum',
            'control'
        ];
        this.setCart();
    }

    public setCart(): void {
        this.cartService.getProductsInCart().subscribe((resultModel: CartModel) => {
            this.dataSource = new MatTableDataSource(resultModel.items);
            this.cart = resultModel;
            this.total = 0;
            resultModel.items.forEach((element: CartModelItem) => {
                this.total += element.price * element.quantity;
            });
        });
    }

    public updateItem(cartItem: CartModelItem): void {
        this.cartService.update(cartItem);
        this.setCart();
    }

    public removeItem(index: number): void {
        this.cartService.remove(index);
    }

    public applyPurchasing(): void {
        this.orderService.createOrder(this.cart);
        this.cartService.clear();
        this.close();
    }

    public close(): void {
        this.dialogRef.close();
    }
}
