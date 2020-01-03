import { BaseFilterModel } from 'src/app/shared/models/base/base-filter.model';
import { UserLockStatus } from 'src/app/shared/enums';

export class UserFilterModel extends BaseFilterModel {
    public searchQuery?: string;
    public lockStatuses?: Array<UserLockStatus>;
}