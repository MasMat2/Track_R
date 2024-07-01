import { Component, Input, OnInit } from '@angular/core';
import { AsistenteDoctorDto } from '@dtos/seguridad/asistente-doctor-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { LoadingSpinnerService } from '@services/loading-spinner.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { Observable, firstValueFrom, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-gestion-asistente',
  templateUrl: './gestion-asistente.component.html',
  styleUrls: ['./gestion-asistente.component.scss'],
})
export class GestionAsistenteComponent implements OnInit {
  public idUsuario: number;
  protected asistentes: Observable<Usuario[]>;
  protected cargando: boolean = false;

  //Asistentes disponibles
  protected auxiliaresFiltrados: Usuario[];
  protected auxiliares: Usuario[];
  protected asistentesSelecionados: Usuario[] = [];

  //Mis Asistentes
  protected misAsistentesArray: AsistenteDoctorDto[];
  protected misAsistentesFiltrados: AsistenteDoctorDto[];
  protected misAsistentesSelecionados: AsistenteDoctorDto[];

  protected esAsistente: boolean;
  protected titulo: string;

  constructor(
    private usuarioService: UsuarioService,
    private mensajeService: MensajeService,
    private spinnerService: LoadingSpinnerService
  ) {}

  async ngOnInit() {
    await this.cargarAsistentesYDoctores();
    this.titulo = this.esAsistente ? 'Mis doctores' : 'Auxiliares';
  }

  private async cargarAsistentesYDoctores() {
    this.spinnerService.openSpinner();
    const doctorOrAssistantPromise = this.esAsistente
      ? this.misDoctores()
      : this.misAsistentes();
    const consultarPromise = this.consultarAsistentes();
    await Promise.all([doctorOrAssistantPromise, consultarPromise]);
    this.spinnerService.closeSpinner();
  }

  private async consultarAsistentes() {
    const data = await firstValueFrom(
      this.usuarioService.consultarAsistentes()
    );
    this.auxiliares = data;
    this.auxiliaresFiltrados = data;
  }

  private async misAsistentes() {
    const data = await firstValueFrom(this.usuarioService.misAsistentes());
    this.misAsistentesArray = data;
    this.misAsistentesFiltrados = data;
  }

  private async misDoctores() {
    const data = await firstValueFrom(this.usuarioService.misDoctores());
    this.misAsistentesArray = data;
    this.misAsistentesFiltrados = data;
  }

  public async agregarAsistentes() {
    this.cargando = true;
    this.usuarioService
      .agregarAsistente(
        this.asistentesSelecionados.map((asistente) => asistente.idUsuario)
      )
      .subscribe(async () => {
        await this.cargarAsistentesYDoctores();
        this.asistentesSelecionados = [];
        this.cargando = false;
      });
  }

  public async eliminarAsistente() {
    this.cargando = true;
    this.usuarioService
      .eliminarAsistente(
        this.misAsistentesSelecionados.map(
          (asistente) => asistente.idAsistenteDoctor
        )
      )
      .subscribe(async () => {
        await this.cargarAsistentesYDoctores();
        this.misAsistentesSelecionados = [];
        this.cargando = false;
      });
  }

  protected filtrarAsistentesDisponibles(event: any) {
    const text = event.target.value;
    this.auxiliaresFiltrados = this.auxiliares;
    if (text && text.trim() != '') {
      this.auxiliaresFiltrados = this.auxiliaresFiltrados.filter(
        (paciente: Usuario) => {
          return (
            paciente.nombreCompleto.toLowerCase().indexOf(text.toLowerCase()) >
            -1
          );
        }
      );
    }
  }
  protected filtrarMisAsistentes(event: any) {
    const text = event.target.value;
    this.misAsistentesFiltrados = this.misAsistentesArray;
    if (text && text.trim() != '') {
      this.misAsistentesFiltrados = this.misAsistentesFiltrados.filter(
        (paciente: AsistenteDoctorDto) => {
          return (
            paciente.nombreAsistente.toLowerCase().indexOf(text.toLowerCase()) >
            -1
          );
        }
      );
    }
  }
}
