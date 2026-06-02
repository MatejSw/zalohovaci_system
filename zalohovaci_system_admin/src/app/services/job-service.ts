import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackupJob } from '../models/backup-job';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class JobService {
  //private http: HttpClient = inject(HttpClient);

  public constructor(private http: HttpClient) {}

  public findAll(): Observable<BackupJob[]> {
    return this.http.get<BackupJob[]>('http://localhost:5210/api/BackupJobs');
  }

  public findById(id: number): Observable<BackupJob> {
    return this.http.get<BackupJob>('http://localhost:5210/api/BackupJobs/full/' + id);
  }

  public save(job: BackupJob): Observable<BackupJob> {
    return this.http.put<BackupJob>('http://localhost:5210/api/BackupJobs/' + job.id, job)
  }
}
