import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BootstrapModalModule } from 'ng2-bootstrap-modal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import {TreeTableModule} from 'primeng/treetable';
import { CalendarModule, MessageService } from 'primeng/primeng';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import {ToastModule} from 'primeng/toast';

import {
  MatInputModule,
  MatSelectModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatDialogModule,
  MatButtonModule,
  MatCheckboxModule,
  MatCardModule,
  MatMenuModule,
  MatTabsModule,
  MatListModule,
  MatDividerModule,
  MatTooltipModule
} from '@angular/material';

import {
  //services
  AuthenticationService,
  UserService,
  ProjectService,
  HoursService,
  WorkerHoursService,
  TeamLeaderService,
  ExcelService,
  ReportService,
  //components
  AppComponent,
  HeaderComponent,
  MainComponent,
  LoginComponent,
  UserManagementComponent,
  UserListComponent,
  AllProjectsComponent,
  TmpUserComponent,
  UserFormComponent,
  AddUserComponent,
  EditUserComponent,
  ConfirmComponent,
  UploadImgComponent,
  ProjectManagementComponent,
  AddProjectComponent,
  EditProjectComponent,
  ProjectDetailsComponent,
  ManagerComponent,
  TeamLeaderComponent,
  WorkerTasksComponent,
  WorkerComponent,
  WorkerHoursComponent,
  DatePickerComponent,
  ChartComponent,
  HoursChartComponent,
  TeamLeaderWorkersComponent,
  WorkerDeatailsComponent,
  TmpWorkerComponent,
  HoursComponent,
  SendEmailComponent,
  ProjectReportListComponent,
  FooterComponent,
  routing
} from './imports';
import { AuthGuard } from './shared/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    LoginComponent,
    UserManagementComponent,
    UserListComponent,
    TmpUserComponent,
    UserFormComponent,
    ProjectManagementComponent,
    AddUserComponent,
    EditUserComponent,
    ConfirmComponent,
    UploadImgComponent,
    AddProjectComponent,
    EditProjectComponent,
    ProjectDetailsComponent,
    ManagerComponent,
    TeamLeaderComponent,
    WorkerTasksComponent,
    WorkerComponent,
    WorkerHoursComponent,
    AllProjectsComponent,
    DatePickerComponent,
    ChartComponent,
    HoursChartComponent,
    TeamLeaderWorkersComponent,
    WorkerDeatailsComponent,
    TmpWorkerComponent,
    HoursComponent,
    SendEmailComponent,
    ProjectReportListComponent,
    FooterComponent
  ],

  imports: [
    BrowserModule,
    routing,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    BrowserAnimationsModule,
    BootstrapModalModule.forRoot({ container: document.body }),
    MatNativeDateModule,
    MatDatepickerModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
    MatButtonModule,
    MatCheckboxModule,
    MatCardModule,
    MatMenuModule,
    MatTabsModule,
    MatListModule,
    MatDividerModule,
    MatTooltipModule,
    TreeTableModule,
    CalendarModule,
    ProgressSpinnerModule,
    MessageModule,
    MessagesModule,
    ToastModule
  ],

  entryComponents: [
    ConfirmComponent, SendEmailComponent,EditProjectComponent
  ],

  providers: [
    AuthenticationService,
    UserService,
    ProjectService,
    HoursService,
    WorkerHoursService,
    TeamLeaderService,
    ExcelService,
    ReportService,
    MessageService,
    AuthGuard   
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
