import { BaseFilterModel } from 'src/app/shared/models/base/base-filter.model';
import { OrderStatus } from 'src/app/shared/enums';

export class OrderFilterModel extends BaseFilterModel {
    public userId?: number;
    public statuses?: Array<OrderStatus>;
}
