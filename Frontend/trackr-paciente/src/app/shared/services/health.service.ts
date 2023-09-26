import { Injectable } from '@angular/core';
import { HealthData } from '../Dtos/health-data/health-data-interface';
import { checkPermission, getHealthData, openAppSettings } from '../utils/health-plugin';

import { CheckPermissionResult } from "capacitor-health-data-plugin";


@Injectable({
  providedIn: 'root'
})
export class HealthService {

  constructor() { 
  }

  async getPasos(): Promise<HealthData> {

    //codigo para obtener del Plugin la cantidad de pasos
    const res = await getHealthData();
  
    return {
      name: res.name || "NO SENSOR NAME",
      value: res.count || 0
    };
  }

  async getPermissionState(): Promise<CheckPermissionResult>{
    const permission = await checkPermission();
    return permission;
  }
  
  
}