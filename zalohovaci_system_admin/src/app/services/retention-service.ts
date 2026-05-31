import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BackupRetention } from '../models/backup-retention';

@Injectable({
  providedIn: 'root',
})
export class RetentionService {
  //private http: HttpClient = inject(HttpClient);

  public constructor(private http: HttpClient) {}

  public findAll(): Observable<BackupRetention[]> {
    return this.http.get<BackupRetention[]>('http://localhost:5210/api/BackupRetention');
  }

  public findById(id: number): Observable<BackupRetention> {
    return this.http.get<BackupRetention>('http://localhost:5210/api/BackupRetention/' + id);
  }
}
