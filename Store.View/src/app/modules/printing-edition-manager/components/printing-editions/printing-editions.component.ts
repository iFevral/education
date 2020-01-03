import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';

import { PrintingEditionModel, PrintingEditionModelItem, PrintingEditionFilterModel } from 'src/app/shared/models';
import { PrintingEditionService } from 'src/app/shared/services';
import { CRUDOperations, PrintingEditionCurrency, PrintingEditionType } from 'src/app/shared/enums';
import { Constants } from 'src/app/shared/constants/constants';

import { ListComponent } from 'src/app/shared/components/base';
import { PrintingEditionDialogComponent } from 'src/app/modules/printing-edition-manager/components/dialog/dialog.component';

@Component({
    selector: 'app-list',
    templateUrl: './printing-editions.component.html',
    styleUrls: ['./printing-editions.component.scss']
})
export class PrintingEditionManagerListComponent extends ListComponent<PrintingEditionModelItem, PrintingEditionModel, PrintingEditionFilterModel, PrintingEditionService> {

    private allTypes: Array<string>;
    private types: Array<PrintingEditionType>;

    constructor(
        printingEditionService: PrintingEditionService,
        private dialog: MatDialog
    ) {
        super(new PrintingEditionFilterModel(), printingEditionService);

        this.displayedColumns = Constants.displayedColumns.printingEditions;

        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];

        this.filterModel.types = this.types;
        this.filterModel.currency = PrintingEditionCurrency.USD;

        console.log(PrintingEditionDialogComponent);
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
