import { CRUDOperations } from '../../../../../shared/enums';
import { UserModelItem } from '../../../../../shared/models';

export class DialogData {
    constructor(
        public type: CRUDOperations,
        public model: UserModelItem
    ) {}
}
