import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseModel, BaseFilterModel } from '../../models';
import { MatSnackBar } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService {

    private messageContainer: MatSnackBar;
    constructor(protected http: HttpClient, protected url: string) { }

    public getAll(filterModel: BaseFilterModel) {
        return this.http.post<BaseModel>(this.url, filterModel);
    }

    public create(model: BaseModel) {
        return this.http.put<BaseModel>(this.url, model);
    }

    public update(model: BaseModel) {
        return this.http.patch<BaseModel>(this.url, model);
    }

    public delete(id: number) {
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
