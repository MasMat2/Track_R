import { TestBed } from '@angular/core/testing';

import { DataJitsiService } from './data-jitsi.service';

describe('DataJitsiService', () => {
  let service: DataJitsiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataJitsiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
