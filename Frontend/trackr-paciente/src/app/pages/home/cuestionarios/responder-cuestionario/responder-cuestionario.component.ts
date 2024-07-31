import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExamenReactivoService } from '@http/cuestionarios/examen-reactivo.service';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { IonicModule, AlertController } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import { Examen } from '@models/examen/examen';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { Respuesta } from '@models/examen/respuesta';
import { FechaService } from '@services/fecha.service';
import { ImageOnlyModalComponent } from '@sharedComponents/image-only-modal/image-only-modal.component';
import { addIcons } from 'ionicons';
import { BehaviorSubject, map } from 'rxjs';
import { OnExit } from 'src/app/shared/guards/exit.guard';


interface ExamenReactivoRespuestasArray extends ExamenReactivo{
  respuestasSeparadas: Respuesta[]
  respuestaSeleccionadaIndex: number | undefined;
}

@Component({
  selector: 'app-responder-cuestionario',
  templateUrl: './responder-cuestionario.component.html',
  styleUrls: ['./responder-cuestionario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
  ]
})
export class ResponderCuestionarioComponent  implements OnInit, OnExit {

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  protected cargando$ = this.cargandoSubject.asObservable();
  private loading : any;
  
  private idExamen: any;
  protected examen = new Examen();
  protected reactivoList: ExamenReactivoRespuestasArray[] = [];

  protected submitting = false;
  protected presentando = false;
  protected examenTerminado = false;
  protected mostrarMsgFinal = false;

  protected indice = 0;
  protected progreso = 0;
  private porcentajePorPregunta = 0;
  protected segundosRestantes = 0;
  protected lastMinute = false;
  private interval: NodeJS.Timer;

