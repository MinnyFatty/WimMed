import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { PatientsComponent } from './patients.component';

import { PatientsRoutingModule } from './patients-routing.module';

@NgModule({
  declarations: [PatientsComponent],
  imports: [
    CommonModule,
    FormsModule,
    NzTableModule,
    NzInputModule,
    NzPaginationModule,
  PatientsRoutingModule
  ]
})
export class PatientsModule {}
