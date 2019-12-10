import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from '.';

const printingEditionRoutes: Routes = [
    { path: 'List', component: ListComponent },
];

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
