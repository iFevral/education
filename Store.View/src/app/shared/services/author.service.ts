import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AuthorModel } from 'src/app/shared/models';
import { Constants } from 'src/app/shared/constants/constants';
import { BaseService } from 'src/app/shared/services/base/base.service';
import { MatSnackBar } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class AuthorService extends BaseService<AuthorModel> {

    constructor(
        http: HttpClient,
        messageContainer: MatSnackBar
    ) {
        super(http, Constants.apiUrls.authorControllerUrl, messageContainer);
    }
}
