import { BaseModel } from '../base/base.model';
import { AuthorModelItem } from './author.model.item';

export class AuthorModel extends BaseModel {
    public items: Array<AuthorModelItem>;
    public counter: number;
}
