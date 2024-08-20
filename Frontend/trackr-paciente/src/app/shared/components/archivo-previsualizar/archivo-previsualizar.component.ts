import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Directory, Filesystem } from '@capacitor/filesystem';
import { IonicModule, ModalController } from '@ionic/angular';
import { PlataformaService } from 'src/app/services/dashboard/plataforma.service';
import { ArchivoGetDTO } from 'src/app/shared/Dtos/archivos/archivo-get-dto';
import { addIcons } from 'ionicons';
import { ArchivoService } from '@http/archivo/archivo.service';
import { BehaviorSubject } from 'rxjs';
import { AlertController } from '@ionic/angular/standalone';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { FileOpener, FileOpenerOptions } from '@capacitor-community/file-opener';
import { async } from 'rxjs'; 
import { Capacitor } from '@capacitor/core';


@Component({
  selector: 'app-archivo-previsualizar',
  templateUrl: './archivo-previsualizar.component.html',
  styleUrls: ['./archivo-previsualizar.component.scss'],
  standalone: true,
  imports: [
    CommonModule, 
    IonicModule, 
    PdfVisorComponent
  ],
  schemas: []
})
export class ArchivoPrevisualizarComponent implements OnInit, AfterViewInit {
  
  protected fileSource: 'id' | 'url';
  protected urlArchivo: string;
  protected idArchivo: number;

  protected archivo: ArchivoGetDTO;
  protected archivoBase64: string;

  protected tipo: string;
  protected tipoMime: string;

  protected mostrarTitulo: boolean = false;
  protected titulo: string;

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading : any;
  protected isIOSNative: boolean = false;

  constructor(
    private modalController: ModalController, 
    private plataformaService: PlataformaService,
    private archivoService: ArchivoService,
    private alertController: AlertController
  ) { 
    addIcons({ 
      'chevron-left': 'assets/img/svg/chevron-left.svg', 
      'download': 'assets/img/svg/download.svg',
    }); 
  }
  
  async ngOnInit() {
    if (Capacitor.isNativePlatform() && Capacitor.getPlatform() == 'ios') {
      this.isIOSNative = true;
    }

    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else if (this.archivo) {
        this.dismissLoading();

        if (this.isIOSNative && this.tipoMime == 'application') {
          this.openFile(this.archivo.nombre, this.archivoBase64);
        }
      }
    });

    this.cargandoSubject.next(true);

    switch(this.fileSource){
      case 'id' : {
        this.descargarArchivoById(this.idArchivo);
        break;
      }
      case 'url' : {
        this.descargarArchivoByUrl(this.urlArchivo);
        break;
      }
      default: {
        console.error("TIPO NO ESPECIFICADO");
        this.presentAlertError();
      }
      
    }
  }

  ngAfterViewInit(): void {
  }

  private async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: "custom-alert-loading",
      backdropDismiss: false,
    })
    return await this.loading.present();
  }

  private async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }

  protected descargarArchivo() {
    if (this.plataformaService.isMobile()) {
      this.downloadFileMobile(this.archivo.archivo, this.archivo.nombre, this.archivo.archivoMime)
    }
    else if (this.plataformaService.isWeb()) {
      this.downloadFileWeb(this.archivo.archivo, this.archivo.nombre, this.archivo.archivoMime)
    }
  }

  private async downloadFileMobile(fileBase64: string, nombre?: string, mime?: string) {
    try {

      let downloadDirectory = Directory.Documents

      // Crear un archivo en el sistema de archivos
      const result = await Filesystem.writeFile({
        path: `${nombre}`,
        data: fileBase64,
        directory: Directory.External,
        recursive: true
        //encoding: Encoding.UTF8,
      });

      // Obtener la URL del archivo creado
      //const url = result.uri;
      //console.log(url)

    } catch (error) {
      console.error('Error al descargar el archivo:', error);
    }
  }

  private downloadFileWeb(fileBase64: string, nombre?: string, mime?: string) {
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

  private descargarArchivoById(idArchivo: number){
    if(idArchivo){
      this.archivoService.getArchivo(idArchivo).subscribe({
        next: (data) => {
          this.archivo = data;
          this.tipoMime = this.archivo.archivoMime.split('/')[0];
          this.archivoBase64 = 'data:' + this.archivo.archivoMime + ';base64,' + this.archivo.archivo;
        },
        error: () => {
          this.cargandoSubject.next(false);
          this.presentAlertError();
        },
        complete: () => {
          this.cargandoSubject.next(false);
        }
      })
    }
  }

  private descargarArchivoByUrl(urlArchivo: string){
    if(urlArchivo){
      this.archivoService.getArchivoByUrl(urlArchivo).subscribe({
        next: (data) => {
          this.archivo = data;
          this.tipoMime = this.archivo.archivoMime.split('/')[0];
          this.archivoBase64 = 'data:' + this.archivo.archivoMime + ';base64,' + this.archivo.archivo;
        },
        error: () => {
          this.cargandoSubject.next(false);
          this.presentAlertError();
        },
        complete: () => {
          this.cargandoSubject.next(false);
        }
      })
    }
  }

  protected regresarBtn() {
    this.modalController.dismiss();
  }

  protected async presentAlertError(){
    const alert = await this.alertController.create({
      header: 'Error',
      subHeader: 'Ha ocurrido un error al mostrar este archivo',
      cssClass: 'custom-alert color-error icon-trash',
      buttons: [{
        text: 'Ok',
        role: 'confirm',
        handler: () => {
          this.regresarBtn();
        }
      }]
    })

    alert.present();
  }

  async openFile(nombre:string, base64: string): Promise<void> {

    const result = await Filesystem.writeFile({
      path: nombre,
      data: base64,
      directory: Directory.Cache,
      recursive: true
      //encoding: Encoding.UTF8,
    });

    var url = result.uri;

    const fileOpenerOptions: FileOpenerOptions = {
      filePath: url,
      contentType: 'application/pdf',
      openWithDefault: true
    };
    await FileOpener.open(fileOpenerOptions);

    await Filesystem.deleteFile({
      path: nombre,
      directory: Directory.Cache
      //encoding: Encoding.UTF8,
    });

    console.log("Archivo cerrado");
    this.regresarBtn();

  }
}
