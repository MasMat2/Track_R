import { Injectable } from '@angular/core';
import { checkAvailability, checkHealthPermissions, getSteps, openHealthConnectSetting, requestPermissions, writeSteps, writeWeight, readRecords, writeHeartRate, writeSleepSession, writeBloodPressure } from '../../../app/shared/utils/healthConect-util';
import { StoredRecord, GetRecordsOptions, Record, PermissionsStatus, HealthConnectAvailabilityStatus, RecordType } from '../../pages/home/dashboard/interfaces/healthconnect-interfaces';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HealthConnectService {

  constructor() { }

  private _setupComplete = new BehaviorSubject<boolean>(false);
  setupComplete$ = this._setupComplete.asObservable();

  notifySetupComplete() {
    this._setupComplete.next(true);
  }

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
      let res = await checkAvailability();
      console.log('[HealthConnect util] checkAvailability - res', res);
      return res; 
    } catch (error) {
      console.log('[HealthConnect util] Error check Availability', error);
      throw error;
    }
  }

  async checkHealthPermissions(
    options: { readPerms?: RecordType[]; writePerms?: RecordType[] } = {}
  ): Promise<{ grantedPermissions: string[]; hasAllPermissions: boolean; }> {
    return await checkHealthPermissions(options);
  }

  async openHealthConnectSetting(): Promise<void>{
    return await openHealthConnectSetting();
  }
}