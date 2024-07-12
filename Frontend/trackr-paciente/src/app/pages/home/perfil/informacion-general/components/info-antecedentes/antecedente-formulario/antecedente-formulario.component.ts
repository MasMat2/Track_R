import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { EntidadEstructuraService } from '../../../../../../../shared/http/gestion-entidad/entidad-estructura.service';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { lastValueFrom } from 'rxjs';
import { addIcons } from 'ionicons';
import { validarCamposRequeridos } from '@utils/utileria';
import { AgregarExpedientePadecimientoDto } from 'src/app/shared/Dtos/seguridad/agregar-expediente-padecimiento-dto';
import { ExpedientePadecimientoService } from '../../../../../../../shared/http/gestion-expediente/expediente-padecimiento.service';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { ExpedientePadecimientoDto } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ModalController } from '@ionic/angular/standalone';


@Component({
  selector: 'app-antecedente-formulario',
  templateUrl: './antecedente-formulario.component.html',
  styleUrls: ['./antecedente-formulario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule
  ]
})
export class AntecedenteFormularioComponent  implements OnInit {

  protected expedientePadecimientoDto = new AgregarExpedientePadecimientoDto();
  protected antecedenteFiltradoList: ExpedientePadecimientoSelectorDTO[];
  protected misDoctoresList: UsuarioDoctoresDto[];
  protected btnSubmit: boolean = false;
  protected fechaActual = new Date().toISOString();
  @Input("antecedentesUsuario") antecedentesUsuario: ExpedientePadecimientoDto[];

  constructor(
    private entidadEstructuraService: EntidadEstructuraService,
    private expedientePadecimientoService: ExpedientePadecimientoService,
    private doctoresService: MisDoctoresService,
    private modalController: ModalController
    
  ) { 
    addIcons({
      'chevron-up': 'assets/img/svg/chevron-up.svg',
      'chevron-down': 'assets/img/svg/chevron-down.svg',
      'calendar': 'assets/img/svg/calendar.svg'
    }) 
  }

  ngOnInit() {
    this.consultarAntecedentesSelector();
    this.consultarDoctores();
  }

  private async consultarAntecedentesSelector(){
    return lastValueFrom(this.entidadEstructuraService.consultarAntecedentesParaSelector())
    .then((antecedentes: ExpedientePadecimientoSelectorDTO[]) => {
     this.antecedenteFiltradoList = antecedentes.filter(ante => !this.antecedentesUsuario.some(pad => pad.idPadecimiento === ante.idPadecimiento));
    })
  }

  private consultarDoctores() {
    this.doctoresService.consultarExpediente().subscribe({
      next: (doctores) => {
        this.misDoctoresList = doctores;
      },
      error: () => {
      },
      complete: () => {
      }
    });
  }

  protected enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;
    if(this.expedientePadecimientoDto.fechaDiagnostico == null){
      this.expedientePadecimientoDto.fechaDiagnostico = new Date();
    }
    if (!formulario.valid) {
      validarCamposRequeridos(formulario);
      return;
    }

    this.agregarExpedientePadecimiento(this.expedientePadecimientoDto);
  }

  private agregarExpedientePadecimiento(expedientePadecimientoDto: AgregarExpedientePadecimientoDto){
    this.expedientePadecimientoService.agregarPadecimiento(expedientePadecimientoDto).subscribe({
      next: () => {
      },
      error: () => {
      },
      complete: () => {
        this.modalController.dismiss();
      }
    })
  }
}
