import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AuthorModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';
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
