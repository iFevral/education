import { BaseFilterModel } from '../base/base-filter.model';
import { PrintingEditionType } from '../../enums';

export class PrintingEditionFilterModel extends BaseFilterModel {
    constructor(
        public title?: string,
        public description?: string,
        public types?: Array<PrintingEditionType>
    ) {
        super();
    }
}
