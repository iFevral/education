import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CRUDOperations } from '../../../../shared/enums';
import { DialogData, BaseModel } from '../../../../shared/models';
import { Constants } from '../../../constants/constants';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog-crud.component.html',
    styleUrls: ['./dialog-crud.component.scss']
})
export class DialogCrudComponent<ModelItem extends BaseModel> implements OnInit {
    protected title: string;
    protected isFormVisible: boolean;
    protected titles: Array<string>;

    constructor(
        public dialogRef: MatDialogRef<DialogCrudComponent<ModelItem>>,
        @Inject(MAT_DIALOG_DATA) protected data: DialogData<ModelItem>
    ) {
        this.titles = Constants.enumsAttributes.crudOperations;

    }

    public ngOnInit(): void {
        switch (this.data.type) {
            case CRUDOperations.Create:
                this.title = this.titles[CRUDOperations.Create];
                this.isFormVisible = true;
                break;
            case CRUDOperations.Update:
                this.title = this.titles[CRUDOperations.Update];
                this.isFormVisible = true;
                break;
            case CRUDOperations.Delete:
                this.title = this.titles[CRUDOperations.Delete];
                this.isFormVisible = false;
                break;
        }
    }
    public onCancelClick(): void {
        this.dialogRef.close();
    }
}
