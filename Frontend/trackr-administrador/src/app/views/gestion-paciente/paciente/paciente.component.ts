import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { EncryptionService } from '@services/encryption.service';
import { GeneralConstant } from '@utils/general-constant';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrls: ['./paciente.component.scss']
})
export class PacienteComponent implements OnInit {
  protected pacientes: UsuarioExpedienteGridDTO[] = [];
  protected isVistaCuadricula: boolean = true;
  protected patologias: string[] = [];

  // App Grid View
  public HEADER_GRID = 'Pacientes';
  public columnaEditar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columnaEliminar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_ELIMINAR,
      title: 'Eliminar',
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );


  public columns = [
    { headerName: 'Paciente', field: 'nombreCompleto', minWidth: 150 },
    this.columnaEditar,
    this.columnaEliminar
  ];
  constructor(
    private router: Router,
    private encryptionService: EncryptionService,
    private usuarioService: UsuarioService,
    private expedienteTrackrService: ExpedienteTrackrService
  ) { }

  ngOnInit(): void {
    this.consultarPacientes();
  }

  protected descargarExcel(): void {
  }

  protected ver(gridData: { accion: string; event: UsuarioExpedienteGridDTO }): void {

  }

  /**
   * Redirige a la pantalla de Expediente - Formulario en modo Editar
   * debido a que se pasa un idUsuario como parametro en la URL
   * @param idUsuario del usuario seleccionado
   */
  protected editar(idUsuario: any): void {
    this.router.navigate(['/administrador/gestion-paciente/paciente/expediente-formulario'], {
      queryParams: this.encryptionService.generateURL({
        i: idUsuario.toString(),
      })
    });
  }

  /**
   * Redirige a la pantalla de Expediente - Formulario
   * debido a que no se pasa ningun parametro en la URL
   */
  protected agregar(): void {
    this.router.navigate(['/administrador/gestion-paciente/paciente/expediente-formulario']);
  }

  protected eliminar(data: any): void {
    this.expedienteTrackrService.eliminar(data.idExpediente)
  }

  /**
   * Consulta los pacientes para mostrar en el Grid de la tabla Usuarios,
   * aquellos con clave de perfil PACIENTE.
   */
  protected consultarPacientes(): void {
    lastValueFrom(this.expedienteTrackrService.consultarParaGrid())
    .then((pacientes: UsuarioExpedienteGridDTO[]) => {
      this.pacientes = pacientes;
    });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: any }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idUsuario);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    }
  }

}
