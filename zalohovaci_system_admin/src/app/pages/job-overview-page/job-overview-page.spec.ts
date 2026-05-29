import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobOverviewPage } from './job-overview-page';

describe('JobOverviewPage', () => {
  let component: JobOverviewPage;
  let fixture: ComponentFixture<JobOverviewPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobOverviewPage],
    }).compileComponents();

    fixture = TestBed.createComponent(JobOverviewPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
