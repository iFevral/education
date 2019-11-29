import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialElementsModule } from '../../shared/modules/material-elements/material-elements.module';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';

import { PrintingEditionService } from '../../shared/services/printing-edition.service';

import { ListComponent } from './pages/list/list.component';

@NgModule({
    declarations: [ListComponent],
    imports: [
        BrowserModule,
        MaterialElementsModule,
        PrintingEditionRoutingModule
    ],
    providers: [PrintingEditionService]
})
export class PrintingEditionModule { }
