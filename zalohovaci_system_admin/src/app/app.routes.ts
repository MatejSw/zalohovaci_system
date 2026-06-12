import { Routes } from '@angular/router';
import { DashboardPage } from './pages/dashboard-page/dashboard-page';
import { JobOverviewPage } from './pages/job-overview-page/job-overview-page';
import { JobPage } from './pages/job-page/job-page';
import { ClientOverviewPage } from './pages/client-overview-page/client-overview-page';
import { EditJobPage } from './pages/edit-job-page/edit-job-page';
import { LogPage } from './pages/log-page/log-page';
import { ClientPage } from './pages/client-page/client-page';

export const routes: Routes = [
  {
    path: '',
    component: DashboardPage,
  },
  {
    path: 'jobs',
    component: JobOverviewPage,
  },
  {
    path: 'clients',
    component: ClientOverviewPage,
  },
  {
    path: 'job/:id',
    component: JobPage,
  },
  {
    path: 'editJob/:id',
    component: EditJobPage,
  },
  {
    path: 'logs',
    component: LogPage,
  },
  {
    path: 'client/:id',
    component: ClientPage,
  },
];
