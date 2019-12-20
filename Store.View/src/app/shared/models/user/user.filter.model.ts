import { BaseFilterModel } from '../base/base-filter.model';
import { UserLockStatus } from '../../enums';

export class UserFilterModel extends BaseFilterModel {
    public searchQuery?: string;
    public lockStatuses?: Array<UserLockStatus>;
}