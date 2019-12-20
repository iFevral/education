import { BaseModel } from '../base/base.model';
import { CRUDOperations } from '../../enums';

export class DialogData<ModelItem extends BaseModel> {
    constructor(
        public type: CRUDOperations,
        public model: ModelItem
    ) {}
}
