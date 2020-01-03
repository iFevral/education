import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MaterialElementsModule } from 'src/app/shared/modules';
import { PrintingEditionManagerRoutingModule } from 'src/app/modules/printing-edition-manager/printing-edition-manager-routing.module';
import { PrintingEditionService, AuthorService } from 'src/app/shared/services';

import {
    PrintingEditionManagerListComponent,
    PrintingEditionDialogComponent
} from 'src/app/modules/printing-edition-manager/index';

@NgModule({
    declarations: [PrintingEditionManagerListComponent, PrintingEditionDialogComponent],
    imports: [
        FormsModule,
        CommonModule,
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
