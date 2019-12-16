import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorService } from '../../shared/services';
import { AuthorRoutingModule } from './author-manager-routing.module';

import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';

import {
    AuthorListComponent,
    DialogCrudComponent
} from '.';

@NgModule({
    declarations: [AuthorListComponent, DialogCrudComponent],
    imports: [
        CommonModule,
        AuthorRoutingModule,
        MaterialElementsModule,
        FormsModule
    ],
    entryComponents: [
        DialogCrudComponent
    ],
    providers: [AuthorService]
})
export class AuthorManagerModule { }
