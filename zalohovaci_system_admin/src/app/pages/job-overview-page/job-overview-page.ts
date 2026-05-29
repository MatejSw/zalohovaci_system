import { Component, signal, WritableSignal } from '@angular/core';
import { JobTable } from '../../components/job-table/job-table';
import { BackupJob } from '../../models/backup-job';
import { JobService } from '../../services/job-service';

@Component({
  selector: 'app-job-overview-page',
  imports: [JobTable],
  templateUrl: './job-overview-page.html',
  styleUrl: './job-overview-page.scss',
})
export class JobOverviewPage {
  public data: WritableSignal<BackupJob[]> = signal([]);

  public constructor(private service: JobService) {
    this.service.findAll().subscribe((result) => this.data.set(result));
  }
}
