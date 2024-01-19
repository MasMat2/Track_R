import { Component, Input, OnInit } from '@angular/core';
import { AsistenteDoctorDto } from '@dtos/seguridad/asistente-doctor-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { Observable} from 'rxjs';


@Component({
  selector: 'app-gestion-asistente',
  templateUrl: './gestion-asistente.component.html',
  styleUrls: ['./gestion-asistente.component.scss']
})
export class GestionAsistenteComponent implements OnInit {
  public idUsuario : number;
  protected asistentes : Observable<Usuario[]>;
  protected auxiliares : Usuario[];
  protected misAsistentesData : Observable<AsistenteDoctorDto[]>;
  protected misAsistentesArray : AsistenteDoctorDto[];
  protected esAsistente : boolean;
  protected titulo : string;

  constructor(private usuarioService : UsuarioService, 
              private mensajeService : MensajeService) { }

  ngOnInit() {
    this.esAsistente ? this.misDoctores() : this.misAsistentes();
    this.titulo = this.esAsistente ? 'Mis doctores' : 'Auxiliares';
    this.consultarAsistentes();
  }

  private async consultarAsistentes()
  {
 
    this.usuarioService.consultarAsistentes().subscribe((data) => {
      this.auxiliares = data;
    });

  }

  private async misAsistentes()
  {
    this.misAsistentesData = this.usuarioService.misAsistentes();
    this.usuarioService.misAsistentes().subscribe((data) => {
      this.misAsistentesArray = data;
    });
  }

  private async misDoctores()
  {
    this.usuarioService.misDoctores().subscribe((data) => {
      this.misAsistentesArray = data;
    });
  }

  public async agregarAsistente(asistente : Usuario)
  {
    this.misAsistentesData.subscribe((data) => {
      if (data.find((asistenteDoctor) => asistenteDoctor.idUsuario == asistente.idUsuario)) {
        this.presentarMensajeError();
      } else {
        this.usuarioService.agregarAsistente(asistente.idUsuario).subscribe(() => {
          this.misAsistentes();
        });
      }
    });
  }

  public async eliminarAsistente(asistente : AsistenteDoctorDto)
  {
    this.usuarioService.eliminarAsistente(asistente.idAsistenteDoctor).subscribe(() => {
      this.misAsistentes();
    });
  }

  private presentarMensajeError() {
    this.mensajeService.modalError('El asistente ya se encuentra agregado');
  }
}
