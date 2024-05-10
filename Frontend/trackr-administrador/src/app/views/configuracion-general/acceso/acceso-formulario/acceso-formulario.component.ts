import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { NgForm } from '@angular/forms';
import { AccesoService } from '@http/seguridad/acceso.service';
import { Acceso } from '@models/seguridad/acceso';
import { EncryptionService } from '@services/encryption.service';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { GeneralConstant } from 'src/app/shared/utils/general-constant';
import { environment } from 'src/environments/environment';
import { RolAcceso } from '@models/seguridad/rol-acceso';
import { TipoAcceso } from '@models/seguridad/tipo-acceso';
import { IconoService } from '@http/catalogo/icono.service';
import { RolAccesoService } from '@http/seguridad/acceso-rol.service';
import { TipoAccesoService } from '@http/seguridad/tipo-acceso.service';

@Component({
  templateUrl: 'acceso-formulario.component.html',
  styleUrls: ['./acceso-formulario.component.css']
})
export class AccesoFormularioComponent implements OnInit {

  public titulo = 'Agregar';
  public TITULO_MODAL_ELIMINAR = 'Eliminar imagen acceso';
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;
  public MENSAJE_AGREGAR = 'El acceso ha sido agregado';
  public MENSAJE_EDITAR = 'El acceso ha sido modificado';
  public nombreImagenPrev: string;
  public urlBackend = environment.urlBackend;

  public acceso = new Acceso();
  public accesoList: Acceso[] = [];
  public tipoAccesoList: TipoAcceso[] = [];
  public rolAccesoList: RolAcceso[] = [];
  public iconoList: any[] = [];

  public tipoAccesoMenu: boolean = false;
  public tipoAccesoEvento: boolean = false;
  public btnSubmit: boolean = false;
  public esModal: boolean = false;
  public onClose: any;

