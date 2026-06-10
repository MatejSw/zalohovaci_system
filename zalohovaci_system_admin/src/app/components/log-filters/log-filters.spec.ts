import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogFilters } from './log-filters';

describe('LogFilters', () => {
  let component: LogFilters;
  let fixture: ComponentFixture<LogFilters>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogFilters],
    }).compileComponents();

    fixture = TestBed.createComponent(LogFilters);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
