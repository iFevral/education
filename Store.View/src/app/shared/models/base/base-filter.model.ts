import { SortProperty } from '../../enums';

export class BaseFilterModel {
    public sortProperty?: SortProperty;
    public IsAscending = true;
    public startIndex?: number;
    public quantity: number;
}
