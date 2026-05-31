import { Component, signal, WritableSignal } from '@angular/core';
import { JobTable } from '../../components/job-table/job-table';
import { JobService } from '../../services/job-service';
import { BackupJob } from '../../models/backup-job';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard-page',
  imports: [JobTable, RouterLink],
  templateUrl: './dashboard-page.html',
  styleUrl: './dashboard-page.scss',
})
export class DashboardPage {}
