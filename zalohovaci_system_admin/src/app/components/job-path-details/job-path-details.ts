import { Component, Input } from '@angular/core';
import { Path } from '../../models/path';

@Component({
  selector: 'app-job-path-details',
  imports: [],
  templateUrl: './job-path-details.html',
  styleUrl: './job-path-details.scss',
})
export class JobPathDetails {
  @Input()
  public paths: Path[];
  @Input()
  public title: string;
}
