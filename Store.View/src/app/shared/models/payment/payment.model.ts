import { BaseModel } from '../base/base.model';

export class PaymentModel extends BaseModel {
    constructor(
        public transactionId?: string,
        public orderId?: number
    ) { super(); }
}
