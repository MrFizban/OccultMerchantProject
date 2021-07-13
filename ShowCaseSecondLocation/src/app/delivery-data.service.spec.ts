import { TestBed } from '@angular/core/testing';

import { DeliveryDataService } from './delivery-data.service';

describe('DeliveryDataService', () => {
  let service: DeliveryDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeliveryDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
