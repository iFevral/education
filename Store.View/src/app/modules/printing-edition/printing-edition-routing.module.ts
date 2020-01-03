import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    PrintingEditionListComponent,
    DetailsComponent
} from '.';

const printingEditionRoutes: Routes = [
    { path: '', component: PrintingEditionListComponent, data: { animation: 'Home' } },
    { path: 'Details/:id', component: DetailsComponent, data: { animation: 'PrintingEditionDetails' } },
];


@NgModule({
    imports: [
        RouterModule.forChild(printingEditionRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class PrintingEditionRoutingModule { }
