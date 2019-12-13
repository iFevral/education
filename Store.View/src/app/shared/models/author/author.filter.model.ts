import { BaseFilterModel } from '../base/base-filter.model';

export class AuthorFilterModel extends BaseFilterModel {
    constructor(
        public name?: string
    ) {
        super();
    }
}
