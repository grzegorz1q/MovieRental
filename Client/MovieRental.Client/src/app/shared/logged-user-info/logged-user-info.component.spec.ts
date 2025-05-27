import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggedUserInfoComponent } from './logged-user-info.component';

describe('LoggedUserInfoComponent', () => {
  let component: LoggedUserInfoComponent;
  let fixture: ComponentFixture<LoggedUserInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoggedUserInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoggedUserInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
