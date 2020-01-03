import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from 'src/app/shared/helpers';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialElementsModule } from 'src/app/shared/modules';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { OrderManagerModule } from 'src/app/modules/order-manager/order-manager.module';

import { AppComponent } from 'src/app/app.component';
import { HeaderComponent, FooterComponent } from 'src/app/shared/components/';
import { AccountService, CartService } from 'src/app/shared/services';
import { CartComponent } from 'src/app/modules/order-manager';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        FooterComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        FontAwesomeModule,
        BrowserAnimationsModule,
        MaterialElementsModule,
        OrderManagerModule
    ],
    entryComponents: [
        CartComponent
    ],
    providers: [
        AccountService,
        CartService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            multi: true
        }
    ],

    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
