import { Component, Input } from '@angular/core';
import { PrintingEditionModelItem } from '../../../../shared/models';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent {
    private allCurrencies: Array<string>;
    private allCurrenciesSymbols: Array<string>;

    @Input() printingEdition: PrintingEditionModelItem;

    public constructor() {
        this.allCurrencies = Constants.enumsKeys.printingEditionCurrencies;
        this.allCurrenciesSymbols = Constants.enumsKeys.printingEditionCurrenciesSymbols;
    }
}