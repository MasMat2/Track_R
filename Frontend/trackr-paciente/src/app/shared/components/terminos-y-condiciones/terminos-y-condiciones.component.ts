import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { ModalController } from '@ionic/angular/standalone';

@Component({
  selector: 'app-terminos-y-condiciones',
  templateUrl: './terminos-y-condiciones.component.html',
  styleUrls: ['./terminos-y-condiciones.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, RouterModule]
})
export class TerminosYCondicionesComponent  implements OnInit {


  protected readonly TERMINOS_Y_CONDICIONES: string = `
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
    `
  ;

  constructor(private modalCtrl: ModalController) { }

  ngOnInit() {}

  cerrarModal() {
    return this.modalCtrl.dismiss(null, 'cancel');
  }

}
