import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { OrderModel, OrderFilterModel, OrderModelItem, PaymentModel } from '../../../../shared/models';
import { OrderService } from '../../../../shared/services';
import { SortProperty, OrderStatus } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';
import { map } from 'rxjs/operators';

@Component({
    selector: 'app-list-by-user',
    templateUrl: './list-by-user.component.html',
    styleUrls: ['./list-by-user.component.scss']
})
export class OrderListByUserComponent implements OnInit {
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

        this.applyFilters();
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
        this.orderService.getAll<OrderFilterModel>(this.filterModel)
            .pipe(map((resultModel: OrderModel) => {
                resultModel.items.forEach((element: OrderModelItem) => {
                    const date = new Date(element.date);
                    element.date = date.toDateString();
                });
                return resultModel;
            }))
            .subscribe((data: OrderModel) => {
                this.dataSource = new MatTableDataSource(data.items);
                this.orderModel = data;
                this.paginator.length = data.counter;
            });
    }

    public pay(order: OrderModelItem) {

        const handler = (window as any).StripeCheckout.configure({
            key: 'pk_test_1XvcRG66xztreFUG2M8LKXTx00BVSuXbLs',
            locale: 'auto',
            token: (paymentLog: any) => {

                const paymentModel: PaymentModel = new PaymentModel();
                paymentModel.orderId = order.id;
                paymentModel.transactionId = paymentLog.id;
                this.orderService.addPaymentTransaction(paymentModel).subscribe(data => {
                    if (data.errors.length == 0) {
                        this.applyFilters();
                    }
                });
            }
        });

        handler.open({
            name: 'Add payment',
            description: `Payment for order №${order.id}`,
            amount: order.orderPrice * 100
        });

    }
}
