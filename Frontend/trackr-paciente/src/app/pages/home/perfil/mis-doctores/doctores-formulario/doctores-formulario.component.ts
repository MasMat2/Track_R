import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { IonicModule } from '@ionic/angular';
import { UsuarioDoctorDto } from 'src/app/shared/Dtos/usuario-doctor-dto';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';

@Component({
  selector: 'app-doctores-formulario',
  templateUrl: './doctores-formulario.component.html',
  standalone: true,
  imports: [
    IonicModule,
    NgFor
  ]
})
export class DoctoresFormularioComponent implements OnInit {

  constructor(
    private doctoresService: MisDoctoresService
  ) { }
  ngOnInit() {
    this.consultarSelector();
    console.log('hola');
  }

  protected currentDoctor: UsuarioDoctorDto;

  protected doctoresSelector: UsuarioDoctoresSelectorDto[];

  handleChange(ev: any) {
    this.currentDoctor = ev.target.value;
    this.agregar();
  }

  agregar() {
    var doctor = {
      idUsuarioDoctor: this.currentDoctor.idUsuarioDoctor
    }
    console.log('doc tor', doctor);
    console.log('cirrent doctor', this.currentDoctor);

    const subscription = this.doctoresService.agregar(this.currentDoctor)
      .subscribe({
        next: () => {
          this.consultarSelector();
          console.log('exito gg');
        },
        error: () => { },
        complete: () => {
          subscription.unsubscribe();
        }
      }
      );
  }

  consultarSelector() {
    this.doctoresService.consultarSelector().subscribe((data) => {
      this.doctoresSelector = data;
      console.log("selector ", this.doctoresSelector);
    })
  }

}