  constructor(
    private examenService: ExamenService,
    private examenReactivoService: ExamenReactivoService,
    private route: ActivatedRoute,
    private router: Router,
    private alertController: AlertController,
    private modalController: ModalController,
    private fechaService: FechaService

  ) { 
    addIcons({
      'calendar': '/assets/img/svg/calendar.svg',
      'clock': '/assets/img/svg/clock-2.svg',
      'file-check': '/assets/img/svg/file-check-2.svg'
    })
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.idExamen = params.get('id');
      if(this.idExamen > 0){
        this.consultarExamen(this.idExamen);
      }
    });

    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
  }

  onExit(){
    if(!this.examenTerminado && this.presentando){
      const rta  = this.presentAlertSalir();
      rta.then((rol: boolean)=> {
        if(rol == true){
          this.terminarExamen();
        }
      })
      return rta;
    }

    return true;
  };

  async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: "custom-alert-loading",
      backdropDismiss: false
    })
    return await this.loading.present();
  }

  async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }

  private consultarExamen(idExamen: number) {
    this.cargandoSubject.next(true);
    this.examenService.consultarMiExamen(idExamen).pipe(
      map((data) => {
        data.fechaExamen = this.fechaService.fechaUTCAFechaLocal(data.fechaExamen);
        return data;
      })
    ).subscribe({
      next: (data)=> {
        this.examen = data;
        this.porcentajePorPregunta = 1/data.totalPreguntas;
        this.progreso = this.porcentajePorPregunta;
        this.segundosRestantes = this.examen.duracion * 60;
      },
      error: ()=>{
        this.cargandoSubject.next(false);
      },
      complete: ()=> {
        this.cargandoSubject.next(false);
      }
    })
  }

  consultarReactivos() {
    this.examenReactivoService.consultarReactivosExamen(this.examen.idExamen).subscribe({
      next: (data) => {
        this.reactivoList = data.map(reactivo => ({
          ...reactivo,
          respuestasSeparadas: reactivo.respuestas,
          respuestaSeleccionadaIndex: undefined
        }));
        if((this.examen.totalPreguntas != this.reactivoList.length) || this.reactivoList.length == 0){
          this.cargandoSubject.next(false);
        }
      },
      error: ()=> {
        this.cargandoSubject.next(false);
        this.presentAlertError();
      },
      complete: ()=> {
        this.cargandoSubject.next(false);
        this.iniciarTimer();
        this.presentando = true;
      }
    })
  }

  //TODO: Temporal mientras se migra el sistema de roadis y se estandariza lo de las respuestas
  private separarRespuestas(StringRespuestas: string) {

    // Dividir el string utilizando la expresión regular que busca números seguidos de paréntesis
    const respuestasArray = StringRespuestas.split(/(?=\d\))/).map(str => str.trim());
    return respuestasArray;
  }

  protected seleccionarRespuesta(preguntaIndex: number, respuestaIndex: number , respuesta : Respuesta){
    this.reactivoList[preguntaIndex].respuestaAlumno = respuesta.clave; //asignar respuesta
    this.reactivoList[preguntaIndex].respuestaSeleccionadaIndex = respuestaIndex; //marcar item como seleccionado
    this.reactivoList[preguntaIndex].respuestaValor = respuesta.valor;
  }

  protected hayRespuestaSeleccionada(preguntaIndex: number): boolean{
    if((this.reactivoList[preguntaIndex].respuestaSeleccionadaIndex != undefined) && this.reactivoList[preguntaIndex].respuestaAlumno != ""){
      return true
    }
     return false;
  }

  protected iniciarExamen(){
    this.cargandoSubject.next(true);
    this.consultarReactivos()
  }

  private terminarExamen() {
    if(this.examenTerminado){
      return;
    }
    this.cargandoSubject.next(true);
    clearInterval(this.interval);
    this.submitting = true;

    this.examenReactivoService.revisar(this.reactivoList).subscribe({
      next: (data)=> {
        this.examen.resultado = data;
      },
      error: ()=> {
        this.cargandoSubject.next(false);
      },
      complete: ()=>{
        this.submitting = false;
        this.examenTerminado = true;
        this.cargandoSubject.next(false);
        this.presentAlertSuccess();
      }
    });
  }

  protected siguiente() {
    if((this.indice + 1) == this.reactivoList.length){
      this.mostrarMsgFinal = true;
      this.progreso = 1;
      return;
    }

    this.indice ++;
    this.progreso = ((this.indice + 1) * this.porcentajePorPregunta);
  }

  protected anterior() {
    if(this.indice == 0){
      return;
    }
    if(this.mostrarMsgFinal){
      this.progreso = ((this.indice + 1) * this.porcentajePorPregunta);
      this.mostrarMsgFinal = false;
      return;
    }

    this.indice--;
    this.progreso = ((this.indice + 1) * this.porcentajePorPregunta);
  }

  private iniciarTimer(){
    this.interval = setInterval(() => this.updateTimer(), 1000);
    setTimeout(() => this.terminarExamen(), this.segundosRestantes * 1000);
  }

  private updateTimer() {
    this.segundosRestantes--;
    if (this.segundosRestantes == 0) {
      clearInterval(this.interval);
    }
    else if (this.segundosRestantes < 60 && !this.lastMinute) {
      this.lastMinute = true;
      this.presentAlertTiempo();
    }
  }

  
  protected preguntaTieneImagen(imagen: string): boolean {
    if((imagen !== "") && (imagen !== "data:;base64,") && (imagen != null)){
      return true
    }
    return false
  }

  protected async VerImagen(imagen: string){
    const modal = await this.modalController.create({
      component: ImageOnlyModalComponent,
      cssClass: 'image-only-modal',
      componentProps: {archivo: imagen}
    })
    if((imagen !== "") && (imagen !== "data:;base64,") && (imagen != null)){
      modal.present();
    }
  }

  protected cerrar(): void {
    this.router.navigateByUrl('/home/cuestionarios/misCuestionarios');
    this.examenService.actualizarListadoExamenes();
  }

  private async presentAlertSalir(): Promise<boolean> {
    const alert = await this.alertController.create({
      header: '¿Está seguro(a) que desea salir?',
      subHeader: 'El cuestionario se enviará incompleto',
      cssClass: 'custom-alert color-error icon-info two-buttons',
      backdropDismiss: false,
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: () => {
            return false;
          }
        },
        {
          text: 'Salir',
          role: 'confirm',
          handler: () => {
            return true
          }
        }
      ],
    });
    await alert.present();
    
    const data = await alert.onWillDismiss();
    
    return data.role === 'cancel' ? false : true;
  }

  private async presentAlertError(){
    const alert = await this.alertController.create({
      header: 'Ha ocurrido un error',
      subHeader: 'No fue posible consultar este cuestionario',
      cssClass: 'custom-alert color-error icon-info',
      buttons: [{
        text: 'Ok',
        role: 'confirm',
        handler: () => {
          this.cerrar();
        }
      }]
    });

    alert.present();
  }

  private async presentAlertTiempo() {
    const alert = await this.alertController.create({
      header: "Advertencia",
      subHeader: "Sólo queda 1 minuto para responder el examen",
      cssClass: 'custom-alert color-error icon-info',
      buttons: ['Ok'],
    });
    await alert.present();
  }

  private async presentAlertSuccess(){
    const alert = await this.alertController.create({
      header: '¡Cuestionario enviado exitosamente!',
      cssClass: 'custom-alert color-primary icon-check',
      buttons: [{
        text: 'Cerrar',
        role: 'confirm',
        handler: ()=> {
          this.cerrar();
        }
      }]
    })

    await alert.present()
  }

  protected async presentAlertTerminar() {
    const alert = await this.alertController.create({
      header: '¿Envíar cuestionario?',
      subHeader: '¿Seguro(a) que desea terminar y enviar el cuestionario?',
      cssClass: 'custom-alert color-primary icon-info two-buttons',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel'
        },
        {
          text: 'Enviar',
          role: 'confirm',
          handler: () => {
            this.terminarExamen();
          }
        }
      ],
    });
    await alert.present();
  }
}
