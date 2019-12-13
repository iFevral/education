import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthorModel, AuthorFilterModel, AuthorModelItem, BaseModel } from '../models';

@Injectable({
    providedIn: 'root'
})
export class AuthorService {

    private url = 'https://localhost:44312/Authors';
    private url2 = 'https://localhost:44312/Authors/Get/';
    private url3 = 'https://localhost:44312/Authors/Create';
    private url4 = 'https://localhost:44312/Authors/Update';
    constructor(private http: HttpClient) { }

    public getAll(filterModel: AuthorFilterModel): Observable<AuthorModel> {
        return this.http.post<AuthorModel>(this.url, filterModel);
    }

    public create(authorModel: AuthorModelItem): Observable<BaseModel> {
        return this.http.put<BaseModel>(this.url3, authorModel);
    }

    public update(authorModel: AuthorModelItem): Observable<BaseModel> {
        return this.http.put<BaseModel>(this.url4, authorModel);
    }
}
