import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ColoniaSelectorDto } from '@dtos/catalogo/colonia-selector-dto';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { GeneroSelectorDto } from '@dtos/catalogo/genero-selector-dto';
import { LocalidadSelectorDto } from '@dtos/catalogo/localidad-selector-dto';
import { municipioSelectorDto } from '@dtos/catalogo/municipio-selector-dto';
import { PaisSelectorDto } from '@dtos/catalogo/pais-selector-dto';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { AlertController, IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { Observable, tap, BehaviorSubject } from 'rxjs';
import * as Utileria from '@utils/utileria';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { RouterModule } from '@angular/router';
import { OnExit } from 'src/app/shared/guards/exit.guard';
import { InformacionDomicilioDto } from 'src/app/shared/Dtos/perfil/informacion-domicilio-dto';

@Component({
  selector: 'app-info-domicilio',
  templateUrl: './info-domicilio.component.html',
  styleUrls: ['./info-domicilio.component.scss'],
  standalone: true,
  imports: [CommonModule, IonicModule, FormsModule, RouterModule],
})
export class InfoDomicilioComponent implements OnInit, OnExit {
  protected informacionUsuario$: Observable<InformacionDomicilioDto>;
  protected infoUsuario: InformacionDomicilioDto;
  protected submiting = false;

  protected esPaisExtranjero: boolean = false;
  private idPaisMexico: 1;
  protected paisList: PaisSelectorDto[] = [];
  protected estadoList: EstadoSelectorDto[] = [];
  protected municipioList: municipioSelectorDto[] = [];
  protected localidadList: LocalidadSelectorDto[] = [];
  protected coloniaList: ColoniaSelectorDto[] = [];

  //Estado de "cargando" para mostrar el alert con spinner
  private cargandoSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(false);
  private cargando$ = this.cargandoSubject.asObservable();
  private loading: any;

  @ViewChild('formulario') formulario: NgForm;

  constructor(
    private usuarioService: UsuarioService,
    private paisService: PaisService,
    private estadoService: EstadoService,
    private municipioService: MunicipioService,
    private localidadService: LocalidadService,
    private coloniaService: ColoniaService,
    private codigoPostalService: CodigoPostalService,
    private alertController: AlertController
  ) {
    addIcons({
      'chevron-down': 'assets/img/svg/chevron-down.svg',
    });
  }

  async ngOnInit() {
    await this.consultarPaises();
    this.obtenerUsuario();

    this.cargando$.subscribe((cargando) => {
      if (cargando) {
        this.presentLoading();
      } else {
        this.dismissLoading();
      }
    });
  }

  onExit() {
    if (this.formulario.dirty) {
      const rta = this.presentAlertSalir();
      return rta;
    }

    return true;
  }

  async presentLoading() {
    this.loading = await this.alertController.create({
      cssClass: 'custom-alert-loading',
    });
    return await this.loading.present();
  }

  async dismissLoading() {
    if (this.loading) {
      await this.loading.dismiss();
      this.loading = null;
    }
  }

  private async obtenerUsuario() {
    this.informacionUsuario$ = this.usuarioService
      .consultarInformacionDomicilio()
      .pipe(
        tap(async (infoUsuario) => {
          this.infoUsuario = infoUsuario;

          await Promise.all([
            this.consultarEstados(infoUsuario.idPais),
            this.consultarMunicipios(infoUsuario.idEstado),
            this.consultarLocalidades(infoUsuario.idEstado),
            this.consultarColonias(infoUsuario.codigoPostal),
          ]);
        })
      );
  }

  protected async enviarFormulario(formulario: NgForm) {
    if (formulario.invalid) {
      this.submiting = false;
      Utileria.validarCamposRequeridos(formulario);
      this.presentAlertError(
        'Campos requeridos',
        'Debe completar todos los campos obligatorios'
      );
      return;
    }
    this.submiting = true;
    this.cargandoSubject.next(true);
    this.actualizarInformacionUsuario(this.infoUsuario);
  }

  private actualizarInformacionUsuario(informacion: InformacionDomicilioDto) {
    this.usuarioService.actualizarInformacionDomicilio(informacion).subscribe({
      next: () => { },
      error: () => {
        this.submiting = false;
        this.cargandoSubject.next(false);
      },
      complete: () => {
        this.presentAlertSuccess(
          'Información actualizada',
          'La información se actualizó correctamente'
        );
        this.submiting = false;
        this.cargandoSubject.next(false);
        this.formulario.form.markAsPristine();
      },
    });
  }

  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService
      .consultarTodosParaSelector()
      .toPromise();

    this.paisList = paises ?? [];
  }

  private async consultarEstados(idPais: number | null): Promise<void> {
    if (idPais) {
      const estados =
        idPais > 0
          ? await this.estadoService
            .consultarPorPaisParaSelector(idPais)
            .toPromise()
          : [];

      this.estadoList = estados ?? [];
    }
  }

  private async consultarMunicipios(idEstado: number | null): Promise<void> {
    if (idEstado) {
      const municipios =
        idEstado > 0
          ? await this.municipioService
            .consultarPorEstadoParaSelector(idEstado)
            .toPromise()
          : [];

      this.municipioList = municipios ?? [];
    }
  }

  private async consultarLocalidades(idEstado: number | null): Promise<void> {
    if (idEstado) {
      const localidades =
        idEstado > 0
          ? await this.localidadService.consultarPorEstado(idEstado).toPromise()
          : [];

      this.localidadList = localidades ?? [];
    }
  }

  private async consultarColonias(codigoPostal: string): Promise<void> {
    const colonias =
      codigoPostal && codigoPostal.length === 5
        ? await this.coloniaService
          .consultarPorCodigoParaSelector(codigoPostal)
          .toPromise()
        : [];

    this.coloniaList = colonias ?? [];
  }

  protected async onChangeCodigoPostal() {
    this.infoUsuario.idColonia = 0;
    const codigoPostalValue: string = this.infoUsuario?.codigoPostal;
    if (codigoPostalValue.length !== 5) {
      return;
    }

    await this.asignarValoresDeCodigoPostal(codigoPostalValue);
  }

  private async asignarValoresDeCodigoPostal(codigoPostalValue: string): Promise<void> {
    const codigoPostal = await this.codigoPostalService
      .consultarPorCodigoPostal(codigoPostalValue)
      .toPromise()
      .then((codigosPostales) => {
        return codigosPostales && codigosPostales.length > 0
          ? codigosPostales[0]
          : null;
      });

    if (!codigoPostal) {
      return;
    }

    this.infoUsuario.idPais = codigoPostal.idPais;
    this.infoUsuario.idEstado = codigoPostal.idEstado;
    this.infoUsuario.idMunicipio = codigoPostal.idMunicipio;
    this.infoUsuario.idLocalidad = null;

    await Promise.all([
      this.consultarEstados(codigoPostal.idPais),
      this.consultarMunicipios(codigoPostal.idEstado),
      this.consultarLocalidades(codigoPostal.idEstado),
    ]);

    await this.consultarColonias(codigoPostalValue);
    const colonia =
      this.coloniaList.find((colonia) =>
        Utileria.equalsNormalized(colonia.nombre, codigoPostal.colonia)
      ) || this.coloniaList[0];

    if (colonia) {
      this.infoUsuario.idColonia = colonia.idColonia;
    }
  }

  protected async onChangePais() {
    this.esPaisExtranjero = this.infoUsuario.idPais !== this.idPaisMexico;

    this.infoUsuario.idEstado = null;
    this.infoUsuario.idMunicipio = null;
    this.infoUsuario.idLocalidad = null;
    this.infoUsuario.idColonia = null;
    this.infoUsuario.codigoPostal = '';

    await Promise.all([
      this.consultarEstados(this.infoUsuario.idPais),
      this.consultarMunicipios(this.infoUsuario.idEstado),
      this.consultarLocalidades(this.infoUsuario.idEstado),
      //this.consultarColonias(this.infoUsuario.idEstado),
    ]);
  }

  protected async onChangeEstado() {
    this.infoUsuario.idMunicipio = null;
    this.infoUsuario.idLocalidad = null;
    this.infoUsuario.idColonia = null;

    await Promise.all([
      this.consultarMunicipios(this.infoUsuario.idEstado),
      this.consultarLocalidades(this.infoUsuario.idEstado),
    ]);
  }

  private async presentAlertError(header: string, subheader: string) {
    const alert = await this.alertController.create({
      header: header,
      subHeader: subheader,
      buttons: ['Ok'],
      cssClass: 'custom-alert color-error icon-info',
    });

    await alert.present();
  }

  private async presentAlertSuccess(header: string, subheader: string) {
    const alert = await this.alertController.create({
      header: header,
      subHeader: subheader,
      buttons: ['Ok'],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alert.present();
  }

  private async presentAlertSalir(): Promise<boolean> {
    const alert = await this.alertController.create({
      header: '¿Está seguro que desea salir?',
      subHeader: 'Perderá los cambios que no haya guardado',
      cssClass: 'custom-alert color-error icon-info two-buttons',
      backdropDismiss: false,
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: () => {
            return false;
          },
        },
        {
          text: 'Salir',
          handler: () => {
            return true;
          },
        },
      ],
    });
    await alert.present();

    const data = await alert.onDidDismiss();
    return data.role === 'cancel' ? false : true;
  }

  protected codigoPostalDisabled() {
    return !(this.infoUsuario.idPais != null);
  }
  protected estadoDisabled() {
    return !(this.infoUsuario.idPais != null);
  }
  protected municipioDisabled() {
    return !(
      this.infoUsuario.idPais != null && this.infoUsuario.idEstado != null
    );
  }
  protected localidadDisabled() {
    return !(
      this.infoUsuario.idPais != null && this.infoUsuario.idEstado != null
    );
  }
  protected coloniaDisabled() {
    return !(
      this.infoUsuario.idPais != null &&
      this.infoUsuario.idEstado != null &&
      this.infoUsuario.idMunicipio != null &&
      this.infoUsuario.codigoPostal != null &&
      this.infoUsuario.codigoPostal != ''
    );
  }
}
