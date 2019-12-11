import { Component, OnInit } from '@angular/core';
import { PrintingEditionService } from '../../../../shared/services';
import { PrintingEditionModel, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionType, SortProperty } from '../../../../shared/enums';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss'],
    providers: [PrintingEditionService]
})
export class ListComponent {

    private types = [
        PrintingEditionType[1],
        PrintingEditionType[2],
        PrintingEditionType[3]
    ];

    private isSidebarOpened: boolean;
    private printingEditionModel: PrintingEditionModel;
    private filterModel: PrintingEditionFilterModel;

    constructor(private pringtingEditionService: PrintingEditionService) {

        this.filterModel = new PrintingEditionFilterModel();
        this.filterModel.types = [1,2,3];
        this.filterModel.IsAscending = true;
        this.filterModel.sortProperty = SortProperty.Id;
        console.log(this.types);
        this.pringtingEditionService.getAll(this.filterModel).subscribe((data) => {
            this.printingEditionModel = data;
        });
    }
    arr2: any = [];

    public onCheckboxChecked(event, element) {
        if (event.checked) {
            this.filterModel.types.push(element);
        } else {
            let index = this.filterModel.types.indexOf(element);
            if (index > -1) {
                this.filterModel.types.splice(index, 1);
            }
        }
        console.log(JSON.stringify(this.filterModel.types));
    }

    public applyFilters() {
        this.pringtingEditionService.getAll(this.filterModel).subscribe((data) => {
            this.printingEditionModel = data;
        });
    }
}
