import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientRentsListComponent } from './client-rents-list.component';

describe('ClientRentsListComponent', () => {
  let component: ClientRentsListComponent;
  let fixture: ComponentFixture<ClientRentsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientRentsListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientRentsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
