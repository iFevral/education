import { BaseModel } from '../base/base.model';
import { OrderModelItem } from './order.model.item';

export class OrderModel extends BaseModel {
    constructor(
        public items?: Array<OrderModelItem>,
        public counter?: number
    ) {
        super();
    }
}
