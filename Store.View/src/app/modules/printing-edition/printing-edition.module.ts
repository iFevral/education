import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MaterialElementsModule } from '../../shared/modules';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';
import { ListComponent, CardComponent } from '.';

@NgModule({
    declarations: [ListComponent, CardComponent],
    imports: [
        BrowserModule,
        MaterialElementsModule,
        PrintingEditionRoutingModule,
    ],
})
export class PrintingEditionModule { }
