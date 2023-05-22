import { NgForm } from '@angular/forms';
import { Paciente } from './../paciente';
import { Component, OnInit } from '@angular/core';
import { GeneralConstant } from '@utils/general-constant';
import { PacienteCreationDTO } from '@dtos/seguridad/paciente-creation-dto';
import { PadecimientoCreationDTO } from '@dtos/seguridad/padecimiento-creation-dto';

@Component({
  selector: 'app-paciente-formulario',
  templateUrl: './paciente-formulario.component.html',
  styleUrls: ['./paciente-formulario.component.scss']
})
export class PacienteFormularioComponent implements OnInit {

  public paciente: PacienteCreationDTO = new PacienteCreationDTO();
  public btnSubmit: boolean = false;
  public accion: string;

  public padecimientoList: PadecimientoCreationDTO[] = [];

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  constructor() { }

  ngOnInit(): void {
    this.crearPaciente();
  }

  protected enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;

    if (!formulario.valid) {
      // this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      // this.agregar();
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      // this.editar();
    }
  }

  protected cancelar(): void {

  }

  protected agregarPadecimiento(){
    const padecimiento = new PadecimientoCreationDTO();
    this.paciente.padecimientos.push(padecimiento);
    console.log(this.paciente.padecimientos)
  }

  protected eliminarPadecimiento(index: number){
    if(index < 0 || index >= this.paciente.padecimientos.length){
      return;
    }
    this.paciente.padecimientos.splice(index, 1);

    if(this.paciente.padecimientos.length === 0){
      this.agregarPadecimiento();
    }
  }

  protected crearPaciente(): void {
    this.paciente = 
      {
          numeroExpediente: 1,
          fechaAlta: new Date(),
          genero: 'Masculino',
          nombre: 'Paciente',
          apellidoPaterno: 'Uno',
          apellidoMaterno: 'Apellido',
          fechaNacimiento: new Date(1980, 1, 1),
          edad: '41',
          telefonoMovil: '1234567890',
          correo: 'paciente1@gmail.com',
          peso: 75,
          cintura: 85,
          estatura: 170,
          domicilio: 'Domicilio 1',
          padecimientos: [
              {
                  idPadecimiento: 1,
                  nombre: 'Patologia 1',
                  fechaDiagnostico: new Date(2021, 1, 1),
              },
              {
                  idPadecimiento: 2,
                  nombre: 'Patologia 2',
                  fechaDiagnostico: new Date(2021, 2, 1),
              },
          ],
      };
  }
}
