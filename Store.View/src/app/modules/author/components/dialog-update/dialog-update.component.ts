import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog-update.component.html',
    styleUrls: ['./dialog-update.component.scss']
})
export class DialogUpdateComponent {
    constructor(
        public dialogRef: MatDialogRef<DialogUpdateComponent>,
        @Inject(MAT_DIALOG_DATA) public authorName?: string) {
    }

    onNoClick(): void {
        this.dialogRef.close();
    }
}
