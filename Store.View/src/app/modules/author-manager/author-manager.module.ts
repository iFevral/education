import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorService } from '../../shared/services';
import { AuthorRoutingModule } from './author-manager-routing.module';

import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';

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
