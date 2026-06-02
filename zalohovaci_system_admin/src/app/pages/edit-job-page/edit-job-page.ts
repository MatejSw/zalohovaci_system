import { Component, signal, WritableSignal } from '@angular/core';
import { BackupJob } from '../../models/backup-job';
import { JobService } from '../../services/job-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { form } from '@angular/forms/signals';
import { JobDetails } from '../../components/job-details/job-details';
import { JobForm } from '../../components/job-form/job-form';

@Component({
  selector: 'app-edit-job-page',
  imports: [JobDetails, JobForm, RouterLink],
  templateUrl: './edit-job-page.html',
  styleUrl: './edit-job-page.scss',
})
export class EditJobPage {
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

  public form = form(this.job);

  public sourcePaths: WritableSignal<string[]> = signal([]);
  public targetPaths: WritableSignal<string[]> = signal([]);

  public constructor(
    private jobService: JobService,
    public route: ActivatedRoute,
    private router: Router
  ) {
    const id = this.route.snapshot.params['id'];
    this.jobService.findById(id).subscribe((result) => {
      this.job.set(result);
      this.sourcePaths.set(result.sources);
      this.targetPaths.set(result.targets);
    });
  }

  public save(): void {
    this.jobService.save(this.job()).subscribe((result) => this.router.navigate(['/job/' + this.route.snapshot.params['id']]));
  }
}
