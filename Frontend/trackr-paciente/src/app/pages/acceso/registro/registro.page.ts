import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { HospitalService } from '@http/catalogo/hospital.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { AlertController, CheckboxCustomEvent, IonicModule, ModalController } from '@ionic/angular';
import { Hospital } from '@models/catalogo/hospital';
import { NgSelectModule } from '@ng-select/ng-select';
import * as Utileria from '@utils/utileria';
import { UsuarioNuevoTrackrDto } from 'src/app/shared/Dtos/seguridad/usuario-nuevo-trackr-dto';
import { addIcons } from 'ionicons';
import { BehaviorSubject } from 'rxjs';
import { TerminosYCondicionesComponent } from '@sharedComponents/terminos-y-condiciones/terminos-y-condiciones.component';


@Component({
  selector: 'app-registro',
  templateUrl: './registro.page.html',
  styleUrls: ['./registro.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule, NgSelectModule],
})
export class RegistroPage implements OnInit {
  protected usuario = new UsuarioNuevoTrackrDto();
  protected fecha = new Date().toISOString();
  protected confirmarContrasena: string = '';
  protected submitting: boolean = false;
  protected termsAccepted: boolean = false;
  protected hospitalList : Hospital[] = [];
  protected idHospital : number;
  protected placeHolderSelectHospital : string = "Seleccione un hospital";
  protected placeHolderNoOptions : string = "No hay hospitales disponibles";
  protected pswInputType: string = "password";
  protected pswInputType2: string = "password";
  protected mostrarPwd: boolean = false;
  protected mostrarPwd2: boolean = false;
  protected procesoContinuado: boolean = false;
  protected parteProceso: '1' | '2' = '1';

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading : any;

  constructor(
    private usuarioService: UsuarioService,
    private router: Router,
    private alertController: AlertController,
    private modalController: ModalController,
    private hospitalService : HospitalService
  ) { addIcons({
    'eye': 'assets/img/svg/eye.svg',
    'eye-off': 'assets/img/svg/eye-off.svg',
    'calendar': 'assets/img/svg/calendar.svg',
    'x': 'assets/img/svg/x.svg',
    'check': 'assets/img/svg/check.svg'
  }) }


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

  public ngOnInit() {
    // this.hospitalService.consultarTodosParaSelector().subscribe(
    //   (data) => {
    //       this.hospitalList = data;
    //   }
    // );
    this.cargando$.subscribe(cargando => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
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
      cssClass: 'custom-alert color-primary icon-check',
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

  protected mostrarConfirmarContrasena(){
    if(this.mostrarPwd2 == true){
      this.mostrarPwd2 = false;
      this.pswInputType2 = "password";
    }
    else{
      this.mostrarPwd2 = true;
      this.pswInputType2 = "text";
    }
  }

  protected contrasenasCoincidenValidation(): boolean {
    if(this.usuario.contrasena != this.confirmarContrasena){
      return false;
    }
    else{
      return true;
    }
  }

  protected contrasenaMinimoCaracteresValidation(){
    const minCaracteres = 8;
    if(this.usuario.contrasena?.length < minCaracteres){
      return false;
    }
    else{
      return true;
    }
  }

  protected contrasenaNumerosYSimbolosValidation(){
    if (!/\d/.test(this.usuario.contrasena) || !/[!@#$%^&*(),.?":{}|<>]/.test(this.usuario.contrasena)) {
      return false;
    }
    else{
      return true;
    };
  }

  protected contrasenaMayusculasYMinusculasValidation(){
    if (!/[A-Z]/.test(this.usuario.contrasena) || !/[a-z]/.test(this.usuario.contrasena)){
      return false;
    }
    else{
      return true;
    }
  }



  protected continuarProceso(input: boolean){
    this.procesoContinuado = input;
    this.procesoContinuado == true ? this.parteProceso = "2" : this.parteProceso = "1";
    this.termsAccepted = this.procesoContinuado;
  }

  protected contrasenaValida(){
    return(
      this.contrasenasCoincidenValidation() &&
      this.contrasenaNumerosYSimbolosValidation() &&
      this.contrasenaMayusculasYMinusculasValidation() &&
      this.contrasenaMinimoCaracteresValidation()
    )
  }
}
