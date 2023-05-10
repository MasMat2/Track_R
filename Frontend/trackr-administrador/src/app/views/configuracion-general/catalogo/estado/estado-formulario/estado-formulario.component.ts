import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoService } from '@http/catalogo/estado.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Estado } from '@models/catalogo/estado';
import { Pais } from '@models/catalogo/pais';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-estado-formulario',
  templateUrl: './estado-formulario.component.html',
})
export class EstadoFormularioComponent implements OnInit {
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El estado ha sido agregado';
  public mensajeEditar = 'El estado ha sido modificado';
  public btnSubmit = false;
  public estado = new Estado();
  public paisList: Pais[] = [];
  public idPais: number;

  constructor(
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private estadoService: EstadoService,
    private paisService: PaisService,
  ) {}

  public ngOnInit(): void {
    this.consultarPaises();
  }

  public consultarPaises(): void {
    this.paisService.consultarGeneral().subscribe(
      (data) => {
        this.paisList = data;

        if (this.estado.idPais > 0) {
          this.idPais = this.estado.idPais;
        }
      }
    );
  }

  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    this.estado.idPais = this.idPais;

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      this.agregar();
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editar();
    }
  }

  public agregar(): void {
    this.estadoService.agregar(this.estado).subscribe(
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
    this.estadoService.editar(this.estado).subscribe(
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
