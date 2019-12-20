import { BaseFilterModel } from '../base/base-filter.model';
import { OrderStatus } from '../../enums';

export class OrderFilterModel extends BaseFilterModel {
    public userId?: number;
    public statuses?: Array<OrderStatus>;
}
