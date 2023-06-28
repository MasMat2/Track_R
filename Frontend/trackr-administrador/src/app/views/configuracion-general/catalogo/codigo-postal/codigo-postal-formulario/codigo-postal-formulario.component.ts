
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MunicipioSelector } from '@dtos/catalogo/municipio-selector';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-codigo-postal-formulario',
  templateUrl: './codigo-postal-formulario.component.html'
})
export class CodigoPostalFormularioComponent implements OnInit {

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El código postal ha sido agregado';
  public mensajeEditar = 'El código postal ha sido modificado';
  public btnSubmit = false;

  public codigoPostal= new CodigoPostal();

  public idMunicipio: number;
  public municipioList: MunicipioSelector[] = [];

  constructor(
    public bsModalRef: BsModalRef,
    private municipioService: MunicipioService,
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private codigoPostalService: CodigoPostalService,
  ) { }

  public ngOnInit(): void {
    this.consultarMunicipio();
  }

  public consultarMunicipio(): void  {
    this.municipioService.consultarParaSelector().subscribe((data) => {
      this.municipioList = data;

      if (this.codigoPostal.idMunicipio > 0) {
        this.idMunicipio = this.codigoPostal.idMunicipio;

      }

    });

  }

  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    this.codigoPostal.idMunicipio = this.idMunicipio;
    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregar();
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editar();
    }
  }

  public agregar(): void {
    this.codigoPostalService.agregar(this.codigoPostal).subscribe(
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
    this.codigoPostalService.editar(this.codigoPostal).subscribe(
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
