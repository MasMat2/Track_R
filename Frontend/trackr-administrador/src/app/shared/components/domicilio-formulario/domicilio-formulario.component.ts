import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { CodigoPostalService } from '@http/catalogo/codigo-postal.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { DomicilioService } from '@http/inventario/domicilio.service';
import { CodigoPostal } from '@models/catalogo/codigo-postal';
import { Colonia } from '@models/catalogo/colonia';
import { Estado } from '@models/catalogo/estado';
import { Localidad } from '@models/catalogo/localidad';
import { Municipio } from '@models/catalogo/municipio';
import { Pais } from '@models/catalogo/pais';
import { Domicilio } from '@models/seguridad/domicilio';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-domicilio-formulario',
  templateUrl: './domicilio-formulario.component.html',
  styleUrls: ['./domicilio-formulario.component.scss']
})


export class DomicilioFormularioComponent implements OnChanges {
  @Input() idDomicilio: number;
  @Output() domicilioChange = new EventEmitter<Domicilio>();
  @Input() domicilio = new Domicilio();

  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  
  // Listas de Selectores
  public paisList: Pais[] = [];
  public estadoList: Estado[] = [];
  public municipioList: Municipio[] = [];
  public localidadList: Localidad[] = [];
  public coloniaList: Colonia[] = [];

