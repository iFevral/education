import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';

import { UserModel, UserFilterModel, UserModelItem } from 'src/app/shared/models';
import { UserService } from 'src/app/shared/services';

import { UserDialogComponent } from 'src/app/modules/user-manager/components/dialog/dialog.component';

import { Constants } from 'src/app/shared/constants/constants';
import { UserLockStatus } from 'src/app/shared/enums';
import { ListComponent } from 'src/app/shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.scss']
})
export class UserListComponent extends ListComponent<UserModelItem, UserModel, UserFilterModel, UserService> {
    private allStatuses: Array<string>;
    private statuses: Array<UserLockStatus>;
    constructor(
        userService: UserService,
        dialog: MatDialog,
    ) {
        super(new UserFilterModel(), userService, dialog);

        this.dialogType = UserDialogComponent;
        this.dialogWidth = '400px';

        this.displayedColumns = Constants.displayedColumns.users;

        this.allStatuses = Constants.enumsKeys.userLockStatuses;
        this.statuses = Constants.enumsValues.userLockStatuses;

        this.filterModel.lockStatuses = this.statuses;

    }

    public setLockout(row: UserModelItem): void {
        this.dataService.setLockout(row.email, row.isLocked);
    }
}
