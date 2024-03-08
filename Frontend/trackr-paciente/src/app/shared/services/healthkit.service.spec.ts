import { TestBed } from '@angular/core/testing';

import { HealthkitService } from './healthkit.service';

describe('HealthkitService', () => {
  let service: HealthkitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HealthkitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
