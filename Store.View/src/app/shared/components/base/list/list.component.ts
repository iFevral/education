import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { BaseModel, BaseListModel, BaseFilterModel } from 'src/app/shared/models';
import { BaseService } from 'src/app/shared/services';
import { SortProperty } from 'src/app/shared/enums';


@Component({
    selector: 'app-home',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class ListComponent
    <ModelItem extends BaseModel,
    Model extends BaseListModel<ModelItem>,
    FilterModel extends BaseFilterModel,
    Service extends BaseService<Model>>
    implements OnInit {

    protected pageSizeOptions = [5, 10, 15, 20];
    protected pageSize = this.pageSizeOptions[0];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    protected dataModel: Model;
    protected filterModel: FilterModel;

    protected displayedColumns: string[];
    protected dataSource: MatTableDataSource<ModelItem>;
    protected dataService: Service;

    constructor(
        filterModel: FilterModel,
        dataService: Service) {

        this.dataService = dataService;
        this.filterModel = filterModel;
    }

    ngOnInit() {
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
}
