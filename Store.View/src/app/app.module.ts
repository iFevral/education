import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/helpers';

import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialElementsModule } from './shared/modules';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AuthorManagerModule } from './modules/author-manager/author-manager.module';
import { AccountModule } from './modules/account/account.module';
import { OrderModule } from './modules/order/order.module';
import { PrintingEditionModule } from './modules/printing-edition/printing-edition.module';
import { PrintingEditionManagerModule } from './modules/printing-edition-manager/printing-edition-manager.module';


import { AppComponent } from './app.component';
import { HeaderComponent, FooterComponent, HomeComponent } from './shared/components/';
import { AccountService } from './shared/services';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
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
        AccountModule,
        AuthorManagerModule,
        OrderModule,
        PrintingEditionModule,
        PrintingEditionManagerModule
    ],
    providers: [
        AccountService,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
