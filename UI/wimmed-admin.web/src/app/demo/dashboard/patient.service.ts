import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface Patient {
  id: string;
  name: string;
  surname?: string;
  idNumber: string;
  phone?: string;
  email?: string;
  dateOfBirth: string;
}

export interface PatientInfo {
  id: string;
  patientId: string;
  infoType: string;
  infoValue: string;
  // Add more fields as needed based on API response
}

@Injectable({ providedIn: 'root' })
export class PatientService {

  editPatientInfo(patientInfoId: string | number, patientInfo: any) {
    return this.http.put(`${this.patientInfoUrl}/EditPatientInfo/${patientInfoId}`, patientInfo);
  }

  
    editPatient(patientId: string, patient: Partial<Patient>) {
    return this.http.put(`${this.baseUrl}/EditPatient/${patientId}`, patient);
  }
  private baseUrl = environment.apiBaseUrl + '/Patients';
  private patientInfoUrl = environment.apiBaseUrl + '/PatientInfos';

  constructor(private http: HttpClient) {}

  getAllPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.baseUrl}/all`);
  }

  searchPatients(criteria: { [key: string]: any }): Observable<Patient[]> {
    // Always send the search string as 'query' param
  return this.http.get<Patient[]>(`${this.baseUrl}/search`, { params: { query: criteria['search'] } });
  }

  getPatientInfoByPatientId(patientId: string): Observable<PatientInfo[]> {
    return this.http.get<PatientInfo[]>(`${this.patientInfoUrl}/GetPatientInfoByPatientId/${patientId}`);
  }

  deletePatient(patientId: string) {
    // This will delete the patient and related PatientInfos on the backend
    return this.http.delete(`${this.baseUrl}/DeletePatient/${patientId}`);
  }
}
