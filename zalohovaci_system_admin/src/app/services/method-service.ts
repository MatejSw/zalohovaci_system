import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BackupMethod } from '../models/backup-method';

@Injectable({
  providedIn: 'root',
})
export class MethodService {
  //private http: HttpClient = inject(HttpClient);

  public constructor(private http: HttpClient) {}

  public findAll(): Observable<BackupMethod[]> {
    return this.http.get<BackupMethod[]>('http://localhost:5210/api/BackupMethods');
  }

  public findById(id: number): Observable<BackupMethod> {
    return this.http.get<BackupMethod>('http://localhost:5210/api/BackupMethods/' + id);
  }
}
