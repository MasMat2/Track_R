import { GeneralConstant } from '@utils/general-constant';
import { first } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpedienteEstudioGridDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-estudio-grid-dto';
import { ExpedienteEstudioService } from '@http/gestion-expediente/expediente-estudio.service';
import { ExpedienteEstudio } from '@models/gestion-expediente/expediente-estudio';
import { EncryptionService } from '@services/encryption.service';
import { Observable, lastValueFrom } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PdfVisorComponent } from '@sharedComponents/pdf-visor/pdf-visor.component';
import { ImgVisorComponent } from '@sharedComponents/img-visor/img-visor.component';
import { ColDef, ICellRendererParams, ValueGetterParams } from 'ag-grid-community';
import { format } from 'date-fns';
import { CONFIG_COLUMN_ACTION, GRID_ACTION } from '@utils/constants/grid';
import { ExpedienteEstudioDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-estudio-dto';

@Component({
  selector: 'app-expediente-estudio',
  templateUrl: './expediente-estudio.component.html',
  styleUrls: ['./expediente-estudio.component.scss'],
  entryComponents: [PdfVisorComponent, ImgVisorComponent]
})
export class ExpedienteEstudioComponent implements OnInit {

  protected estudioPacienteList: ExpedienteEstudioGridDTO[] = [];
  protected estudios$: Observable<ExpedienteEstudioGridDTO[]>;
  protected estudio: ExpedienteEstudio;
  protected estudioDto : ExpedienteEstudioDTO;
  protected urlImagen = "";
  // protected pdfSrc: any;
  protected idUsuario: number;
  public HEADER_GRID = 'Estudios';
  
  private readonly COLUMNA_VER: ColDef = Object.assign(
    {
      action: GRID_ACTION.Ver,
      cellRendererSelector: (params: ICellRendererParams) => {
        const component = {
          component: 'actionButton',
          params: { disabled: false },
        };
        return component;
      },
      minWidth: 44,
      maxWidth: 44,
    },
    CONFIG_COLUMN_ACTION
  );

  protected columns: ColDef[] = [
    { headerName: 'Estudio', field: 'nombre', minWidth: 150 },
    { headerName: 'Fecha de Realización', field: 'fechaRealizacion', minWidth: 150,
      valueGetter: (params: ValueGetterParams) => {
        const fechaEstudio = new Date(params.data.fechaRealizacion);
        return format(fechaEstudio, 'dd/MM/yyyy');
      }  
    },
    this.COLUMNA_VER,
  ];

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

  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarEstudios();
  }

  private consultarEstudios(){
    this.estudios$ = this.expedienteEstudioService.consultarPorUsuario(this.idUsuario);
  }

  protected onGridClick(gridData: { accion: string; data: ExpedienteEstudioGridDTO }): void {
    if(gridData.accion === GRID_ACTION.Ver){
      this.onVer(gridData.data);
    }
  }

  protected async onVer(estudio: ExpedienteEstudioGridDTO){
    await lastValueFrom(this.expedienteEstudioService.consultar(estudio.idExpedienteEstudio))
    .then((expedienteEstudio: ExpedienteEstudioDTO) => {
      this.estudioDto = expedienteEstudio;

    if(this.estudioDto.archivoTipoMime == 'application/pdf'){
      this.abrirModal();
    }
    if(this.estudioDto.archivoTipoMime == 'image/png' || this.estudioDto.archivoTipoMime == 'image/jpeg' || this.estudioDto.archivoTipoMime == 'image/gif'){
      this.abrirModalImagen();
    }
    else{
      return
    }
    });
      
  }

  private abrirModalImagen(){

    const initialState = {
      nombreEstudio: this.estudioDto.nombre,
      archivo: this.estudioDto.archivoBase64,
      archivoTipoMime: this.estudioDto.archivoTipoMime,
      nombre: this.estudioDto.nombre
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
    this.bsModalRef.content.archivo = this.estudioDto.archivoBase64;
    this.bsModalRef.content.nombre = this.estudioDto.nombre;
    this.bsModalRef.content.archivoNombre = this.estudioDto.nombre;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      this.bsModalRef.hide();
    };
  }

}
