import { BaseItemModel } from 'src/app/shared/models/base/base-item.model';
import { OrderStatus } from 'src/app/shared/enums';
import { UserModelItem } from 'src/app/shared/models/user/user.model.item';
import { OrderItemModel } from 'src/app/shared/models/order/order-item.model';

export class OrderModelItem extends BaseItemModel {
    public description?: string;
    public status?: OrderStatus;
    public date?: string;
    public orderPrice?: number;
    public user?: UserModelItem;
    public orderItems?: Array<OrderItemModel>;
}
