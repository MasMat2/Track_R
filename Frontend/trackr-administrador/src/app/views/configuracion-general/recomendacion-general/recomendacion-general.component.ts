import { Component, OnInit} from '@angular/core';
import { ExpedienteRecomendacionGridDTO } from '@dtos/gestion-expediente/expediente-recomendacion/expediente-recomendacion-grid-dto';
import { GridOptions } from 'ag-grid-community';
import { EncryptionService } from '@services/encryption.service';
import { Observable, lastValueFrom} from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GRID_ACTION } from '@utils/constants/grid';
import { FormsModule, NgForm } from '@angular/forms';
import { FormularioService } from '@services/formulario.service';
import { CommonModule } from '@angular/common';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { ExpedienteRecomendacionGeneralFormDTO } from '@dtos/gestion-expediente/expediente-recomendacion-general/expediente-recomendacion-general-form-dto';
import { ExpedienteRecomendacionGeneralService } from '../../../shared/http/gestion-expediente/expediente-recomendacion-general.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { EntidadEstructuraService } from '../../../shared/http/gestion-entidad/entidad-estructura.service';
import { ExpedientePadecimientoSelectorDTO } from '../../../shared/dtos/seguridad/expediente-padecimiento-selector-dto';
import { ExpedienteTrackrService } from '../../../shared/http/seguridad/expediente-trackr.service';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';

@Component({
  selector: 'app-recomendacion-general',
  standalone: true,
  imports: [CommonModule, FormsModule, MatExpansionModule, GridGeneralModule, NgSelectModule],
  templateUrl: './recomendacion-general.component.html',
  styleUrls: ['./recomendacion-general.component.scss']
})
export class RecomendacionGeneralComponent {
  //Variables relacionadas con el componente y la vista
  protected submiting : boolean = false;
  protected esAgregar : boolean = true;

  //Variables relacionadas con la consulta de datos y la interaccion con el servicio
  protected idUsuario : number;
  protected recomendacion : ExpedienteRecomendacionGeneralFormDTO = new ExpedienteRecomendacionGeneralFormDTO;

  //Configuraciones y datos del grid
  public gridOptions : GridOptions;
  public HEADER_GRID = 'Recomendaciones';
  public columns = [
    { headerName: 'Num', valueGetter: (params: any) => params.node.rowIndex + 1, maxWidth: 70 },
    { headerName: 'Fecha', field: 'fecha', maxWidth: 90},
    { headerName: 'Recomendacion', field: 'descripcion', minWidth: 150 },
    { headerName: 'A quien se enviará el mensaje', field: 'tipo', maxwidth: 90},
    { headerName: 'Administrador', field: 'doctor', minWidth: 80 },
  ];
  public recomendacionesList$: Observable<ExpedienteRecomendacionGridDTO[]>;
  protected padecimientos: ExpedientePadecimientoSelectorDTO[];
  protected expedientes: UsuarioExpedienteGridDTO[];
  
  constructor(
    private expedienteRecomendacionGeneralService: ExpedienteRecomendacionGeneralService,
    private encryptionService: EncryptionService,
    private route: ActivatedRoute,
    private mensajeService : MensajeService,
    private formularioService : FormularioService,
    private entidadEstructuraService:EntidadEstructuraService,
    private expedienteTrackrService:ExpedienteTrackrService
    ) 
  {}

  ngOnInit(): void
   {
    this.obtenerParametrosURL();
    this.recomendacion.fecha = new Date();
    this.entidadEstructuraService.consultarPadecimientosParaSelector().subscribe(res => {
      this.padecimientos = res
    })
    this.expedienteTrackrService.consultarParaGrid().subscribe(res =>{
      this.expedientes = res;
      console.log(this.expedientes)
    })
    
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
      this.expedienteRecomendacionGeneralService.eliminar(idExpedienteRecomendacion).subscribe(() => {
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
        this.expedienteRecomendacionGeneralService.consultar(recomendacionGrid.idExpedienteRecomendacion)
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
        this.esAgregar = true;
        this.submiting = false;
        subscription.unsubscribe();
        formulario.reset();
        this.limpiarCampos();
      }
    })
  }

  private consultarGrid()
  {
    this.recomendacionesList$ = this.expedienteRecomendacionGeneralService.consultarGrid();
  }
  
  private limpiarCampos() : void
  {
    this.recomendacion  = new ExpedienteRecomendacionGeneralFormDTO;
    this.recomendacion.fecha = new Date();
  }

  protected editar() : Observable<void>
  { 
   return this.expedienteRecomendacionGeneralService.editarRecomendacionGeneral(this.recomendacion);
  }

  protected agregar() : any
  {
    if(this.recomendacion.tipo == 1){
      return this.expedienteRecomendacionGeneralService.agregarTodos(this.recomendacion);
    }
    else if(this.recomendacion.tipo == 2){
      return this.expedienteRecomendacionGeneralService.agregarPadecimiento(this.recomendacion);
    }
    else if(this.recomendacion.tipo == 3){
      return this.expedienteRecomendacionGeneralService.agregarPaciente(this.recomendacion);
    }
    //return this.expedienteRecomendacionGeneralService.editarRecomendacionGeneral(this.recomendacion);
  }

}
