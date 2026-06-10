import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from '../models/client';
import { Log } from '../models/log';

@Injectable({
  providedIn: 'root',
})
export class LogService {
  //private http: HttpClient = inject(HttpClient);

  public constructor(private http: HttpClient) {}

  public findAll(): Observable<Log[]> {
    return this.http.get<Log[]>('http://localhost:5210/api/Logs');
  }

  public findByJobId(id: number): Observable<Log[]> {
    return this.http.get<Log[]>('http://localhost:5210/api/Logs/job/' + id);
  }

  public findByClientId(id: number): Observable<Log[]> {
    return this.http.get<Log[]>('http://localhost:5210/api/Logs/client/' + id);
  }
}
