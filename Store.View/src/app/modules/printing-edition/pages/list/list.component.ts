import { Component, OnInit, ViewChild } from '@angular/core';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { PrintingEditionService } from '../../../../shared/services/printing-edition.service';
import { PrintingEditionModel } from '../../../../shared/models/printing-edition/printing-edition.model';
import { PrintingEditionModelItem } from '../../../../shared/models/printing-edition/printing-edition.model.item';

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
        this.pringtingEditionService.getAll().then((resp) => {
            this.printingEditionModel = resp;
        });
    }
}
