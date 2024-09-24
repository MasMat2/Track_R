import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ExamenReactivoService } from '@http/cuestionarios/examen-reactivo.service';
import { ExamenService } from '@http/cuestionarios/examen.service';
import { AlertController, IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';
import { Examen } from '@models/examen/examen';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { Respuesta } from '@models/examen/respuesta';
import { ImageOnlyModalComponent } from '@sharedComponents/image-only-modal/image-only-modal.component';
import { addIcons } from 'ionicons';
import { BehaviorSubject, map, tap } from 'rxjs';

interface ExamenReactivoRespuestasArray extends ExamenReactivo {
  respuestasSeparadas: string[];
}

@Component({
  selector: 'app-ver-cuestionario',
  templateUrl: './ver-cuestionario.component.html',
  styleUrls: ['./ver-cuestionario.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    RouterModule,
  ],
})
export class VerCuestionarioComponent implements OnInit {
  private idExamen: any;
  private examen = new Examen();
  protected tipoExamen: string;
  protected fechaExamen: string;
  protected reactivoList: ExamenReactivo[] = [];

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading: any;

  constructor(
    private route: ActivatedRoute,
    private examenService: ExamenService,
    private examenReactivoService: ExamenReactivoService,
    private router: Router,
    private modalController: ModalController,
    private alertController: AlertController
  ) {
    addIcons({
      'external-link': 'assets/img/svg/external-link.svg',
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.idExamen = params.get('id');
    });
    this.cargando$.subscribe((cargando) => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
  }

  ionViewWillEnter() {
    if (this.idExamen > 0) {
      this.consultarExamen(this.idExamen);
    }
  }

  async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: 'custom-alert-loading',
      backdropDismiss: false,
    });
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
    this.examenService.consultarMiExamen(idExamen).subscribe((data) => {
      this.examen = data;
      this.tipoExamen = this.examen.tipoExamen;
      this.fechaExamen = new Date(this.examen.fechaExamen).toISOString();
      this.consultarReactivos();
    });
  }

  private consultarReactivos() {
    this.examenReactivoService
      .consultarReactivosExamen(this.examen.idExamen)
      .subscribe({
        next: (data) => {
          this.reactivoList = data;
        },
        error: () => {
          this.cargandoSubject.next(false);
        },
        complete: () => {
          this.cargandoSubject.next(false);
        },
      });
  }

  protected async VerImagen(imagen: string) {
    const modal = await this.modalController.create({
      component: ImageOnlyModalComponent,
      cssClass: 'image-only-modal',
      componentProps: { archivoBase64: imagen },
    });
    if (imagen !== '' && imagen !== 'data:;base64,' && imagen != null) {
      modal.present();
    }
  }

  protected preguntaTieneImagen(imagen: string): boolean {
    if (imagen !== '' && imagen !== 'data:;base64,' && imagen != null) {
      return true;
    }
    return false;
  }

  protected regresar(): void {
    this.router.navigate(['/home/cuestionarios/misCuestionarios'], {});
  }

  //TODO: Temporal mientras se migra el sistema de roadis y se estandariza lo de las respuestas
  private separarRespuestas(StringRespuestas: string) {
    // Dividir el string utilizando la expresión regular que busca números seguidos de paréntesis
    const respuestasArray = StringRespuestas.split(/(?=\d\))/).map((str) =>
      str.trim()
    );
    return respuestasArray;
  }

  protected esRespuestaAlumno(
    respuesta: Respuesta,
    respuestaAlumno: string
  ): boolean {
    const match = respuesta.clave == respuestaAlumno;
    return match;
  }

  protected async presentAlertError() {
    const alert = await this.alertController.create({
      header: 'Ha ocurrido un error',
      subHeader: 'No fue posible consultar este cuestionario',
      cssClass: 'custom-alert color-error icon-info',
      buttons: [
        {
          text: 'Ok',
          role: 'confirm',
          handler: () => {
            this.regresar();
          },
        },
      ],
    });

    alert.present();
  }
}
