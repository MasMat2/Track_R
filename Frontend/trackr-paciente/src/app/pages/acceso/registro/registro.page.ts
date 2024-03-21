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
  protected spinner: string = Constants.ALERT_SPINNER;

  //Estado de "cargando" para mostrar el alert con spinner
  cargandoSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  cargando$ = this.cargandoSubject.asObservable();

  private readonly TERMINOS_Y_CONDICIONES: string = `
**Términos y Condiciones de Uso de la Aplicación Track.r**

Esta aplicación es propiedad de Christus Muguerza. Antes de utilizar esta aplicación, por favor lee detenidamente los siguientes Términos y Condiciones que rigen su uso. Al acceder y utilizar nuestra aplicación, aceptas cumplir con estos términos. Si no estás de acuerdo con alguno de estos términos, te recomendamos que no utilices la aplicación.
**1. Uso de la Aplicación:**
- La aplicación de seguimiento a pacientes hospitalarios tiene como objetivo facilitar el seguimiento y monitoreo de pacientes durante su estancia hospitalaria. No se debe utilizar con fines distintos a los establecidos por Christus Muguerza.
**2. Datos Personales:**
- Al utilizar nuestra aplicación, podríamos recopilar y procesar cierta información personal del paciente para mejorar la calidad de nuestros servicios. Nos comprometemos a proteger la privacidad y confidencialidad de estos datos de acuerdo con las leyes y regulaciones aplicables.
**3. Confidencialidad:**
- Toda la información proporcionada por los usuarios, incluida la información médica y personal, se manejará de manera confidencial. Christus Muguerza implementará medidas de seguridad para proteger estos datos contra accesos no autorizados.
**4. Responsabilidades del Usuario:**
- El usuario se compromete a proporcionar información precisa y actualizada al utilizar la aplicación. Asimismo, es responsable de mantener la confidencialidad de su información de acceso.
**5. Modificaciones y Actualizaciones:**
- Christus Muguerza se reserva el derecho de modificar, actualizar o descontinuar la aplicación en cualquier momento. También nos reservamos el derecho de modificar estos Términos y Condiciones, y cualquier cambio se hará efectivo al ser publicado en la aplicación.
**6. Limitación de Responsabilidad:**
- Christus Muguerza no se hace responsable de daños directos, indirectos, incidentales, especiales o consecuentes que surjan del uso de la aplicación, incluso si hemos sido advertidos de la posibilidad de tales daños.
**7. Ley Aplicable:**
- Estos Términos y Condiciones se rigen por las leyes vigentes en el lugar de operación de Christus Muguerza.
Al utilizar esta aplicación, aceptas estos Términos y Condiciones. Si tienes alguna pregunta o inquietud, contáctanos a través de los canales de atención proporcionados. ¡Gracias por confiar en Christus Muguerza!
`;

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
      message: Constants.ALERT_SUCCESS,
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

  //TODO: Obtener los terminos y condiciones (texto) de la base de datos
  protected async mostrarTerminosYCondiciones() {
    // const alert = await this.alertController.create({
    //   header: 'Terminos y Condiciones',
    //   message: this.TERMINOS_Y_CONDICIONES,
    //   buttons: [{
    //     text: 'De acuerdo',
    //   },]
    // });

    // await alert.present();

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
