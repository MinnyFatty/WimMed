import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PatientService, Patient } from './patient.service';
import { PatientInfo } from './patient.service';
import { SharedModule } from 'src/app/theme/shared/shared.module';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, SharedModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  lastPatientInfoMessage: string | null = null;
  errorMessage: string | null = null;

  editPatientInfoModalOpen = false;
  editingPatientInfo: any = null;
  editPatientInfoForm: any = {};

  openEditPatientInfo(info: any) {
    this.editingPatientInfo = { ...info };
    this.editPatientInfoForm = { ...info };
    this.editPatientInfoModalOpen = true;
  }

  saveEditedPatientInfo() {
    if (!this.editingPatientInfo) return;
    this.patientService.editPatientInfo(this.editingPatientInfo.id, this.editPatientInfoForm).subscribe({
      next: (updatedInfo: any) => {
        // Show API response message if present
        if (updatedInfo && updatedInfo.message) {
          this.lastPatientInfoMessage = updatedInfo.message;
        } else {
          this.lastPatientInfoMessage = 'Patient info updated successfully.';
        }
        // Refresh patientInfos from API to get latest info
        if (this.expandedPatientId) {
          this.patientInfosLoading = true;
          this.patientService.getPatientInfoByPatientId(this.expandedPatientId).subscribe({
            next: (data) => {
              this.patientInfos = data;
              this.patientInfosLoading = false;
            },
            error: (err) => {
              this.patientInfos = [];
              this.patientInfosLoading = false;
            }
          });
        }
        this.editPatientInfoModalOpen = false;
        this.editingPatientInfo = null;
      },
      error: (err) => {
        let message = 'Failed to update patient info.';
        if (err && err.error) {
          if (typeof err.error === 'string') {
            message += '\n' + err.error;
            this.lastPatientInfoMessage = err.error;
          } else if (err.error.message) {
            message += '\n' + err.error.message;
            this.lastPatientInfoMessage = err.error.message;
          } else {
            this.lastPatientInfoMessage = message;
          }
        } else {
          this.lastPatientInfoMessage = message;
        }
        alert(message);
        console.error(err);
      }
    });
  }

  cancelEditPatientInfo() {
  this.lastPatientInfoMessage = null;
    this.editPatientInfoModalOpen = false;
    this.editingPatientInfo = null;
  }
  editingPatient: Patient | null = null;
  editPatientForm: Partial<Patient> = {};
  editModalOpen = false;

  editPatient(patient: Patient) {
    this.editingPatient = { ...patient };
    this.editPatientForm = { ...patient };
    this.editModalOpen = true;
  }

  saveEditedPatient() {
    if (!this.editingPatient) return;
    this.patientService.editPatient(this.editingPatient.id, this.editPatientForm).subscribe({
      next: (updatedPatient) => {
        const idx = this.patients.findIndex(p => p.id === this.editingPatient!.id);
        if (idx !== -1) {
          this.patients[idx] = { ...this.patients[idx], ...this.editPatientForm } as Patient;
        }
        this.editModalOpen = false;
        this.editingPatient = null;
      },
      error: (err) => {
        let message = 'Failed to update patient.';
        if (err && err.error) {
          if (typeof err.error === 'string') {
            message += '\n' + err.error;
          } else if (err.error.message) {
            message += '\n' + err.error.message;
          }
        }
        alert(message);
        console.error(err);
      }
    });
  }

  cancelEditPatient() {
    this.editModalOpen = false;
    this.editingPatient = null;
  }
  patients: Patient[] = [];
  searchString: string = '';
  loading: boolean = false;

  expandedPatientId: string | null = null;
  patientInfos: PatientInfo[] = [];

  patientInfosLoading: boolean = false;

  formatFieldName(field: string): string {
    // Insert space before capital letters and capitalize first letter
    return field
      .replace(/([A-Z])/g, ' $1')
      .replace(/^./, (c) => c.toUpperCase());
  }


  deletePatient(patient: Patient) {
    if (!confirm(`Are you sure you want to delete patient '${patient.name}' and all related info? This action cannot be undone.`)) {
      return;
    }
    this.patientService.deletePatient(patient.id).subscribe({
      next: () => {
        this.getAllPatients();
        if (this.expandedPatientId === patient.id) {
          this.expandedPatientId = null;
          this.patientInfos = [];
        }
        this.errorMessage = null;
        alert('Patient and related info deleted successfully.');
      },
      error: (err) => {
        let message = '';
        if (err && err.error) {
          if (typeof err.error === 'string') {
            message = err.error;
          } else if (err.error.text) {
            message = err.error.text;
          } else if (err.error.message) {
            message = err.error.message;
          } else {
            message = '';
          }
        } else {
          message = 'An unknown error occurred.';
        }
        // Beautify: capitalize first letter, replace underscores, etc.
        if (message) {
          message = message.replace(/_/g, ' ');
          message = message.charAt(0).toUpperCase() + message.slice(1);
        } else {
          message = 'An unknown error occurred.';
        }
        this.errorMessage = message;
        console.error(err);
      }
    });
  }

  togglePatientInfo(patient: Patient) {
    console.log('Fetching info for PatientId:', patient.id);
    if (this.expandedPatientId === patient.id) {
      this.expandedPatientId = null;
      this.patientInfos = [];
      return;
    }
    this.expandedPatientId = patient.id;
    this.patientInfosLoading = true;
    this.patientService.getPatientInfoByPatientId(patient.id).subscribe({
      next: (data) => {
        console.log('Fetched patient info:', data);
        this.patientInfos = data;
        this.patientInfosLoading = false;
      },
      error: (err) => {
        console.error('Error fetching patient info:', err);
        this.patientInfos = [];
        this.patientInfosLoading = false;
      }
    });
  }

  constructor(private patientService: PatientService) {}

  ngOnInit() {
    this.getAllPatients();
  }


  getAllPatients() {
    this.loading = true;
    this.patientService.getAllPatients().subscribe({
      next: (data) => {
        console.log('Fetched patients:', data);
        this.patients = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching patients:', err);
        this.patients = [];
        this.loading = false;
      }
    });
  }

  searchPatients() {
    this.loading = true;
    this.patientService.searchPatients({ search: this.searchString }).subscribe({
      next: (data) => {
        console.log('Searched patients:', data);
        this.patients = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error searching patients:', err);
        this.patients = [];
        this.loading = false;
      }
    });
  }

  // public method
  sales = [
    {
      title: 'Daily Sales',
      icon: 'icon-arrow-up text-c-green',
      amount: '$249.95',
      percentage: '67%',
      progress: 50,
      design: 'col-md-6',
      progress_bg: 'progress-c-theme'
    },
    {
      title: 'Monthly Sales',
      icon: 'icon-arrow-down text-c-red',
      amount: '$2,942.32',
      percentage: '36%',
      progress: 35,
      design: 'col-md-6',
      progress_bg: 'progress-c-theme2'
    },
    {
      title: 'Yearly Sales',
      icon: 'icon-arrow-up text-c-green',
      amount: '$8,638.32',
      percentage: '80%',
      progress: 70,
      design: 'col-md-12',
      progress_bg: 'progress-c-theme'
    }
  ];

  card = [
    {
      design: 'border-bottom',
      number: '235',
      text: 'TOTAL IDEAS',
      icon: 'icon-zap text-c-green'
    },
    {
      number: '26',
      text: 'TOTAL LOCATIONS',
      icon: 'icon-map-pin text-c-blue'
    }
  ];

  social_card = [
    {
      design: 'col-md-12',
      icon: 'fab fa-facebook-f text-primary',
      amount: '12,281',
      percentage: '+7.2%',
      color: 'text-c-green',
      target: '35,098',
      progress: 60,
      duration: '3,539',
      progress2: 45,
      progress_bg: 'progress-c-theme',
      progress_bg_2: 'progress-c-theme2'
    },
    {
      design: 'col-md-6',
      icon: 'fab fa-twitter text-c-blue',
      amount: '11,200',
      percentage: '+6.2%',
      color: 'text-c-purple',
      target: '34,185',
      progress: 40,
      duration: '4,567',
      progress2: 70,
      progress_bg: 'progress-c-theme',
      progress_bg_2: 'progress-c-theme2'
    },
    {
      design: 'col-md-6',
      icon: 'fab fa-google-plus-g text-c-red',
      amount: '10,500',
      percentage: '+5.9%',
      color: 'text-c-blue',
      target: '25,998',
      progress: 80,
      duration: '7,753',
      progress2: 50,
      progress_bg: 'progress-c-theme',
      progress_bg_2: 'progress-c-theme2'
    }
  ];

  progressing = [
    {
      number: '5',
      amount: '384',
      progress: 70,
      progress_bg: 'progress-c-theme'
    },
    {
      number: '4',
      amount: '145',
      progress: 35,
      progress_bg: 'progress-c-theme'
    },
    {
      number: '3',
      amount: '24',
      progress: 25,
      progress_bg: 'progress-c-theme'
    },
    {
      number: '2',
      amount: '1',
      progress: 10,
      progress_bg: 'progress-c-theme'
    },
    {
      number: '1',
      amount: '0',
      progress: 0,
      progress_bg: 'progress-c-theme'
    }
  ];

  tables = [
    {
      src: 'assets/images/user/avatar-1.jpg',
      title: 'Isabella Christensen',
      text: 'Requested account activation',
      time: '11 MAY 12:56',
      color: 'text-c-green'
    },
    {
      src: 'assets/images/user/avatar-2.jpg',
      title: 'Ida Jorgensen',
      text: 'Pending document verification',
      time: '11 MAY 10:35',
      color: 'text-c-red'
    },
    {
      src: 'assets/images/user/avatar-3.jpg',
      title: 'Mathilda Andersen',
      text: 'Completed profile setup',
      time: '9 MAY 17:38',
      color: 'text-c-green'
    },
    {
      src: 'assets/images/user/avatar-1.jpg',
      title: 'Karla Soreness',
      text: 'Requires additional information',
      time: '19 MAY 12:56',
      color: 'text-c-red'
    },
    {
      src: 'assets/images/user/avatar-2.jpg',
      title: 'Albert Andersen',
      text: 'Approved and verified account',
      time: '21 July 12:56',
      color: 'text-c-green'
    }
  ];
}
