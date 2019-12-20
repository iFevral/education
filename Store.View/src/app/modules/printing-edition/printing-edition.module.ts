import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { PrintingEditionService, AccountService, CartService } from '../../shared/services';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';

import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialElementsModule } from '../../shared/modules';

import {
    PrintingEditionListComponent,
    CardComponent,
    DetailsComponent
} from '.';

@NgModule({
    declarations: [PrintingEditionListComponent, CardComponent, DetailsComponent],
    imports: [
        BrowserModule,
        MaterialElementsModule,
        PrintingEditionRoutingModule,
        FormsModule,
        FontAwesomeModule
    ],
    providers: [PrintingEditionService, CartService, AccountService]
})
export class PrintingEditionModule { }
