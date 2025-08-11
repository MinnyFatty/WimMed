import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { signal } from '@angular/core';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class WimMedApiService {
  patients = signal<any[]>([]);

  constructor(private http: HttpClient) {}

  fetchPatients() {
    // Change the URL to match your API endpoint
    this.http.get<any[]>('https://localhost:7189/api/Patients/all').subscribe(data => {
      this.patients.set(data);
    });
  }
}