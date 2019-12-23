import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-manager-routing.module';

import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';

import { UserListComponent, UserDialogComponent } from '.';
import { UserService } from '../../shared/services';

@NgModule({
    declarations: [UserListComponent, UserDialogComponent],
    imports: [
        CommonModule,
        FormsModule,
        MaterialElementsModule,
        UserRoutingModule
    ],
    entryComponents: [
        UserDialogComponent
    ],
    providers: [UserService]
})
export class UserManagerModule { }
