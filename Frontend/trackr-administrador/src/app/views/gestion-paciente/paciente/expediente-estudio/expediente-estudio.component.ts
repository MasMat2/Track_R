import { GeneralConstant } from '@utils/general-constant';
import { first } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpedienteEstudioGridDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-estudio-grid-dto';
import { ExpedienteEstudioService } from '@http/gestion-expediente/expediente-estudio.service';
import { ExpedienteEstudio } from '@models/gestion-expediente/expediente-estudio';
import { EncryptionService } from '@services/encryption.service';
import { lastValueFrom } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { ImgVisorComponent } from '@sharedComponents/img-visor/img-visor.component';

@Component({
  selector: 'app-expediente-estudio',
  templateUrl: './expediente-estudio.component.html',
  styleUrls: ['./expediente-estudio.component.scss']
})
export class ExpedienteEstudioComponent implements OnInit {

  protected estudioPacienteList: ExpedienteEstudioGridDTO[] = [];
  protected estudio: ExpedienteEstudio;
  protected urlImagen = "";
  // protected pdfSrc: any;
  protected idUsuario: number;


  constructor(
    private expedienteEstudioService: ExpedienteEstudioService,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
  ) { }

  ngOnInit() {
    this.obtenerParametrosURL();
  }

  protected async onVer(estudio: ExpedienteEstudioGridDTO){
    await lastValueFrom(this.expedienteEstudioService.consultar(estudio.idExpedienteEstudio))
    .then((expedienteEstudio: ExpedienteEstudio) => {
      this.estudio = expedienteEstudio;
    });

    if(this.estudio.archivoTipoMime == 'application/pdf'){
      this.abrirModal();
    }
    if(this.estudio.archivoTipoMime == 'image/png' || this.estudio.archivoTipoMime == 'image/jpeg' || this.estudio.archivoTipoMime == 'image/gif'){
      this.abrirModalImagen();
    }
    else{
      return
    }
      
  }

  consultarEstudios(){
    lastValueFrom(this.expedienteEstudioService.consultarPorUsuario(this.idUsuario))
    .then((estudioPacienteList: ExpedienteEstudioGridDTO[]) => {
      this.estudioPacienteList = estudioPacienteList;
    });
  }

  /**
   * Obtiene los parámetros de la URL y los asigna a las variables del componente.
   */
  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarEstudios();
  }

  private abrirModalImagen(){

    const initialState = {
      nombreEstudio: this.estudio.nombre,
      archivo: this.estudio.archivo,
      archivoTipoMime: this.estudio.archivoTipoMime,
      nombre: this.estudio.nombre
    };

    this.bsModalRef = this.modalService.show(
      ImgVisorComponent,
      {
        initialState,
        ... GeneralConstant.CONFIG_MODAL_DEFAULT
      }
      );
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      this.bsModalRef.hide();
    };
  }

  private abrirModal(){
    this.bsModalRef = this.modalService.show(
      PdfVisorComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
      );
    this.bsModalRef.content.archivo = this.estudio.archivo;
    this.bsModalRef.content.nombre = this.estudio.nombre;
    this.bsModalRef.content.archivoNombre = this.estudio.archivoNombre;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      this.bsModalRef.hide();
    };
  }

}
