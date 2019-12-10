import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PrintingEditionModel } from '../models/printing-edition/printing-edition.model';
import { PrintingEditionFilterModel } from '../models/printing-edition/printing-edition.filter.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService {

    private url = 'https://localhost:44312/PrintingEditions';

    constructor(private http: HttpClient) {}

    public getAll(filterModel: PrintingEditionFilterModel): Observable<PrintingEditionModel> {
        return this.http.post<PrintingEditionModel>(this.url, filterModel);
    }
}
