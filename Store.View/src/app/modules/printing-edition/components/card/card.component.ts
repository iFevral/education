import { Component, Input } from '@angular/core';
import { PrintingEditionModelItem } from '../../../../shared/models';

@Component({
    selector: 'app-card',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.scss']
})
export class CardComponent {
    @Input() printingEdition: PrintingEditionModelItem;
}
