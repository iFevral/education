import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrintingEditionModelItem, PrintingEditionFilterModel, PrintingEditionModel, BaseModel } from '../models';
import { Constants } from '../constants/constants';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService {

    private url = Constants.apiUrls.printingEditionControllerUrl;
    constructor(private http: HttpClient) { }

    public getAll(filterModel: PrintingEditionFilterModel): Observable<PrintingEditionModel> {
        filterModel = this.validateFilterData(filterModel);
        return this.http.post<PrintingEditionModel>(this.url, filterModel);
    }

    public getById(id: number): Observable<PrintingEditionModelItem> {
        return this.http.post<PrintingEditionModelItem>(this.url + id, {});
    }

    public create(printingEditionModel: PrintingEditionModelItem): Observable<BaseModel> {
        return this.http.put<BaseModel>(this.url, printingEditionModel);
    }

    public update(printingEditionModel: PrintingEditionModelItem): Observable<BaseModel> {
        return this.http.patch<BaseModel>(this.url, printingEditionModel);
    }

    public delete(printingEditionModel: PrintingEditionModelItem): Observable<BaseModel> {
        return this.http.delete<BaseModel>(this.url + printingEditionModel.id);
    }

    private validateFilterData(filterModel: PrintingEditionFilterModel) {
        if (filterModel.searchQuery) {
            filterModel.searchQuery = filterModel.searchQuery.trim();
        }
        return filterModel;
    }
}
