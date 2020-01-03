import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorService } from 'src/app/shared/services';
import { AuthorRoutingModule } from 'src/app/modules/author-manager/author-manager-routing.module';

import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from 'src/app/shared/modules';

import {
    AuthorListComponent,
    AuthorDialogComponent
} from '.';

@NgModule({
    declarations: [AuthorListComponent, AuthorDialogComponent],
    imports: [
        CommonModule,
        AuthorRoutingModule,
        MaterialElementsModule,
        FormsModule
    ],
    entryComponents: [
        AuthorDialogComponent
    ],
    providers: [AuthorService]
})
export class AuthorManagerModule { }
