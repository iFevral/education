import { BaseFilterModel } from 'src/app/shared/models/base/base-filter.model';
import { PrintingEditionType, PrintingEditionCurrency } from 'src/app/shared/enums';

export class PrintingEditionFilterModel extends BaseFilterModel {
    public searchQuery?: string;
    public types?: Array<PrintingEditionType>;
    public minPrice?: number;
    public maxPrice?: number;
    public currency?: PrintingEditionCurrency;
}
