import { BaseModel } from 'src/app/shared/models/base/base.model';

export class CartModelItem extends BaseModel {
    public productId?: number;
    public productTitle?: string;
    public quantity?: number;
    public price?: number;
}
