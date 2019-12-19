import { Component } from '@angular/core';
import { MatDialogRef, MatTableDataSource } from '@angular/material';
import { OrderService } from '../../../../shared/services';
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
    private displayedColumns: string[] = [
        'title',
        'price',
        'quantity',
        'sum',
        'control'
    ];

    private dataSource: MatTableDataSource<CartModelItem>;

    constructor(
        public dialogRef: MatDialogRef<CartComponent>,
        public orderService: OrderService
    ) {
        this.getCart();
    }

    onClick(): void {
        this.dialogRef.close();
    }

    public applyPurchasing() {
        this.orderService.applyPurchasing();
        this.onClick();
    }

    public removeItem(index: number) {
        this.orderService.removeProductFromCart(index);
    }

    public changeQuantity(cartItem: CartModelItem) {
        this.orderService.updateProductinCart(cartItem);
        this.getCart();
    }

    public getCart() {
        this.orderService.getProductsInCart().subscribe((resultModel: CartModel) => {
            this.dataSource = new MatTableDataSource(resultModel.items);
            this.cart = resultModel;
            this.total = 0;
            resultModel.items.forEach((element: CartModelItem) => {
                this.total += element.price * element.quantity;
            });
        });
    }
}
