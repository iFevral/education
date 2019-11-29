import { Component, OnInit } from '@angular/core';
import { PrintingEditionService } from '../../../../shared/services/printing-edition.service';
import { PrintingEditionModel } from '../../../../shared/models/printing-edition/printing-edition.model';
import { PrintingEditionModelItem } from '../../../../shared/models/printing-edition/printing-edition.model.item';

const ELEMENT_DATA: PrintingEditionModelItem[] = [
    { id: 1, title: 'Hydrogen' },
    { id: 2, title: 'Helium' }
]

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css']
})
export class ListComponent {
    displayedColumns: string[] = ['id', 'title'];
    dataSource = ELEMENT_DATA;
}
