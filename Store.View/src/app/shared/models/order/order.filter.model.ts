import { BaseFilterModel } from '../base/base-filter.model';
import { OrderStatus } from '../../enums';

export class OrderFilterModel extends BaseFilterModel {
    constructor(
        public statuses?: Array<OrderStatus>
    ) {
        super();
    }
}
