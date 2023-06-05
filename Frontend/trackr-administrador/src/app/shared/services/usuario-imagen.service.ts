import { Injectable } from '@angular/core';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UsuarioImagenService {
    private imagenBase64Subject = new BehaviorSubject<string | undefined>(undefined);
    public imagenBase64$: Observable<string | undefined> = this.imagenBase64Subject.asObservable();
    
    constructor(
        private archivoService: ArchivoService
    ) {   
        this.consultarUsuarioImagen();
    }

    public consultarImagen(): void {
        this.consultarUsuarioImagen();
    }

    public actualizarImagen(imagenBase64: string): void {
        this.imagenBase64Subject.next(imagenBase64);
    }

    private consultarUsuarioImagen() {
        this.archivoService.obtenerUsuarioEnSesionImagen().subscribe((blob) => {
            const reader = new FileReader();
            reader.readAsDataURL(blob); 
            reader.onloadend = () => {
                const base64data = reader.result;
                this.imagenBase64Subject.next(base64data as string);
            }
        });
    }
}