import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from 'src/app/shared/modules';

import { PrintingEditionService, AccountService, CartService } from 'src/app/shared/services';
import { PrintingEditionRoutingModule } from 'src/app/modules/printing-edition/printing-edition-routing.module';

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
        FormsModule
    ],
    providers: [PrintingEditionService, CartService, AccountService]
})
export class PrintingEditionModule { }
