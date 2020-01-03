import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from 'src/app/modules/user-manager/user-manager-routing.module';

import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from 'src/app/shared/modules';

import { UserListComponent, UserDialogComponent } from 'src/app/modules/user-manager/index';
import { UserService } from 'src/app/shared/services';

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
