import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CRUDOperations, UserLockStatus } from 'src/app/shared/enums';
import { Constants } from 'src/app/shared/constants/constants';
import { DialogData, UserModelItem } from 'src/app/shared/models';
import { DialogCrudComponent } from 'src/app/shared/components/base';

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
