import { RoleName } from '../../enums';

export class AccountModel {
    constructor(
        public photo?: string,
        public role?: RoleName
    ) { }
}
