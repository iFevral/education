import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountRoutingModule } from './account-routing.module';
import { MaterialElementsModule } from '../../shared/modules/material-elements/material-elements.module';

@NgModule({
  imports: [
    FormsModule,
    BrowserModule,
    AccountRoutingModule,
    MaterialElementsModule,
    ReactiveFormsModule
  ],
  declarations: [
    SignUpComponent,
    SignInComponent
  ],
  bootstrap: [
    SignInComponent
  ],
  exports: [
    SignUpComponent,
    SignInComponent
  ]
})
export class AccountModule { }
