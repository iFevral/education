import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';

import { UserModel, UserFilterModel, UserModelItem } from '../../../../shared/models';
import { UserService } from '../../../../shared/services';

import { DialogCrudComponent } from '../dialog/dialog-crud.component';

import { Constants } from '../../../../shared/constants/constants';
import { CRUDOperations, SortProperty, UserLockStatus } from '../../../../shared/enums';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class UserListComponent implements OnInit {
    private pageSizeOptions = [5, 10, 15, 20];
    private pageSize = this.pageSizeOptions[0];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    private userModel: UserModel;
    private filterModel: UserFilterModel;

    private displayedColumns: string[] = [
        'id',
        'userName',
        'email',
        'status',
        'control'
    ];

    private dataSource: MatTableDataSource<UserModelItem>;
    private allStatuses: Array<string>;
    private statuses: Array<UserLockStatus>;

    constructor(
        private userService: UserService,
        private dialog: MatDialog,
    ) {
        this.allStatuses = Constants.enumsAttributes.userLockStatuses;
        this.statuses = [
            UserLockStatus.Active,
            UserLockStatus.Blocked
        ];
    }

    public ngOnInit() {
        this.filterModel = new UserFilterModel();
        this.filterModel.quantity = this.pageSize;
        this.filterModel.lockStatuses = [
            UserLockStatus.Active,
            UserLockStatus.Blocked
        ];
        this.userService.getAll(this.filterModel).subscribe(data => {
            this.userModel = data;
            this.dataSource = new MatTableDataSource(data.items);
            this.paginator.length = data.counter;
        });
    }

    public setAmountOfPrintingEdition() {
        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.filterModel.quantity = this.paginator.pageSize;
    }

    public update(inputModel: UserModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Update,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: UserModelItem) => {
                if (resultModel) {
                    this.userService.update(resultModel)
                        .subscribe(messageModel => {
                            this.userService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public delete(inputModel: UserModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Delete,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: UserModelItem) => {
                if (resultModel) {
                    this.userService.delete(resultModel.id)
                        .subscribe(messageModel => {
                            this.userService.showDialogMessage(messageModel.errors.toString());
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

    public setLockout(row: UserModelItem): void {
        this.userService.setLockout(row.email, row.isLocked).subscribe(messageModel => {
            this.userService.showDialogMessage(messageModel.errors.toString());
        });
    }


    public applyFilters() {
        this.userService.getAll(this.filterModel).subscribe((data) => {
            this.dataSource = new MatTableDataSource(data.items);
            this.userModel = data;
            this.paginator.length = data.counter;
        });
    }
}
