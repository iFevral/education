import { BaseModel } from '../base/base.model';
import { PrintingEditionModelItem } from '../printing-edition/printing-edition.model.item';

export class AuthorModelItem extends BaseModel {
    public id?: number;
    public name?: string;
    public printingEditions?: Array<PrintingEditionModelItem>;
}
