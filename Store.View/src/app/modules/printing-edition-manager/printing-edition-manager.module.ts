import { NgModule } from '@angular/core';
import { PrintingEditionManagerRoutingModule } from './printing-edition-manager-routing.module';
import { PrintingEditionService, AuthorService } from '../../shared/services';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialElementsModule } from '../../shared/modules';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
    PrintingEditionManagerListComponent,
    PrintingEditionDialogComponent
} from '.';

@NgModule({
    declarations: [PrintingEditionManagerListComponent, PrintingEditionDialogComponent],
    imports: [
        FormsModule,
        BrowserModule,
        FontAwesomeModule,
        MaterialElementsModule,
        PrintingEditionManagerRoutingModule
    ],
    entryComponents: [
        PrintingEditionDialogComponent
    ],
    providers: [
        PrintingEditionService,
        AuthorService
    ]
})
export class PrintingEditionManagerModule { }
