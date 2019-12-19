import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material';

import { PrintingEditionService } from '../../../../shared/services';
import { PrintingEditionModel, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionType, SortProperty, PrintingEditionCurrency } from '../../../../shared/enums';

import { faSortAmountUpAlt, faSortAmountDownAlt, faFilter } from '@fortawesome/free-solid-svg-icons';
import { Constants } from '../../../../shared/constants/constants';


@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss'],
    providers: [PrintingEditionService]
})
export class PrintingEditionListComponent implements OnInit {

    private isSidebarOpened: boolean;
    private sortIcon = faSortAmountUpAlt;
    private filterIcon = faFilter;

    private pageSizeOptions = [5, 10, 15, 20];
    private pageSize = this.pageSizeOptions[0];

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

    private types: Array<string>;
    private currencies: Array<PrintingEditionCurrency>;
    private allCurrencies: Array<string>;
    private sortProperties: Array<SortProperty>;
    private allSortProperties: Array<string>;

    private filterModel: PrintingEditionFilterModel;
    private printingEditionModel: PrintingEditionModel;

    constructor(private printingEditionService: PrintingEditionService) {
    }

    public ngOnInit() {
        this.types = Constants.enumsAttributes.printingEditionTypes;
        this.allCurrencies = Constants.enumsAttributes.printingEditionCurrencies;
        this.currencies = [
            PrintingEditionCurrency.USD,
            PrintingEditionCurrency.EUR,
            PrintingEditionCurrency.GBP,
            PrintingEditionCurrency.CHF,
            PrintingEditionCurrency.JPY,
            PrintingEditionCurrency.UAH
        ];

        this.sortProperties = [
            SortProperty.Date,
            SortProperty.Price
        ];

        this.allSortProperties = Constants.enumsAttributes.sortProperties;

        this.filterModel = new PrintingEditionFilterModel();
        this.filterModel.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];
        this.filterModel.quantity = this.pageSize;
        this.filterModel.currency = PrintingEditionCurrency.USD;
        this.filterModel.sortProperty = SortProperty.Price;
        this.printingEditionService.getAll<PrintingEditionFilterModel>(this.filterModel)
            .subscribe((data: PrintingEditionModel) => {
                this.printingEditionModel = data;
                this.paginator.length = data.counter;
            });


    }

    public onCheckboxChecked(event, element) {
        if (event.checked) {
            this.filterModel.types.push(element);
        } else {
            const index = this.filterModel.types.indexOf(element);
            if (index > -1) {
                this.filterModel.types.splice(index, 1);
            }
        }
    }

    public toggleDataOrder() {
        this.filterModel.IsAscending = !this.filterModel.IsAscending;
        this.sortIcon = this.sortIcon === faSortAmountUpAlt ? faSortAmountDownAlt : faSortAmountUpAlt;
    }

    public setAmountOfPrintingEdition() {
        this.filterModel.quantity = this.paginator.pageSize;
    }

    public applyFilters(event) {

        this.paginator.pageIndex = event
            ? this.paginator.pageIndex
            : 0;

        this.filterModel.startIndex = this.paginator.pageIndex * this.paginator.pageSize;

        this.printingEditionService.getAll<PrintingEditionFilterModel>(this.filterModel).subscribe((data: PrintingEditionModel) => {
            this.printingEditionModel = data;
            this.paginator.length = data.counter;
        });
    }
}
