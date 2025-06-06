import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeminiDashboardComponent } from './gemini-dashboard.component';

describe('GeminiDashboardComponent', () => {
  let component: GeminiDashboardComponent;
  let fixture: ComponentFixture<GeminiDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GeminiDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GeminiDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
