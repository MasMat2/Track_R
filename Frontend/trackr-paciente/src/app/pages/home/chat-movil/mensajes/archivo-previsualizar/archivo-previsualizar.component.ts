import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Directory, Filesystem } from '@capacitor/filesystem';
import { IonicModule, ModalController } from '@ionic/angular';
import { NgxExtendedPdfViewerComponent, NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { PlataformaService } from 'src/app/services/dashboard/plataforma.service';
import { ArchivoGetDTO } from 'src/app/shared/Dtos/archivos/archivo-get-dto';

@Component({
  selector: 'app-archivo-previsualizar',
  templateUrl: './archivo-previsualizar.component.html',
  styleUrls: ['./archivo-previsualizar.component.scss'],
  standalone: true,
  imports: [CommonModule,IonicModule, NgxExtendedPdfViewerModule],
})
export class ArchivoPrevisualizarComponent  implements OnInit {
  protected archivo:ArchivoGetDTO;
  protected type:string;
  protected archivoBase64:string;

  constructor(private modalController:ModalController , private plataformaService : PlataformaService) { }

  ngOnInit() {
    this.type = this.archivo.archivoMime.split('/')[0]
    this.archivoBase64 = 'data:' + this.archivo.archivoMime + ';base64,' + this.archivo.archivo
  }

  regresarBtn(){
    this.modalController.dismiss();
  }

  descargarArchivo(){
    
    if (this.plataformaService.isMobile()) {
      
         this.downloadFileMobile(this.archivo.archivo, this.archivo.nombre, this.archivo.archivoMime)
       }
       else if (this.plataformaService.isWeb()) {
         this.downloadFileWeb(this.archivo.archivo, this.archivo.nombre, this.archivo.archivoMime)
       }
  }

  async downloadFileMobile(fileBase64: string, nombre?: string, mime?: string) {
    try {

      let downloadDirectory = Directory.Documents

      // Crear un archivo en el sistema de archivos
      const result = await Filesystem.writeFile({
        path: `${Directory.Data}/${nombre}`,
        data: fileBase64,
        directory: Directory.External,
        recursive: true,
        //encoding: Encoding.UTF8,
      });

      // Obtener la URL del archivo creado
      const url = result.uri;

      console.log(url)

    } catch (error) {
      console.error('Error al descargar el archivo:', error);
    }
  }

  downloadFileWeb(fileBase64: string, nombre?: string, mime?: string) {
    // Decodificar la cadena Base64
    const decodedData = atob(fileBase64);

    // Convertir a un array de bytes
    const byteNumbers = new Array(decodedData.length);
    for (let i = 0; i < decodedData.length; i++) {
      byteNumbers[i] = decodedData.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);

    // Crear un Blob con los datos binarios
    const blob = new Blob([byteArray], { type: 'application/octet-stream' });

    // Crear un object URL y asignarlo al enlace de descarga
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    document.body.appendChild(a);
    a.style.display = 'none';
    a.href = url;

    a.download = `${nombre}`;

    // Nombre del archivo
    a.click();

    // Limpiar el object URL después de la descarga
    URL.revokeObjectURL(url);
  }

}
