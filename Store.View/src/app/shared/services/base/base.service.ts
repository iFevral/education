import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseModel, BaseFilterModel } from '../../models';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService<T extends BaseModel> {

    private messageContainer: MatSnackBar;
    constructor(protected http: HttpClient, protected url: string) { }

    public getAll<F extends BaseFilterModel>(filterModel: F): Observable<T> {
        return this.http.post<T>(this.url, filterModel);
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
