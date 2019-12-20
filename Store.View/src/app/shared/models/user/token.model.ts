import { BaseModel } from '../base/base.model';

export class TokenModel extends BaseModel {
    public accessToken?: string;
    public refreshToken?: string;
}
