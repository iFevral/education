import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ListComponent } from './list/list.component';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';

@NgModule({
    declarations: [ListComponent],
    imports: [
        BrowserModule,
        PrintingEditionRoutingModule
    ]
})
export class PrintingEditionModule { }
