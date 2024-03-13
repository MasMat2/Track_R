import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { MunicipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Localidad } from '@models/catalogo/localidad';
import { Pais } from '@models/catalogo/pais';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-localidad-formulario',
  templateUrl: 'localidad-formulario.component.html',
})
export class LocalidadFormularioComponent implements OnInit {

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'La localidad ha sido agregada';
  public mensajeEditar = 'La localidad ha sido modificada';
  public btnSubmit = false;
  public tempLocalidad = new Localidad();
  public localidad = new Localidad();
  public municipioList: MunicipioSelectorDto[] = [];
  public estadoList: EstadoSelectorDto[] = [];
  public paisList: Pais[] = [];
  public opcion = false;

  constructor(
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private estadoService: EstadoService,
    private paisService: PaisService,
    private municipioService: MunicipioService,
    private localidadService: LocalidadService
  ) { }

  public ngOnInit(): void {
    this.consultarPaises();
  }

  // tslint:disable-next-line: use-lifecycle-interface
  public ngAfterContentChecked(): void {
    if (this.localidad.idEstado !== undefined && this.opcion === false) {
      this.consultarMunicipios(this.localidad.idEstado + '');
      this.opcion = true;
    }
  }

  public consultarPaises(): void {
    this.paisService.consultarGeneral().subscribe(
      (data) => this.paisList = data
    );
  }

  // tslint:disable-next-line: typedef
  public async consultarEstados(idPais: string) {
    if (idPais !== '') {
       await this.estadoService.consultarPorPaisParaSelector(+idPais).subscribe(
        (data) => this.estadoList = data
      );
    } else {
      this.localidad.idEstado = 0;
      //this.localidad.idMunicipio = null;

      this.estadoList = [];
      this.municipioList = [];
    }
  }

  // tslint:disable-next-line: typedef
  public async consultarMunicipios(idEstado: string) {
    if (idEstado !== '') {
      await this.municipioService.consultarPorEstadoParaSelector(+idEstado).subscribe(
        (data) => this.municipioList = data
      );
    } else {
      //this.localidad.idMunicipio = null;
      this.municipioList = [];
    }
  }

  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregar();
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      // this.localidadService.consultar(this.localidad.idLocalidad).subscribe((data) => {
      //   if (data.nombre !== this.localidad.nombre || data.idMunicipio !== this.localidad.idMunicipio) {
      //     this.editar();
      //   } else {
      //     this.mensajeService.modalExito(this.mensajeEditar);
      //     this.onClose(true);
      //   }
      // });
    }
  }

  public agregar(): void {
    // this.localidadService.agregar(this.localidad).subscribe(
    //   (data) => {
    //     this.mensajeService.modalExito(this.mensajeAgregar);
    //     this.onClose(true);
    //   },
    //   (error) => {
    //     this.btnSubmit = false;
    //   }
    // );
  }

  public editar(): void {
    // this.localidadService.editar(this.localidad).subscribe(
    //   (data) => {
    //     this.mensajeService.modalExito(this.mensajeEditar);
    //     this.onClose(true);
    //   },
    //   (error) => {
    //     this.btnSubmit = false;
    //   }
    // );
  }

  public cancelar(): void {
    this.onClose(true);
  }
}
