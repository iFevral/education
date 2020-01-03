import { BaseItemModel } from 'src/app/shared/models/base/base-item.model';
import { PrintingEditionModelItem } from 'src/app/shared/models/printing-edition/printing-edition.model.item';

export class OrderItemModel extends BaseItemModel {
    public amount?: number;
    public printingEdition?: PrintingEditionModelItem;
}
