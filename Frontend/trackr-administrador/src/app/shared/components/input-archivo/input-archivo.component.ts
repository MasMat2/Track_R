import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ArchivoDto } from '@dtos/archivos/archivo-dto';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-input-archivo',
  templateUrl: './input-archivo.component.html',
  styleUrls: ['./input-archivo.component.scss']
})
export class InputArchivoComponent implements OnInit {

  @Input() label: string = "Etiqueta";
  @Input() nombreArchivo: string;
  @Input() archivo: any;
  @Input() tipoMime: string;
  @Input() categoria: string;
  @Input() habilitado: boolean = false;
  @Input() contieneAyuda: boolean = true;
  @Input() id: string;
  @Input() indice1: number;
  @Input() indice2: number;
  @Input() idRegistroArchivo: number;
  @Input() idRegistroArchivoAyuda: number;
  @Input() downl_endpoint!: (id: any) => Observable<any>;

  @Output() public archivoDto = new EventEmitter<ArchivoDto>;
  @Output() public idArchivoDescarga = new EventEmitter<number>;
  @Output() public idArchivoAyuda = new EventEmitter<number>;

  @ViewChild('inputArchivo') inputArchivo: any;

  public constructor(
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) {

  }

  public ngOnInit() {
  }

  public previsualizar() {
    this.bsModalRef = this.modalService.show(
      PdfVisorComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
    );
    this.bsModalRef.content.archivo = this.archivo;
    this.bsModalRef.content.nombre = this.nombreArchivo;
    this.bsModalRef.content.archivoNombre = this.nombreArchivo;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      this.bsModalRef.hide();
    };
  }

  public previsualizarAyuda() {
    this.idArchivoAyuda.emit(this.idRegistroArchivoAyuda);
  }

  public fileChange(event: any) {
    if (event.target.files && event.target.files[0]) {
      this.inputArchivo.control.markAsTouched();
      const reader = new FileReader();
      const file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.archivo = reader.result!.toString().split(',')[1];
        this.tipoMime = file.type;
        this.nombreArchivo = file.name;
        let archivoFtpDto = new ArchivoDto();
        archivoFtpDto.idArchivo = this.idRegistroArchivo;
        archivoFtpDto.archivo = this.archivo;
        archivoFtpDto.nombreArchivo = this.nombreArchivo;
        archivoFtpDto.tipoMime = this.tipoMime;
        this.archivoDto.emit(archivoFtpDto)
      };
    }
  }
  public getElement() {
    document.getElementById(this.id)!.click();
  }


  public descargarArchivo() {

    const b64toBlob = (b64Data: any, contentType = '', sliceSize = 512) => {
      const byteCharacters = atob(b64Data);
      const byteArrays = [];

      for (
        let offset = 0;
        offset < byteCharacters.length;
        offset += sliceSize
      ) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);

        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
          byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
      }

      const blob = new Blob(byteArrays, { type: contentType });
      return blob;
    };

    this.downl_endpoint(this.idRegistroArchivo).subscribe((archivoLocal: ArchivoDto) => {
      let href: string;
      let blob = b64toBlob(archivoLocal.archivo, archivoLocal.tipoMime);
      href = URL.createObjectURL(blob);
      var a = document.createElement('a');
      a.href = href;
      a.download = archivoLocal.nombreArchivo;
      a.click();

    });
  }
}
