import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class PaymentService {

    pay(amount) {

        var handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_1XvcRG66xztreFUG2M8LKXTx00BVSuXbLs',
            locale: 'auto',
            token: function (token: any) {
                // You can access the token ID with `token.id`.
                // Get the token ID to your server-side code for use.
                console.log(token)
                alert('Token Created!!');
            }
        });

        handler.open({
            name: 'Demo Site',
            description: '2 widgets',
            amount: amount * 100
        });

    }
}
