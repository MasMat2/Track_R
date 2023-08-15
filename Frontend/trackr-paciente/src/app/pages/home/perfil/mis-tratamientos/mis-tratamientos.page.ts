import { Component, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { Observable, map } from 'rxjs';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { RouterModule } from '@angular/router';

interface Tratamiento extends PerfilTratamientoDto {
  expandido: boolean;
}

@Component({
  selector: 'app-mis-tratamientos',
  templateUrl: './mis-tratamientos.page.html',
  styleUrls: ['./mis-tratamientos.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule, HeaderComponent],
})
export class MisTratamientosPage implements OnInit {

  protected tratamientos$: Observable<Tratamiento[]>;
  protected weekDays: string[] = ['Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá', 'Do'];

  constructor(
    private perfilTratamientoService: PerfilTratamientoService
  ) { }

  public ngOnInit(): void {
    this.consultarTratamientos();
  }

  protected consultarTratamientos(): void {
    this.tratamientos$ = this.perfilTratamientoService.consultarTratamientos().pipe(
      map(tratamientos =>
        tratamientos.map(tratamiento => ({
          ...tratamiento,
          expandido: false
        }))
      )
    );
  }

  public toggleExpandido(tratamiento: Tratamiento) {
    tratamiento.expandido = !tratamiento.expandido;
  }

}
