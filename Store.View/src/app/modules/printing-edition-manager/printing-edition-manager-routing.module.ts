import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PrintingEditionManagerListComponent } from '.';


const printingEditionRoutes: Routes = [{
    path: 'PrintingEditions',
    children: [
        { path: '', component: PrintingEditionManagerListComponent }
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
export class PrintingEditionManagerRoutingModule { }
