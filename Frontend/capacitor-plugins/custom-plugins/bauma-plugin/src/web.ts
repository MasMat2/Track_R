import { WebPlugin } from '@capacitor/core';

import type { BaumaPluginPlugin } from './definitions';

export class BaumaPluginWeb extends WebPlugin implements BaumaPluginPlugin {
  async echo(options: { value: string }): Promise<{ value: string }> {
    console.log('ECHO', options);
    return options;
  }
  async readBauma(options: { value: string }): Promise<{ value: string }> {
    console.log('READBAUMA', options);
    return options;
  }
}


import type { OmronCustomPlugin } from './definitions';


export class OmronCustomWeb extends WebPlugin implements OmronCustomPlugin {
  /**
   * Simulates scanning for devices by returning a list of dummy devices.
   */
  async scanDevices(): Promise<{ devices: Array<{ model: string; identifier: string }> }> {
    console.log('scanDevices called on web');
    // Return dummy data
    const devices = [
      { model: 'Dummy Device 1', identifier: 'dummy-device-1' },
      { model: 'Dummy Device 2', identifier: 'dummy-device-2' },
    ];
    return { devices };
  }

  /**
   * Simulates retrieving readings from a device by returning dummy readings.
   * @param options Contains the deviceId of the device to get readings from.
   */
  async getReadings(options: { deviceId: string }): Promise<{ readings: Array<{ systolic?: number; diastolic?: number; pulseRate?: number }> }> {
    console.log('getReadings called on web with deviceId:', options.deviceId);
    // Return dummy readings based on the provided deviceId
    const readings = [
      {
        systolic: Math.floor(Math.random() * 20) + 110,   // Random systolic between 110 and 130
        diastolic: Math.floor(Math.random() * 20) + 70,   // Random diastolic between 70 and 90
        pulseRate: Math.floor(Math.random() * 40) + 60,   // Random pulse rate between 60 and 100
      },
      {
        systolic: Math.floor(Math.random() * 20) + 110,
        diastolic: Math.floor(Math.random() * 20) + 70,
        pulseRate: Math.floor(Math.random() * 40) + 60,
      },
    ];
    return { readings };
  }

  /**
   * Example echo method that returns a fixed string.
   */
  async echo(): Promise<{ value: string }> {
    console.log('echo called on web');
    return { value: 'This is a dummy echo response from the web implementation.' };
  }
}