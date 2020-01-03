import { BaseItemModel } from 'src/app/shared/models/base/base-item.model';

export class UserModelItem extends BaseItemModel {
    public firstName?: string;
    public lastName?: string;
    public email?: string;
    public password?: string;
    public newPassword?: string;
    public isLocked?: boolean;
    public avatar?: string;
    public role?: string;
}
