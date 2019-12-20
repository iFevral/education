import { Component } from '@angular/core';

import { PrintingEditionService } from '../../../../shared/services';
import { PrintingEditionModel, PrintingEditionFilterModel, PrintingEditionModelItem } from '../../../../shared/models';
import { PrintingEditionType, SortProperty, PrintingEditionCurrency } from '../../../../shared/enums';

import { faSortAmountUpAlt, faSortAmountDownAlt, faFilter } from '@fortawesome/free-solid-svg-icons';
import { Constants } from '../../../../shared/constants/constants';
import { ListComponent } from '../../../../shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class PrintingEditionListComponent extends ListComponent<PrintingEditionModelItem, PrintingEditionModel, PrintingEditionFilterModel, PrintingEditionService> {

    private isSidebarOpened: boolean;
    private sortIcon = faSortAmountUpAlt;
    private filterIcon = faFilter;

    private allTypes: Array<string>;
    private allCurrencies: Array<string>;
    private allSortProperties: Array<string>;

    private types: Array<PrintingEditionType>;
    private sortProperties: Array<SortProperty>;
    private currencies: Array<PrintingEditionCurrency>;

    constructor(
        printingEditionService: PrintingEditionService
    ) {
        super(new PrintingEditionFilterModel(), printingEditionService);
        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.allCurrencies = Constants.enumsAttributes.printingEditionCurrencies;

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
}
