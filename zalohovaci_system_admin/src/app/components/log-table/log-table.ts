import { Component, Input } from '@angular/core';
import { Log } from '../../models/log';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-log-table',
  imports: [RouterLink],
  templateUrl: './log-table.html',
  styleUrl: './log-table.scss',
})
export class LogTable {
  @Input()
  public logs: Log[];
}
