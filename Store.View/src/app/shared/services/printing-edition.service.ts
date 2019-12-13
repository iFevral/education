import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PrintingEditionModelItem, PrintingEditionFilterModel, PrintingEditionModel } from '../models';

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService {

    private url = 'https://localhost:44312/PrintingEditions';
    private url2 = 'https://localhost:44312/PrintingEditions/Get/';
    constructor(private http: HttpClient) { }

    public getAll(filterModel: PrintingEditionFilterModel): Observable<PrintingEditionModel> {
        filterModel = this.validateFilterData(filterModel);

        return this.http.post<PrintingEditionModel>(this.url, filterModel);
    }

    public getById(id: number): Observable<PrintingEditionModelItem> {
        return this.http.post<PrintingEditionModelItem>(this.url2 + id, {});
    }

    private validateFilterData(filterModel: PrintingEditionFilterModel) {
        if (filterModel.searchQuery) {
            filterModel.searchQuery = filterModel.searchQuery.trim();
        }
        return filterModel;
    }
}
