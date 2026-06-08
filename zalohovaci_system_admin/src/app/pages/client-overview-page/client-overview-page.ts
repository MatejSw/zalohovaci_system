import { Component } from '@angular/core';
import { ClientTable } from '../../components/client-table/client-table';

@Component({
  selector: 'app-client-overview-page',
  imports: [ClientTable],
  templateUrl: './client-overview-page.html',
  styleUrl: './client-overview-page.scss',
})
export class ClientOverviewPage {}
