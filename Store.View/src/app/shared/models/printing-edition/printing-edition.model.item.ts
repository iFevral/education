import { BaseItemModel } from 'src/app/shared/models/base/base-item.model';
import { AuthorModelItem } from 'src/app/shared/models/author/author.model.item';
import { PrintingEditionType, PrintingEditionCurrency } from 'src/app/shared/enums';

export class PrintingEditionModelItem extends BaseItemModel {
    public title?: string;
    public description?: string;
    public authors?: Array<AuthorModelItem>;
    public price?: number;
    public type?: PrintingEditionType;
    public currency?: PrintingEditionCurrency;
    public image?: string;
}
