import { BaseModel } from '../base/base.model';
import { UserModelItem } from '..';

export class UserModel extends BaseModel {
    public items: Array<UserModelItem> = new Array<UserModelItem>();
    public counter: number;
}
