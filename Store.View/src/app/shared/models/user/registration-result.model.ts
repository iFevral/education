import { BaseModel } from 'src/app/shared/models/base/base.model';

export class RegistrationResultModel extends BaseModel {
    public email?: string;
    public message?: string;
}
