// ...existing imports...
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from '../models/Patient.model';
import { FormsModule } from '@angular/forms';
import { NewPatient } from '../models/NewPatient.model';
import { PatientInfo } from '../models/PatientInfo.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class AppComponent {
  http = inject(HttpClient);

  patients$: Observable<Patient[]> = this.getPatients();

  newPatient: NewPatient = {
    name: '',
    surname: '',
    idNumber: '',
    phone: '',
    email: '',
    dateOfBirth: new Date()
  };

  // Fetch all patients
  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>('https://localhost:7189/api/Patients/all');
  }

  // Refresh patient list
  refreshPatients() {
    this.patients$ = this.getPatients();
  }

  // Add a new patient
  onAddPatient() {
    this.http.post<Patient>('https://localhost:7189/api/Patients', this.newPatient)
      .subscribe({
        next: () => {
          this.refreshPatients();
          this.newPatient = {
            name: '',
            surname: '',
            idNumber: '',
            phone: '',
            email: '',
            dateOfBirth: new Date()
          };
        }
      });
  }
  // Update an existing patient
  onUpdatePatient(id: string) {
    this.http.put<Patient>(`https://localhost:7189/api/Patients/${id}`, this.newPatient)
      .subscribe(() => {
        this.refreshPatients();
        this.newPatient = {
          name: '',
          surname: '',
          idNumber: '',
          phone: '',
          email: '',
          dateOfBirth: new Date()
        };
      });
  }

  // Select a patient for editing
  onSelectPatient(patient: Patient) {
    this.newPatient = { ...patient };
  }

  // Delete a patient
  onDeletePatient(id: string) {
    this.http.delete<void>(`https://localhost:7189/api/Patients/${id}`)
      .subscribe(() => this.refreshPatients());
  }

  // Deselect the currently selected patient
  onDeselectPatient() {
    this.newPatient = {
      name: '',
      surname: '',
      idNumber: '',
      phone: '',
      email: '',
      dateOfBirth: new Date()
    };
  } 

// Update PatientInfo using the specific endpoint
onEditPatientInfo(patientId: string, patientInfo: PatientInfo) {
  this.http.put<PatientInfo>(`https://localhost:7189/api/PatientInfos/EditPatientInfo/${patientId}`, patientInfo)
    .subscribe(() => this.refreshPatients());
}
}