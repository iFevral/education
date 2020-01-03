import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';

import { UserModel, UserFilterModel, UserModelItem } from 'src/app/shared/models';
import { UserService } from 'src/app/shared/services';

import { UserDialogComponent } from 'src/app/modules/user-manager/components/dialog/dialog.component';

import { Constants } from 'src/app/shared/constants/constants';
import { CRUDOperations, UserLockStatus } from 'src/app/shared/enums';
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
        private dialog: MatDialog,
    ) {
        super(new UserFilterModel(), userService);

        this.displayedColumns = Constants.displayedColumns.users;

        this.allStatuses = Constants.enumsAttributes.userLockStatuses;
        this.statuses = [
            UserLockStatus.Active,
            UserLockStatus.Blocked
        ];
        this.filterModel.lockStatuses = this.statuses;
    }

    public update(inputModel: UserModelItem): void {
        const dialogRef = this.dialog.open(UserDialogComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Update,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: UserModelItem) => {
                if (resultModel) {
                    this.dataService.update(resultModel)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public delete(inputModel: UserModelItem): void {
        const dialogRef = this.dialog.open(UserDialogComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Delete,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: UserModelItem) => {
                if (resultModel) {
                    this.dataService.delete(resultModel.id)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public setLockout(row: UserModelItem): void {
        this.dataService.setLockout(row.email, row.isLocked);
    }
}
