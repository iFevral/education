import { Component, OnInit } from '@angular/core';

import { PrintingEditionService } from '../../../../shared/services/printing-edition.service';
import { PrintingEditionModel } from '../../../../shared/models/printing-edition/printing-edition.model';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css'],
    providers: [PrintingEditionService]
})
export class ListComponent implements OnInit {
    opened: boolean;
    printingEditionModel: PrintingEditionModel;

    constructor(private pringtingEditionService: PrintingEditionService) { }

    ngOnInit() {
        this.pringtingEditionService.getAll().subscribe((data) => {
            this.printingEditionModel = data;
        });
    }
}
