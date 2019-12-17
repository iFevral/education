import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {
    PrintingEditionListComponent,
    DetailsComponent
} from '.';

const printingEditionRoutes: Routes = [{
    path: '',
    children: [
        { path: '', component: PrintingEditionListComponent, data: { animation: 'Home' } },
        { path: 'Details/:id', component: DetailsComponent, data: { animation: 'PrintingEditionDetails' } },
    ]
}];

export const printingEditionRouting = RouterModule.forChild(printingEditionRoutes);

@NgModule({
    imports: [
        RouterModule.forRoot(printingEditionRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class PrintingEditionRoutingModule { }
