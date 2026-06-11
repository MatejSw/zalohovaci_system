import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Log } from '../../models/log';

@Component({
  selector: 'app-log-filters',
  imports: [RouterLink],
  templateUrl: './log-filters.html',
  styleUrl: './log-filters.scss',
})
export class LogFilters {
  @Input()
  public jobs: Log[];
  @Input()
  public clients: Log[];
}
