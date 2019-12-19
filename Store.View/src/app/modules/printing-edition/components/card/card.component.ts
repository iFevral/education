import { Component, Input, OnInit } from '@angular/core';
import { PrintingEditionModelItem } from '../../../../shared/models';
import { Constants } from '../../../../shared/constants/constants';

@Component({
    selector: 'app-card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit{
    private allCurrencies: Array<string>;
    @Input() printingEdition: PrintingEditionModelItem;

    public ngOnInit(): void {
        this.allCurrencies = Constants.enumsAttributes.printingEditionCurrencies;
    }
}