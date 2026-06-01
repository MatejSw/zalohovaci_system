import { Component, Input } from '@angular/core';
import { BackupJob } from '../../models/backup-job';

@Component({
  selector: 'app-job-details',
  imports: [],
  templateUrl: './job-details.html',
  styleUrl: './job-details.scss',
})
export class JobDetails {
  @Input()
  public job: BackupJob;
}
