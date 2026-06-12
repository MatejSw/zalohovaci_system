import { Component, ElementRef, EmbeddedViewRef, signal, ViewChild, WritableSignal } from '@angular/core';
import { ClientDetails } from '../../components/client-details/client-details';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Client } from '../../models/client';
import { ClientService } from '../../services/client-service';
import { JobService } from '../../services/job-service';
import { BackupJob } from '../../models/backup-job';
import { ClientJob } from '../../models/client-job';
import { form } from '@angular/forms/signals';

@Component({
  selector: 'app-client-page',
  imports: [ClientDetails, RouterLink],
  templateUrl: './client-page.html',
  styleUrl: './client-page.scss',
})
export class ClientPage {
  public client: WritableSignal<Client> = signal({
    id: 0,
    pcname: '',
    ip: '',
    jobs: [],
  });

  public jobs: WritableSignal<BackupJob[]> = signal([]);

  public enabledJobs: WritableSignal<ClientJob[]> = signal([]);

  public form = form(this.enabledJobs);

  @ViewChild('liveAlertPlaceholder') alertPlaceholder: ElementRef;
  public constructor(
    private clientService: ClientService,
    private jobService: JobService,
    public route: ActivatedRoute,
    private router: Router,
  ) {
    const id = this.route.snapshot.params['id'];
    const array: ClientJob[] = [];

    this.clientService.findById(id).subscribe((result) => {
      this.client.set(result);
      const test: Client = result;

      this.jobService.findAll().subscribe((resultB) => {
        this.jobs.set(resultB);
        for (let i = 0; i < resultB.length; i++) {
          array.push({
            id: resultB[i].id,
            enabled: test.jobs.includes(resultB[i].id),
          });
        }
        this.enabledJobs.set(array);
      });
    });
  }

  public save(): void {
    const data: Client = this.client();
    data.jobs = [];

    for (let x = 0; x < this.enabledJobs().length; x++) {
      if (this.enabledJobs()[x].enabled) {
        data.jobs.push(this.enabledJobs()[x].id);
      }
    }

    this.client.set(data);

    this.clientService.save(this.client()).subscribe((result) => {

    });
  }
}
