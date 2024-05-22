import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { HospitalService } from '@http/catalogo/hospital.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { AlertController, CheckboxCustomEvent, IonicModule, ModalController } from '@ionic/angular';
import { Hospital } from '@models/catalogo/hospital';
import { Usuario } from '@models/usuario';
import { NgSelectModule } from '@ng-select/ng-select';
import * as Utileria from '@utils/utileria';
import { UsuarioNuevoTrackrDto } from 'src/app/shared/Dtos/seguridad/usuario-nuevo-trackr-dto';
import { addIcons } from 'ionicons';
import { chevronBack, eyeOutline, eyeOffOutline} from 'ionicons/icons';
import { Constants } from '@utils/constants/constants';
import { BehaviorSubject } from 'rxjs';
import { TerminosYCondicionesComponent } from './components/terminos-y-condiciones/terminos-y-condiciones.component';


@Component({
  selector: 'app-registro',
  templateUrl: './registro.page.html',
  styleUrls: ['./registro.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule, NgSelectModule],
})
export class RegistroPage implements OnInit {
  protected usuario = new UsuarioNuevoTrackrDto();
  protected confirmarContrasena: string = '';
  protected submitting: boolean = false;
  protected termsAccepted: boolean = false;
  protected hospitalList : Hospital[] = [];
  protected idHospital : number;
  protected placeHolderSelectHospital : string = "Seleccione un hospital";
  protected placeHolderNoOptions : string = "No hay hospitales disponibles";
  protected pswInputType: string = "password";
  protected mostrarPwd: boolean = false;
  protected procesoContinuado: boolean = false;
  protected parteProceso: string = '1';

  //Estado de "cargando" para mostrar el alert con spinner
  cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  cargando$ = this.cargandoSubject.asObservable();

  constructor(
    private usuarioService: UsuarioService,
    private router: Router,
    private alertController: AlertController,
    private modalController: ModalController,
    private hospitalService : HospitalService
  ) { addIcons({chevronBack, eyeOffOutline, eyeOutline}) }

  public ngOnInit() {
    this.hospitalService.consultarTodosParaSelector().subscribe(
      (data) => {
          this.hospitalList = data;
      }
    );
  }

  protected enviarFormulario(formulario: NgForm): void {
    this.submitting = true;
    this.cargandoSubject.next(true);

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    this.agregar();
  }

  protected changeEvent(event: Event) {
    const checkboxEvent = event as CheckboxCustomEvent;
    this.termsAccepted = checkboxEvent.target.checked;
  }

  private agregar(){
    this.usuario.nombreUsuario = this.usuario.correoPersonal;
    this.usuarioService.agregarTrackr(this.usuario).subscribe({
      next: () => {
      },
      error: ()=> {
        this.submitting = false;
        this.cargandoSubject.next(false);
      },
      complete: ()=> {
        this.submitting = false;
        this.cargandoSubject.next(false);
        this.presentAlertSuccess();
      }
    })
  }
  
  protected async presentAlertSuccess() {

    const alertSuccess = await this.alertController.create({
      header: '¡Listo, estás registrado!',
      subHeader: 'Se ha enviado un correo para verificar tu cuenta.',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.router.navigateByUrl('/acceso/login');
        }
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

  protected async mostrarTerminosYCondiciones() {

    const modal = await this.modalController.create({
      component: TerminosYCondicionesComponent,
    });

    modal.present();
  }

  protected mostrarContrasena(){
    if(this.mostrarPwd == true){
      this.mostrarPwd = false;
      this.pswInputType = "password";
    }
    else{
      this.mostrarPwd = true;
      this.pswInputType = "text";
    }
  }

  protected verificarContrasena(): boolean {
    if(this.usuario.contrasena != this.confirmarContrasena){
      return false;
    }

    return true;
  }

  protected continuarProceso(input: boolean){
    this.procesoContinuado = input;
    this.procesoContinuado == true ? this.parteProceso = "2" : this.parteProceso = "1";
    this.termsAccepted = this.procesoContinuado;
  }
}
