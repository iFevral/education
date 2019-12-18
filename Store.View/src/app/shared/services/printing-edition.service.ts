import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrintingEditionModelItem, PrintingEditionFilterModel, PrintingEditionModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService extends BaseService<PrintingEditionModel> {

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.printingEditionControllerUrl);
    }

    public getById(id: number): Observable<PrintingEditionModelItem> {
        return this.http.post<PrintingEditionModelItem>(this.url + id, {});
    }
}
