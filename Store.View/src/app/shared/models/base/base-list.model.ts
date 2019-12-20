import { BaseModel } from '../base/base.model';

export class BaseListModel<ModelItem extends BaseModel> extends BaseModel {
    public items: Array<ModelItem>;
    public counter: number;
}
