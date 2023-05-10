import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { Colonia } from '@models/catalogo/colonia';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  selector: 'app-colonia-formulario',
  templateUrl: './colonia-formulario.component.html',
  styleUrls: ['./colonia-formulario.component.scss']
})
export class ColoniaFormularioComponent implements OnInit {

  public onClose: any;
  public accion: string;
  public mensajeAgregar = 'La colonia ha sido agregada';
  public mensajeEditar = 'La colonia ha sido modificada';
  public btnSubmit = false;
  public colonia = new Colonia();

  constructor(private formularioService: FormularioService,
    private coloniaService: ColoniaService,
    private mensajeService: MensajeService) { }

  ngOnInit() {
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
    this.coloniaService.agregar(this.colonia).subscribe((data) => {
      this.mensajeService.modalExito(this.mensajeAgregar);
      this.onClose(true);
    },
      (error) => {
        this.btnSubmit = false;
      });
  }

  public editar(): void {
    this.coloniaService.editar(this.colonia).subscribe((data) => {
      this.mensajeService.modalExito(this.mensajeEditar);
      this.onClose(true);
    },
      (error) => {
        this.btnSubmit = false;
      });
  }

  public cancelar(): void {
    this.onClose(true);
  }

}
