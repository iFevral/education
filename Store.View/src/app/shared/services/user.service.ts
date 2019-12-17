import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { UserModel, UserFilterModel, BaseModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';

@Injectable({
    providedIn: 'root'
})
export class UserService extends BaseService {

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.userControllerUrl);
    }

    public getAll(filterModel: UserFilterModel): Observable<UserModel> {
        return this.http.post<UserModel>(this.url, filterModel);
    }

    public setLockout(email: string, isLocked: boolean): Observable<BaseModel> {
        return this.http.post<BaseModel>(Constants.apiUrls.userBlockingUrl, { email, isLocked });
    }
}
