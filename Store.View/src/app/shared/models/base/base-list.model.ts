import { BaseModel } from 'src/app/shared/models/base/base.model';

export class BaseListModel<ModelItem extends BaseModel> extends BaseModel {
    public items: Array<ModelItem>;
    public counter: number;
}
