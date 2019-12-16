import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog, MatSnackBar } from '@angular/material';
import { PrintingEditionModel, PrintingEditionModelItem, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionService } from '../../../../shared/services';
import { CRUDOperations, SortProperty, PrintingEditionCurrency, PrintingEditionType } from '../../../../shared/enums';
import { DialogCrudComponent } from '../dialog/dialog-crud.component';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class PrintingEditionManagerListComponent implements OnInit {

    private allTypes: Array<string>;
    private types: Array<PrintingEditionType>;
    private pageSize = 2;
    private pageSizeOptions = [2, 3, 5, 10];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    private printingEditionModel: PrintingEditionModel;
    private filterModel: PrintingEditionFilterModel;

    private displayedColumns: string[] = [
        'id',
        'title',
        'description',
        'type',
        'authors',
        'price',
        'control'];
    private dataSource: MatTableDataSource<PrintingEditionModelItem>;


    constructor(
        private printingEditionService: PrintingEditionService,
        private dialog: MatDialog,
        private messageContainer: MatSnackBar
    ) {
        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];
        console.log(this.allTypes[0]);
    }

    public ngOnInit() {
        this.filterModel = new PrintingEditionFilterModel();
        this.filterModel.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];
        this.filterModel.quantity = this.pageSize;
        this.filterModel.currency = PrintingEditionCurrency.USD;

        this.printingEditionService.getAll(this.filterModel).subscribe(data => {
            this.printingEditionModel = data;
            this.dataSource = new MatTableDataSource(data.items);
            this.paginator.length = data.counter;
        });
    }

    public setAmountOfPrintingEdition() {
        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        this.filterModel.quantity = this.paginator.pageSize;
    }

    public create(): void {
        const newprintingEditionModel = new PrintingEditionModelItem();
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '800px',
            data: {
                type: CRUDOperations.Create,
                model: newprintingEditionModel
            }
        });

        dialogRef.afterClosed()
            .subscribe(resultModel => {
                if (resultModel) {
                    this.printingEditionService.create(resultModel)
                        .subscribe(messageModel => {
                            this.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public update(inputModel: PrintingEditionModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '800px',
            data: {
                type: CRUDOperations.Update,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe(resultModel => {
                if (resultModel) {
                    this.printingEditionService.update(resultModel)
                        .subscribe(messageModel => {
                            this.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public delete(inputModel: PrintingEditionModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Delete,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe(resultModel => {
                if (resultModel) {
                    this.printingEditionService.delete(resultModel)
                        .subscribe(messageModel => {
                            this.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public setOrder(event): void {
        this.filterModel.IsAscending = this.sort.direction === 'desc'
            ? false : true;

        const sortProp: string = this.sort.active.charAt(0).toUpperCase() + this.sort.active.slice(1);
        console.log(this.filterModel.sortProperty);
        this.filterModel.sortProperty = this.sort.direction !== ''
            ? SortProperty[sortProp]
            : SortProperty.Id;
    }

    public applyFilters() {
        console.log(this.filterModel.types);
        this.printingEditionService.getAll(this.filterModel).subscribe((data) => {
            this.dataSource = new MatTableDataSource(data.items);

            this.printingEditionModel = data;
            this.paginator.length = data.counter;
        });
    }

    public showDialogMessage(message: string) {
        if (message === '') {
            return;
        }

        this.messageContainer.open(message, 'X', {
            duration: 5000,
            verticalPosition: 'top'
        });
    }

}
