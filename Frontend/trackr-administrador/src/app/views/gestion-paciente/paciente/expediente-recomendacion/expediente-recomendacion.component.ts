import { Component, OnInit} from '@angular/core';
import { ExpedienteRecomendacionService } from '../../../../shared/http/gestion-expediente/expediente-recomendacion.service';
import { ExpedienteRecomendacionGridDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-grid-dto';
import { GridOptions } from 'ag-grid-community';
import { EncryptionService } from '@services/encryption.service';
import { Observable, lastValueFrom} from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { MensajeService } from '../../../../shared/components/mensaje/mensaje.service';
import { GRID_ACTION } from '@utils/constants/grid';
import { NgForm } from '@angular/forms';
import { FormularioService } from '../../../../shared/services/formulario.service';
import { ExpedienteRecomendacionFormDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-form.dto';


@Component({
  selector: 'app-expediente-recomendacion',
  templateUrl: './expediente-recomendacion.component.html',
})
export class ExpedienteRecomendacionComponent implements OnInit{
  //Variables relacionadas con el componente y la vista
  protected submiting : boolean = false;
  protected esAgregar : boolean = true;

  //Variables relacionadas con la consulta de datos y la interaccion con el servicio
  protected idUsuario : number;
  protected recomendacion : ExpedienteRecomendacionFormDTO = new ExpedienteRecomendacionFormDTO;

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
    private mensajeService : MensajeService,
    private formularioService : FormularioService
    ) 
  {}

  ngOnInit(): void
   {
    this.obtenerParametrosURL();
    this.recomendacion.fecha = new Date();
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
        this.esAgregar = true;
      })
    })
  }

  protected onGridClick(gridData : {accion : string, data: ExpedienteRecomendacionGridDTO}): void 
  { 
    var recomendacionGrid = gridData.data;
    
    const acciones = {
      [GRID_ACTION.Eliminar as string] : () => this.eliminar(recomendacionGrid.idExpedienteRecomendacion),
      [GRID_ACTION.Editar as string] : () => {
        this.expedienteRecomendacionService.consultar(recomendacionGrid.idExpedienteRecomendacion)
        .subscribe(recomendacion => {
        
          this.recomendacion = recomendacion;
          this.recomendacion.fecha = new Date(recomendacion.fecha);
          this.esAgregar = false;
        });
      }
    };
    
    acciones[gridData.accion]();
  }


  public async enviarFormulario(formulario : NgForm) : Promise<void>
  {
    this.submiting = true;

    if(!formulario.valid){
      this.formularioService.validarCamposRequeridos(formulario);
      this.submiting = false;
      return;
    }

    const MENSAJE_EDITAR: string = 'La recomendacion ha sido editada';
    const MENSAJE_AGREGAR: string = 'La recomendacion ha sido agregada';


    const [observable , mensajeExito] : [Observable<void>  , string] =
    this.esAgregar === true
    ? [this.agregar() , MENSAJE_AGREGAR]
    : [this.editar() , MENSAJE_EDITAR];

     const subscription = observable.subscribe({
      next : () => {
        this.mensajeService.modalExito(mensajeExito)
      }, 
      complete: () => {
        this.consultarGrid();
        this.limpiarCampos();
        this.esAgregar = true;
        this.submiting = false;
        subscription.unsubscribe();
        formulario.reset();
      }
    })
  }

  private consultarGrid()
  {
    this.recomendacionesList$ = this.expedienteRecomendacionService.consultarGridPorUsuario(this.idUsuario);
  }
  
  private limpiarCampos() : void
  {
    this.recomendacion  = new ExpedienteRecomendacionFormDTO;
    this.recomendacion.fecha = new Date();
  }

  protected editar() : Observable<void>
  { 
   return this.expedienteRecomendacionService.editar(this.recomendacion);
  }

  protected agregar() : Observable<void>
  {
    this.recomendacion.idUsuario = this.idUsuario;
    return this.expedienteRecomendacionService.agregar(this.recomendacion);
  }

}

