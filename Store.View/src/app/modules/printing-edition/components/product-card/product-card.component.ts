import { Component, Input, OnInit } from '@angular/core';
import { PrintingEditionModelItem } from '../../../../shared/models';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit{
    private allCurrencies: Array<string>;
    private allCurrenciesSymbols: Array<string>;

    @Input() printingEdition: PrintingEditionModelItem;

    public ngOnInit(): void {
        this.allCurrencies = Constants.enumsAttributes.printingEditionCurrencies;
        this.allCurrenciesSymbols = Constants.enumsAttributes.printingEditionCurrenciesSymbols;
    }
}