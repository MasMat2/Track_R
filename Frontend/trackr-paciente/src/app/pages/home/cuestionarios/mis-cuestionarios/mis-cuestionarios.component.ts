import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { Examen } from '@models/examen/examen';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { format, parse } from 'date-fns';

@Component({
  selector: 'app-mis-cuestionarios',
  templateUrl: './mis-cuestionarios.component.html',
  styleUrls: ['./mis-cuestionarios.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    HeaderComponent
  ]
})
export class MisCuestionariosComponent  implements OnInit {

  protected examenPendienteList: any[] = [];
  protected examenContestadoList: any[] = [];
  protected mostrarTodosContestados: boolean = false;

  constructor(
    private examenService: ExamenService,
    private router: Router,
    private alertController: AlertController
  ) { }

  ngOnInit() {
  }

  ionViewWillEnter(){
    this.consultarCuestionariosPendientes();
    this.consultarCuestionariosContestados();
  }

  private consultarCuestionariosPendientes(){
    this.examenService.consultarMisExamenes().subscribe({
      next: (examenes) => {
        this.examenPendienteList = examenes.map(examen => {
          const fechaFormateada = format(new Date(examen.fechaExamen), 'dd-MM-yyyy');
          return { ...examen, fechaExamen: fechaFormateada};
        });
      }
    })
  }

  private consultarCuestionariosContestados(){
    this.examenService.consultarMisExamenesContestados().subscribe({
      next: (examenes) => {
        this.examenContestadoList = examenes.map(examen => {
          const fechaFormateada = format(new Date(examen.fechaExamen), 'dd-MM-yyyy');
          return { ...examen, fechaExamen:fechaFormateada};
        });
      }
    })
  }

  protected verMas(opcion: boolean){
    this.mostrarTodosContestados = opcion;
  }

  protected responderCuestionario(idExamen: number) {
    this.examenService
      .consultarMiExamenIndividual(idExamen)
      .subscribe((examen) => {
          if (!this.esFechaValida(examen)) {
           this.presentAlertError();
           return;
         }
        this.router.navigate(['/home/cuestionarios/cuestionario', idExamen]);
      });
  }

  private esFechaValida(examen: Examen): boolean {
    const fechaExamen = new Date(`${new Date(examen.fechaExamen).toDateString()} ${examen.horaExamen}`);
    const fechaActual = new Date();

    if (fechaExamen.toDateString() !== fechaActual.toDateString()) {
      return false;
    }

    const milisegundos = fechaExamen.getTime() - fechaActual.getTime();

    const MS_IN_A_DAY: number = 86_400_000;
    const MS_IN_AN_HOUR: number = 3_600_000;
    const MS_IN_A_MINUTE: number = 60_000;

    const diferenciaMinutos = Math.round(
      ((milisegundos % MS_IN_A_DAY) % MS_IN_AN_HOUR) / MS_IN_A_MINUTE
    );

    if (diferenciaMinutos > 5 || diferenciaMinutos <= -15) {
      return false;
    }

    return true;
  }

  private async presentAlertError() {
    const alert = await this.alertController.create({
      header: 'Error',
      message: 'Aún no tiene acceso a este cuestionario',
      buttons: ['OK'],
    });

    await alert.present();
  }

}
