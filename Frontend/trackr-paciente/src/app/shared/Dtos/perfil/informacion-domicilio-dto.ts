export class InformacionDomicilioDto {
    idPais: number | null;
    idEstado: number | null;
    idMunicipio: number | null;
    idLocalidad: number | null;
    idColonia: number | null;
    codigoPostal: string;
    calle: string;
    entreCalles: string;
    numeroInterior: string;
    numeroExterior: string;
}