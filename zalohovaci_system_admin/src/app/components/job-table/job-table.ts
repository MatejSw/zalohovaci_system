import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BackupJob } from '../../models/backup-job';

@Component({
  selector: 'app-job-table',
  imports: [RouterLink],
  templateUrl: './job-table.html',
  styleUrl: './job-table.scss',
})
export class JobTable {
  @Input()
  public jobs: BackupJob[];
}
