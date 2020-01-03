import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PrintingEditionManagerListComponent } from 'src/app/modules/printing-edition-manager/index';

const printingEditionRoutes: Routes = [
    { path: '', component: PrintingEditionManagerListComponent, data: { animation: 'PringtingEditions' } }
];


@NgModule({
    imports: [
        RouterModule.forChild(printingEditionRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class PrintingEditionManagerRoutingModule { }
