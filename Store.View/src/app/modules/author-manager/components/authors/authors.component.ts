import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';

import { Constants } from 'src/app/shared/constants/constants';
import { AuthorService } from 'src/app/shared/services';
import { AuthorFilterModel, AuthorModel, AuthorModelItem } from 'src/app/shared/models';

import { AuthorDialogComponent } from 'src/app/modules/author-manager/components/dialog/dialog.component';
import { ListComponent } from 'src/app/shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './authors.component.html',
    styleUrls: ['./authors.component.scss']
})
export class AuthorListComponent extends ListComponent<AuthorModelItem, AuthorModel, AuthorFilterModel, AuthorService> {

    constructor(
        authorService: AuthorService,
        dialog: MatDialog
    ) {
        super(new AuthorFilterModel(), authorService, dialog);

        this.dialogType = AuthorDialogComponent;
        this.dialogWidth = '400px';

        this.displayedColumns = Constants.displayedColumns.authors;
    }

    public createAuthor(): void {
        const newAuthorModel = new AuthorModelItem();
        this.create(newAuthorModel);
    }
}
