import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthorModel, AuthorFilterModel, AuthorModelItem, BaseModel } from '../models';
import { Constants } from '../constants/constants';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthorService {

    private url = Constants.apiUrls.authorControllerUrl;

    constructor(private http: HttpClient) { }

    public getAll(filterModel: AuthorFilterModel): Observable<AuthorModel> {
        return this.http.post<AuthorModel>(this.url, filterModel);
    }

    public create(authorModel: AuthorModelItem): Observable<BaseModel> {
        return this.http.put<BaseModel>(this.url, authorModel);
    }

    public update(authorModel: AuthorModelItem): Observable<BaseModel> {
        return this.http.patch<BaseModel>(this.url, authorModel);
    }

    public delete(authorModel: AuthorModelItem): Observable<BaseModel> {
        return this.http.delete<BaseModel>(this.url + authorModel.id);
    }
}
