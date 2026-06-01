import { Component, signal, WritableSignal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { JobService } from '../../services/job-service';
import { BackupJob } from '../../models/backup-job';
import { JobDetails } from '../../components/job-details/job-details';
import { JobPathDetails } from '../../components/job-path-details/job-path-details';

@Component({
  selector: 'app-job-page',
  imports: [JobDetails, RouterLink, JobPathDetails],
  templateUrl: './job-page.html',
  styleUrl: './job-page.scss',
})
export class JobPage {
  public job: WritableSignal<BackupJob> = signal({
    id: 0,
    jobId: '',
    sources: [],
    targets: [],
    retentionSize: 0,
    retentionCount: 0,
    method: '',
    timing: '',
    createdAt: '',
  });

  public sourcePaths: WritableSignal<string[]> = signal([]);
  public targetPaths: WritableSignal<string[]> = signal([]);

  public constructor(
    private jobService: JobService,
    private route: ActivatedRoute,
  ) {
    const id = this.route.snapshot.params['id'];
    this.jobService.findById(id).subscribe((result) => {
      this.job.set(result);
      this.sourcePaths.set(result.sources)
      this.targetPaths.set(result.targets)
    });
  }
}
