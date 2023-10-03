import { Observable, lastValueFrom } from 'rxjs';
import { ExpedientePadecimientoService } from '@http/seguridad/expediente-padecimiento.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AfterContentInit, AfterViewInit, Component, ComponentFactoryResolver, EventEmitter, Input, OnInit, Output, QueryList, Sanitizer, TemplateRef, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { TabDirective } from 'ngx-bootstrap/tabs';
import * as moment from 'moment';
import { EncryptionService } from '@services/encryption.service';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { GeneralConstant } from '@utils/general-constant';
import { DomSanitizer } from '@angular/platform-browser';
import { ExternalTemplate } from '@sharedComponents/tabulador-entidad/external-template';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { ExpedienteGeneralFormularioComponent } from '../expediente-general-formulario/expediente-general-formulario.component';
import { ExpedientePadecimientoGridDTO } from '@dtos/gestion-expediente/expediente-padecimiento-grid-dto';
import { ExpedientePadecimientoDTO } from '@dtos/seguridad/expediente-padecimiento-dto';
import { DashboardPadecimientoComponent } from '../dashboard-padecimiento/dashboard-padecimiento.component';
import { ExpedienteEstudioComponent } from '../expediente-estudio/expediente-estudio.component';
import { ExpedientePadecimientoComponent } from '../expediente-padecimiento/expediente-padecimiento.component';
import { ArchivoService } from '@http/catalogo/archivo.service';
import { ExpedienteRecomendacionComponent } from '../expediente-recomendacion/expediente-recomendacion.component';
import { ExpedienteTratamientoComponent } from '../expediente-tratamiento/expediente-tratamiento.component';
import { NavItem } from '@components/layout-administrador/layout-administrador.component';
import { SafeUrl } from '@angular/platform-browser';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Usuario } from '@models/seguridad/usuario';
import { UsuarioImagenService } from '@services/usuario-imagen.service';
import { Genero } from '@models/catalogo/genero';
import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';

@Component({
  selector: 'app-expediente-formulario',
  templateUrl: './expediente-formulario.component.html',
  styleUrls: ['./expediente-formulario.component.scss']
})
export class ExpedienteFormularioComponent implements OnInit, AfterContentInit {
  [x: string]: any;



  @ViewChild(ExpedienteGeneralFormularioComponent, { static: false }) public informacionGeneral: ExpedienteGeneralFormularioComponent;

  public padecimientosList: ExpedientePadecimientoDTO[] = [];

  /**
   * Configuracion para el uso de componentes externos
   */
  public externalTemplates: ExternalTemplate[] = [];

  public accion: string;
  public claveEntidadExpedienteTrackr = GeneralConstant.ClaveEntidadExpedienteTrackr;
  public idUsuario: number;


  //Imagen
  public imagenBase64: any;
  public url: any;
  public urlImagenDefault = './assets/img/svg/ico-36x36-header-usuario.svg'
  protected readonly imagenUsuario = 'assets/img/pruebas/user-image.png';
  protected urlImagen?: SafeUrl = undefined;

  nombreCompleto: string;
  correo: string;
  direccion: string;
  genero: number;
  edad: string;
  idHospital: number;
  colonia: string;
  municipio: string;
  estado: string;
ciudad: string


  constructor(
    private encryptionService: EncryptionService,
    private route: ActivatedRoute,
    private expedientePadecimientoService: ExpedientePadecimientoService,
    private archivoService: ArchivoService,
    private usuarioImagenService: UsuarioImagenService,
    private usuarioService: UsuarioService,
    private expedienteTrackrService: ExpedienteTrackrService,

    private sanitizer: DomSanitizer
  ) { }


