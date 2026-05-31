import { Component, signal, WritableSignal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { JobService } from '../../services/job-service';
import { BackupJob } from '../../models/backup-job';
import { MethodService } from '../../services/method-service';
import { BackupMethod } from '../../models/backup-method';
import { RetentionService } from '../../services/retention-service';
import { BackupRetention } from '../../models/backup-retention';
import { JobDetails } from '../../components/job-details/job-details';
import { JobPathDetails } from '../../components/job-path-details/job-path-details';
import { PathService } from '../../services/path-service';
import { Path } from '../../models/path';

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
    retention: 0,
    method: 0,
    timing: '',
    createdAt: '',
  });

  public method: WritableSignal<BackupMethod> = signal({
    id: 0,
    methodName: '',
  });

  public retention: WritableSignal<BackupRetention> = signal({
    id: 0,
    count: 0,
    size: 0,
  });

  public sourcePaths: WritableSignal<Path[]> = signal([]);
  public targetPaths: WritableSignal<Path[]> = signal([]);

  public constructor(
    private jobService: JobService,
    private methodService: MethodService,
    private retentionService: RetentionService,
    private pathService: PathService,
    private route: ActivatedRoute,
  ) {
    const id = this.route.snapshot.params['id'];
    this.jobService.findById(id).subscribe((result) => {
      this.job.set(result);

      this.methodService.findById(result.method).subscribe((resultM) => {
        this.method.set(resultM);
      });

      this.retentionService.findById(result.retention).subscribe((resultR) => {
        this.retention.set(resultR);
      });

      this.pathService.findByJobId(result.id, 1).subscribe((resultSP) => {
        this.sourcePaths.set(resultSP);
      });

      this.pathService.findByJobId(result.id, 2).subscribe((resultTP) => {
        this.targetPaths.set(resultTP);
      });
    });
  }
}
