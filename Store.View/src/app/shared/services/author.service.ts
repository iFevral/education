import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthorModel, AuthorFilterModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';

@Injectable({
    providedIn: 'root'
})
export class AuthorService extends BaseService<AuthorModel> {

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.authorControllerUrl);
    }
}
