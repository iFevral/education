import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/helpers';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialElementsModule } from './shared/modules';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { OrderManagerModule } from './modules/order-manager/order-manager.module';

import { AppComponent } from './app.component';
import { HeaderComponent, FooterComponent } from './shared/components/';
import { AccountService } from './shared/services';
import { CartComponent } from './modules/order-manager';

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
