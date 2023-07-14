import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { EncryptionService } from '@services/encryption.service';
import { GeneralConstant } from '@utils/general-constant';
import { lastValueFrom } from 'rxjs';

import { EstadoService } from '../../../shared/http/catalogo/estado.service';
import * as Utileria from '@utils/utileria';
import { ExpedienteTrackR } from '@models/seguridad/expediente-trackr';
import { ExpedientePadecimientoService } from '@http/seguridad/expediente-padecimiento.service';
import { UsuarioExpedienteSidebarDTO } from '@dtos/seguridad/usuario-expediente-sidebar-dto';

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrls: ['./paciente.component.scss'],
})
export class PacienteComponent implements OnInit {
  protected pacientes: UsuarioExpedienteGridDTO[] = [];
  protected isVistaCuadricula: boolean = true;
  protected patologias: string[] = [];
  protected mostrarSidebar: boolean = false;
  anchoContenedor: string = '100%';
  paciente: UsuarioExpedienteSidebarDTO = {
    idUsuario: 0,
    nombreCompleto: '',
    genero: '',
    edad: '',
    idestado: 0,
    estado: '',
    direccion: '',
    padecimientos: []
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

  public columns = [
    { headerName: 'Paciente', field: 'nombreCompleto', minWidth: 150 },
    this.columnaEditar,
    this.columnaEliminar,
  ];
  constructor(
    private router: Router,
    private encryptionService: EncryptionService,
    private expedienteTrackrService: ExpedienteTrackrService,
    private estadoService: EstadoService,
    private expedienteService: ExpedientePadecimientoService,
  ) {}

  ngOnInit(): void {
    this.consultarPacientes();
  }

  protected descargarExcel(): void {}

  protected ver(idUsuario: number): void {

    this.mostrarSidebar = !this.mostrarSidebar;
    this.limpiarZona();
    this.consultaExpediente(idUsuario);

  }

  protected consultaExpediente(idUsuario: number) {
    this.expedienteTrackrService.consultarWrapperPorUsuario(idUsuario).subscribe(
      (data) => {
        this.paciente.nombreCompleto = data.paciente.nombreCompleto;
        this.paciente.genero = data.expediente.idGenero === 1 ? 'Hombre' : 'Mujer';
        this.paciente.ciudad = data.paciente.ciudad;
        this.paciente.colonia = data.paciente.colonia;
        this.paciente.idestado = data.paciente.idEstado;
        this.paciente.tipoMime = data.paciente.imagenTipoMime;

        this.calcularEdad(data.expediente);
        this.paciente.edad = data.expediente.edad;

        this.consultaEstado(this.paciente.idestado);
        this.consultarPadecimientos(idUsuario);
      },
      (error) => {
        console.error('Error al consultar el expediente:', error);
      }
    );
  }

  protected calcularEdad(expediente: ExpedienteTrackR) {
    let fechaNacimiento = new Date(expediente.fechaNacimiento);
    let edadObject = Utileria.diferenciaFechas(fechaNacimiento, new Date());
    let edadString = edadObject.years + ' años ';
    expediente.edad = edadString;
  }

  protected consultaEstado(idEstado: number) {
    this.estadoService.consultar(idEstado).subscribe(
      (data) => {
        this.paciente.estado = data.nombre;
      },
      (error) => {
        console.error('Error al consultar', error);
      }
    );
  }


  protected consultarPadecimientos(idUsuario:number){
    this.expedienteService.consultaParaSidebar(idUsuario).subscribe(
      (data) => {
       this.paciente.padecimientos=data;
      },
      (error) => {
        console.error('Error al consultar el expediente:', error);
      }
    );
  }

  limpiarZona(): void {
    this.paciente= {
      idUsuario: 0,
      nombreCompleto: '',
      genero: '',
      edad: '',
      idestado: 0,
      estado: '',
      direccion: '',
      padecimientos: []
    };
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

  protected eliminar(data: any): void {
    this.expedienteTrackrService.eliminar(data.idExpediente);
  }

  /**
   * Consulta los pacientes para mostrar en el Grid de la tabla Usuarios,
   * aquellos con clave de perfil PACIENTE.
   */
  protected consultarPacientes(): void {
    lastValueFrom(this.expedienteTrackrService.consultarParaGrid()).then(
      (pacientes: UsuarioExpedienteGridDTO[]) => {
        this.pacientes = pacientes;
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
      console.log(gridData.data.patologias);
      this.ver(gridData.data.idUsuario);
    }
  }
}