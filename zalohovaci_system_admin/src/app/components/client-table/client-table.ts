import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Client } from '../../models/client';

@Component({
  selector: 'app-client-table',
  imports: [RouterLink],
  templateUrl: './client-table.html',
  styleUrl: './client-table.scss',
})
export class ClientTable {
  @Input()
  public clients: Client[];
}
