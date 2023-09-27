export interface HealthData {
    name: string
    value: number;
}

export interface HealthDataResponse{
    name? : string,
    count?: number
}

export interface CheckPermissionResult{
    granted? : boolean,
    denied? : boolean,
    asked? : boolean,
    restricted? : boolean, //IOs Only use
    unknown? : boolean //IOs Only use
}