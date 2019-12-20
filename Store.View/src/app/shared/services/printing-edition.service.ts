import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrintingEditionModelItem, PrintingEditionModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';
import { MatSnackBar } from '@angular/material';

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService extends BaseService<PrintingEditionModel> {

    constructor(
        http: HttpClient,
        messageContainer: MatSnackBar
    ) {
        super(http, Constants.apiUrls.printingEditionControllerUrl, messageContainer);
    }

    public getById(id: number): Observable<PrintingEditionModelItem> {
        return this.http.post<PrintingEditionModelItem>(this.url + id, {});
    }
}
