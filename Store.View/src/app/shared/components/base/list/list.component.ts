import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { BaseItemModel, BaseListModel, BaseFilterModel } from 'src/app/shared/models';
import { BaseService } from 'src/app/shared/services';
import { SortProperty, CRUDOperations } from 'src/app/shared/enums';
import { Observable } from 'rxjs';


@Component({
    selector: 'app-home',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class ListComponent
    <ModelItem extends BaseItemModel,
    Model extends BaseListModel<ModelItem>,
    FilterModel extends BaseFilterModel,
    Service extends BaseService<Model>>
    implements OnInit {

    protected pageSizeOptions = [5, 10, 15, 20];
    protected pageSize = this.pageSizeOptions[0];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    protected dataModel: Model;

    protected displayedColumns: string[];
    protected dataSource: MatTableDataSource<ModelItem>;

    protected dialogType: any;
    protected dialogWidth: string;

    constructor(
        protected filterModel: FilterModel,
        protected dataService: Service,
        protected dialog?: MatDialog
    ) {
        this.dialogWidth = '400px';
    }

    public ngOnInit() {
        this.paginator.pageSize = this.pageSizeOptions[0];
        this.filterModel.quantity = this.pageSize;

        this.applyFilters();
    }

    public setAmountOfItems() {
        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.filterModel.quantity = this.paginator.pageSize;
    }

    public setOrder(event): void {
        this.filterModel.IsAscending = this.sort.direction === 'desc'
            ? false : true;

        const sortProp: string = this.sort.active.charAt(0).toUpperCase() + this.sort.active.slice(1);
        this.filterModel.sortProperty = this.sort.direction !== ''
            ? SortProperty[sortProp]
            : SortProperty.Id;
    }

    public applyFilters(event?) {
        this.paginator.pageIndex = event
            ? this.paginator.pageIndex
            : 0;

        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;

        this.dataService.getAll<FilterModel>(this.filterModel).subscribe((data: Model) => {
            this.setData(data);
        });
    }

    private setData(data: Model) {
        this.dataSource = new MatTableDataSource(data.items);
        this.dataModel = data;
        this.paginator.length = data.counter;
    }

    public create(newModel: ModelItem): void {

        this.showDialog(CRUDOperations.Create, newModel)
            .subscribe((resultModel: ModelItem) => {
                if (!resultModel) {
                    return;
                }

                this.dataService.create(resultModel)
                    .subscribe(messageModel => {
                        this.dataService.showDialogMessage(messageModel.errors.toString());
                        this.applyFilters();
                    });

            });
    }

    public update(inputModel: ModelItem): void {

        this.showDialog(CRUDOperations.Update, inputModel)
            .subscribe((resultModel: ModelItem) => {
                if (!resultModel) {
                    return;
                }

                this.dataService.update(resultModel)
                    .subscribe(messageModel => {
                        this.dataService.showDialogMessage(messageModel.errors.toString());
                        this.applyFilters();
                    });
            });
    }

    public delete(inputModel: ModelItem): void {

        this.showDialog(CRUDOperations.Delete, inputModel)
            .subscribe((resultModel: ModelItem) => {
                if (!resultModel) {
                    return;
                }

                this.dataService.delete(resultModel.id)
                    .subscribe(messageModel => {
                        this.dataService.showDialogMessage(messageModel.errors.toString());
                        this.applyFilters();
                    });
            });
    }

    private showDialog(operationType: CRUDOperations, inputModel: ModelItem): Observable<any> {
        const dialogRef = this.dialog.open(this.dialogType, {
            width: this.dialogWidth,
            data: {
                type: operationType,
                model: inputModel
            }
        });

        return dialogRef.afterClosed();
    }
}
