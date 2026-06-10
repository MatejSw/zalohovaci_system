import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogTable } from './log-table';

describe('LogTable', () => {
  let component: LogTable;
  let fixture: ComponentFixture<LogTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogTable],
    }).compileComponents();

    fixture = TestBed.createComponent(LogTable);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
