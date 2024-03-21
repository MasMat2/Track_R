import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExamenReactivoService } from '@http/cuestionarios/examen-reactivo.service';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { IonicModule, AlertController } from '@ionic/angular';
import { Examen } from '@models/examen/examen';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { Reactivo } from '@models/examen/reactivo';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { GeneralConstant } from '@utils/general-constant';
import { addIcons } from 'ionicons';
import { chevronBack, calendarClearOutline, timeOutline, documentTextOutline } from 'ionicons/icons';

@Component({
  selector: 'app-responder-cuestionario',
  templateUrl: './responder-cuestionario.component.html',
  styleUrls: ['./responder-cuestionario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    HeaderComponent
  ]
})
export class ResponderCuestionarioComponent  implements OnInit {

  private idExamen: any;
  protected submitting = false;
  protected presentando = false;

  protected progreso = 0;
  private porcentajePorPregunta = 0;
  protected reactivo = new Reactivo();
  protected examen = new Examen();
  protected examenTerminado = false;
  protected blockQuestions = false;
  protected mostrarMsgFinal = false;

  protected reactivoList: ExamenReactivo[] = [];

  protected respuestas: { nombre: string }[] = [
    { nombre: 'a' },
    { nombre: 'b' },
    { nombre: 'c' },
    { nombre: 'd' },
    { nombre: 'e' },
  ];

  protected indice = 0;

  protected segundosRestantes = 0;
  protected lastMinute = false;

  private interval: NodeJS.Timer;
  constructor(
    private examenService: ExamenService,
    private examenReactivoService: ExamenReactivoService,
    private route: ActivatedRoute,
    private router: Router,
    private alertController: AlertController,

  ) { 
    addIcons({chevronBack,calendarClearOutline,timeOutline,documentTextOutline})
  }

  ngOnInit() {
    this.submitting = true;
    this.route.paramMap.subscribe(params => {
      this.idExamen = params.get('id');
      if(this.idExamen > 0){
        this.consultarExamen(this.idExamen);
      } else {
        this.blockQuestions = true;
        this.consultarReactivosNoExamen();
      }
    });
  }

  ionViewWillLeave(){
    this.terminarExamenTiempo();
  }


  consultarExamen(idExamen: number) {
    this.examenService.consultarMiExamen(idExamen).subscribe((data) => {
      this.examen = data;
      this.porcentajePorPregunta = 1/data.totalPreguntas;
      this.segundosRestantes = this.examen.duracion * 60;
      this.submitting = false;
    });
  }

  consultarReactivos() {
    this.submitting = true;
    this.examenReactivoService
      .consultarReactivosExamen(this.examen.idExamen)
      .subscribe((data) => {
        if (data.length != this.examen.totalPreguntas) {
          this.cancelar();
        } else {
          this.reactivoList = data;
          this.submitting = false;
          this.interval = setInterval(() => this.updateTimer(), 1000);
          setTimeout(() => this.terminarExamenTiempo(), this.segundosRestantes * 1000);
          this.presentando = true;
        }
      });
  }

  consultarReactivosNoExamen() {
    this.examenReactivoService
      .consultarReactivosExamen(this.examen.idExamen)
      .subscribe((data) => {
        this.reactivoList = data;
        this.submitting = false;
      });
  }

  public cancelar(): void {
    if (!this.blockQuestions) {
      this.router.navigate(['/home/cuestionarios/misCuestionarios'], {});
    }
  }

  private terminarExamen() {
    clearInterval(this.interval);
    this.indice++;
    this.submitting = true;
    this.examenReactivoService.revisar(this.reactivoList).subscribe(
      (data) => {
        this.examen.resultado = data;
        this.examenTerminado = true;
        this.submitting = false;
        this.router.navigate(['/home/cuestionarios/misCuestionarios'], {});
      });
  }

  protected terminarExamenTiempo() {
    this.submitting = true;
    this.examenReactivoService.revisar(this.reactivoList).subscribe((data) => {
      this.examen.resultado = data;
      this.examenTerminado = true;

      this.submitting = false;
    });
  }

  protected siguiente() {
    if (this.indice + 1 < this.reactivoList.length) {
      this.indice++;
      this.progreso = this.indice*this.porcentajePorPregunta;
    }
    else{ //cuando ya se encuentra en la ultima pregunta
      this.progreso = 1;
      this.mostrarMsgFinal = true;
    }
  }

  protected anterior() {
    if (this.indice > 0 && !this.mostrarMsgFinal) {
      this.indice--;
      this.progreso = this.indice*this.porcentajePorPregunta;
    }
    else{//cuando ya se encuentra en el mensaje final
      this.progreso = this.indice*this.porcentajePorPregunta;
      this.mostrarMsgFinal = false;

    }
  }

  private updateTimer() {
    this.segundosRestantes--;
    if (this.segundosRestantes == 0) {
      clearInterval(this.interval);
    }
    else if (this.segundosRestantes < 60 && !this.lastMinute) {
      this.lastMinute = true;
      this.presentAlert("Advertencia", "Sólo queda 1 minuto para responder el examen");
    }
  }

  protected async presentAlertTerminar() {
    const alert = await this.alertController.create({
      header: 'Enviar cuestionario',
      message: '¿Seguro que desea terminar y enviar el cuestionario?',
      cssClass: 'custom-alert',
      buttons: [
        {text: 'Cancelar'},
        {
          text: 'Enviar',
          handler: () => {
            this.terminarExamen();
          }
        }
      ],
    });
    await alert.present();
  }

  protected async presentAlert(header: string, msg: string ) {
    const alert = await this.alertController.create({
      header: header,
      message: msg,
      buttons: ['Ok'],
      cssClass: 'custom-alert'
    });
    await alert.present();
  }

  imprimirFecha(fecha:Date){
    let meses: { [key: number]: string } = {
      1: 'Enero',
      2: 'Febrero',
      3: 'Marzo',
      4: 'Abril',
      5: 'Mayo',
      6: 'Junio',
      7: 'Julio',
      8: 'Agosto',
      9: 'Septiembre',
      10: 'Octubre',
      11: 'Noviembre',
      12: 'Diciembre'
    };

    let fec = new Date(fecha)

    let dia = fec.getDate();
    let mes = meses[fec.getMonth() + 1];
    let anio = fec.getFullYear()
    
    return `${mes} ${dia}, ${anio}`;
  }

}
