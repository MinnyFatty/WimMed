import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WimMedApiService } from './wimmed-api.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'patient-list',
  standalone: true,
  imports: [CommonModule],
  providers: [WimMedApiService],
  template: `
    <h2>Patients</h2>
    <!--button to load all patients from https://localhost:7189/api/Patients/all-->
    <button (click)="load()">Load Patients</button>
    <ul>
      <li *ngFor="let patient of api.patients()">
        {{ patient.name }}
      </li>
    </ul>
  `
})
export class PatientListComponent {
  constructor(public api: WimMedApiService) {}

  load() {

    this.api.fetchPatients();
  }
}