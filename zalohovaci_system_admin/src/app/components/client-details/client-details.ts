import { Component, EventEmitter, Input, Output, signal, WritableSignal } from '@angular/core';
import { Client } from '../../models/client';
import { BackupJob } from '../../models/backup-job';
import { FieldTree, FormField } from '@angular/forms/signals';
import { ClientJob } from '../../models/client-job';

@Component({
  selector: 'app-client-details',
  imports: [FormField],
  templateUrl: './client-details.html',
  styleUrl: './client-details.scss',
})
export class ClientDetails {
  @Input()
  public client: Client;
  @Input()
  public jobs: BackupJob[];
  @Input()
  public jobList: FieldTree<ClientJob[]>;
  @Output()
  public saved: EventEmitter<void> = new EventEmitter();

  public save(): void {
    this.saved.emit();
  }
}
