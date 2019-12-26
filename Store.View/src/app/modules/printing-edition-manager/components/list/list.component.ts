import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { PrintingEditionModel, PrintingEditionModelItem, PrintingEditionFilterModel } from '../../../../shared/models';
import { PrintingEditionService } from '../../../../shared/services';
import { CRUDOperations, PrintingEditionCurrency, PrintingEditionType } from '../../../../shared/enums';
import { PrintingEditionDialogComponent } from '../dialog/dialog.component';
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
        const newPrintingEditionModel = new PrintingEditionModelItem();
        const dialogRef = this.dialog.open(PrintingEditionDialogComponent, {
            width: '800px',
            data: {
                type: CRUDOperations.Create,
                model: newPrintingEditionModel
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
        const dialogRef = this.dialog.open(PrintingEditionDialogComponent, {
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
        const dialogRef = this.dialog.open(PrintingEditionDialogComponent, {
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
