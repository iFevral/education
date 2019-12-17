import { BaseModel } from '../base/base.model';
import { PrintingEditionModelItem } from '../printing-edition/printing-edition.model.item';

export class OrderItemModel extends BaseModel {
    constructor(
        public id?: number,
        public amount?: number,
        public printingEdition?: PrintingEditionModelItem
    ) {
        super();
    }
}
