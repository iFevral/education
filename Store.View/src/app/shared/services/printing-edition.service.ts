import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';

import { PrintingEditionModelItem, PrintingEditionModel } from 'src/app/shared/models';
import { Constants } from 'src/app/shared/constants/constants';
import { BaseService } from 'src/app/shared/services/base/base.service';

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
