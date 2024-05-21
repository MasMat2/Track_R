import { Injectable } from '@angular/core';
import { checkAvailability, checkHealthPermissions, getSteps, openHealthConnectSetting, requestPermissions, writeSteps, writeWeight, readRecords, writeHeartRate, writeSleepSession, writeBloodPressure } from '../../../app/shared/utils/healthConect-util';
import { StoredRecord, GetRecordsOptions, Record, PermissionsStatus, HealthConnectAvailabilityStatus } from '../../pages/home/dashboard/interfaces/healthconnect-interfaces';

@Injectable({
  providedIn: 'root'
})
export class HealthConnectService {

  constructor() { }


  async requestPermisons(): Promise<PermissionsStatus> {
    return await requestPermissions();
  }

  async getSteps(recordId: string): Promise<{ record: StoredRecord }> {
    return await getSteps(recordId);
  }

  async writeSteps(): Promise<{recordIds: string[]}> {
    return await writeSteps();
  }

  async writeWeight(): Promise<{recordIds: string[]}> {
    return await writeWeight();
  }

  async writeHeartRate(): Promise<{recordIds: string[]}> {
    return await writeHeartRate();
  }

  async writeSleepSession(): Promise<{recordIds: string[]}> {
    return await writeSleepSession();
  }

  async writeBloodPressure(): Promise<{recordIds: string[]}> {
    return await writeBloodPressure();
  }

  async readRecords(options : GetRecordsOptions): Promise<{records: StoredRecord[], pageToken?: string}> {
    return await readRecords(options);
  }

  async checkAvailability(): Promise<{ availability: HealthConnectAvailabilityStatus; }> {
    try {
    return await checkAvailability(); 
    } catch (error) {
      console.log('[HealthConnect util] Error check Availability', error);
      throw error;
    }
  }

  async checkHealthPermissions(): Promise<{ grantedPermissions: string[]; hasAllPermissions: boolean; }> {
    return await checkHealthPermissions();
  }

  async openHealthConnectSetting(): Promise<void>{
    return await openHealthConnectSetting();
  }
}