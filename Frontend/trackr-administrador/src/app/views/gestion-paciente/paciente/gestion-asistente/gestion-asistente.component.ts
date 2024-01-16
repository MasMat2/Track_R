import { Component, Input, OnInit } from '@angular/core';
import { AsistenteDoctorDto } from '@dtos/seguridad/asistente-doctor-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { Observable, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-gestion-asistente',
  templateUrl: './gestion-asistente.component.html',
  styleUrls: ['./gestion-asistente.component.scss']
})
export class GestionAsistenteComponent implements OnInit {
  public idUsuario : number;
  protected asistentes : Observable<Usuario[]>;
  protected misAsistentesData : Observable<AsistenteDoctorDto[]>;
  constructor(private usuarioService : UsuarioService) { }

  async ngOnInit() {
    await this.consultarAsistentes();
    await this.misAsistentes();
  }

  private async consultarAsistentes()
  {
    this.asistentes = this.usuarioService.consultarAsistentes();
  }

  private async misAsistentes()
  {
    this.misAsistentesData = this.usuarioService.misAsistentes();
  }

  public async agregarAsistente(asistente : Usuario)
  {
    this.usuarioService.agregarAsistente(asistente.idUsuario).subscribe( () => {
      this.misAsistentes();
    });
  }

  public async eliminarAsistente(asistente : AsistenteDoctorDto)
  {
    this.usuarioService.eliminarAsistente(asistente.idAsistenteDoctor).subscribe(() => {
      this.misAsistentes();
    });
  }

}
