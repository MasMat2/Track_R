import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { WidgetComponent } from '../widget/widget.component';
import { HealthkitService } from '@services/healthkit.service';
import { addIcons } from 'ionicons';

@Component({
  selector: 'app-widget-peso',
  templateUrl: './widget-peso.component.html',
  styleUrls: ['./widget-peso.component.scss'],
  standalone: true,
  
  imports: [
    WidgetComponent,
    IonicModule,
  ]
})
export class WidgetPesoComponent  implements OnInit {

  protected pesoPerdido: number = 12;
  protected pesoFaltante: number = 5;
  protected pesoActual: string = "0.0";
  protected unidadMedida: string = "kg";
  protected dias: number = 40;

  constructor(private healthKitService: HealthkitService) { 
    addIcons({
      'peso': '/assets/img/svg/Peso.svg'
    })
  }

  ngOnInit() {
    this.cargarDatosWeight();
  }

  async cargarDatosWeight() {
    try {
      const dataWeight = await this.healthKitService.getWeight();
      //Tomar el ultimo en el arreglo ya que el usuario puede ingresar varias medidas en un dia
      if (dataWeight.resultData.length > 0) {
        const ultimoElemento = dataWeight.resultData.pop(); 
        this.pesoActual = ultimoElemento!.value.toString();
      } else {
          this.pesoActual = '0.0';
      }
    
    } catch (error) {
      console.error('Error al obtener datos de peso:', error);
    }
  }

}