  public ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (this.encryptionService.readUrlParams(params).i == null) {
        this.accion = GeneralConstant.COMPONENT_ACCION_AGREGAR;
      } else {
        this.accion = GeneralConstant.COMPONENT_ACCION_EDITAR;
        const idUsuario = this.encryptionService.readUrlParams(params).i;
        this.idUsuario = idUsuario;

        this.archivoService.obtenerUsuarioImagen(idUsuario).subscribe((imagen) => {
          let objectURL = URL.createObjectURL(imagen);
          this.url = this.sanitizer.bypassSecurityTrustUrl(objectURL);
        });

        this.usuarioService.consultar(idUsuario).subscribe
          ({
            next: (data) => {
              this.nombreCompleto = data.nombre + " " + data.apellidoPaterno + " " + data.apellidoMaterno;
              this.idHospital = data.idHospital;
              this.ciudad = data.ciudad,
              this.colonia = data.colonia
            }, error: (error) => { }
          });

          this.expedienteTrackrService.consultaParaSidebar(idUsuario)
  .subscribe({
    next: (data) => {
      setTimeout(() => {
        this.edad = data.edad;
      });
    }
  });
          this.usuarioService.consultaDomicilioPorId(idUsuario).subscribe({
            next: (data) => {
              this.estado =data.estado
            }
          });




      }
    });

  }

  public async ngAfterContentInit() {
    this.agregarTabInformacionGeneral();
    this.agregarTabPadecimientos();
    this.agregarTabEstudios();
    await this.agregarTabsDashboardPadecimiento();
    this.agregarTabTratamientos();
    this.agregarTabRecomendaciones();
  }

  private agregarTabInformacionGeneral(): void {
    const informacionGeneral: ExternalTemplate = {
      component: ExpedienteGeneralFormularioComponent,
      args: {
        idUsuario: this.idUsuario,
      },
      label: 'General',
      enabled: true,
      externalSubmit: true,
      submitControl: false
    };

    this.externalTemplates.push(informacionGeneral);

  }

  private agregarTabPadecimientos(): void {
    const padecimientos: ExternalTemplate = {
      component: ExpedientePadecimientoComponent,
      args: {},
      label: 'Padecimientos',
      enabled: this.idUsuario != null ? true : false,
      externalSubmit: true,
      submitControl: false
    };

    this.externalTemplates.push(padecimientos);

  }

  private agregarTabEstudios(): void {
    const estudios: ExternalTemplate = {
      component: ExpedienteEstudioComponent,
      label: 'Estudios',
      args: {
        idUsuario: this.idUsuario,
      },
      enabled: this.idUsuario != null ? true : false,
      externalSubmit: true,
      submitControl: false
    };

    this.externalTemplates.push(estudios);
  }

  private async agregarTabsDashboardPadecimiento(): Promise<void> {
    await this.consultarPadecimientos();

    this.padecimientosList.forEach((padecimiento: ExpedientePadecimientoDTO) => {
      const padecimientoExtTpl: ExternalTemplate = {
        component: DashboardPadecimientoComponent,
        args: {
          idPadecimiento: padecimiento.idPadecimiento,
          idUsuario: this.idUsuario,
          nombrePadecimiento: padecimiento.nombrePadecimiento,
        },
        label: padecimiento.nombrePadecimiento,
        enabled: this.idUsuario != null ? true : false,
        externalSubmit: true,
        submitControl: false
      };

      this.externalTemplates.push(padecimientoExtTpl);
    });
  }

  private agregarTabTratamientos(): void {
    const tratamientos: ExternalTemplate = {
      component: ExpedienteTratamientoComponent,
      label: 'Tratamientos',
      args: {},
      enabled: this.idUsuario != null ? true : false,
      externalSubmit: true,
      submitControl: false
    };

    this.externalTemplates.push(tratamientos);
  }

  private agregarTabRecomendaciones() {
    const recomendaciones: ExternalTemplate = {
      component: ExpedienteRecomendacionComponent,
      args: {},
      label: 'Recomendaciones',
      enabled: this.idUsuario != null ? true : false,
      externalSubmit: true,
      submitControl: false
    };

    this.externalTemplates.push(recomendaciones);
  }

  public async consultarPadecimientos() {
    await lastValueFrom(this.expedientePadecimientoService.consultarPorUsuario(this.idUsuario))
      .then((padecimientos) => {
        this.padecimientosList = padecimientos;
      });
  }


}
