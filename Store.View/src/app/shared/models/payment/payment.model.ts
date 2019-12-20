import { BaseModel } from '../base/base.model';

export class PaymentModel extends BaseModel {
    public transactionId?: string;
    public orderId?: number;
}
