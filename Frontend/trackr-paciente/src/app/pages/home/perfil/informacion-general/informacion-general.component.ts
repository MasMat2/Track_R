import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { HeaderComponent } from '../../layout/header/header.component';
import { InformacionGeneralDto } from '@models/perfil/informacion-general-dto';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Observable, lastValueFrom, of, tap } from 'rxjs';
import { PaisSelectorDto } from '@models/catalogo/pais-selector-dto';
import { EstadoSelectorDto } from '@models/catalogo/estado-selector-dto';
import { municipioSelectorDto } from '@models/catalogo/municipio-selector-dto';
import { LocalidadSelectorDto } from '@models/catalogo/localidad-selector-dto';
import { ColoniaSelectorDto } from '@models/catalogo/colonia-selector-dto';
import { PaisService } from '@http/catalogo/pais.service';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { LocalidadService } from '@http/catalogo/localidad.service';
import { ColoniaService } from '@http/catalogo/colonia.service';
import { PadecimientoDto } from '@models/perfil/padecimiento-dto';
import * as Utileria from '@utils/utileria';
import { GeneroSelectorDto } from '@models/catalogo/genero-selector-dto';


@Component({
  selector: 'app-informacion-general',
  templateUrl: './informacion-general.component.html',
  styleUrls: ['./informacion-general.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    HeaderComponent,
  ]
})
export class InformacionGeneralComponent implements OnInit {

  protected informacionUsuario$: Observable<InformacionGeneralDto>;
  protected infoUsuario: InformacionGeneralDto;
  protected edadUsuario: string;

  protected nuevoPadecimiento: PadecimientoDto = new PadecimientoDto();

   public paisList: PaisSelectorDto[] = [];
   public estadoList: EstadoSelectorDto[] = [];
   public municipioList: municipioSelectorDto[] = [];
   public localidadList: LocalidadSelectorDto[] = [];
   public coloniaList: ColoniaSelectorDto[] = [];
   public generoList: GeneroSelectorDto[] = [];


  public idPais: number = 0;
  public idEstado: number = 0;
  public idMunicipio: number = 0;
  public idLocalidad: number = 0;
  public idColonia: number = 0;
  public codigoPostal: string = '';

  constructor( 
    private usuarioService: UsuarioService,
    private paisService: PaisService,
    private estadoService: EstadoService,
    private municipioService: MunicipioService,
    private localidadService: LocalidadService,
    private coloniaService: ColoniaService,
  ) {  }
  
  ngOnInit(){
    this.consultarGeneros();
    //this.consultarPadecimientos();
    this.obtenerUsuario();
  }

  public async onChangePais() {
    this.infoUsuario.idEstado = 0;
    this.infoUsuario.codigoPostal = '';

    await this.consultarEstados(this.infoUsuario.idPais);

    this.onChangeEstado();
  }

  public async onChangeEstado() {
    this.infoUsuario.idMunicipio = 0;
    this.infoUsuario.idLocalidad = 0;
    this.infoUsuario.idColonia = 0;


    await Promise.all([
      this.consultarMunicipios(this.infoUsuario.idEstado),
      this.consultarLocalidades(this.infoUsuario.idEstado),
      this.consultarColonias(this.infoUsuario.codigoPostal)

    ]);
  }

  public async onChangeCodigoPostal() {
    this.infoUsuario.idColonia = 0;

    await this.consultarColonias(this.infoUsuario.codigoPostal);
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


  private consultarGeneros() {
    this.generoList = [
      {
        idGenero: 1,
        descripcion: "Hombre"
      },
      {
        idGenero: 2,
        descripcion: "Mujer"
      },
      {
        idGenero: 3,
        descripcion: "Otro"
      },
    ];

    return lastValueFrom(of(this.generoList));
  }

  // private consultarPadecimientos(){
  //   return lastValueFrom(this.entidadEstructuraService.consultarPadecimientosParaSelector())
  //   .then((padecimientos: ExpedientePadecimientoSelectorDTO[]) => {
  //     this.padecimientoList = padecimientos;
  //   })
  // } 

  private obtenerUsuario(){

    this.informacionUsuario$ = this.usuarioService.consultarInformacionGeneral().pipe(
      tap(
        (data) => {
          this.infoUsuario = data;

          this.consultarPaises();
          this.consultarEstados(this.infoUsuario.idPais);
          this.consultarMunicipios(this.infoUsuario.idEstado);
          this.consultarLocalidades(this.infoUsuario.idEstado);
          this.consultarColonias(this.infoUsuario.codigoPostal);
          this.calcularEdad();
        }
      )
    );

  }

  private actualizarInformacionUsuario(informacion: InformacionGeneralDto){
    this.usuarioService.actualizarInformacionGeneral(informacion);
  }
  
  protected calcularEdad(){
    let fechaNacimiento = new Date(this.infoUsuario.fechaNacimiento);
    let edadObject = Utileria.diferenciaFechas(fechaNacimiento, new Date());
    let edadString = edadObject.years + ' años, ' + edadObject.months + ' meses, ' + edadObject.days + ' días';
    this.edadUsuario = edadString;
  }

  protected async enviarFormulario(formulario: NgForm){

    if(formulario.invalid){
      Utileria.validarCamposRequeridos(formulario);
    }
    this.actualizarInformacionUsuario(this.infoUsuario);
  }


  protected agregarPadecimiento(){
    console.log('padecimiento agregado')
  }

  protected eliminarPadecimiento(){
    console.log('padecimiento eliminado')
  }


}
