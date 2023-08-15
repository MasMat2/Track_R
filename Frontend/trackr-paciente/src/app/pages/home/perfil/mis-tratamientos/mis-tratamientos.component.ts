import { Component, OnInit } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { Observable, map } from 'rxjs';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../../auth/auth.service';

interface Tratamiento extends PerfilTratamientoDto {
  expandido: boolean;
}

@Component({
  selector: 'app-mis-tratamientos',
  templateUrl: './mis-tratamientos.component.html',
  styleUrls: ['./mis-tratamientos.component.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, RouterModule, HeaderComponent],
})
export class MisTratamientosComponent implements OnInit {

  private idUsuario: number;
  protected tratamientos$: Observable<Tratamiento[]>;
  protected weekDays: string[] = ['Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá', 'Do'];

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    public modalController: ModalController,
    private authService: AuthService
  ) { }

  public ngOnInit(): void {
    this.consultarTratamientos();
  }

  protected consultarPorUsuario(): void {
    this.tratamientos$ = this.perfilTratamientoService.consultarTratamientos(this.idUsuario).pipe(
      map(tratamientos =>
        tratamientos.map(tratamiento => ({
          ...tratamiento,
          expandido: false
        }))
      )
    );
  }

  public toggleExpand(tratamiento: Tratamiento) {
    tratamiento.expandido = !tratamiento.expandido;
  }


  // Obtener usuario de Jwt
  public base64UrlDecode(str: string) {
    str = str.replace('-', '+').replace('_', '/');
    while (str.length % 4) {
      str += '=';
    }
    return atob(str);
  }

  public decodeJwt(jwtToken: string) {

    const parts = jwtToken.split('.');
    if (parts.length !== 3) {
      throw new Error('Not a valid JWT token');
    }

    const header = JSON.parse(this.base64UrlDecode(parts[0]));
    const payload = JSON.parse(this.base64UrlDecode(parts[1]));

    return { header, payload };
  }

  private async consultarTratamientos(): Promise<void> {
    const token = await this.authService.obtenerToken();
    if (token) {
      const decoded = this.decodeJwt(token);
      this.idUsuario = decoded.payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      console.log(this.idUsuario);
    }
    this.idUsuario = 5315;
    this.consultarPorUsuario();
  }

}
