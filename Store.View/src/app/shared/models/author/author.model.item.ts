import { BaseItemModel } from 'src/app/shared/models/base/base-item.model';
import { PrintingEditionModelItem } from 'src/app/shared/models/printing-edition/printing-edition.model.item';

export class AuthorModelItem extends BaseItemModel {
    public name?: string;
    public printingEditions?: Array<PrintingEditionModelItem>;
}
