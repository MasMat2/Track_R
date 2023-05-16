import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SeccionCampoService } from '@http/gestion-entidad/seccion-campo.service';
import { SeccionService } from '@http/gestion-entidad/seccion.service';
import { Dominio } from '@models/catalogo/dominio';
import { DominioService } from '@http/catalogo/dominio.service';
import { Seccion } from '@models/gestion-entidad/seccion';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';

@Component({
  selector: 'app-configuracion-secciones-formulario',
  templateUrl: './configuracion-secciones-formulario.component.html',
  styleUrls: ['./configuracion-secciones-formulario.component.scss']
})
export class ConfiguracionSeccionesFormularioComponent implements OnInit {
  // Constantes
  public MENSAJE_EXITO_AGREGAR_SECCION = "La sección ha sido agregada";
  public MENSAJE_EXITO_EDITAR_SECCION = "La sección ha sido editada";
  public MENSAJE_EXITO_AGREGAR_CAMPO = "El campo ha sido agregado";
  public MENSAJE_EXITO_EDITAR_CAMPO = "El campo ha sido editado";
  public MENSAJE_EXITO_ELIMINAR_CAMPO = "El campo ha sido eliminado";
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  //Configuración
  public accion = GeneralConstant.MODAL_ACCION_AGREGAR;
  public disableSubmit: boolean = false;
  public onClose: (actualizarGrid: boolean) => void;
  public seccion = new Seccion();
  public campo = new SeccionCampo();

  // Grid Campos
  public HEADER_GRID_CAMPOS = 'Campos';
  public camposList: SeccionCampo[];
  public columnsCampos = [
    { headerName: 'Clave', field: 'clave', minWidth: 30 },
    { headerName: 'Descripción', field: 'descripcion', minWidth: 30 },
    { headerName: 'Dominio', field: 'nombreDominio', minWidth: 30 },
    { headerName: 'Orden', field: 'orden', minWidth: 30 },
    { headerName: 'Requerido', field: 'requerido', minWidth: 30, valueGetter: (params: any) => this.convertirSiNo(params.data)}
  ];

  // DropDown Dominio
  public dominioList: Dominio[];

  constructor(
    private dominioService: DominioService,
    private seccionCampoService: SeccionCampoService,
    private seccionService: SeccionService,
    private mensajeService: MensajeService
  ) { }

  public ngOnInit(): void {
    this.consultarDominios();
  }

  private consultarDominios(): void {
    this.dominioService.consultarTodosParaSelector().subscribe((data) => {
      this.dominioList = data;
    });
  }

  // #region Secciones
  public async enviarFormularioSeccion(formulario: NgForm): Promise<void> {
    this.disableSubmit = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      return;
    }

    let exito: boolean = false;
    let mensajeExito: string = '';

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      exito = await this.agregarSeccion(this.seccion);
      mensajeExito = this.MENSAJE_EXITO_AGREGAR_SECCION;
    }
    else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      exito = await this.editarSeccion(this.seccion);
      mensajeExito = this.MENSAJE_EXITO_EDITAR_SECCION;
    }

    if (exito) {
      this.mensajeService.modalExito(mensajeExito);
      this.onClose(true);
    }
    else {
      this.disableSubmit = false;
    }
  }

  private async agregarSeccion(seccion: Seccion) : Promise<boolean> {
    return this.seccionService.agregar(seccion)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }

  private async editarSeccion(seccion: Seccion) : Promise<boolean>{
    return this.seccionService.editar(seccion)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }
  // #endregion

  // #region Campos
  public onGridClickCampo(gridData: { accion: string; data: SeccionCampo }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.consultarCampo(gridData.data.idSeccionCampo);
    }
    else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminarCampo(gridData.data);
    }
  }

  public async enviarFormularioCampo(formulario: NgForm): Promise<void> {
    this.disableSubmit = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      return;
    }

    let exito: boolean = false;
    let mensajeExito: string;

    if (this.campo.idSeccionCampo > 0) {
      exito = await this.editarCampo(this.campo);
      mensajeExito = this.MENSAJE_EXITO_EDITAR_CAMPO;
    }
    else {
      this.campo.idSeccion = this.seccion.idSeccion;
      exito = await this.agregarCampo(this.campo);
      mensajeExito = this.MENSAJE_EXITO_AGREGAR_CAMPO;
    }

    if (exito) {
      this.mensajeService.modalExito(mensajeExito);
      this.consultarGridCampos(this.seccion.idSeccion);
      this.limpiarFormularioCampos();
    }
    else {
      this.disableSubmit = false;
    }
  }

  private consultarGridCampos(idSeccion: number): void {
    this.seccionCampoService.consultarPorSeccion(idSeccion)
      .subscribe((data) => {
        this.camposList = data;
      });
  }

  private consultarCampo(idSeccionCampo: number): void {
    this.seccionCampoService.consultar(idSeccionCampo)
      .subscribe((data) => {
        this.campo = data;
      });
  }

  private async agregarCampo(campo: SeccionCampo): Promise<boolean> {
    return this.seccionCampoService.agregar(campo)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }

  private async editarCampo(campo: SeccionCampo) : Promise<boolean> {
    return this.seccionCampoService.editar(campo)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }

  private eliminarCampo(seccionCampo: SeccionCampo): void {
    this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el campo <string>' + seccionCampo.descripcion + '</string>?',
        'Eliminar Campo'
      )
      .then((aceptar) => {
        this.seccionCampoService.eliminar(seccionCampo.idSeccionCampo).subscribe((exito) => {
          this.mensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR_CAMPO);
          this.consultarGridCampos(this.seccion.idSeccion);
          this.limpiarFormularioCampos();
        })
      });
  }
  // #endregion

  public onRequeridoChanged(value: boolean): void {
    this.campo.requerido = value;
  }

  public onDeshbilitadoChanged(value: boolean): void {
    this.campo.deshabilitado = value;
  }

  public limpiarFormularioCampos(): void {
    this.campo = new SeccionCampo();
    this.campo.idSeccionCampo = 0;
    const form = <HTMLFormElement> document.getElementById('formularioCampo');
    form.reset();
  }

  public cancelar(): void {
    this.onClose(false);
  }

  private convertirSiNo(valor: SeccionCampo): string {
    return valor.requerido ? 'Sí' : 'No';
  }
}
