import { PrintingEditionModelItem } from '../printing-edition/printing-edition.model.item';
import { BaseModel } from '../base/base.model';

export class AuthorModelItem extends BaseModel {
    constructor(
        public id?: number,
        public name?: string,
        public printingEditions?: Array<PrintingEditionModelItem>
    ) { super(); }
}
