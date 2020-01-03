import { BaseModel } from 'src/app/shared/models/base/base.model';
import { CRUDOperations } from 'src/app/shared/enums';

export class DialogData<ModelItem extends BaseModel> {
    constructor(
        public type: CRUDOperations,
        public model: ModelItem
    ) {}
}
