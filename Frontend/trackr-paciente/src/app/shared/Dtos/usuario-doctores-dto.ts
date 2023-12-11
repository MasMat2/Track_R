import { SafeUrl } from "@angular/platform-browser";

export class UsuarioDoctoresDto {
        public idExpediente: number;
        public idUsuarioDoctor: number;
        public idExpedienteDoctor: number;
        public nombre: string;
        public ambito: string;
        public hospital: string;
        public urlImagen? : any;
}