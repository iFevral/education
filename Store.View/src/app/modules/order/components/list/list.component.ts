import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { OrderModel, OrderFilterModel, OrderModelItem } from '../../../../shared/models';
import { OrderService } from '../../../../shared/services';
import { SortProperty, OrderStatus, PrintingEditionType } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class OrderListComponent implements OnInit {
    private allStatuses: Array<string>;
    private statuses: Array<OrderStatus>;
    private allTypes: Array<string>;

    private pageSizeOptions = [5, 10, 15, 20];
    private pageSize = this.pageSizeOptions[0];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    private orderModel: OrderModel;
    private filterModel: OrderFilterModel;

    private displayedColumns: string[] = [
        'id',
        'date',
        'userName',
        'email',
        'productType',
        'title',
        'quantity',
        'amount',
        'status'
    ];
    private dataSource: MatTableDataSource<OrderModelItem>;


    constructor(
        private orderService: OrderService,
        private dialog: MatDialog
    ) {
        this.allStatuses = Constants.enumsAttributes.orderStatuses;
        this.statuses = [
            OrderStatus.Paid,
            OrderStatus.Unpaid
        ];

        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
    }

    public ngOnInit() {
        this.filterModel = new OrderFilterModel();
        this.filterModel.quantity = this.pageSize;
        this.filterModel.statuses = [1, 2];
        this.orderService.getAll(this.filterModel).subscribe((data: OrderModel) => {
            this.orderModel = data;
            this.dataSource = new MatTableDataSource(data.items);
            this.paginator.length = data.counter;
            console.log(data);
        });
    }

    public setAmountOfPrintingEdition() {
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

    public applyFilters() {
        this.orderService.getAll(this.filterModel).subscribe((data) => {
            this.dataSource = new MatTableDataSource(data.items);

            this.orderModel = data;
            this.paginator.length = data.counter;
        });
    }
}
