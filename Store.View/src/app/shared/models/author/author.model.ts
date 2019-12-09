import { BaseModel } from '../base/base.model';
import { AuthorModelItem } from './author.model.item';

export class AuthorModel extends BaseModel {
    items: Array<AuthorModelItem>;
}