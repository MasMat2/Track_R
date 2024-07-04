import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { ExpedienteEstudioService } from '@services/expediente-estudio.service';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { TableModule } from 'primeng/table';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ExpedienteEstudio } from 'src/app/shared/dtos/expediente-estudio-dto';
import { ExpedienteEstudioGridDTO } from 'src/app/shared/dtos/expediente-estudio-grid-dto';
import { MisEstudiosFormularioPage } from './mis-estudios-formulario/mis-estudios-formulario.component';
import { addIcons } from 'ionicons';
import { SearchbarComponent } from '@sharedComponents/searchbar/searchbar.component';
import { ArchivoPrevisualizarComponent } from '@sharedComponents/archivo-previsualizar/archivo-previsualizar.component';

@Component({
  selector: 'app-mis-estudios',
  templateUrl: './mis-estudios.component.html',
  styleUrls: ['./mis-estudios.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    TableModule,
    HeaderComponent,
    NgxExtendedPdfViewerModule,
    RouterModule,
    SearchbarComponent
  ],
})

export class MisEstudiosPage implements OnInit {
  protected expedientes$: Observable<ExpedienteEstudioGridDTO[]>;
  protected misEstudios: ExpedienteEstudioGridDTO[];
  protected filtrando: boolean = false;
  protected estudiosFiltradosPorBusqueda: ExpedienteEstudioGridDTO[];

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading : any;

  constructor(
    private expedienteEstudioService: ExpedienteEstudioService,
    private modalController: ModalController,
    private alertController: AlertController,
  ) { 
    addIcons({
      'plus': 'assets/img/svg/plus.svg',
      'chevron-left': 'assets/img/svg/chevron-left.svg',
      'trash': 'assets/img/svg/trash-2.svg',
      'chevron-right': 'assets/img/svg/chevron-right.svg',
    })
  }

  ngOnInit(): void {
    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });

    this.consultarEstudios();
  }

  private async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: "custom-alert-loading",
      backdropDismiss: false,
    })
    return await this.loading.present();
  }

  private async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }

  private consultarEstudios(): void {
    this.expedientes$ = this.expedienteEstudioService.consultarParaGrid().pipe(
      tap((estudios => {
        this.misEstudios = estudios;
      }))
    );
  }

  protected async abrirEstudio(estudio: ExpedienteEstudioGridDTO){
    this.abrirModal(ArchivoPrevisualizarComponent, {
      fileSource: 'url',
      urlArchivo: estudio.urlArchivo,
      mostrarTitulo: true,
      titulo: estudio.nombre
    })
  }

  protected eliminar(expedienteEstudio: ExpedienteEstudioGridDTO) {
    this.cargandoSubject.next(true);
    this.expedienteEstudioService.eliminar(expedienteEstudio.idExpedienteEstudio).subscribe({
        next: ()=> {
          this.consultarEstudios();
        },
        error: () => {
          this.cargandoSubject.next(false);
        },
        complete : ()=> {
          this.cargandoSubject.next(false);
          this.presentarAlertaEliminadoExitosamente();
        }
      });
  }

  protected async abrirModal(component: any, componentProps: any) {
    const modal = await this.modalController.create({
      component: component,
      componentProps: componentProps,
    });

    await modal.present();
  }

  protected listaEstudiosVacia(){
    return this.misEstudios?.length <= 0;
  }

  protected async agregarEstudio(){
    const modal = await this.modalController.create({
      component: MisEstudiosFormularioPage,
    });
    //cuando se cierre el modal la lista de estudios ya estará actualizada
    modal.onWillDismiss().then(() => {
      this.consultarEstudios();
    })

    await modal.present();
  }

  protected async presentarAlertaEliminar(expediente: ExpedienteEstudioGridDTO) {
    const alert = await this.alertController.create({
      header: '¿Seguro que deseas eliminar este elemento?',
      subHeader: 'No podrás recuperarlo',
      cssClass: 'custom-alert color-error icon-trash two-buttons',
      buttons: [
        {
          text: 'No, regresar',
          role: 'cancel',
        },
        {
          text: 'Sí, eliminar',
          role: 'confirm',
          handler: ()=> {
            this.eliminar(expediente);
          }
          },
      ]
    });

    await alert.present();
  }

  protected async presentarAlertaEliminadoExitosamente() {
    const alertSuccess = await this.alertController.create({
      header: 'Elemento eliminado exitosamente',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm'
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

  private formatDate(date: Date, monthFormat: '2-digit' | 'long'): string {
    return date?.toLocaleDateString('es-MX', {
      day: '2-digit',
      month: monthFormat,
      year: 'numeric',
    });
  }

  protected handleSearch(searchTerm: string): void {
    if( searchTerm == ""){
      this.filtrando = false;
      return
    }
    else{
      this.filtrando = true;

      //filtrar por nombre de estudio o por fecha(Filtra por ambos formatos '01/01/2024' y '01 de enero 2024')
      this.estudiosFiltradosPorBusqueda = this.misEstudios.filter(estudio => {
        const nombreMatch = estudio.nombre?.toLowerCase().includes(searchTerm.toLowerCase());
        const fechaString2Digit = estudio.fechaRealizacion ? this.formatDate(new Date(estudio?.fechaRealizacion), '2-digit') : '';
        const fechaStringLong = estudio.fechaRealizacion ? this.formatDate(new Date(estudio?.fechaRealizacion), 'long') : '';
        const fechaMatch = (fechaString2Digit.includes(searchTerm.toLowerCase()) || fechaStringLong.includes(searchTerm.toLowerCase()));
        return nombreMatch || fechaMatch;
      }
      );
    }
  }

}
