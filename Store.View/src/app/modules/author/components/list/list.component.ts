import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { AuthorService } from '../../../../shared/services';
import { AuthorFilterModel, AuthorModel, AuthorModelItem } from '../../../../shared/models';
import { SortProperty } from '../../../../shared/enums';
import { MatDialog } from '@angular/material';
import { DialogUpdateComponent } from '../dialog-update/dialog-update.component';

export interface UserData {
    id: string;
    name: string;
    progress: string;
    color: string;
}

/** Constants used to fill up our data base. */
const COLORS: string[] = [
    'maroon', 'red', 'orange', 'yellow', 'olive', 'green', 'purple', 'fuchsia', 'lime', 'teal',
    'aqua', 'blue', 'navy', 'black', 'gray'
];
const NAMES: string[] = [
    'Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack', 'Charlotte', 'Theodore', 'Isla', 'Oliver',
    'Isabella', 'Jasper', 'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'
];

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class AuthorListComponent implements OnInit {

    private pageSize = 2;
    private pageSizeOptions = [2, 3, 5, 10];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    private authorModel: AuthorModel;
    private filterModel: AuthorFilterModel;

    private displayedColumns: string[] = ['id', 'name', 'printingEdition', 'control'];
    private dataSource: MatTableDataSource<AuthorModelItem>;


    constructor(private authorService: AuthorService, private dialog: MatDialog) {
    }

    ngOnInit() {
        this.filterModel = new AuthorFilterModel();
        this.filterModel.quantity = this.pageSize;

        this.authorService.getAll(this.filterModel).subscribe(data => {
            this.authorModel = data;
            this.dataSource = new MatTableDataSource(data.items);
            this.paginator.length = data.counter;
            console.log(this.paginator.length);
        });
    }

    public setAmountOfPrintingEdition() {
        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.filterModel.quantity = this.paginator.pageSize;
    }

    create(): void {
        const dialogRef = this.dialog.open(DialogUpdateComponent, {
            width: '250px'
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                const author: AuthorModelItem = new AuthorModelItem();
                author.name = result;
                this.authorService.create(author).subscribe(result => {
                    console.log(result);
                });
            }
            console.log(result);
        });
    }

    update(row: AuthorModelItem): void {
        const dialogRef = this.dialog.open(DialogUpdateComponent, {
            width: '250px',
            data: row.name
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                row.name = result;
                this.authorService.update(row).subscribe(result => {
                    console.log(result);
                });
            }
            console.log(result);
        });
    }

    public setOrder(event) {
        console.log(event);
        this.filterModel.IsAscending = this.sort.direction === 'desc'
            ? false : true;

        const sortProp: string = this.sort.active.charAt(0).toUpperCase() + this.sort.active.slice(1);

        this.filterModel.sortProperty = sortProp === ''
            ? SortProperty[sortProp]
            : SortProperty.Id;
    }

    applyFilters() {
        this.authorService.getAll(this.filterModel).subscribe((data) => {
            this.dataSource = new MatTableDataSource(data.items);

            this.authorModel = data;
            this.paginator.length = data.counter;
        });
    }
}