import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrintingEditionModelItem, PrintingEditionFilterModel, PrintingEditionModel } from '../models';
import { Constants } from '../constants/constants';
import { BaseService } from './base/base.service';

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService extends BaseService{

    constructor(protected http: HttpClient) {
        super(http, Constants.apiUrls.printingEditionControllerUrl);
    }

    public getAll(filterModel: PrintingEditionFilterModel): Observable<PrintingEditionModel> {
        filterModel = this.validateFilterData(filterModel);
        return this.http.post<PrintingEditionModel>(this.url, filterModel);
    }

    public getById(id: number): Observable<PrintingEditionModelItem> {
        return this.http.post<PrintingEditionModelItem>(this.url + id, {});
    }

    private validateFilterData(filterModel: PrintingEditionFilterModel) {
        if (filterModel.searchQuery) {
            filterModel.searchQuery = filterModel.searchQuery.trim();
        }
        return filterModel;
    }
}
