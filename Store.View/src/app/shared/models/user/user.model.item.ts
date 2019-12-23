import { BaseModel } from '../base/base.model';

export class UserModelItem extends BaseModel {
    public id?: number;
    public firstName?: string;
    public lastName?: string;
    public email?: string;
    public password?: string;
    public newPassword?: string;
    public isLocked?: boolean;
    public avatar?: string;
    public roles?: Array<string>;
}
