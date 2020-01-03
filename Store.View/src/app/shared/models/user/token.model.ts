import { BaseModel } from 'src/app/shared/models/base/base.model';

export class TokenModel extends BaseModel {
    public accessToken?: string;
    public refreshToken?: string;
}
