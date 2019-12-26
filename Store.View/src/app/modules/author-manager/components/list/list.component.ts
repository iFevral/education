import { Component } from '@angular/core';
import { AuthorService } from '../../../../shared/services';
import { AuthorFilterModel, AuthorModel, AuthorModelItem } from '../../../../shared/models';
import { CRUDOperations } from '../../../../shared/enums';
import { MatDialog } from '@angular/material';
import { AuthorDialogComponent } from '../dialog/dialog.component';
import { ListComponent } from '../../../../shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class AuthorListComponent extends ListComponent<AuthorModelItem, AuthorModel, AuthorFilterModel, AuthorService> {

    constructor(
        authorService: AuthorService,
        private dialog: MatDialog
    ) {
        super(new AuthorFilterModel(), authorService);
        this.displayedColumns = [
            'name',
            'printingEdition',
            'control'
        ];
    }

    public create(): void {
        const newAuthorModel = new AuthorModelItem();
        const dialogRef = this.dialog.open(AuthorDialogComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Create,
                model: newAuthorModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: AuthorModelItem) => {
                if (resultModel) {
                    this.dataService.create(resultModel)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public update(inputModel: AuthorModelItem): void {
        const dialogRef = this.dialog.open(AuthorDialogComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Update,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: AuthorModelItem) => {
                if (resultModel) {
                    this.dataService.update(resultModel)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public delete(inputModel: AuthorModelItem): void {
        const dialogRef = this.dialog.open(AuthorDialogComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Delete,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: AuthorModelItem) => {
                if (resultModel) {
                    this.dataService.delete(resultModel.id)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }
}
