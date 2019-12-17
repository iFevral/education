import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { AuthorService } from '../../../../shared/services';
import { AuthorFilterModel, AuthorModel, AuthorModelItem } from '../../../../shared/models';
import { SortProperty, CRUDOperations } from '../../../../shared/enums';
import { MatDialog } from '@angular/material';
import { DialogCrudComponent } from '../dialog/dialog-crud.component';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class AuthorListComponent implements OnInit {

    private pageSizeOptions = [5, 10, 15, 20];
    private pageSize = this.pageSizeOptions[0];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    private authorModel: AuthorModel;
    private filterModel: AuthorFilterModel;

    private displayedColumns: string[] = ['id', 'name', 'printingEdition', 'control'];
    private dataSource: MatTableDataSource<AuthorModelItem>;


    constructor(
        private authorService: AuthorService,
        private dialog: MatDialog
    ) {
    }

    public ngOnInit() {
        this.filterModel = new AuthorFilterModel();
        this.filterModel.quantity = this.pageSize;

        this.authorService.getAll(this.filterModel).subscribe(data => {
            this.authorModel = data;
            this.dataSource = new MatTableDataSource(data.items);
            this.paginator.length = data.counter;
        });
    }

    public setAmountOfPrintingEdition() {
        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.filterModel.quantity = this.paginator.pageSize;
    }

    public create(): void {
        const newAuthorModel = new AuthorModelItem();
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Create,
                model: newAuthorModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: AuthorModelItem) => {
                if (resultModel) {
                    this.authorService.create(resultModel)
                        .subscribe(messageModel => {
                            this.authorService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public update(inputModel: AuthorModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Update,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: AuthorModelItem) => {
                if (resultModel) {
                    this.authorService.update(resultModel)
                        .subscribe(messageModel => {
                            this.authorService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public delete(inputModel: AuthorModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Delete,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: AuthorModelItem) => {
                if (resultModel) {
                    this.authorService.delete(resultModel.id)
                        .subscribe(messageModel => {
                            this.authorService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public setOrder(event): void {
        this.filterModel.IsAscending = this.sort.direction === 'desc'
            ? false : true;

        const sortProp: string = this.sort.active.charAt(0).toUpperCase() + this.sort.active.slice(1);
        this.filterModel.sortProperty = this.sort.direction !== ''
            ? SortProperty[sortProp]
            : SortProperty.Id;
    }

    public applyFilters() {
        this.authorService.getAll(this.filterModel).subscribe((data) => {
            this.dataSource = new MatTableDataSource(data.items);

            this.authorModel = data;
            this.paginator.length = data.counter;
        });
    }
}
