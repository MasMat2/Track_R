import { DomicilioCreationDTO } from '@dtos/seguridad/domicilio-creation-dto';
import { Input, Output } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { Colonia } from '@models/catalogo/colonia';
import { Estado } from '@models/catalogo/estado';
import { Localidad } from '@models/catalogo/localidad';
import { Municipio } from '@models/catalogo/municipio';
import { Pais } from '@models/catalogo/pais';
import { Usuario } from '@models/seguridad/usuario';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-domicilio-formulario',
  templateUrl: './domicilio-formulario.component.html',
  styleUrls: ['./domicilio-formulario.component.scss']
})


export class DomicilioFormularioComponent implements OnInit {
  @Input() idDomicilio: number = 0;
  @Output() domicilio: DomicilioCreationDTO;
  public usuario = new Usuario();

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  
  // Datos de Contacto
  public paisList: Pais[] = [];
  public estadoList: Estado[] = [];
  public municipioList: Municipio[] = [];
  public localidadList: Localidad[] = [];
  public coloniaList: Colonia[] = [];

  public sugerenciasCodigoPostal$: Observable<CodigoPostal[]>;

  public esPaisExtranjero: boolean = false;
  public idPaisMexico: number;

  constructor(
    private paisService: PaisService,
    private estadoService: EstadoService,
    private municipioService: MunicipioService,
    private codigoPostalService: CodigoPostalService,
    private localidadService: LocalidadService,
    private coloniaService: ColoniaService,
  ) { }

  ngOnInit() {
    if(this.idDomicilio > 0){
      this.consultarDomicilio(this.idDomicilio);
    }
  }

  public consultarDomicilio(idDomicilio: number): void {
    // this.domicilioService.consultar(idDomicilio).subscribe((domicilio: any) => { this.domicilio = domicilio});
  }

  public async onChangePais(): Promise<void> {
    this.esPaisExtranjero = this.usuario.idPais !== this.idPaisMexico;
    this.usuario.idEstado = 0;
    await this.consultarEstados(this.usuario.idPais);

    this.onChangeEstado();
  }

  public async onChangeEstado(): Promise<void> {
    this.usuario.idMunicipio = 0;
    this.usuario.idLocalidad = 0;

    await Promise.all([
      this.consultarMunicipios(this.usuario.idEstado),
      this.consultarLocalidades(this.usuario.idEstado)
    ]);
  }

  public async onChangeCodigoPostal(): Promise<void> {
    this.usuario.idColonia = 0;

    const codigoPostalValue: string = this.usuario.codigoPostal;

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

    this.usuario.idEstado = codigoPostal.idEstado;

    await Promise.all([
      this.consultarMunicipios(codigoPostal.idEstado),
      this.consultarLocalidades(codigoPostal.idEstado)
    ]);
    this.usuario.idMunicipio = codigoPostal.idMunicipio;
    this.usuario.idLocalidad = 0

    await this.consultarColonias(codigoPostalValue);
    const colonia = this.coloniaList
      .find((colonia) => Utileria.equalsNormalized(colonia.nombre, codigoPostal.colonia));

    if (colonia) {
      this.usuario.idColonia = colonia.idColonia;
    }
  }

  
  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService
      .consultarTodosParaSelector()
      .toPromise();

    this.paisList = paises ?? [];
  }

  private async consultarEstados(idPais: number): Promise<void> {
    const estados = idPais > 0
      ? await this.estadoService.consultarPorPaisParaSelector(idPais).toPromise()
      : [];

    this.estadoList = estados ?? [];
  }

  private async consultarMunicipios(idEstado: number): Promise<void> {
    const municipios = idEstado > 0
      ? await this.municipioService.consultarPorEstadoParaSelector(idEstado).toPromise()
      : [];

    this.municipioList = municipios ?? [];
  }

  private async consultarLocalidades(idEstado: number): Promise<void> {
    const localidades = idEstado > 0
      ? await this.localidadService.consultarPorEstado(idEstado).toPromise()
      : [];

    this.localidadList = localidades ?? [];
  }

  private async consultarColonias(codigoPostal: string): Promise<void> {
    const colonias = codigoPostal && codigoPostal.length === 5
      ? await this.coloniaService.consultarPorCodigoParaSelector(codigoPostal).toPromise()
      : [];

    this.coloniaList = colonias ?? [];
  }


}
