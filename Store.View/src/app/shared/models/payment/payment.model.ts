import { BaseModel } from 'src/app/shared/models/base/base.model';

export class PaymentModel extends BaseModel {
    public transactionId?: string;
    public orderId?: number;
}
