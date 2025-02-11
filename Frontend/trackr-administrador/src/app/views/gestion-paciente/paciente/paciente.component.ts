import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { Component, OnInit, ViewChild } from '@angular/core';
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
import { MatDialog } from '@angular/material/dialog';
import { CustomAlertComponent } from '@sharedComponents/custom-alert/custom-alert.component';
import { CustomAlertData } from '@sharedComponents/interface/custom-alert-data';
import { FechaService } from '@services/fecha.service';
import { BsDatepickerDirective } from 'ngx-bootstrap/datepicker';

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

  protected ordenFiltro: 'ascendente' |'descendente' = 'ascendente';
  protected filtradoPorFecha: boolean = false;
  protected fechaFiltro: Date;
  @ViewChild('dp', { static: false }) datepicker: BsDatepickerDirective;
  fechaSeleccionada: Date;


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
  protected valoresFueraRangoFiltrados: ValoresFueraRangoGridDTO[];
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
    private spinnerService  : LoadingSpinnerService,
    private dialog: MatDialog,
    private fechaService: FechaService
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
    this.filtradoPorFecha = false;
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
    this.presentAlertEliminarPaciente(paciente);
  }

  private presentAlertEliminarPaciente(paciente: UsuarioExpedienteGridDTO){
    const alert = this.dialog.open(CustomAlertComponent, {
      panelClass: 'custom-alert',
      data:{
        header: 'Eliminar paciente',
        subHeader: '¿Seguro(a) que desea eliminar este paciente?',
        Icono: 'info',
        Color: 'error',
        twoButtons: true,
        cancelButtonText: 'No, cancelar',
        confirmButtonText: "Si, aceptar"
      } as CustomAlertData,
      autoFocus: false,
      restoreFocus: false,
    });

    alert.beforeClosed().subscribe(result => {
      var expedienteDoctorDto = {
        idExpediente: paciente.idExpedienteTrackr,
      } as UsuarioDoctorDto;

      if(result == "confirm"){
        this.misDoctoresService.eliminar(expedienteDoctorDto).subscribe(() => {
          this.consultarPacientes();
        });
      }
    })
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
        valoresFueraRango.map(data => {
          data.fechaHora = this.fechaService.fechaUTCAFechaLocal(data.fechaHora);
          return data;
        })
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

  protected cambiarFiltroOrden(){
    this.ordenFiltro === 'ascendente' ? (this.ordenFiltro = 'descendente') : (this.ordenFiltro = 'ascendente');
    this.ordenarlista(this.ordenFiltro);
  }

  protected ordenarlista(opcion: 'ascendente' | 'descendente'){
    this.filtradoPorFecha = false;
    this.valoresFueraRango?.sort((a, b) => {
      const fechaA = new Date(a.fechaHora).getTime();
      const fechaB = new Date(b.fechaHora).getTime();

      if (opcion === 'ascendente') {
        return fechaA - fechaB;
      }
      if(opcion === 'descendente') {
        return fechaB - fechaA;
      }

      return 0;
    });
  }

  protected filtrarPorFecha(){
    this.filtradoPorFecha = true;
    const fechaString = this.fechaService.fechaUTCAFechaLocal(new Date(this.fechaFiltro).toISOString());
    const targetDate = fechaString?.split('T')[0]; // Solo toma la parte de la fecha, ignorando la hora
    const coincidencias = this.valoresFueraRango?.filter(obj => obj.fechaHora?.split('T')[0] === targetDate);
    this.valoresFueraRangoFiltrados = coincidencias;
  }

  openDatepicker() {
    this.datepicker.show();
  }

}
