import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Path } from '../models/path';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PathService {
  //private http: HttpClient = inject(HttpClient);

  public constructor(private http: HttpClient) {}

  public findAll(): Observable<Path[]> {
    return this.http.get<Path[]>('http://localhost:5210/api/FilePaths');
  }

  public findById(id: number): Observable<Path> {
    return this.http.get<Path>('http://localhost:5210/api/FilePaths/' + id);
  }

  public findByJobId(id: number, type: number): Observable<Path[]> {
    return this.http.get<Path[]>('http://localhost:5210/api/FilePaths/combo/' + id + '?type=' + type);
  }
}
