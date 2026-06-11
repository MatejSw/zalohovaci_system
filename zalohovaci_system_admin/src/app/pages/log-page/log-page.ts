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

  public jobIds: WritableSignal<Log[]> = signal([]);
  public clientIds: WritableSignal<Log[]> = signal([]);

  public filter: WritableSignal<string> = signal("Žádný");

  public constructor(
    private service: LogService,
    private route: ActivatedRoute,
  ) {
    const jobId: number = route.snapshot.queryParams['jobId'];
    const clientId: number = route.snapshot.queryParams['clientId'];

    this.service.findAll().subscribe((result) => {
      this.data.set(result)
      this.clientIds.set(
        result.filter((Log, i, arr) => arr.findIndex(t => t.clientId === Log.clientId) === i),
      );
      this.jobIds.set(
        result.filter((Log, i, arr) => arr.findIndex((t) => t.jobId === Log.jobId) === i),
      );
    })

    if (jobId != null) {
      this.service.findByJobId(jobId).subscribe((result) => {
        this.data.set(result);
      });
      this.filter.set('Záložní úloha ' + jobId.toString());
    } else if (clientId != null) {
      this.service.findByJobId(jobId).subscribe((result) => this.data.set(result));
      this.filter.set('Klient ' + clientId.toString());
    }
  }
}
