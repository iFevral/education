import { BaseModel } from '../base/base.model';
import { OrderStatus } from '../../enums';
import { UserModelItem } from '../user/user.model.item';
import { OrderItemModel } from './order-item.model';

export class OrderModelItem extends BaseModel {
    constructor(
        public id?: number,
        public description?: string,
        public status?: OrderStatus,
        public date?: string,
        public orderPrice?: number,
        public user?: UserModelItem,
        public orderItems?: Array<OrderItemModel>
    ) { super(); }
}
