import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PatientService {
  private apiUrl = 'https://your-api-url/api/patients';

  constructor(private http: HttpClient) {}

  addPatient(patient: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, patient);
  }
}