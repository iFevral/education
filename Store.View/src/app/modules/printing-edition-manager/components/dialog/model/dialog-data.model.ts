import { CRUDOperations } from '../../../../../shared/enums';
import { PrintingEditionModelItem } from '../../../../../shared/models';

export class DialogData {
    constructor(
        public type: CRUDOperations,
        public model: PrintingEditionModelItem
    ) {}
}
