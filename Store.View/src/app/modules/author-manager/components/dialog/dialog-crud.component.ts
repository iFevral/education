import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from './model/dialog-data.model';
import { CRUDOperations } from '../../../../shared/enums';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog-crud.component.html',
    styleUrls: ['./dialog-crud.component.scss']
})
export class DialogCrudComponent {
    private title: string;
    private isFormVisible: boolean;
    constructor(
        public dialogRef: MatDialogRef<DialogCrudComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData) {
        switch (data.type) {
            case CRUDOperations.Create:
                this.title = 'Add new author';
                this.isFormVisible = true;
                break;
            case CRUDOperations.Update:
                this.title = 'Update author';
                this.isFormVisible = true;
                break;
            case CRUDOperations.Delete:
                this.title = `Delete "${data.model.name}"?`;
                this.isFormVisible = false;
                break;
        }
    }

    onNoClick(): void {
        this.dialogRef.close();
    }
}
