import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CRUDOperations, UserLockStatus } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';
import { DialogData, UserModelItem } from '../../../../shared/models';
import { DialogCrudComponent } from '../../../../shared/components/base';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog.component.html',
    styleUrls: ['./dialog.component.scss']
})
export class UserDialogComponent extends DialogCrudComponent<UserModelItem> {

    private passwordConfirmation: string;
    private allStatuses: Array<string>;
    private statuses: Array<UserLockStatus>;

    constructor(
        dialogRef: MatDialogRef<UserDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data: DialogData<UserModelItem>
    ) {
        super(dialogRef, data);

        this.allStatuses = Constants.enumsAttributes.userLockStatuses;
        this.statuses = [
            UserLockStatus.Active,
            UserLockStatus.Blocked
        ];

        this.titles = [``,
            `Create user`,
            `Update user`,
            `Delete user "${data.model.firstName} ${data.model.lastName}"?`
        ];
    }
}
