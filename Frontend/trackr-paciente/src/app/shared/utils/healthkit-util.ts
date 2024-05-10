import {
    ActivityData,
    CapacitorHealthkit,
    OtherData,
    QueryOutput,
    SampleNames,
    SleepData,
} from '@perfood/capacitor-healthkit';


export const requestAuthorization = async (): Promise<void> => {
    const readPermissions = ['calories', 'heartRate', 'activity', 'steps', 'distance', 'duration', 'weight',];

    await CapacitorHealthkit.requestAuthorization({
      all: [],
      read: readPermissions,
      write: [],
    });
}

export const getActivitySleep = async (): Promise<QueryOutput<SleepData>> => {
    try {
        //Se obtiene las horas de sueño desde el dia anterior a las 7:00 PM hasta la hora actual
        const current = new Date();
        const startDate = new Date(current);
        startDate.setDate(current.getDate() - 1);
        startDate.setHours(19, 0, 0, 0);
        const endDate = new Date();

        const queryOptions = {
            sampleName: SampleNames.SLEEP_ANALYSIS,
            startDate: startDate.toISOString(),
            endDate: endDate.toISOString(),
            limit: 0,
        };
        
        return await CapacitorHealthkit.queryHKitSampleType<SleepData>(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener la actividad de dormir');       
        console.error(error);
        throw error;
    }
};

export const getWeight = async (): Promise<QueryOutput<OtherData>> => {
    try {
        const current = new Date();
        const startOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 0, 0, 0);
        const endOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 23, 59, 59);
        const startDate = startOfDay.toISOString();
        const endDate = endOfDay.toISOString();

        const queryOptions = {
            sampleName: SampleNames.WEIGHT,
            startDate,
            endDate,
            limit: 0,
        };

        return await CapacitorHealthkit.queryHKitSampleType<OtherData>(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener el peso de la persona');       
        console.error(error);
        throw error; 
    }
};

export const getSteps = async (): Promise<QueryOutput<OtherData>> => {
    try {
        const current = new Date();
        const startOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 0, 0, 0);
        const endOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 23, 59, 59);
        const startDate = startOfDay.toISOString();
        const endDate = endOfDay.toISOString();

        const queryOptions = {
            sampleName: SampleNames.STEP_COUNT,
            startDate,
            endDate,
            limit: 0,
        };

        return await CapacitorHealthkit.queryHKitSampleType<OtherData>(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener los pasos de la persona');       
        console.error(error);
        throw error; 
    }
};

export const getHR = async (): Promise<QueryOutput<OtherData>> => {
    try {
        const current = new Date();
        const startOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 0, 0, 0);
        const endOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 23, 59, 59);
        const startDate = startOfDay.toISOString();
        const endDate = endOfDay.toISOString();

        const queryOptions = {
            sampleName: SampleNames.HEART_RATE,
            startDate,
            endDate,
            limit: 0,
        };

        return await CapacitorHealthkit.queryHKitSampleType<OtherData>(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener la frecuencia cardiaca');       
        console.error(error);
        throw error; 
    }
}

export const getBloodPressureSystolic = async (): Promise<QueryOutput<OtherData>> => {
    try {
        const current = new Date();
        const startOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 0, 0, 0);
        const endOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 23, 59, 59);
        const startDate = startOfDay.toISOString();
        const endDate = endOfDay.toISOString();

        const queryOptions = {
            sampleName: SampleNames.BLOOD_PRESSURE_SYSTOLIC,
            startDate,
            endDate,
            limit: 0,
        };

        return await CapacitorHealthkit.queryHKitSampleType<OtherData>(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener la informacion de presion de sangre sistolica');       
        console.error(error);
        throw error; 
    }
}

export const getBloodPressureDiastolic = async (): Promise<QueryOutput<OtherData>> => {
    try {
        const current = new Date();
        const startOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 0, 0, 0);
        const endOfDay = new Date(current.getFullYear(), current.getMonth(), current.getDate(), 23, 59, 59);
        const startDate = startOfDay.toISOString();
        const endDate = endOfDay.toISOString();

        const queryOptions = {
            sampleName: SampleNames.BLOOD_PRESSURE_DIASTOLIC,
            startDate,
            endDate,
            limit: 0,
        };

        return await CapacitorHealthkit.queryHKitSampleType<OtherData>(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener la informacion de presion de sangre diastolica');       
        console.error(error);
        throw error; 
    }
}

export const isAvailable = async (): Promise<void> => {
    try {
        return await CapacitorHealthkit.isAvailable();
    } catch (error) {
        console.error('[HealthKit-Util] No availablet:', error);
    }
}

export const isEditionSleepAnalysisAuth = async (): Promise<void> => {
    try {
        return await CapacitorHealthkit.isEditionAuthorized({sampleName: 'activity'});
    } catch (error) {
        console.error('[HealthKit-Util] No availablet:', error);
    }
}

export const getActivityAllData = async (startDate: Date, endDate: Date = new Date()): Promise<any> => {
    try {
        const queryOptions = {
            sampleNames: [SampleNames.SLEEP_ANALYSIS, SampleNames.HEART_RATE],
            startDate: startDate.toISOString(),
            endDate: endDate.toISOString(),
            limit: 0,
        };
        
        return await CapacitorHealthkit.multipleQueryHKitSampleType(queryOptions);
    } catch (error) {
        console.error('[HealthKit util] Error al obtener alguna actividad');       
        console.error(error);
        throw error; 
    }
};