  // Propiedades de selectores
  public idPais: number | null;
  public idEstado: number | null;
  public idMunicipio: number | null;
  public idLocalidad: number | null;
  public idColonia: number | null;

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
    private domicilioService: DomicilioService
  ) { }

  ngOnChanges(): void {
    if(this.idDomicilio > 0){
      this.consultarDomicilio(this.idDomicilio);
      this.consultarEstados(this.domicilio.idPais);
      this.consultarMunicipios(this.domicilio.idEstado);
      this.consultarLocalidades(this.domicilio.idEstado);
      this.consultarColonias(this.domicilio.codigoPostal);
      
    }
    this.consultarPaises();
  }

  public enviarDomicilioAlPadre(){
    if(this.idPais != null){
      this.domicilio.idPais = this.idPais;
    }
  }

  /**
   * Consulta el domicilio si es que existe uno en la BD
   * @param idDomicilio a consultar
   */
  public consultarDomicilio(idDomicilio: number): void {
    this.domicilioService.consultar(idDomicilio).subscribe((domicilio: any) => { 
      this.domicilio = domicilio
      this.idPais = this.domicilio.idPais || null;
      this.idEstado = this.domicilio.idEstado || null;
    });
  }

  /**
   * Al cambiar el pais se ejecuta el método para consultar los estados
   * de acuerdo al pais seleccionado.
   */
  public async onChangePais(): Promise<void> {
    this.esPaisExtranjero = this.idPais !== this.idPaisMexico;
    this.domicilio.idEstado = 0;
    if(this.idPais != undefined || this.idPais != null){
      this.domicilio.idPais = this.idPais;
      await this.consultarEstados(this.idPais);
    }

    this.onChangeEstado();
  }

  /**
   * Al cambiar el estado se ejecuta el método para consultar los municipios
   * y localidades de acuerdo al estado seleccionado.
   */
  public async onChangeEstado(): Promise<void> {
    this.domicilio.idMunicipio = 0;
    this.domicilio.idLocalidad = 0;

    await Promise.all([
      this.consultarMunicipios(this.domicilio.idEstado),
      this.consultarLocalidades(this.domicilio.idEstado)
    ]);
  }

  /**
   * Al cambiar el código postal se ejecuta el método para asignar los valores
   * de acuerdo al código postal. (IdEstado, IdLocalidad e IdColonia)
   * @returns Promesa
   */
  public async onChangeCodigoPostal(): Promise<void> {
    this.domicilio.idColonia = 0;

    const codigoPostalValue: string = this.domicilio.codigoPostal;

    if (codigoPostalValue.length !== 5) {
      return;
    }

    await this.asignarValoresDeCodigoPostal(codigoPostalValue);
  }

  /**
   * Promesa que permite asignar los valores de acuerdo al codigo postal.
   * Si el codigo postal no existe, no se asignan valores.
   * Se asignan valores de idEstado, se consultan los municipios y localidades
   * con este último y se consultan las colonias, para asignar el valor
   * de acuerdo al código postal
   * @param codigoPostalValue 
   * @returns Promesa
   */
  private async asignarValoresDeCodigoPostal(codigoPostalValue: string): Promise<void> {
    // Consultamos el código postal con el valor recibido como parámetro.
    const codigoPostal = await this.codigoPostalService
      .consultarPorCodigoPostal(codigoPostalValue)
      .toPromise()
      .then((codigosPostales) => {
        // Si encontramos un código postal lo retornamos, en caso contrario retornamos null.
        return codigosPostales && codigosPostales.length > 0
          ? codigosPostales[0]
          : null;
      });

    // Si no encontramos un código postal con el valor recibido como parámetro, no hacemos nada.
    if (!codigoPostal) {
      return;
    }

    // Asignamos el idEstado del código postal a la propiedad idEstado del domicilio.
    this.domicilio.idEstado = codigoPostal.idEstado;

    // Consultamos los municipios del estado del código postal.
    await Promise.all([
      this.consultarMunicipios(codigoPostal.idEstado),
      this.consultarLocalidades(codigoPostal.idEstado)
    ]);
    // Set the default value of idLocalidad to 0 to avoid a null value.
    this.domicilio.idLocalidad = 0

    // Consultamos las colonias del código postal.
    await this.consultarColonias(codigoPostalValue);

    // Buscamos la colonia que coincida con el nombre del código postal.
    const colonia = this.coloniaList
      .find((colonia) => Utileria.equalsNormalized(colonia.nombre, codigoPostal.colonia));

    // Si encontramos la colonia, asignamos su id a la propiedad idColonia del domicilio.
    if (colonia) {
      this.domicilio.idColonia = colonia.idColonia;
    }
  }

  /**
   * Consulta los paises para el selector.
   */
  private async consultarPaises(): Promise<void> {
    const paises = await this.paisService
      .consultarTodosParaSelector()
      .toPromise();

    this.paisList = paises ?? [];
  }

  /**
   * Consulta los paises para el selector de acuerdo al pais
   * @param idPais para filtrar los estados
   */
  private async consultarEstados(idPais: number): Promise<void> {
    const estados = idPais > 0
      ? await this.estadoService.consultarPorPaisParaSelector(idPais).toPromise()
      : [];

    this.estadoList = estados ?? [];
  }

  /**
   * Consulta los municipios para el selector de acuerdo al estado
   * @param idEstado para filtrar los municipios
   */
  private async consultarMunicipios(idEstado: number): Promise<void> {
    const municipios = idEstado > 0
      ? await this.municipioService.consultarPorEstadoParaSelector(idEstado).toPromise()
      : [];

    this.municipioList = municipios ?? [];
  }

  /**
   * Consulta las localidades para el selector de acuerdo al estado
   * @param idEstado para filtrar los localidades
   */
  private async consultarLocalidades(idEstado: number): Promise<void> {
    const localidades = idEstado > 0
      ? await this.localidadService.consultarPorEstado(idEstado).toPromise()
      : [];

    this.localidadList = localidades ?? [];
  }

  /**
   * Consulta las colonias para el selector de acuerdo al codigo postal
   * @param codigoPostal para filtrar las colonias
   */
  private async consultarColonias(codigoPostal: string): Promise<void> {
    const colonias = codigoPostal && codigoPostal.length === 5
      ? await this.coloniaService.consultarPorCodigoParaSelector(codigoPostal).toPromise()
      : [];

    this.coloniaList = colonias ?? [];
  }

}
