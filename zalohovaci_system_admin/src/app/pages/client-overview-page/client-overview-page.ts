import { Component, signal, WritableSignal } from '@angular/core';
import { ClientTable } from '../../components/client-table/client-table';
import { Client } from '../../models/client';
import { ClientService } from '../../services/client-service';

@Component({
  selector: 'app-client-overview-page',
  imports: [ClientTable],
  templateUrl: './client-overview-page.html',
  styleUrl: './client-overview-page.scss',
})
export class ClientOverviewPage {
  public data: WritableSignal<Client[]> = signal([]);

  public constructor(private service: ClientService) {
    this.service.findAll().subscribe((result) => this.data.set(result));
  }
}
