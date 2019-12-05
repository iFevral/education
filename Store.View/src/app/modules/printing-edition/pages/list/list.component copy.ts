import { Component, OnInit, ViewChild } from '@angular/core';
import { PrintingEditionService } from '../../../../shared/services/printing-edition.service';
import { PrintingEditionModel } from '../../../../shared/models/printing-edition/printing-edition.model';
import { PrintingEditionModelItem } from '../../../../shared/models/printing-edition/printing-edition.model.item';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.css'],
    providers: [PrintingEditionService]
})
export class ListComponent implements OnInit {
    opened: boolean;
    displayedColumns: string[] = ['id', 'title', 'price'];
    dataSource: MatTableDataSource<PrintingEditionModelItem>;
    data: Array<PrintingEditionModelItem>;
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    constructor(private pringtingEditionService: PrintingEditionService) { }

    ngOnInit() {
        this.pringtingEditionService.getAll().then((resp) => {
            this.dataSource = new MatTableDataSource(resp.printingEditions);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        });
    }


    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
}