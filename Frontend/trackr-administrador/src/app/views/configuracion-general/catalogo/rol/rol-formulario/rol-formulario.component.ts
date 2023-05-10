import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { RolService } from '@http/seguridad/rol.service';
import { Rol } from '@models/seguridad/rol';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-rol-formulario',
  templateUrl: './rol-formulario.component.html'
})
export class RolFormularioComponent implements OnInit {

  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'El rol ha sido agregado';
  public mensajeEditar = 'El rol ha sido modificado';
  public btnSubmit = false;
  public rol = new Rol();
  constructor(
    private mensajeService: MensajeService,
    private formularioService: FormularioService,
    private rolService: RolService
  ) { }

  public ngOnInit(): void {
  }

  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;

    if (this.rol.filtrado == undefined)
      this.rol.filtrado = false;

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
    this.rolService.agregar(this.rol).subscribe(
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
    this.rolService.editar(this.rol).subscribe(
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
