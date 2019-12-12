import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent, DetailsComponent } from '.';

const printingEditionRoutes: Routes = [
    { path: 'List', component: ListComponent, data: { animation: 'List' } },
    { path: 'Details/:id', component: DetailsComponent, data: { animation: 'Details' } },
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
