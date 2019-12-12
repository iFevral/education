import { Component } from '@angular/core';

import { PrintingEditionService } from '../../../../shared/services';
import { PrintingEditionModel, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionType, SortProperty, PrintingEditionCurrency } from '../../../../shared/enums';

import { faSortAmountUpAlt, faSortAmountDownAlt, faFilter } from '@fortawesome/free-solid-svg-icons';
@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss'],
    providers: [PrintingEditionService]
})
export class ListComponent {

    private isSidebarOpened: boolean;
    private sortIcon = faSortAmountUpAlt;
    private settingsIcon = faFilter;

    private types: Array<string>;
    private currencies: Array<string>;
    private currency: string;
    private sortProperties: Array<string>;


    private filterModel: PrintingEditionFilterModel;
    private printingEditionModel: PrintingEditionModel;


    constructor(private printingEditionService: PrintingEditionService) {

        this.currencies = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in PrintingEditionCurrency) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.currencies.push(PrintingEditionCurrency[enumMember]);
            }
        }
        this.currency = this.currencies[0];
        this.types = new Array<string>();

        this.filterModel = new PrintingEditionFilterModel();
        this.filterModel.types = new Array<PrintingEditionType>();
        // tslint:disable-next-line: forin
        for (const enumMember in PrintingEditionType) {
            const typeIndex = parseInt(enumMember, 10);
            if (typeIndex >= 0) {
                this.types.push(PrintingEditionType[enumMember]);
                this.filterModel.types.push(typeIndex);
            }
        }
        this.filterModel.currency = PrintingEditionCurrency.USD;
        this.filterModel.IsAscending = true;
        this.filterModel.sortProperty = SortProperty.Id;
        this.printingEditionService.getAll(this.filterModel).subscribe((data) => {
            this.printingEditionModel = data;
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

    public toggleSorting() {

        this.filterModel.IsAscending = !this.filterModel.IsAscending;

        this.sortIcon = this.sortIcon === faSortAmountUpAlt ? faSortAmountDownAlt : faSortAmountUpAlt;
    }

    public applyFilters() {
        console.log(this.filterModel)
        this.printingEditionService.getAll(this.filterModel).subscribe((data) => {
            this.printingEditionModel = data;
        });
    }
}
