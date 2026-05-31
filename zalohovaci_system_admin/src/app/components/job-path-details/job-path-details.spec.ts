import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobPathDetails } from './job-path-details';

describe('JobPathDetails', () => {
  let component: JobPathDetails;
  let fixture: ComponentFixture<JobPathDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobPathDetails],
    }).compileComponents();

    fixture = TestBed.createComponent(JobPathDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
