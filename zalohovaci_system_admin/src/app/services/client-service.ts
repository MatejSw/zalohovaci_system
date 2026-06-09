import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from '../models/client';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  //private http: HttpClient = inject(HttpClient);

  public constructor(private http: HttpClient) {}

  public findAll(): Observable<Client[]> {
    return this.http.get<Client[]>('http://localhost:5210/api/Clients');
  }

  public findById(id: number): Observable<Client> {
    return this.http.get<Client>('http://localhost:5210/api/Clients/' + id);
  }
}
