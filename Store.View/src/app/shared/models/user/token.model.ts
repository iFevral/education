import { BaseModel } from '../base/base.model';

export class TokenModel extends BaseModel {
    constructor(
        public accessToken?: string,
        public refreshToken?: string
    ) {
        super();
    }
}
