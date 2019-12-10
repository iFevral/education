import { Component, OnInit } from '@angular/core';
import { PrintingEditionService } from '../../../../shared/services';
import { PrintingEditionModel, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionType, SortProperty } from 'src/app/shared/enums';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss'],
    providers: [PrintingEditionService]
})
export class ListComponent implements OnInit {
    private isSidebarOpened: boolean;
    private printingEditionModel: PrintingEditionModel;
    private filterModel: PrintingEditionFilterModel;

    constructor(private pringtingEditionService: PrintingEditionService) {
        this.filterModel = new PrintingEditionFilterModel();
        this.filterModel.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Journals,
            PrintingEditionType.Newspapers];
        this.filterModel.IsAscending = true;
        this.filterModel.sortProperty = SortProperty.Id;
        this.pringtingEditionService.getAll(this.filterModel).subscribe((data) => {
            this.printingEditionModel = data;
        });
    }

    ngOnInit() { }


}
