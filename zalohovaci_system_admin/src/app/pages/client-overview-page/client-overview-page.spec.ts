import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientOverviewPage } from './client-overview-page';

describe('ClientOverviewPage', () => {
  let component: ClientOverviewPage;
  let fixture: ComponentFixture<ClientOverviewPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientOverviewPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ClientOverviewPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
