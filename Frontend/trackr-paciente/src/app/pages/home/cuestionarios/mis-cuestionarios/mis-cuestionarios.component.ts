import { CommonModule, Time } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
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
import { LoadingSpinnerService } from 'src/app/services/dashboard/loading-spinner.service';
import { Subject, takeUntil } from 'rxjs';

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
export class MisCuestionariosComponent  implements OnInit, OnDestroy {

  protected examenPendienteList: ExamenDto[] = [];
  protected examenContestadoList: ExamenDto[] = [];
  protected cantidadCuestionariosContestados: number;
  protected cantidadCuestionariosContestadosOcultos: number;
  protected mostrarTodosContestados: boolean = false;
  protected masDeCincoExamenes: boolean = false;

  private destroy$ = new Subject<void>();

  protected segmentoSeleccionado = 'pendientes';

  constructor(
    private examenService: ExamenService,
    private router: Router,
    private alertController: AlertController,
    private tabService: TabService,
    private spinnerService : LoadingSpinnerService
  ) { 
    addIcons({chevronForward});

    //Simula ionViewWillEnter
    this.tabService.tabChange$
      .pipe(takeUntil(this.destroy$))
      .subscribe((tabId) => {
        if (tabId === 'cuestionarios') {
          this.cargarCuestionarios();
        }
      });

    this.examenService.actualizarCuestionarios$.subscribe(() => {
      this.cargarCuestionarios();
    });


  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private async cargarCuestionarios() {
    this.spinnerService.presentLoading();
    const cuestionariosPendientesPromise = this.consultarCuestionariosPendientes();
    const cuestionariosContestadosPromise = this.consultarCuestionariosContestados();
    await Promise.all([cuestionariosPendientesPromise, cuestionariosContestadosPromise]);
    this.spinnerService.dismissLoading();
  }
  
  private consultarCuestionariosPendientes() {
    return new Promise((resolve, reject) => {
      this.examenService.consultarMisExamenes().subscribe({
        next: (examenes) => {
          this.examenPendienteList = examenes.map(examen => {
            const fechaFormateada = this.formatearFecha(examen.fechaExamen, examen.horaExamen);
            return {...examen, fechaExamen: fechaFormateada};
          });
          resolve(examenes);
        },
        error: (error) => reject(error)
      });
    });
  }

  private consultarCuestionariosContestados() {
    return new Promise((resolve, reject) => {
      this.examenService.consultarMisExamenesContestados().subscribe({
        next: (examenes) => {
          this.examenContestadoList = examenes.map(examen => {
            const fechaFormateada = this.formatearFecha(examen.fechaExamen, examen.horaExamen);
            return {...examen, fechaExamen: fechaFormateada};
          });
          if(examenes.length > 5){
            this.masDeCincoExamenes = true;
            this.cantidadCuestionariosContestadosOcultos = (examenes.length - 5);
          }
          resolve(examenes);
        },
        error: (error) => reject(error)
      });
    });
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
