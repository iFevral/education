import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { MaterialElementsModule } from './shared/modules/material-elements/material-elements.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AccountModule } from './modules/account/account.module';
import { PrintingEditionModule } from './modules/printing-edition/printing-edition.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { HeaderComponent } from './shared/pages/header/header.component';
import { FooterComponent } from './shared/pages/footer/footer.component';

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
    BrowserAnimationsModule,
    MaterialElementsModule,
    AccountModule,
    PrintingEditionModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
