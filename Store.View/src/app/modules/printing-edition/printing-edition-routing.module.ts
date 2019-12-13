import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PrintingEditionListComponent, DetailsComponent } from '.';

const printingEditionRoutes: Routes = [{
    path: 'PrintingEdition',
    children: [
        { path: 'List', component: PrintingEditionListComponent },
        { path: 'Details/:id', component: DetailsComponent },
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
