import { Injectable } from '@angular/core';
import { requestAuthorization, getActivityAllData, isAvailable, isEditionSleepAnalysisAuth, getActivitySleep, getWeight, getSteps, getHR} from 'src/app/shared/utils/healthkit-util';
import { CapacitorHealthkit, SampleNames, QueryOutput, ActivityData, SleepData, OtherData } from '@perfood/capacitor-healthkit'; 


@Injectable({
  providedIn: 'root'
})
export class HealthkitService {

  constructor() { }

  async getPermissions(): Promise<void>{
    const res = await requestAuthorization;
    console.log("Permiso IOS HealthKit: "+res.toString());
  }

  async getActivitySleep(): Promise<QueryOutput<SleepData>> {
    return await getActivitySleep(); 
  }

  async getWeight(): Promise<QueryOutput<OtherData>> {
    return await getWeight();
  }

  async getSteps(): Promise<QueryOutput<OtherData>> {
    return await getSteps();
  }

  async getHR(): Promise<QueryOutput<OtherData>> {
    return await getHR();
  }

  async getActivityAllData(startDate: Date, endDate: Date = new Date()): Promise<any> {
    return await getActivityAllData(startDate, endDate);
  }

  async getAvailable(): Promise<void>{
    const res = await isAvailable;
    console.log("HealthKit disponible: "+res.toString());
  }

  async getEditionSleepAnalysisAuth(): Promise<void>{
    const res = await isEditionSleepAnalysisAuth;
    console.log("HealthKit Sleep Analaysis Edition disponible: "+res.toString());
  }
}
