import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PrintingEditionType, PrintingEditionCurrency, CRUDOperations } from '../../../../shared/enums';
import { Constants } from '../../../../shared/constants/constants';
import { AuthorService } from '../../../../shared/services';
import { AuthorFilterModel, AuthorModelItem, AuthorModel, DialogData, PrintingEditionModelItem } from '../../../../shared/models';
import { DialogCrudComponent } from '../../../../shared/components/base';

@Component({
    selector: 'app-dialog-update',
    templateUrl: './dialog.component.html',
    styleUrls: ['./dialog.component.scss']
})
export class PrintingEditionDialogComponent extends DialogCrudComponent<PrintingEditionModelItem> {

    private allTypes: Array<string>;
    private allCurrencies: Array<string>;
    private types: Array<PrintingEditionType>;
    private currencies: Array<PrintingEditionCurrency>;

    private allAuthors: Array<AuthorModelItem>;
    private authors: Array<number>;

    constructor(
        public dialogRef: MatDialogRef<PrintingEditionDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData<PrintingEditionModelItem>,
        private authorService: AuthorService
    ) {
        super(dialogRef, data);

        const authorFilterModel: AuthorFilterModel = new AuthorFilterModel();

        authorService.getAll<AuthorFilterModel>(authorFilterModel).subscribe((resultModel: AuthorModel) => {
            this.allAuthors = resultModel.items;
        });

        this.authors = new Array<number>();
        if (data.type === CRUDOperations.Update) {
            data.model.authors.forEach(element => {
                this.authors.push(element.id);
            });
        }
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

        this.titles = [``,
            `Create printing edition`,
            `Update printing edition`,
            `Delete "${data.model.title}"?`
        ];
    }

    changeAuthors() {
        this.data.model.authors = new Array<AuthorModelItem>();

        this.authors.forEach(element => {
            const authorModel = new AuthorModelItem();
            authorModel.id = element;
            this.data.model.authors.push(authorModel);
        });
    }

    public setTitleImage(event) {
        if (event.target.files.length > 0) {
            const newImage = event.target.files[0];
            const fileReader = new FileReader();

            fileReader.onload = (fileLoadedEvent) => {
                this.data.model.image = (<FileReader>fileLoadedEvent.target).result.toString();
            };

            fileReader.readAsDataURL(newImage);
        }
    }
}
