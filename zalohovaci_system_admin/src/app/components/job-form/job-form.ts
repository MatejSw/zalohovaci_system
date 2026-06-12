import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BackupJob } from '../../models/backup-job';
import { FieldTree, FormField } from '@angular/forms/signals';

@Component({
  selector: 'app-job-form',
  imports: [FormField],
  templateUrl: './job-form.html',
  styleUrl: './job-form.scss',
})
export class JobForm {
  @Input()
  public form: FieldTree<BackupJob>;
}
