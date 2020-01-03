import { SortProperty } from 'src/app/shared/enums';

export class BaseFilterModel {
    public sortProperty?: SortProperty;
    public IsAscending = true;
    public startIndex?: number;
    public quantity: number;
}
