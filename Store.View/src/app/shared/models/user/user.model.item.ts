import { BaseModel } from '../base/base.model';

export class UserModelItem extends BaseModel {
    constructor(
        public id?: number,
        public firstName?: string,
        public lastName?: string,
        public email?: string,
        public password?: string,
        public isLocked?: boolean,
        public roles?: Array<string>
    ) { super(); }
}
