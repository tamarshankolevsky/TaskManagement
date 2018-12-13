import { Routes, RouterModule } from '@angular/router';

import {
    LoginComponent,
    UserManagementComponent,
    UserListComponent,
    AddUserComponent,
    EditUserComponent,
    ProjectManagementComponent,
    ManagerComponent,
    TeamLeaderComponent,
    WorkerComponent,
    AllProjectsComponent,
    AddProjectComponent,
    EditProjectComponent,
    ProjectDetailsComponent,
    WorkerTasksComponent,
    HoursChartComponent,
    ProjectReportListComponent,
    TeamLeaderWorkersComponent,
    WorkerDeatailsComponent
} from './imports';
import { AuthGuard } from './shared/auth.guard';

const appRoutes: Routes = [
    {
        path: 'taskManagement', children: [
            {
                path: 'login', component: LoginComponent
            },
            {
                path: 'manager', component: ManagerComponent, children: [
                    {
                        path: 'userManagement', component: UserManagementComponent, children: [
                            {
                                path: 'userList', component: UserListComponent
                            },
                            {
                                path: 'addUser', component: AddUserComponent
                            },
                            {
                                path: 'editUser/:id', component: EditUserComponent
                            },
                            {
                                path: '**', component: UserListComponent
                            }
                        ]
                    },
                    {
                        path: 'projectManagement', component: ProjectManagementComponent, children: [
                            {
                                path: 'allProjects', component: AllProjectsComponent
                            },
                            {
                                path: 'addProject', component: AddProjectComponent
                            },
                            {
                                path: 'projectDetails/:id', component: ProjectDetailsComponent
                            },
                            {
                                path: 'editProject/:id', component: EditProjectComponent
                            },
                            {
                                path: '**', component: AllProjectsComponent
                            }
                        ]
                    },
                    {
                        path: 'reportsMangement', component:ProjectReportListComponent
                    },
                ]
            },
            {
                path: 'teamLeader', component: TeamLeaderComponent, children: [
                    {
                        path: 'allProjects', component: AllProjectsComponent
                    },
                    {
                        path: 'teamLeaderWorkers', component: TeamLeaderWorkersComponent
                    },
                    {
                        path: 'WorkerDeatails/:id', component: WorkerDeatailsComponent
                    },
                    {
                        path: 'chartHours', component: HoursChartComponent
                    },
                    {
                        path: '**', component: HoursChartComponent
                    }
                ]
            },
            {
                path: 'worker', component: WorkerComponent, children: [
                    {
                        path: 'workerTasks', component: WorkerTasksComponent
                    }
                ]
           }
        ]
    },
    // { path: '', redirectTo: 'taskManagement/login' },
    { path: ' ', component: LoginComponent },
    // otherwise redirect to LoginComponent
    { path: '**', component: LoginComponent, pathMatch: 'full' }
];

export const routing = RouterModule.forRoot(appRoutes);