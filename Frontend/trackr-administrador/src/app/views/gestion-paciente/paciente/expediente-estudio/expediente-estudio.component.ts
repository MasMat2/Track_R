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

@Component({
  selector: 'app-expediente-estudio',
  templateUrl: './expediente-estudio.component.html',
  styleUrls: ['./expediente-estudio.component.scss']
})
export class ExpedienteEstudioComponent implements OnInit {

  protected estudioPacienteList: ExpedienteEstudioGridDTO[] = [];
  protected estudios$: Observable<ExpedienteEstudioGridDTO[]>;
  protected estudio: ExpedienteEstudio;
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
    console.log(gridData);
    if(gridData.accion === GRID_ACTION.Ver){
      this.onVer(gridData.data);
    }
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
