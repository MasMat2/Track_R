import { Component, OnInit, EventEmitter } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { UsuarioDoctoresDto } from '../../../../shared/Dtos/usuario-doctores-dto';
import { CommonModule, NgFor } from '@angular/common';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { DoctoresFormularioComponent } from './doctores-formulario/doctores-formulario.component';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-mis-doctores',
  templateUrl: './mis-doctores.component.html',
  standalone: true,
  imports: [
    IonicModule,
    NgFor,
    CommonModule,
    DoctoresFormularioComponent,
    RouterModule
  ]
})
export class MisDoctoresComponent implements OnInit {
  protected misDoctores: UsuarioDoctoresDto[];
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];
  protected currentDoctor: UsuarioDoctorDto;

  constructor(
    private doctoresService: MisDoctoresService
  ) { }



  ngOnInit() {
    this.consultarDoctores();
  }

  handleChange(ev: any) {
    this.currentDoctor = ev.target.value;

  }

  consultarDoctores() {
    this.doctoresService.consultarDoctores().subscribe((data => {
      this.misDoctores = data;
      console.log(this.misDoctores);
    }));
  }





  eliminar(doctor: UsuarioDoctoresDto) {

    var doctores = {
      idExpedienteDoctor: doctor.idExpedienteDoctor,
      idUsuarioDoctor: doctor.idUsuarioDoctor,
      idExpediente: doctor.idExpediente
    }

    const subscription = this.doctoresService.eliminar(doctores)
      .subscribe({
        next: () => {
          this.consultarDoctores();
          console.log('exito gg');
        },
        error: () => {
          console.log('ocurrio un problema');
        },
        complete: () => {
          subscription.unsubscribe();
        }
      }
      );
  }

}
