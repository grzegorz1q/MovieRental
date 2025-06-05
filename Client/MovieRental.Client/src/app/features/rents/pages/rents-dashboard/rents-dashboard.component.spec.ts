import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentsDashboardComponent } from './rents-dashboard.component';

describe('RentsDashboardComponent', () => {
  let component: RentsDashboardComponent;
  let fixture: ComponentFixture<RentsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentsDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
