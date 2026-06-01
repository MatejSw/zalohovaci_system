import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-path-details',
  imports: [],
  templateUrl: './job-path-details.html',
  styleUrl: './job-path-details.scss',
})
export class JobPathDetails {
  @Input()
  public paths: string[];
  @Input()
  public title: string;
}
