import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { PrintingEditionModel } from '../models/printing-edition/printing-edition.model';

@Injectable({
  providedIn: 'root'
})
export class PrintingEditionService {

    private url = 'https://localhost:44312/PrintingEdition/GetAll';
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get(this.url).toPromise();
    }
}
