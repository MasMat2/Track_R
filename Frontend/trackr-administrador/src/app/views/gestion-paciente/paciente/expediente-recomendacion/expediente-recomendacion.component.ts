import { Component, OnInit} from '@angular/core';
import { ExpedienteRecomendacionService } from '../../../../shared/http/gestion-expediente/expediente-recomendacion.service';
import { ExpedienteRecomendacionGridDTO } from '@models/gestion-expediente/expediente-recomendacion';
import { GridOptions } from 'ag-grid-community';
import { EncryptionService } from '@services/encryption.service';
import { Observable, lastValueFrom} from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { MensajeService } from '../../../../shared/components/mensaje/mensaje.service';
import { GRID_ACTION } from '@utils/constants/grid';
import { ExpedienteRecomendacionFormDTO } from '@dtos/gestion-expediente/expediente- recomendacion-form-dto';

@Component({
  selector: 'app-expediente-recomendacion',
  templateUrl: './expediente-recomendacion.component.html',
})
export class ExpedienteRecomendacionComponent implements OnInit{
  //Variables relacionadas con el componente y la vista
  
  protected fechaString : string;
  protected inputRecomendacion: string = '';
  protected botonTocado : boolean = false;

  //Variables relacionadas con la consulta de datos y la interaccion con el servicio
  protected idUsuario : number;
  protected editarAccion: boolean = false;
  protected recomendacion : ExpedienteRecomendacionFormDTO = new ExpedienteRecomendacionFormDTO;

  //Mensajes de confirmacion
  private MENSAJE_EDITAR: string = 'La recomendacion ha sido editada';
  private MENSAJE_AGREGAR: string = 'La recomendacion ha sido agregada';

  //Configuraciones y datos del grid
  public gridOptions : GridOptions;
  public HEADER_GRID = 'Recomendaciones';
  public columns = [
    { headerName: 'Num', valueGetter: (params: any) => params.node.rowIndex + 1, maxWidth: 90 },
    { headerName: 'Fecha', field: 'fecha', maxWidth: 90},
    { headerName: 'Recomendacion', field: 'descripcion', minWidth: 150 },
    { headerName: 'Doctor', field: 'doctor', minWidth: 150 },
  ];
  public recomendacionesList$: Observable<ExpedienteRecomendacionGridDTO[]>;
  
  constructor(
    private expedienteRecomendacionService : ExpedienteRecomendacionService,
    private encryptionService: EncryptionService,
    private route: ActivatedRoute,
    private mensajeService : MensajeService
    ) 
  {}

  ngOnInit(): void
   {
    this.obtenerParametrosURL();
    this.setFechaString();
   }


  private consultarGrid()
  {
    this.recomendacionesList$ = this.expedienteRecomendacionService.consultarPorUsuario(this.idUsuario);
  }

  private async obtenerParametrosURL(): Promise<void>
   {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    this.consultarGrid();
  }

  protected eliminar(idExpedienteRecomendacion : number) : void
  {
    const MENSAJE_CONFIRMACION : string = '¿Desea eliminar la recomendacion?';
    const TITULO_MODAL : string = 'Eliminar recomendacion'
    const MENSAJE_EXITO: string = 'La recomendacion ha sido eliminada';
    
    this.mensajeService
    .modalConfirmacion(
      MENSAJE_CONFIRMACION,
      TITULO_MODAL
    ).then(() => {
      this.expedienteRecomendacionService.eliminar(idExpedienteRecomendacion).subscribe(() => {
        this.mensajeService.modalExito(MENSAJE_EXITO);
        this.consultarGrid();
      })
    })
  }

  protected editar() : void
  {
    if(this.recomendacion.descripcion.length > 0){
       const subscription = this.expedienteRecomendacionService.editar(this.recomendacion).subscribe({
        next: () => {
          this.mensajeService.modalExito(this.MENSAJE_EDITAR)
        },
        complete: () => {
          subscription.unsubscribe();
          this.consultarGrid();
          this.editarAccion = false;
          this.botonTocado = false;
          this.limpiarCampos();
        }
      }) 
    
    }
  }

  protected agregar()
  {
    if(this.recomendacion.descripcion){

      this.expedienteRecomendacionService.obtenerIdExpediente(this.idUsuario)
      .subscribe(idExpedienteGotten => {
        const recomendacion = {
          idExpedienteRecomendacion: 1,
          idExpediente: idExpedienteGotten,
          fecha: new Date(),
          descripcion: this.recomendacion.descripcion,
          idDoctor : 1
        };
        this.recomendacion = recomendacion;
  
        const subscription = this.expedienteRecomendacionService.agregar(this.recomendacion).subscribe({
          next: () => {
            this.mensajeService.modalExito(this.MENSAJE_AGREGAR)
          },
          complete: () => {
              subscription.unsubscribe();
              this.consultarGrid();
              this.botonTocado = false;
              this.limpiarCampos();
          }
        }); 
    });
    }
  }

  protected onGridClick(gridData : {accion : string, data: ExpedienteRecomendacionGridDTO}): void 
  { 
    var recomendacionGrid = gridData.data;
    
    const acciones = {
      [GRID_ACTION.Eliminar as string] : () => this.eliminar(recomendacionGrid.idExpedienteRecomendacion),
      [GRID_ACTION.Editar as string] : () => {
        this.editarAccion = true;
        this.expedienteRecomendacionService.consultarPorId(recomendacionGrid.idExpedienteRecomendacion)
        .subscribe(recomendacion => {

          this.recomendacion = recomendacion;
          this.setFechaString(); 
        });
      }
    };
    
    acciones[gridData.accion]();
  }

  protected marcarBotonTocado() : void
  { 
    this.botonTocado = true;
  }

  private limpiarCampos() : void
  {
    this.recomendacion  = new ExpedienteRecomendacionFormDTO;
  }

  private setFechaString() : void 
  { 
    this.fechaString = this.recomendacion.fecha.toLocaleString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });
  }

}

