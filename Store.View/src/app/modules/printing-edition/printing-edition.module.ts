import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MaterialElementsModule } from '../../shared/modules';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';
import { ListComponent, CardComponent } from '.';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DetailsComponent } from './components/details/details.component';

@NgModule({
    declarations: [ListComponent, CardComponent, DetailsComponent],
    imports: [
        BrowserModule,
        MaterialElementsModule,
        PrintingEditionRoutingModule,
        FormsModule,
        FontAwesomeModule
    ],
})
export class PrintingEditionModule { }
