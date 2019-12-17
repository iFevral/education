import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from './model/dialog-data.model';
import { CRUDOperations, UserLockStatus } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog-crud.component.html',
    styleUrls: ['./dialog-crud.component.scss']
})
export class DialogCrudComponent {

    private title: string;
    private isFormVisible: boolean;
    private passwordConfirmation: string;
    private allStatuses: Array<string>;
    private statuses: Array<UserLockStatus>;

    constructor(
        public dialogRef: MatDialogRef<DialogCrudComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData) {

        this.allStatuses = Constants.enumsAttributes.userLockStatuses;
        this.statuses = [
            UserLockStatus.Active,
            UserLockStatus.Blocked
        ];

        switch (data.type) {
            case CRUDOperations.Update:
                this.title = 'Update user';
                this.isFormVisible = true;
                break;
            case CRUDOperations.Delete:
                this.title = `Delete user "${data.model.firstName} ${data.model.lastName}"?`;
                this.isFormVisible = false;
                break;

        }
    }

    onNoClick(): void {
        this.dialogRef.close();
    }
}
