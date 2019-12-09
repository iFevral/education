import { BaseModel } from '../base/base.model';
import { PrintingEditionModelItem } from './printing-edition.model.item';

export class PrintingEditionModel extends BaseModel {
    public items: Array<PrintingEditionModelItem> = new Array<PrintingEditionModelItem>();
}
