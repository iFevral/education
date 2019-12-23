import { BaseModel } from '../base/base.model';
import { AuthorModelItem } from '../author/author.model.item';
import { PrintingEditionType, PrintingEditionCurrency } from '../../enums';

export class PrintingEditionModelItem extends BaseModel {
    public id?: number;
    public title?: string;
    public description?: string;
    public authors?: Array<AuthorModelItem>;
    public price?: number;
    public type?: PrintingEditionType;
    public currency?: PrintingEditionCurrency;
    public image?: string;
}
