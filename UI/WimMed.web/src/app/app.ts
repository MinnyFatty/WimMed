import { Component, inject, Input, input, InputSignal, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PatientListComponent } from './patient-list.component';
import { HttpClient, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
// other imports
import { Patient } from '../models/Patient.model'; // Adjust the path if needed
import { AsyncPipe } from '@angular/common';
import { JsonPipe } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
import { WimMedApiService } from './wimmed-api.service';
import { appConfig } from './app.config';
import { NewPatient } from '../models/NewPatient.model';

// import { AppComponent } from './app.component'; // Removed because AppComponent is declared in this file



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class AppComponent {
  protected readonly title = signal('WimMed.web');
  http = inject(HttpClient);

  patients$ = this.getPatients();

//accept input from app.html for newPatient
  name: InputSignal<string> = input('');
  surname: InputSignal<string> = input('');
  idNumber: InputSignal<string> = input('');
  phone: InputSignal<string> = input('');
  email: InputSignal<string> = input('');
  dateOfBirth: InputSignal<Date> = input(new Date());

  newPatient: NewPatient = {
    name: this.name(),
    surname: this.surname(),
    idNumber: this.idNumber(),
    phone: this.phone(),
    email: this.email(),
    dateOfBirth: this.dateOfBirth()
  };

   newPatient$  = this.addPatient(this.newPatient);

private getPatients(): Observable<Patient[]> {
  return this.http.get<Patient[]>('https://localhost:7189/api/Patients/all');
}

public editPatient(id: string, patient: Patient): Observable<Patient> {
  return this.http.put<Patient>(`https://localhost:7189/api/Patients/${id}`, patient);
}

public addPatient(patient: NewPatient): Observable<Patient> {
  return this.http.post<Patient>('https://localhost:7189/api/Patients', patient);
}

public deletePatient(id: string): Observable<void> {
  return this.http.delete<void>(`https://localhost:7189/api/Patients/${id}`);
}

}