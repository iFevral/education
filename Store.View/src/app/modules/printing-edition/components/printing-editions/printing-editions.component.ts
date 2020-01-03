import { Component } from '@angular/core';

import { PrintingEditionService } from 'src/app/shared/services';
import { PrintingEditionModel, PrintingEditionFilterModel, PrintingEditionModelItem } from 'src/app/shared/models';
import { PrintingEditionType, SortProperty, PrintingEditionCurrency } from 'src/app/shared/enums';

import { Constants } from 'src/app/shared/constants/constants';
import { ListComponent } from 'src/app/shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './printing-editions.component.html',
    styleUrls: ['./printing-editions.component.scss']
})
export class PrintingEditionListComponent extends ListComponent<PrintingEditionModelItem, PrintingEditionModel, PrintingEditionFilterModel, PrintingEditionService> {

    private allTypes: Array<string>;
    private allCurrencies: Array<string>;
    private allSortProperties: Array<string>;
    private allCurrenciesSymbols: Array<string>;

    private types: Array<PrintingEditionType>;
    private sortProperties: Array<SortProperty>;
    private currencies: Array<PrintingEditionCurrency>;

    constructor(
        printingEditionService: PrintingEditionService
    ) {
        super(new PrintingEditionFilterModel(), printingEditionService);
        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.allCurrencies = Constants.enumsAttributes.printingEditionCurrencies;
        this.allCurrenciesSymbols = Constants.enumsAttributes.printingEditionCurrenciesSymbols;
        this.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];

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

        this.filterModel.types = this.types;
        this.filterModel.currency = this.currencies[0];
        this.filterModel.sortProperty = this.sortProperties[0];
    }

    public onCheckboxChecked(event, element): void {
        if (event.checked) {
            this.filterModel.types.push(element);
        } else {
            const index = this.filterModel.types.indexOf(element);
            if (index > -1) {
                this.filterModel.types.splice(index, 1);
            }
        }
    }

    public toggleDataOrder(): void {
        this.filterModel.IsAscending = !this.filterModel.IsAscending;
    }
}
