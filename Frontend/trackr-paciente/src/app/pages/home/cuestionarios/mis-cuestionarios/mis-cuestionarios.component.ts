import { CommonModule, Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { Examen } from '@models/examen/examen';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { format} from 'date-fns';
import { chevronForward } from 'ionicons/icons';
import { addIcons } from 'ionicons';
import { ExamenDto } from 'src/app/shared/Dtos/cuestionarios/examen-dto';
import { TabService } from 'src/app/services/dashboard/tab.service';

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

  protected examenPendienteList: ExamenDto[] = [];
  protected examenContestadoList: ExamenDto[] = [];
  protected cantidadCuestionariosContestados: number;
  protected mostrarTodosContestados: boolean = false;

  protected segmentoSeleccionado = 'pendientes';

  constructor(
    private examenService: ExamenService,
    private router: Router,
    private alertController: AlertController,
    private tabService: TabService
  ) { 
    addIcons({chevronForward});

    //Simula ionViewWillEnter
    this.tabService.tabChange$.subscribe((tabId) => {
      if (tabId === 'cuestionarios') {
        this.consultarCuestionariosPendientes();
        this.consultarCuestionariosContestados();
      }
    });
  }

  ngOnInit() {
  }


  private consultarCuestionariosPendientes(){
    this.examenService.consultarMisExamenes().subscribe({
      next: (examenes) => {
        console.log('exámenes pendientes: ', examenes);
        this.examenPendienteList = examenes.map(examen => {
          const fechaFormateada = this.formatearFecha(examen.fechaExamen, examen.horaExamen);
          return {...examen, fechaExamen: fechaFormateada};
        });
      }
    })
  }

  private consultarCuestionariosContestados(){
    this.examenService.consultarMisExamenesContestados().subscribe({
      next: (examenes) => {
        console.log('exámenes contestados: ', examenes);
        this.examenContestadoList = examenes.map(examen => {
          const fechaFormateada = this.formatearFecha(examen.fechaExamen, examen.horaExamen);
          return {...examen, fechaExamen: fechaFormateada};
        });
        (examenes.length > 5) ? (this.cantidadCuestionariosContestados = examenes.length -5) : (this.cantidadCuestionariosContestados = examenes.length);
      }
    })
  }

  protected formatearFecha(fecha:Date ,hora: Time){
    const fechaString = new Date(`${new Date(fecha).toDateString()} ${hora}`);
    return fechaString;
  }

  protected responderCuestionario(idExamen: number) {
    this.examenService.consultarMiExamenIndividual(idExamen)
      .subscribe((examen) => {
          if (!this.esFechaValida(examen)) {
           this.presentAlertError();
           return;
         }
         this.navigateResponderCuestionario(idExamen);
      });
  }

  protected navigateVerCuestionario(idExamen: number) {
    this.router.navigate(['/home/cuestionarios/ver', idExamen]);
  }

  private navigateResponderCuestionario(idExamen: number){
    this.router.navigate(['/home/cuestionarios/responder', idExamen]);
  }

  private esFechaValida(examen: Examen): boolean{
    const fechaExamen = this.formatearFecha(examen.fechaExamen, examen.horaExamen);
    console.log(fechaExamen);
    const fechaActual = new Date();

    if (fechaExamen.toDateString() !== fechaActual.toDateString()) {
      console.log('false 1')
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
      console.log('false 2');
      return false;

    }
    console.log('true');
    return true;
  }

  private async presentAlertError() {
    const alert = await this.alertController.create({
      header: 'Cuestionario no disponible',
      subHeader: 'Aún no tienes acceso a este cuestionario',
      cssClass: 'custom-alert color-error icon-info',
      buttons: ['OK'],
    });

    await alert.present();
  }

  protected changeSection(section:string){
    this.segmentoSeleccionado = section;
  }

  protected verMas(opcion: boolean){
    this.mostrarTodosContestados = opcion;
  }

}
