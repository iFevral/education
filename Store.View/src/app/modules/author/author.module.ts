import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorListComponent } from '.';
import { AuthorRoutingModule } from './author-routing.module';
import { MaterialElementsModule } from '../../shared/modules';
import { DialogUpdateComponent } from './components/dialog-update/dialog-update.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [AuthorListComponent, DialogUpdateComponent],
  imports: [
    CommonModule,
    AuthorRoutingModule,
    MaterialElementsModule,
    FormsModule
  ],
  entryComponents: [
    DialogUpdateComponent
  ]
})
export class AuthorModule { }
