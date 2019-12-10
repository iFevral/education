import { BaseModel } from '../base/base.model';

export class RegistrationResultModel extends BaseModel {
    constructor(
        public email?: string,
        public message?: string
    ) {
        super();
    }
}
