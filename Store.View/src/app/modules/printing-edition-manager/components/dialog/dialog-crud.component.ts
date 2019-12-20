import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CRUDOperations, PrintingEditionType, PrintingEditionCurrency } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';
import { AuthorService } from '../../../../shared/services';
import { AuthorFilterModel, AuthorModelItem, AuthorModel, DialogData, PrintingEditionModelItem } from '../../../../shared/models';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog-crud.component.html',
    styleUrls: ['./dialog-crud.component.scss']
})
export class DialogCrudComponent {

    private allTypes: Array<string>;
    private allCurrencies: Array<string>;
    private types: Array<PrintingEditionType>;
    private currencies: Array<PrintingEditionCurrency>;

    private allAuthors: Array<AuthorModelItem>;
    private authors: Array<number>;

    private title: string;
    private isFormVisible: boolean;
    constructor(
        public dialogRef: MatDialogRef<DialogCrudComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData<PrintingEditionModelItem>,
        private authorService: AuthorService) {

        const authorFilterModel: AuthorFilterModel = new AuthorFilterModel();

        authorService.getAll<AuthorFilterModel>(authorFilterModel).subscribe((resultModel: AuthorModel) => {
            this.allAuthors = resultModel.items;
        });

        this.authors = new Array<number>();

        this.allTypes = Constants.enumsAttributes.printingEditionTypes;
        this.types = [
            PrintingEditionType.Books,
            PrintingEditionType.Magazines,
            PrintingEditionType.Newspapers
        ];

        this.allCurrencies = Constants.enumsAttributes.printingEditionCurrencies;
        this.currencies = [
            PrintingEditionCurrency.USD
        ];

        switch (data.type) {
            case CRUDOperations.Create:
                this.title = 'Add printing edition';
                this.isFormVisible = true;
                break;
            case CRUDOperations.Update:
                this.title = 'Update printing edition';
                this.isFormVisible = true;
                this.data.model.authors.forEach(element => {
                    this.authors.push(element.id);
                });
                break;
            case CRUDOperations.Delete:
                this.title = `Delete "${data.model.title}"?`;
                this.isFormVisible = false;
                break;

        }
    }

    changeAuthors(event) {
        this.data.model.authors = new Array<AuthorModelItem>();
        this.authors.forEach(element => {
            const authorModel = new AuthorModelItem();
            authorModel.id = element;
            this.data.model.authors.push(authorModel);
        });
    }

    onNoClick(): void {
        this.dialogRef.close();
    }
}
