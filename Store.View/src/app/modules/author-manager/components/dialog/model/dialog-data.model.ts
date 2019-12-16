import { CRUDOperations } from '../../../../../shared/enums';
import { AuthorModelItem } from '../../../../../shared/models';

export class DialogData {
    constructor(
        public type: CRUDOperations,
        public model: AuthorModelItem
    ) {}
}
