import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DominioHospitalDto } from '@dtos/catalogo/dominio-hospital-dto';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalService } from 'ngx-bootstrap/modal';
import * as Utileria from '@utils/utileria';
import * as moment from 'moment';

@Component({
  selector: 'app-dominio-hospital-formulario',
  templateUrl: './dominio-hospital-formulario.component.html',
  styleUrls: ['./dominio-hospital-formulario.component.scss']
})
export class DominioHospitalFormularioComponent implements OnInit {
  @Input() public dominioHospital: DominioHospitalDto;
  @Input() public accion: string;
  public configDate = GeneralConstant.CONFIG_DATEPICKER;
  public minDate = Utileria.obtenerFechaActual();
  public minDateMaxima = Utileria.obtenerFechaActual();
  public isDisable = false;
  public btnSubmit = false;

  constructor(private modalService: BsModalService){

  }

  ngOnInit(): void {
    
  }

  // Funciones para validar fechas
  public onMinimaChange(value: any): void {
    if (this.accion !== 'Editar') {
      this.dominioHospital.fechaMinima = value;

      if (this.dominioHospital.fechaMinima && Utileria.isValidDate(value)) {
        this.minDateMaxima = moment(this.dominioHospital.fechaMinima, 'DD-MM-YYYY').toDate();
      }
    }
  }

  public onMaximaChange(value: Date): void {
    if  (this.accion !== 'Editar') {
      this.dominioHospital.fechaMaxima = value;
    }
  }

  public cancelar(): void {
    this.modalService.hide();
  }

  public enviarFormulario(formulario: NgForm): void {
    
  }

}
