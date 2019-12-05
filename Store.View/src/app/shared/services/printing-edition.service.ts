import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { PrintingEditionModel } from '../models/printing-edition/printing-edition.model';


const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};

@Injectable({
    providedIn: 'root'
})
export class PrintingEditionService {

    private url = 'https://localhost:44312/PrintingEditions';
    constructor(private http: HttpClient) { }
    getAll() {
        return this.http.post<PrintingEditionModel>(this.url, new Object(), httpOptions)
            .toPromise();
    }
}
