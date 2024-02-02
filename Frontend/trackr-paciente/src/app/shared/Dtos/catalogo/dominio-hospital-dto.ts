export interface DominioHospitalDto{
    idDominioHospital?:number;
    idDominio:number;
    idHospital:number;
    longitudMinima?: number;
    longitudMaxima?: number;
    valorMinimo?: number;
    valorMaximo?: number;
    fechaMinima?: Date;
    fechaMaxima?: Date;
    permiteFueraDeRango?: boolean;
    unidadMedida?: string;
}