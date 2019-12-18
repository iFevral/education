import { Component, OnInit } from '@angular/core';
import { PageAnimation } from './shared/helpers/page-animation';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    animations: [PageAnimation]
})
export class AppComponent implements OnInit {
    title = 'education-app';

    public ngOnInit() {
        this.loadStripe();
    }

    public loadStripe() {
        if (!window.document.getElementById('stripe-script')) {
            const stripe = window.document.createElement('script');
            stripe.id = 'stripe-script';
            stripe.type = 'text/javascript';
            stripe.src = 'https://checkout.stripe.com/checkout.js';
            window.document.body.appendChild(stripe);
        }
    }
}