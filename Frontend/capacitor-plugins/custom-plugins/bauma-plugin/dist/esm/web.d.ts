import { WebPlugin } from '@capacitor/core';
import type { BaumaPluginPlugin } from './definitions';
export declare class BaumaPluginWeb extends WebPlugin implements BaumaPluginPlugin {
    echo(options: {
        value: string;
    }): Promise<{
        value: string;
    }>;
    readBauma(options: {
        value: string;
    }): Promise<{
        value: string;
    }>;
}
import type { OmronCustomPlugin } from './definitions';
export declare class OmronCustomWeb extends WebPlugin implements OmronCustomPlugin {
    /**
     * Simulates scanning for devices by returning a list of dummy devices.
     */
    scanDevices(): Promise<{
        devices: Array<{
            model: string;
            identifier: string;
        }>;
    }>;
    /**
     * Simulates retrieving readings from a device by returning dummy readings.
     * @param options Contains the deviceId of the device to get readings from.
     */
    getReadings(options: {
        deviceId: string;
    }): Promise<{
        readings: Array<{
            systolic?: number;
            diastolic?: number;
            pulseRate?: number;
        }>;
    }>;
    /**
     * Example echo method that returns a fixed string.
     */
    echo(): Promise<{
        value: string;
    }>;
}
