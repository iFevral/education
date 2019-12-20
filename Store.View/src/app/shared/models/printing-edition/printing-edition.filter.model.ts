import { BaseFilterModel } from '../base/base-filter.model';
import { PrintingEditionType, PrintingEditionCurrency } from '../../enums';

export class PrintingEditionFilterModel extends BaseFilterModel {
    public searchQuery?: string;
    public types?: Array<PrintingEditionType>;
    public minPrice?: number;
    public maxPrice?: number;
    public currency?: PrintingEditionCurrency;
}
