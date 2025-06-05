import { TestBed } from '@angular/core/testing';

import { RentsApiService } from './rents-api.service';

describe('RentsApiService', () => {
  let service: RentsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
