import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintingEditionManagerRoutingModule } from './printing-edition-manager-routing.module';
import { PrintingEditionService, AuthorService } from '../../shared/services';
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
        CommonModule,
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
