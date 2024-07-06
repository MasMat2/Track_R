import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { EncryptionService } from '@services/encryption.service';
import { GeneralConstant } from '@utils/general-constant';
import { lastValueFrom } from 'rxjs';

import { UsuarioExpedienteSidebarDTO } from '@dtos/seguridad/usuario-expediente-sidebar-dto';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { ValoresFueraRangoGridDTO } from '@dtos/gestion-expediente/valores-fuera-rango-grid-dto';
import { BsModalService } from 'ngx-bootstrap/modal';
import { GestionAsistenteComponent } from './gestion-asistente/gestion-asistente.component';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { MisDoctoresService } from '@http/seguridad/mis-doctores.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { UsuarioDoctorDto } from '@dtos/seguridad/usuario-doctor-dto';
import { Usuario } from '@models/seguridad/usuario';
import { LoadingSpinnerService } from '../../../shared/services/loading-spinner.service';

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrls: ['./paciente.component.scss'],
})
export class PacienteComponent implements OnInit {


  private TITULO_MODAL_ELIMINAR = 'Eliminar usuario';
  private MENSAJE_EXITO_ELIMINAR = 'Expediente eliminado correctamente';

  protected pacientes: UsuarioExpedienteGridDTO[] = [];
  protected pacientesFiltrados: UsuarioExpedienteGridDTO[] = [];
  protected isVistaCuadricula: boolean = true;
  protected mostrarSidebar: boolean = false;
  protected esAsistente : boolean | null = null;
  anchoContenedor: string = '100%';
  paciente: UsuarioExpedienteSidebarDTO = {
    idUsuario: 0,
    nombreCompleto: '',
    genero: '',
    edad: '',
    padecimientos: [],
  };

  // App Grid View
  public HEADER_GRID = 'Pacientes';
  public columnaEditar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      cellRendererSelector: (params: any) => {
        const component = {
          component: 'actionButton',
          params: { disabled: false },
        };
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
        const component = {
          component: 'actionButton',
          params: { disabled: false },
        };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  
  protected valoresFueraRango: ValoresFueraRangoGridDTO[];
  protected columnsVariableFueraRango = [
    { headerName: 'Campo', field: 'parametro', minWidth: 10, resizable : false},
    { headerName: 'Valor', field: 'valorRegistrado', maxWidth: 100, resizable : false },
  ];


  public columns = [
    { headerName: 'Paciente', field: 'nombreCompleto', minWidth: 150 },
    { headerName: 'Patologias',field: 'patologias', minWidth:150},
    { headerName: 'Dosis sin tomar',field: 'dosisNoTomadas', minWidth:150},
    { headerName: 'Variables Fuera de Rango',field: 'variablesFueraRango', minWidth:150},
    this.columnaEditar,
    this.columnaEliminar,
  ];
  constructor(
    private router: Router,
    private encryptionService: EncryptionService,
    private expedienteTrackrService: ExpedienteTrackrService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private modalService: BsModalService,
    private usuarioService : UsuarioService,
    private misDoctoresService : MisDoctoresService,
    private modalMensajeService : MensajeService,
    private spinnerService  : LoadingSpinnerService
  ) {}

  ngOnInit(): void {
    this.esAsistenteUsuario();
    this.consultarPacientes();
  }

  protected descargarExcel(): void {}

  protected ver(idUsuario: number): void {
    this.mostrarSidebar = true;

    this.consultarParaSidebar(idUsuario);
  }

  toggleSidebar() {
    this.mostrarSidebar = false;
  }

  protected consultarParaSidebar(idUsuario: number) {
    this.expedienteTrackrService
      .consultaParaSidebar(idUsuario)
      .subscribe((data) => {
        this.paciente = data;
        this.consultarValoresFueraRango(data.idUsuario);
      });
  }

  /**
   * Redirige a la pantalla de Expediente - Formulario en modo Editar
   * debido a que se pasa un idUsuario como parametro en la URL
   * @param idUsuario del usuario seleccionado
   */
  protected editar(idUsuario: any): void {
    this.router.navigate(
      ['/administrador/gestion-paciente/paciente/expediente-formulario'],
      {
        queryParams: this.encryptionService.generateURL({
          i: idUsuario.toString(),
        }),
      }
    );
  }

  /**
   * Redirige a la pantalla de Expediente - Formulario
   * debido a que no se pasa ningun parametro en la URL
   */
  protected agregar(): void {
    this.router.navigate([
      '/administrador/gestion-paciente/paciente/expediente-formulario',
    ]);
  }
  protected agregarAsistente(): void {
    const initialState = {
      esAsistente: this.esAsistente
    };
    const modalRef = this.modalService.show(
      GestionAsistenteComponent,
      {
        initialState,
        ...GeneralConstant.CONFIG_MODAL_SMALL_CUSTOM 
      }
    );
  }

  protected eliminar(paciente: UsuarioExpedienteGridDTO): void {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar el usuario <strong>' +
          paciente.nombreCompleto +
          '</strong>?',
        this.TITULO_MODAL_ELIMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((_: any) => {
        var expedienteDoctorDto = {
          idExpediente: paciente.idExpedienteTrackr,
        } as UsuarioDoctorDto;

        this.misDoctoresService.eliminar(expedienteDoctorDto).subscribe(() => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarPacientes();
        });
      });
  }

  /**
   * Consulta los pacientes para mostrar en el Grid de la tabla Usuarios,
   * aquellos con clave de perfil PACIENTE.
   */
  protected consultarPacientes(): void {
    this.spinnerService.openSpinner();
    lastValueFrom(this.expedienteTrackrService.consultarParaGrid()).then(
      (pacientes: UsuarioExpedienteGridDTO[]) => {

        this.pacientes = pacientes;
        this.pacientesFiltrados = pacientes;
        this.spinnerService.closeSpinner();
      }
    );
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  protected onGridClick(gridData: { accion: string; data: any }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar(gridData.data.idUsuario);
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminar(gridData.data);
    } else if ((gridData.accion = GeneralConstant.GRID_ACCION_VER)) {
      this.ver(gridData.data.idUsuario);
    }
  }

  public consultarValoresFueraRango(idUsuario : number): void {
    lastValueFrom(this.entidadEstructuraTablaValorService.consultarValoresFueraRangoUsuario(idUsuario))
      .then((valoresFueraRango: ValoresFueraRangoGridDTO[]) => {
        this.valoresFueraRango = valoresFueraRango;
      }
    );
  }

  private esAsistenteUsuario()
  {
    this.usuarioService.esAsistente().subscribe((esAsistente) => {
      this.esAsistente = esAsistente;
    });
  }

  protected filtrarPacientes(event: any){
    const text = event.target.value;
    this.pacientesFiltrados = this.pacientes;
    if(text && text.trim() != ''){
      this.pacientesFiltrados = this.pacientesFiltrados.filter((paciente: any) => {
        return (paciente.nombreCompleto.toLowerCase().indexOf(text.toLowerCase()) > -1);
      })
    }
  }

}
