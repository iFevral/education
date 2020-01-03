import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { DialogData, AuthorModelItem } from 'src/app/shared/models';
import { DialogCrudComponent } from 'src/app/shared/components/base';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog.component.html',
    styleUrls: ['./dialog.component.scss']
})
export class AuthorDialogComponent extends DialogCrudComponent<AuthorModelItem>  {

    constructor(
        dialogRef: MatDialogRef<AuthorDialogComponent>,
        @Inject(MAT_DIALOG_DATA) protected data: DialogData<AuthorModelItem>
    ) {
        super(dialogRef, data);

        this.titles = [``,
            `Create author`,
            `Update author`,
            `Delete author "${data.model.name}"?`
        ];
    }
}
