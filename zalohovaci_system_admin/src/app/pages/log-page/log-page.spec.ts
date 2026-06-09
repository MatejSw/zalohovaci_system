import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogPage } from './log-page';

describe('LogPage', () => {
  let component: LogPage;
  let fixture: ComponentFixture<LogPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogPage],
    }).compileComponents();

    fixture = TestBed.createComponent(LogPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
