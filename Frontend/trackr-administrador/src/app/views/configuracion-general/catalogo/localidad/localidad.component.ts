import { Component, OnInit } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Localidad } from '@models/catalogo/localidad';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { LocalidadFormularioComponent } from './localidad-formulario/localidad-formulario.component';

@Component({
  templateUrl: 'localidad.component.html',
})
export class LocalidadComponent implements OnInit {

  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_LOCALIDAD;
  public accesoEliminar = CodigoAcceso.ELIMINAR_LOCALIDAD;
  public HEADER_GRID = 'Localidad';
  public MENSAJE_EXITO_ELIMINAR = 'La localidad ha sido eliminada';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Localidad';
  public localidadList: Localidad[];
  public tempLocalidad = new Localidad();

  public columns = [
    { headerName: 'Pais', field: 'nombrePais', minWidth: 150 },
    { headerName: 'Estado', field: 'nombreEstado', minWidth: 150 },
    { headerName: 'Municipio', field: 'nombreMunicipio', minWidth: 150 },
    { headerName: 'Nombre', field: 'nombre', minWidth: 150 },
  ];

  constructor(
    private mensajeService: MensajeService,
    private localidadService: LocalidadService,
    private municipioService: MunicipioService,
    private paisService: PaisService,
    private estadoService: EstadoService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) { }

  public ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_LOCALIDAD).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });

    this.consultarGrid();
  }

  public consultarGrid(): void {
    // this.localidadService.consultarTodosParaGrid().subscribe((data) => {
    //   this.localidadList = data;
    // });
  }

  public onGridClick(gridData: { accion: string; data: Localidad }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idLocalidad);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void {
    this.bsModalRef = this.modalService.show(
      LocalidadFormularioComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.opcion = true;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public editar(idLocalidad: number): void {
    let estado;
    let pais;
    // this.localidadService.consultar(idLocalidad).subscribe((data) => {
    //   this.bsModalRef = this.modalService.show(
    //     LocalidadFormularioComponent,
    //     GeneralConstant.CONFIG_MODAL_DEFAULT
    //   );

    //   this.municipioService.consultar(data.idMunicipio).subscribe((datos) => {
    //     estado = datos.idEstado;
    //     this.estadoService.consultar(estado).subscribe((dato1) => {
    //       pais = dato1.idPais;
    //       data.idEstado = estado;
    //       data.idPais = pais;
    //     });
    //   });

    //   this.bsModalRef.content.localidad = data;
    //   this.bsModalRef.content.tempLocalidad = data;
    //   this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
    //   this.bsModalRef.content.onClose = (cerrar: boolean) => {
    //     if (cerrar) {
    //       this.consultarGrid();
    //     }
    //     this.bsModalRef.hide();
    //   };
    // });
  }

  public eliminar(localidad: Localidad): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar la localidad <strong>' + localidad.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR
      )
      .then((aceptar) => {
        // this.localidadService.eliminar(localidad.idLocalidad).subscribe((data) => {
        //   this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
        //   this.consultarGrid();
        // });
      });
  }

}
