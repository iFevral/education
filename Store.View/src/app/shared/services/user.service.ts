import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';

import { UserModel, BaseModel } from 'src/app/shared/models';
import { Constants } from 'src/app/shared/constants/constants';
import { BaseService } from 'src/app/shared/services/base/base.service';

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

    public setLockout(email: string, isLocked: boolean): void {
        this.http.post<BaseModel>(Constants.apiUrls.userBlockingUrl, { email, isLocked })
            .subscribe((resultModel: BaseModel) => {
                if (resultModel.errors) {
                    this.showDialogMessage(resultModel.errors.toString());
                }
            });
    }
}
