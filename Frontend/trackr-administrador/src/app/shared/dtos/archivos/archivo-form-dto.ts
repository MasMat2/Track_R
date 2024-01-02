export interface ArchivoFormDTO{
    idArchivo ?: number;
    nombre: string;
    fechaRealizacion: Date;
    archivo: any;
    archivoTipoMime: string;
    archivoNombre: string;
    idUsuario: number;
}