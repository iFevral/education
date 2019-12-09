import { PrintingEditionModelItem } from '../printing-edition/printing-edition.model.item';

export class AuthorModelItem {
    constructor(
        public id?: number,
        public name?: string,
        public printingEditions?: Array<PrintingEditionModelItem>
    ) { }
}
