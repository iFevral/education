import { BaseModel } from 'src/app/shared/models/base/base.model';
import { PrintingEditionModelItem } from 'src/app/shared/models/printing-edition/printing-edition.model.item';

export class AuthorModelItem extends BaseModel {
    public id?: number;
    public name?: string;
    public printingEditions?: Array<PrintingEditionModelItem>;
}
