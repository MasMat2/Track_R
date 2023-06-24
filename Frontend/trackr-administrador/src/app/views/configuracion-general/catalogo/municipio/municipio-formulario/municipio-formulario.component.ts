import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Estado } from '@models/catalogo/estado';
import { Municipio } from '@models/catalogo/municipio';
import { Pais } from '@models/catalogo/pais';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-municipio-formulario',
  templateUrl: './municipio-formulario.component.html',
})
export class MunicipioFormularioComponent implements OnInit {

  public placeHolderSelect: string;
  public placeHolderNoOptions: string;
  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El municipio ha sido agregado';
  public mensajeEditar = 'El municipio ha sido modificado';
  public btnSubmit = false;
  public municipio = new Municipio();
  public estadoList: EstadoSelectorDto[] = [];
  public paisList: Pais[] = [];

  constructor(
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private estadoService: EstadoService,
    private paisService: PaisService,
    private municipioService: MunicipioService
  ) { }

  public ngOnInit(): void {
    this.consultarPaises();

    console.log(this.municipio)

    if (this.municipio.idPais) {
      this.consultarEstados(this.municipio.idPais.toString());
    }
  }

  public consultarPaises(): void {
    this.paisService.consultarGeneral().subscribe(
      (data) => this.paisList = data
    );
  }

  public consultarEstados(idPais: string): void {
    if (idPais !== '') {
      this.estadoService.consultarPorPaisParaSelector(+idPais).subscribe(
        (data) => {
          this.estadoList = data
          console.log(data);
        }
      );
    } else {
      this.estadoList = [];
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
      this.editar();
    }
  }

  public agregar(): void {
    this.municipioService.agregar(this.municipio).subscribe(
      (data) => {
        this.mensajeService.modalExito(this.mensajeAgregar);
        this.onClose(true);
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public editar(): void {
    this.municipioService.editar(this.municipio).subscribe(
      (data) => {
        this.mensajeService.modalExito(this.mensajeEditar);
        this.onClose(true);
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public cancelar(): void {
    this.onClose(true);
  }

}
