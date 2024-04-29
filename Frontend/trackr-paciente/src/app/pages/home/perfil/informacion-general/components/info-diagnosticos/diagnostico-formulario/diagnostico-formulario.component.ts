import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { EntidadEstructuraService } from '../../../../../../../shared/http/gestion-entidad/entidad-estructura.service';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { lastValueFrom } from 'rxjs';
import { addIcons } from 'ionicons';
import { calendarOutline, chevronUp, chevronDown } from 'ionicons/icons';
import { validarCamposRequeridos } from '@utils/utileria';
import { AgregarExpedientePadecimientoDto } from 'src/app/shared/Dtos/seguridad/agregar-expediente-padecimiento-dto';
import { ExpedientePadecimientoService } from '../../../../../../../shared/http/gestion-expediente/expediente-padecimiento.service';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresDto } from 'src/app/shared/Dtos/usuario-doctores-dto';
import { ExpedientePadecimientoDto } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ModalController } from '@ionic/angular/standalone';

@Component({
  selector: 'app-diagnostico-formulario',
  templateUrl: './diagnostico-formulario.component.html',
  styleUrls: ['./diagnostico-formulario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule
  ]
})
export class DiagnosticoFormularioComponent  implements OnInit {


  protected expedientePadecimientoDto = new AgregarExpedientePadecimientoDto();
  protected diagnosticoFiltradoList: ExpedientePadecimientoSelectorDTO[];
  protected misDoctoresList: UsuarioDoctoresDto[];
  protected btnSubmit: boolean = false;
  protected fechaActual = new Date().toISOString();
  @Input("diagnosticosUsuario") diagnosticosUsuario: ExpedientePadecimientoDto[];

  constructor(
    private entidadEstructuraService: EntidadEstructuraService,
    private expedientePadecimientoService: ExpedientePadecimientoService,
    private doctoresService: MisDoctoresService,
    private modalController: ModalController
  ) { addIcons({calendarOutline, chevronUp, chevronDown})}

  ngOnInit() {
    this.consultarDiagnosticosSelector();
    this.consultarDoctores();
  }

  private async consultarDiagnosticosSelector(){
    return lastValueFrom(this.entidadEstructuraService.consultarDiagnosticosParaSelector())
    .then((diagnosticos: ExpedientePadecimientoSelectorDTO[]) => {
     this.diagnosticoFiltradoList = diagnosticos.filter(ante => !this.diagnosticosUsuario.some(pad => pad.idPadecimiento === ante.idPadecimiento));
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
