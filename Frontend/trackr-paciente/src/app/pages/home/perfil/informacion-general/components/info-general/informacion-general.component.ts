import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { AlertController, IonicModule } from '@ionic/angular';
import * as Utileria from '@utils/utileria';
import { BehaviorSubject, Observable, lastValueFrom, of, tap } from 'rxjs';
import { ColoniaSelectorDto } from 'src/app/shared/dtos/catalogo/colonia-selector-dto';
import { EstadoSelectorDto } from 'src/app/shared/dtos/catalogo/estado-selector-dto';
import { LocalidadSelectorDto } from 'src/app/shared/dtos/catalogo/localidad-selector-dto';
import { municipioSelectorDto } from 'src/app/shared/dtos/catalogo/municipio-selector-dto';
import { PaisSelectorDto } from 'src/app/shared/dtos/catalogo/pais-selector-dto';
import { InformacionGeneralDto } from 'src/app/shared/dtos/perfil/informacion-general-dto';
import { ConfirmacionCorreoService } from '@http/seguridad/confirmacion-correo.service';
import { ConfirmarCorreoDto } from '../../../../../../shared/Dtos/seguridad/confirmar-correo-dto';
import { GeneroService } from '@http/catalogo/genero.service'
import { addIcons } from 'ionicons';
import { OnExit } from 'src/app/shared/guards/exit.guard';
import { RouterModule } from '@angular/router';
import { GeneroSelectorDto } from '@dtos/catalogo/genero-selector-dto';
import { ChatPersonaService } from '@http/chat/chat-persona.service';

@Component({
  selector: 'app-informacion-general',
  templateUrl: './informacion-general.component.html',
  styleUrls: ['./informacion-general.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    IonicModule,
  ]
})
export class InformacionGeneralComponent implements OnInit , OnExit {

  protected informacionUsuario$: Observable<InformacionGeneralDto>;
  protected infoUsuario: InformacionGeneralDto;
  protected edadUsuario: string;
  protected submiting = false;
  protected emailsubmiting = false;
  protected generoList: GeneroSelectorDto[];

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading : any;

  @ViewChild('formulario') formulario: NgForm;

  constructor(
    private usuarioService: UsuarioService,
    private alertController: AlertController,
    private confirmacionCorreoService: ConfirmacionCorreoService,
    private generoService: GeneroService,
    private chatPersonaService : ChatPersonaService
  ) {
    addIcons({
      'chevron-down': 'assets/img/svg/chevron-down.svg',
      'calendar': 'assets/img/svg/calendar.svg',
    })
    }

  async ngOnInit(){
    await this.consultarGeneros();
    this.obtenerUsuario();
    
    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
  }

  onExit(){
    if(this.formulario.dirty){
      const rta  = this.presentAlertSalir();
      return rta;
    }

    return true;
  };

  async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: "custom-alert-loading"
    })
    return await this.loading.present();
  }

  async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }

  private async consultarGeneros() {
    this.generoList = await lastValueFrom(this.generoService.consultarGeneros());
  }

  private obtenerUsuario(){
    this.informacionUsuario$ = this.usuarioService.consultarInformacionGeneral().pipe(
      tap(
        (data) => {
          this.infoUsuario = data;
          this.calcularEdad();
        }
      )
    );
  }

  protected async enviarFormulario(formulario: NgForm){
    if(formulario.invalid){
      this.submiting = false;
      Utileria.validarCamposRequeridos(formulario);
      this.presentAlertError('Campos requeridos', 'Debe completar todos los campos obligatorios');
      return;
    }
    this.submiting = true;
    this.cargandoSubject.next(true);
    this.infoUsuario.correo = this.infoUsuario.correoPersonal;
    this.actualizarInformacionUsuario(this.infoUsuario);
  }

  private actualizarInformacionUsuario(informacion: InformacionGeneralDto){
    this.usuarioService.actualizarInformacionGeneral(informacion).subscribe({
      next: ()=> {},
      error: ()=> {
        this.submiting = false;
        this.cargandoSubject.next(false);
      }, 
      complete: ()=> {
        this.submiting = false;
        this.cargandoSubject.next(false);
        this.presentAlertSuccess('Información actualizada', 'La información se actualizó correctamente');
        this.formulario.form.markAsPristine();
      }
    });
  }

  protected calcularEdad(){
    const fechaNacimiento = this.infoUsuario.fechaNacimiento;
    let edadObject = Utileria.diferenciaFechas(new Date(fechaNacimiento), new Date());
    this.edadUsuario = edadObject.years + ' años ';
  }

  protected async reenviarConfirmacionCorreo(correoUsuario: string){
    this.emailsubmiting = true;
    const idUsuario =  await lastValueFrom(this.chatPersonaService.obtenerIdUsuario());
    const confirmarCorreo: ConfirmarCorreoDto = { correo: correoUsuario, token: "", idUsuario: idUsuario};
    this.confirmacionCorreoService.enviarCorreoConfirmacion(confirmarCorreo).subscribe({
      next: () => {
      },
      error: () => {

      },
      complete: () => {
        this.emailsubmiting = false;
      }
    });
  }
  
  private async presentAlertSuccess(header: string, subheader: string) {
    const alert = await this.alertController.create({
      header: header,
      subHeader: subheader,
      buttons: ['Ok'],
      cssClass: 'custom-alert color-primary icon-check'
    });

    await alert.present();
  }

  private async presentAlertError(header: string, subheader: string) {
    const alert = await this.alertController.create({
      header: header,
      message: subheader,
      buttons: ['Ok'],
      cssClass: 'custom-alert color-error icon-info'
    });

    await alert.present();
  }

  private async presentAlertSalir(): Promise<boolean> {
    const alert = await this.alertController.create({
      header: '¿Está seguro que desea salir?',
      subHeader: 'Perderá los cambios que no haya guardado',
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
          handler: () => {
            return true;
          }
        }
      ],
    });
    await alert.present();
    
    const data = await alert.onDidDismiss();
    return data.role === 'cancel' ? false : true;
  }

}
