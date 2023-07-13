import { Component, OnInit, ViewChild } from '@angular/core';
import { ExpedienteRecomendacionService } from '../../../../shared/http/gestion-expediente/expediente-recomendacion.service';
import { ExpedienteRecomendacion } from '@models/gestion-expediente/expediente-recomendacion';
import { GridOptions } from 'ag-grid-community';
import { EncryptionService } from '@services/encryption.service';
import { lastValueFrom, Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { GridGeneralComponent } from '@sharedComponents/grid-general/grid-general.component';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { MensajeService } from '../../../../shared/components/mensaje/mensaje.service';
import { GRID_ACTION } from '@utils/constants/grid';
import { ExpedienteTrackrService } from '../../../../shared/http/seguridad/expediente-trackr.service';

@Component({
  selector: 'app-expediente-recomendacion',
  templateUrl: './expediente-recomendacion.component.html',
})
export class ExpedienteRecomendacionComponent implements OnInit{
  
  @ViewChild('gridRecomendacion', { static: false }) gridRecomendacion: GridGeneralComponent;
 
  protected estudio : ExpedienteRecomendacion;
  protected idExpediente : number;
  protected editarAccion: boolean = false;
  protected botonTocado : boolean;
  protected recomendacion : ExpedienteRecomendacion = new ExpedienteRecomendacion();
  protected fecha : string;
  protected destroy$: Subject<void> = new Subject<void>();
  protected inputRecomendacion: string = '';
  private MENSAJE_EDITAR: string = 'MENSAJE EDITADO';
  private MENSAJE_AGREGAR: string = 'MENSAJE AGREGADO';
  public gridOptions : GridOptions;
  public EDITAR_PERFIL = CodigoAcceso.EDITAR_PERFIL;
  public ELIMINAR_PERFIL = CodigoAcceso.ELIMINAR_PERFIL;
  public HEADER_GRID = 'Recomendaciones';
 

  public columns = [
    { headerName: 'Num', valueGetter: (params: any) => params.node.rowIndex + 1, maxWidth: 90 },
    { headerName: 'Fecha', field: 'fecha', minWidth: 150},
    { headerName: 'Recomendacion', field: 'recomendacion', minWidth: 150 },
    { headerName: 'Doctor', field: 'doctor', minWidth: 150 },
    
  ];
  public recomendacionesList: any[] = [];
  

  constructor(
    private expedienteRecomendacionService : ExpedienteRecomendacionService,
    private encryptionService: EncryptionService,
    private route: ActivatedRoute,
    private mensajeService : MensajeService,
    private expedienteTrackrService : ExpedienteTrackrService
    ) 
  {}

  ngOnInit(): void
   {
    this.obtenerParametrosURL();
    this.fecha = new Date().toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' }); 
  }

  public consultarGrid()
  {
    this.expedienteRecomendacionService.consultar(this.idExpediente).subscribe((data) => {
      this.recomendacionesList = data;
    })
  }

  private async obtenerParametrosURL(): Promise<void>
   {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idExpediente = Number(params.i);
    this.consultarGrid();
  }

  public eliminar(idExpedienteRecomendacion : number) : void
  {
    const MENSAJE_CONFIRMACION : string = '¿Desea eliminar la recomendacion?';
    const TITULO_MODAL : string = 'Eliminar recomendacion'
    const MENSAJE_EXITO: string = 'La recomendacion ha sido eliminada';
    
    this.mensajeService
    .modalConfirmacion(
      MENSAJE_CONFIRMACION,
      TITULO_MODAL
    ).then((aceptar) => {
      this.expedienteRecomendacionService.eliminar(idExpedienteRecomendacion).subscribe((data) => {
        this.mensajeService.modalExito(MENSAJE_EXITO  );
        this.consultarGrid();
      })
    })
  }

  public editar() : void
  {
      const recomendacion = { 
        ExpedienteId: this.expedienteTrackrService.getExpediente().idExpediente,
        Fecha: this.recomendacion.fecha,
        Recomendacion: this.inputRecomendacion,
        DoctorId : this.recomendacion.idDoctor  
      };
  
      const subscription = this.expedienteRecomendacionService.editar(this.recomendacion.idExpedienteRecomendacion, recomendacion).subscribe({
        next: () => {
          this.mensajeService.modalExito(this.MENSAJE_EDITAR)
        },
        complete: () => {
          subscription.unsubscribe();
          this.consultarGrid();
          this.editarAccion = false;
        }
      })
  
  }

  agregar()
  {
    const recomendacion = {
      ExpedienteId: this.expedienteTrackrService.getExpediente().idExpediente,
      Fecha: new Date(),
      Recomendacion : this.inputRecomendacion,
      DoctorId: 1
    }

    const subscription = this.expedienteRecomendacionService.agregar(recomendacion).subscribe({
      next: () => {
        this.mensajeService.modalExito(this.MENSAJE_AGREGAR)
      },
      complete: () => {
          subscription.unsubscribe();
          this.consultarGrid();
      }
    });
 
  }

  onGridClick(gridData : {accion : string, data: ExpedienteRecomendacion}): void 
  { 
    this.recomendacion = gridData.data;
    
    const acciones = {
      [GRID_ACTION.Eliminar as string] : () => this.eliminar(this.recomendacion.idExpedienteRecomendacion),
      [GRID_ACTION.Editar as string] : () => {
        this.editarAccion = !this.editarAccion;
        this.inputRecomendacion = this.recomendacion.recomendacion
      }
    };
    
    acciones[gridData.accion]();
  }

}

