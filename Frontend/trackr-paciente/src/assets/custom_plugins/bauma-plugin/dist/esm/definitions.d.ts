export interface BaumaPluginPlugin {
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
export interface OmronCustomPlugin {
    /**
     * Scans for available devices and returns a list of devices with their names and identifiers.
     */
    scanDevices(): Promise<{
        devices: Array<{
            model: string;
            identifier: string;
        }>;
    }>;
    /**
     * Retrieves readings (such as systolic and diastolic values) from a specific device by ID.
     * @param options The ID of the device to retrieve readings from.
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
     * Example echo method. Kept for compatibility if needed.
     */
    echo(): Promise<{
        value: string;
    }>;
}
