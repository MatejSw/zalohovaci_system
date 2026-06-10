import { Component, signal, WritableSignal } from '@angular/core';
import { LogTable } from '../../components/log-table/log-table';
import { Log } from '../../models/log';
import { LogService } from '../../services/log-service';
import { ActivatedRoute } from '@angular/router';
import { LogFilters } from '../../components/log-filters/log-filters';
import { distinct } from 'rxjs';

@Component({
  selector: 'app-log-page',
  imports: [LogTable, LogFilters],
  templateUrl: './log-page.html',
  styleUrl: './log-page.scss',
})
export class LogPage {
  public data: WritableSignal<Log[]> = signal([]);

  public constructor(
    private service: LogService,
    private route: ActivatedRoute,
  ) {
    const jobId: number = route.snapshot.queryParams['jobId'];
    const clientId: number = route.snapshot.queryParams['clientId'];

    if (jobId != null) {
      this.service.findByJobId(jobId).subscribe((result) => {
        this.data.set(result);
      });
    } else if (clientId != null) {
      this.service.findByJobId(jobId).subscribe((result) => this.data.set(result));
    } else {
      this.service.findAll().subscribe((result) => this.data.set(result));
    }
  }
}
