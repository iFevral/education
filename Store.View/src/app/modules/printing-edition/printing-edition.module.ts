import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintingEditionService, AccountService, CartService } from '../../shared/services';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';

import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialElementsModule } from '../../shared/modules';

import {
    PrintingEditionListComponent,
    ProductCardComponent,
    DetailsComponent
} from '.';

@NgModule({
    declarations: [PrintingEditionListComponent, ProductCardComponent, DetailsComponent],
    imports: [
        CommonModule,
        MaterialElementsModule,
        PrintingEditionRoutingModule,
        FormsModule,
        FontAwesomeModule
    ],
    providers: [PrintingEditionService, CartService, AccountService]
})
export class PrintingEditionModule { }
