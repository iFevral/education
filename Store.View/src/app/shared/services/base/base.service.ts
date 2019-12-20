import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseModel, BaseFilterModel } from '../../models';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService<Model extends BaseModel> {

    constructor(protected http: HttpClient, protected url: string, protected messageContainer: MatSnackBar) { }

    public getAll<FilterModel extends BaseFilterModel>(filterModel: FilterModel): Observable<Model> {
        return this.http.post<Model>(this.url, filterModel);
    }

    public create(model: BaseModel): Observable<BaseModel> {
        console.log(model);
        return this.http.put<BaseModel>(this.url, model);
    }

    public update(model: BaseModel): Observable<BaseModel> {
        return this.http.patch<BaseModel>(this.url, model);
    }

    public delete(id: number): Observable<BaseModel> {
        return this.http.delete<BaseModel>(this.url + id);
    }

    public showDialogMessage(message: string) {
        if (message === '') {
            return;
        }

        this.messageContainer.open(message, 'X', {
            duration: 5000,
            verticalPosition: 'top'
        });
    }
}