  public configTipoAcceso = Object.assign(
    { labelField: 'nombre', valueField: 'idTipoAcceso', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configAcceso = Object.assign(
    { labelField: 'nombre', valueField: 'idAcceso', searchField: ['nombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configIconoMenu = Object.assign(
    { labelField: 'nombre', valueField: 'idIcono', searchField: ['nombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  public configRolAcceso = Object.assign(
    { labelField: 'nombre', valueField: 'idRolAcceso', searchField: ['nombre'] },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );

  constructor(
    private modalMensajeService: MensajeService,
    private router: Router,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService,
    private accesoService: AccesoService,
    private tipoAccesoService: TipoAccesoService,
    private iconoService: IconoService,
    private rolAccesoService: RolAccesoService
  ) {}

  ngOnInit(): void {
    this.consultarRolAcceso();
    this.consultarAccesosPadre();
    this.consultarIconos();
    this.consultarTipoAcceso();
    this.route.queryParams.subscribe((params) => {
      this.acceso.idAcceso = this.encryptionService.readUrlParams(params).i;
    });
  }

  public consultarIconos() {
    this.iconoService.consultarGeneral().subscribe((data) => {
      this.iconoList = data;
      this.consultarAcceso();
    });
  }

  public consultarTipoAcceso() {
    this.tipoAccesoService.consultarGeneral().subscribe((data) => {
      this.tipoAccesoList = data;
      this.consultarAcceso();
    });
  }

  public consultarAccesosPadre() {
    this.accesoService.consultarGeneral().subscribe((data) => {
      data.forEach(d => d.nombre = d.clave + ' - ' + d.nombre);
      this.accesoList = data;
      this.consultarAcceso();
    });
  }

  public consultarRolAcceso() {
    this.rolAccesoService.consultarTodosParaSelector().subscribe((data) => {
      this.rolAccesoList = data;
    });
  }

  public consultarAcceso() {
    if (this.acceso.idAcceso > 0) {
      this.accesoService.consultar(this.acceso.idAcceso).subscribe((data) => {
        this.acceso = data;
        this.onIconoChange(this.acceso.idIcono);
        this.onTipoAccesoChange(this.acceso.idTipoAcceso);
        this.titulo = 'Editar';
      });
    }
  }

  public enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;

    if (!formulario.valid) {
      this.btnSubmit = false;
      this.validarCamposRequeridos(formulario);
      return;
    }

    if (this.acceso.idAcceso > 0) {
      this.editar();
    } else {
      this.agregar(formulario);
    }
  }

  public agregar(formulario: NgForm) {
    this.accesoService.agregar(this.acceso).subscribe(
      (data) => {
        this.modalMensajeService.modalExito(this.MENSAJE_AGREGAR);
        this.limpiarFormulario(formulario);
        this.btnSubmit = false;
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public editar() {
    this.accesoService.editar(this.acceso).subscribe(
      (data) => {
        this.modalMensajeService.modalExito(this.MENSAJE_EDITAR);
        this.btnSubmit = false;
        if (this.esModal) {
          this.onClose(true);
          return;
        } else {
          this.router.navigate(['/administrador/configuracion-general/acceso']);
        }
      },
      (error) => {
        this.btnSubmit = false;
      }
    );
  }

  public limpiarFormulario(formulario: NgForm) {
    formulario.reset();
    this.nombreImagenPrev = '';
    this.acceso = new Acceso();
  }

  public cancelar() {
    if (this.esModal) { this.onClose(true); return; }
    this.router.navigate(['/administrador/configuracion-general/acceso']);
  }

  public onIconoChange(idIcono?: number) {
    const icono = this.iconoList.find((i) => i.idIcono === idIcono);
    if (icono !== undefined) {
      this.acceso.claseIcono = icono.clase;
    }
  }

  public onTipoAccesoChange(idTipoAcceso: number) {
    const tipoAcceso = this.tipoAccesoList.find((i) => +i.idTipoAcceso === +idTipoAcceso);
    const clave = tipoAcceso != null ? tipoAcceso.clave : null;

    switch (clave) {
      case GeneralConstant.CLAVE_TIPO_ACCESO_MENU:
        this.tipoAccesoMenu = true;
        this.tipoAccesoEvento = false;
        this.acceso.url = '';
        break;

      case GeneralConstant.CLAVE_TIPO_ACCESO_EVENTO:
        this.tipoAccesoMenu = false;
        this.tipoAccesoEvento = true;
        this.acceso.url = '';
        this.acceso.ordenMenu = undefined;
        this.acceso.idIcono = undefined;
        break;

      default:
        this.tipoAccesoMenu = false;
        this.tipoAccesoEvento = false;
        break;
    }
  }

  /**
   * Marca en rojo las validaciones de campos requeridos
   */
  private validarCamposRequeridos(formulario: any) {
    Object.keys(formulario.controls).forEach((nombre) => {
      const control = formulario.controls[nombre];
      control.markAsTouched({ onlySelf: true });
    });
  }

  fileChange(event: any) {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      this.acceso.nombreImagen = event.target.files[0].name;
      this.nombreImagenPrev = event.target.files[0].name;
      this.acceso.imagenTipoMime = event.target.files[0].type;

      // tslint:disable-next-line: no-shadowed-variable
      reader.onload = (event: Event) => {
        const result = reader.result;

        this.acceso.imagenBase64 = result
          ? result.toString().split(',')[1]
          : '';
      };
    }
  }

  eliminarAccesoImagen() {
    this.modalMensajeService.modalConfirmacion(
      '¿Confirma la eliminación de la imagen del acceso: <strong>' + this.acceso.nombreImagen + '</strong>?',
      this.TITULO_MODAL_ELIMINAR,
      GeneralConstant.ICONO_CRUZ
    ).then((aceptar) => {
      this.acceso.imagenBase64 = '';
      this.acceso.nombreImagen = '';
      this.acceso.imagenTipoMime = '';
      this.acceso.imagen = '';
    });
  }

  agregarAyuda(idAcceso: number) {
    this.accesoService.consultar(idAcceso).subscribe((data) => {
      this.router.navigate(['/administrador/configuracion-general/acceso/acceso-ayuda'], {
        queryParams: this.encryptionService.generateURL({
          i: idAcceso.toString()
        })
      });
    });
  }
}
