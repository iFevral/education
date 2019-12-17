import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-manager-routing.module';

import { FormsModule } from '@angular/forms';
import { MaterialElementsModule } from '../../shared/modules';

import { UserListComponent, DialogCrudComponent } from '.';
import { UserService } from '../../shared/services';

@NgModule({
    declarations: [UserListComponent, DialogCrudComponent],
    imports: [
        CommonModule,
        FormsModule,
        MaterialElementsModule,
        UserRoutingModule
    ],
    entryComponents: [
        DialogCrudComponent
    ],
    providers: [UserService]
})
export class UserManagerModule { }
