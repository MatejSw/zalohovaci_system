import { Component, Input } from '@angular/core';
import { Path } from '../../models/path';
import { BackupMethod } from '../../models/backup-method';
import { BackupRetention } from '../../models/backup-retention';
import { BackupJob } from '../../models/backup-job';

@Component({
  selector: 'app-job-details',
  imports: [],
  templateUrl: './job-details.html',
  styleUrl: './job-details.scss',
})
export class JobDetails {
  @Input()
  public method: BackupMethod;
  @Input()
  public retention: BackupRetention;
  @Input()
  public job: BackupJob;
}
