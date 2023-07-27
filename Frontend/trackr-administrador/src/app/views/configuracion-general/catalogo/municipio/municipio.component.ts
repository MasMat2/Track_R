import { Component, OnInit } from '@angular/core';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Municipio } from '@models/catalogo/municipio';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { CodigoAcceso } from '@utils/codigo-acceso';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MunicipioFormularioComponent } from './municipio-formulario/municipio-formulario.component';

@Component({
  templateUrl: 'municipio.component.html'
})
export class MunicipioComponent implements OnInit {

  public tieneAccesoAgregar = false;
  public accesoEditar = CodigoAcceso.EDITAR_MUNICIPIO;
  public accesoEliminar = CodigoAcceso.ELIMINAR_MUNICIPIO;
  public HEADER_GRID = 'Municipio';
  public MENSAJE_EXITO_ELIMINAR = 'El municipio ha sido eliminado';
  public TITULO_MODAL_ELIMINAR = 'Eliminar Municipio';
  public municipioList: Municipio[];

  public columns = [
    { headerName: 'Clave', field: 'clave', minWidth: 80, maxWidth: 80 },
    { headerName: 'Pais', field: 'nombrePais', minWidth: 150 },
    { headerName: 'Estado', field: 'nombreEstado', minWidth: 150 },
    { headerName: 'Municipio', field: 'nombre', minWidth: 150 },
  ];

  constructor(
    private mensajeService: MensajeService,
    private municipioService: MunicipioService,
    private estadoService: EstadoService,
    private accesoService: AccesoService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef
  ) { }

  public ngOnInit(): void {
    this.accesoService.tieneAcceso(CodigoAcceso.AGREGAR_MUNICIPIO).subscribe({
      next: (data) => {
        this.tieneAccesoAgregar = data;
      },
    });

    this.consultarGrid();
  }

  public consultarGrid(): void {
    this.municipioService.consultarTodosParaGrid().subscribe((data) => {
      this.municipioList = data;
    });
  }

  public onGridClick(gridData: { accion: string; data: Municipio }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idMunicipio);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
      return;
    }
  }

  public agregar(): void {
    this.bsModalRef = this.modalService.show(
      MunicipioFormularioComponent,
      GeneralConstant.CONFIG_MODAL_DEFAULT
    );
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
    this.bsModalRef.content.placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
    this.bsModalRef.content.onClose = (cerrar: boolean) => {
      if (cerrar) {
        this.consultarGrid();
      }
      this.bsModalRef.hide();
    };
  }

  public editar(idMinicipio: number): void {
    this.municipioService.consultar(idMinicipio).subscribe((data) => {
      this.bsModalRef = this.modalService.show(
        MunicipioFormularioComponent,
        GeneralConstant.CONFIG_MODAL_DEFAULT
      );
      this.estadoService.consultar(data.idEstado).subscribe((data1) => {
        data.idPais = data1.idPais;
      });

      this.bsModalRef.content.municipio = data;
      this.bsModalRef.content.placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
      this.bsModalRef.content.placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN;
      this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_EDITAR;
      this.bsModalRef.content.onClose = (cerrar: boolean) => {
        if (cerrar) {
          this.consultarGrid();
        }
        this.bsModalRef.hide();
      };
    });
  }

  public eliminar(municipio: Municipio): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el municipio <strong>' + municipio.nombre + '</strong>?',
        this.TITULO_MODAL_ELIMINAR
      )
      .then((aceptar) => {
        this.municipioService.eliminar(municipio.idMunicipio).subscribe((data) => {
          this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarGrid();
        });
      });
  }

}
