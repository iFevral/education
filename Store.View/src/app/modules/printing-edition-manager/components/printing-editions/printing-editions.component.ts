import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';

import { PrintingEditionModel, PrintingEditionModelItem, PrintingEditionFilterModel } from 'src/app/shared/models';
import { PrintingEditionService } from 'src/app/shared/services';
import { PrintingEditionCurrency, PrintingEditionType } from 'src/app/shared/enums';
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
        dialog: MatDialog
    ) {
        super(new PrintingEditionFilterModel(), printingEditionService, dialog);

        this.dialogType = PrintingEditionDialogComponent;
        this.dialogWidth = '800px';
        this.displayedColumns = Constants.displayedColumns.printingEditions;

        this.allTypes = Constants.enumsKeys.printingEditionTypes;
        this.types = Constants.enumsValues.printingEditionTypes;

        this.filterModel.types = this.types;
        this.filterModel.currency = PrintingEditionCurrency.USD;

    }

    public createPrintingEdition(): void {
        const newPrintingEditionModel = new PrintingEditionModelItem();
        this.create(newPrintingEditionModel);
    }
}
