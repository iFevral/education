import { Component, Input } from '@angular/core';
import { PrintingEditionModelItem } from '../../../../../shared/models/printing-edition/printing-edition.model.item';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent {
    @Input() printingEdition: PrintingEditionModelItem;
}
