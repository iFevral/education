import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserModel, BaseModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';
import { MatSnackBar } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class UserService extends BaseService<UserModel> {

    constructor(
        http: HttpClient,
        messageContainer: MatSnackBar
    ) {
        super(http, Constants.apiUrls.userControllerUrl, messageContainer);
    }

    public setLockout(email: string, isLocked: boolean) {
        this.http.post<BaseModel>(Constants.apiUrls.userBlockingUrl, { email, isLocked })
            .subscribe((resultModel: BaseModel) => {
                if (resultModel.errors) {
                    this.showDialogMessage(resultModel.errors.toString());
                }
            });
    }
}
