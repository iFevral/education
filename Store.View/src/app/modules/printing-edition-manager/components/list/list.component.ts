import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { PrintingEditionModel, PrintingEditionModelItem, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionService } from '../../../../shared/services';
import { CRUDOperations, SortProperty, PrintingEditionCurrency, PrintingEditionType } from '../../../../shared/enums';
import { DialogCrudComponent } from '../dialog/dialog-crud.component';
import { Constants } from '../../../../shared/constants/constants';
import { ListComponent } from '../../../../shared/components/base';

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrls: ['./list.component.scss']
})
export class PrintingEditionManagerListComponent extends ListComponent<PrintingEditionModelItem, PrintingEditionModel, PrintingEditionFilterModel, PrintingEditionService> {

    private allTypes: Array<string>;
    private types: Array<PrintingEditionType>;

    constructor(
        printingEditionService: PrintingEditionService,
        private dialog: MatDialog
    ) {
        super(new PrintingEditionFilterModel(), printingEditionService);
        this.displayedColumns = [
            'id',
            'title',
            'description',
            'type',
            'authors',
            'price',
            'control'
        ];
        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];

        this.filterModel.types = this.types;
        this.filterModel.currency = PrintingEditionCurrency.USD;
    }

    public create(): void {
        const newprintingEditionModel = new PrintingEditionModelItem();
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '800px',
            data: {
                type: CRUDOperations.Create,
                model: newprintingEditionModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: PrintingEditionModelItem) => {
                if (resultModel) {
                    this.dataService.create(resultModel)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public update(inputModel: PrintingEditionModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '800px',
            data: {
                type: CRUDOperations.Update,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: PrintingEditionModelItem) => {
                if (resultModel) {
                    this.dataService.update(resultModel)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }

    public delete(inputModel: PrintingEditionModelItem): void {
        const dialogRef = this.dialog.open(DialogCrudComponent, {
            width: '400px',
            data: {
                type: CRUDOperations.Delete,
                model: inputModel
            }
        });

        dialogRef.afterClosed()
            .subscribe((resultModel: PrintingEditionModelItem) => {
                if (resultModel) {
                    this.dataService.delete(resultModel.id)
                        .subscribe(messageModel => {
                            this.dataService.showDialogMessage(messageModel.errors.toString());
                            this.applyFilters();
                        });
                }
            });
    }
}
