import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AccountRoutingModule } from './account-routing.module';

import { SignInComponent, SignUpComponent } from '.';
import { AccountService } from '../../shared/services';


@NgModule({
  imports: [
    FormsModule,
    BrowserModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    MaterialElementsModule,
    FontAwesomeModule
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
  ],
  providers: [AccountService]
})
export class AccountModule { }
