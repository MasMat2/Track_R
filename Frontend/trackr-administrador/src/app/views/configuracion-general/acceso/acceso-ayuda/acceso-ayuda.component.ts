import { lastValueFrom } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AccesoAyudaService } from "@http/seguridad/acceso-ayuda.service";
import { AccesoService } from '@http/seguridad/acceso.service';
import { AyudaSeccionService } from '@http/seguridad/ayuda-seccion.service';
import { Acceso } from '@models/seguridad/acceso';
import { AccesoAyuda, AccesoAyudaImagenBase64 } from '@models/seguridad/acceso-ayuda';
import { AyudaSeccion } from '@models/seguridad/ayuda-seccion';
import { EncryptionService } from '@services/encryption.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';

@Component({
  templateUrl: 'acceso-ayuda.component.html',
  styleUrls: ['./acceso-ayuda.component.scss']
})
export class AccesoAyudaComponent implements OnInit {

  public btnSubmit = false;
  public onClose: any;
  public esModal : boolean = false;

  public TITULO_MODAL_ELIMINAR = 'Eliminar imagen de la ayuda';
  // Variables de Imagen
  public acceso = new Acceso();
  public accesoAyuda = new AccesoAyuda();
  public accesoAyudaList: AccesoAyuda[] = [];
  public accesoAyudaImagen: AccesoAyudaImagenBase64 = {} as AccesoAyudaImagenBase64;
  public accesoAyudaImagenList: AccesoAyuda | null;
  public image: any;

  // Miembros del Grid
  public MENSAJE_EXITO_ELIMINAR = 'La ayuda de acceso ha sido eliminada';
  public MENSAJE_AGREGAR = 'La ayuda de acceso ha sido agregada';
  public MENSAJE_EDITAR = 'La ayuda de acceso ha sido modificada';
  public HEADER_GRID = 'Acceso Ayudas';


  public ayudaSeccionList: AyudaSeccion[] = [];
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public columns = [
    { headerName: 'Imagen', field: 'imagen', minWidth: 150,
      valueGetter: (params: any) => {
        return this.transform(params.data.imagen);
      }
  },
    { headerName: 'Descripción', field: 'descripcionAyuda', minWidth: 150 },
  ];

  public configAyudaSeccion = Object.assign(
    { labelField: 'claveNombre', valueField: 'idAyudaSeccion', searchField: ['claveNombre'], dropdownParent: 'body' },
    GeneralConstant.CONFIG_DROPDOWN_DEFAULT
  );


  constructor(
    private modalMensajeService: MensajeService,
    private router: Router,
    private route: ActivatedRoute,
    private encryptionService: EncryptionService,
    private accesoService: AccesoService,
    private accesoAyudaService: AccesoAyudaService,
    private ayudaSeccionService: AyudaSeccionService,
    private sanitizer: DomSanitizer,
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.acceso.idAcceso = this.encryptionService.readUrlParams(params).i;
    });
    this.consultarAcceso()
    this.consultarAccesoAyudasPorAcceso();
    this.consultarAyudaSeccion();
  }

  public consultarAcceso() {
    this.accesoService.consultar(this.acceso.idAcceso).subscribe((data) => {
        this.acceso = data;
      });
  }

  public consultarAyudaSeccion() {
    this.ayudaSeccionService.consultarTodosParaSelector().subscribe((data) => {
        this.ayudaSeccionList = data;
      });
  }

  public consultarAccesoAyudasPorAcceso() {
    lastValueFrom(this.accesoAyudaService.consultarPorAcceso(this.acceso.idAcceso)).then((data) => {
      this.accesoAyudaList = data;
    });
  }

  public limpiarFormulario(formulario: NgForm) {
    formulario.reset();
    this.accesoAyuda = new AccesoAyuda();
  }

  public enviarFormulario(formulario: NgForm) {
    this.btnSubmit = true;

    if (!formulario.valid) {
      this.btnSubmit = false;
      this.validarCamposRequeridos(formulario);
      return;
    }
    if (this.accesoAyuda.idAccesoAyuda > 0) {
      this.editar();
    } else {
      this.agregar(formulario);
    }

    this.limpiarFormulario(formulario)
  }

  public cancelar() {
    if (this.esModal) { this.onClose(true); return; }
    this.router.navigate(['/administrador/configuracion-general/acceso']);
  }

  public onGridClick(gridData: { accion: string; data: AccesoAyuda }): void {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.onClickEditar(gridData.data.idAccesoAyuda);
      return;
    }

    if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      this.eliminarAccesoImagen(gridData.data);
      return;
    }
  }

  public agregar(formulario: NgForm){
    this.accesoAyuda.idAcceso = this.acceso.idAcceso
    this.accesoAyudaService.agregar(this.accesoAyuda).subscribe(
      (data) => {
        this.modalMensajeService.modalExito(this.MENSAJE_AGREGAR);
        this.limpiarFormulario(formulario);
        this.btnSubmit = false;
        this.consultarAccesoAyudasPorAcceso();
      },
      (error) => {
        this.btnSubmit = false;
      })
  }

  public agregarUrl(){
    this.accesoService.editar(this.acceso).subscribe((data) =>
    {
      this.modalMensajeService.modalExito(this.MENSAJE_EDITAR);
      this.btnSubmit = false;
    })
  }

  public editar(){
    this.accesoAyudaService.editar(this.accesoAyuda).subscribe(
      (data) => {
        this.modalMensajeService.modalExito(this.MENSAJE_EDITAR);
        this.btnSubmit = false;
        this.consultarAccesoAyudasPorAcceso();
      })
  }

  public onClickEditar(idAccesoAyuda: number){
    this.accesoAyudaService.consultar(idAccesoAyuda).subscribe(
      (data) => {
        this.accesoAyuda = data;
      }
    )
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

  public fileChangeImagen(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      this.accesoAyuda.nombreArchivo = event.target.files[0].name;
      this.accesoAyuda.tipoMime = event.target.files[0].type;

      reader.onload = (event: Event) => {
        this.accesoAyuda.imagen = reader.result?.toString().split(',')[1];
      };
    }
  }

  public transform(imagen: any){
    return this.sanitizer.bypassSecurityTrustResourceUrl(`data:imagen/;base64, ${imagen}`)
  }

  public eliminarAccesoImagen(accesoAyuda: AccesoAyuda): void {
    this.modalMensajeService
      .modalConfirmacion(
        '¿Desea eliminar la ayuda <strong>' + accesoAyuda.descripcionAyuda + '</strong>?',
        this.TITULO_MODAL_ELIMINAR
      )
      .then((aceptar) => {
        this.accesoAyudaService.eliminar(accesoAyuda.idAccesoAyuda).subscribe((data) => {
          this.modalMensajeService.modalExito(this.MENSAJE_EXITO_ELIMINAR);
          this.consultarAccesoAyudasPorAcceso();
        });
      });
  }

}
