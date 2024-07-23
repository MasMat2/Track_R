import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { EstadoSelectorDto } from '@dtos/catalogo/estado-selector-dto';
import { MunicipioFormularioCapturaDto } from '@dtos/catalogo/municipio-formulario-captura-dto';
import { MunicipioFormularioConsultaDto } from '@dtos/catalogo/municipio-formulario-consulta-dto';
import { EstadoService } from '@http/catalogo/estado.service';
import { MunicipioService } from '@http/catalogo/municipio.service';
import { PaisService } from '@http/catalogo/pais.service';
import { Pais } from '@models/catalogo/pais';
import { CrudFormularioBase } from '@sharedComponents/crud/crud-base/crud-formulario-base';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { DROPDOWN_NO_OPTIONS, DROPDOWN_PLACEHOLDER } from '@utils/constants/constants';
import { Observable, map, tap } from 'rxjs';

@Component({
  selector: 'app-municipio-formulario',
  templateUrl: './municipio-formulario.component.html',
})
export class MunicipioFormularioComponent extends CrudFormularioBase<MunicipioFormularioCapturaDto> implements OnInit {

  public DROPDOWN_PLACEHOLDER: string = DROPDOWN_PLACEHOLDER;
  public DROPDOWN_NO_OPTIONS: string = DROPDOWN_NO_OPTIONS;

  public idPais?: number;
  public paises$: Observable<Pais[]>;

  public idEstado?: number;
  public estados$: Observable<EstadoSelectorDto[]>;

  constructor(
    private estadoService: EstadoService,
    private paisService: PaisService,
    private municipioService: MunicipioService,
    mensajeService: MensajeService,
  ) {
    super(mensajeService);
  }

  public ngOnInit(): void {
    super.onInit();

    this.consultarPaises();
  }

  protected consultar(idMunicipio: number): Observable<MunicipioFormularioCapturaDto> {
    return this.municipioService
      .consultarParaFormulario(idMunicipio)
      .pipe(
        tap((municipio: MunicipioFormularioConsultaDto) => {
          this.idEstado = municipio.idEstado;

          this.estadoService
            .consultar(this.idEstado)
            .subscribe((estado) => {
              this.idPais = estado.idPais;
              this.consultarEstados(this.idPais);
            });

        }),
        map((municipio: MunicipioFormularioConsultaDto) => {
          const municipioCaptura = new MunicipioFormularioCapturaDto();
          municipioCaptura.idMunicipio = municipio.idMunicipio;
          municipioCaptura.clave = municipio.clave;
          municipioCaptura.nombre = municipio.nombre;
          municipioCaptura.idEstado = municipio.idEstado;

          return municipioCaptura;
        })
      );
  }

  protected agregar(municipio: MunicipioFormularioCapturaDto): Observable<void> {
    return this.municipioService.agregar(municipio);
  }

  protected editar(municipio: MunicipioFormularioCapturaDto): Observable<void> {
    return this.municipioService.editar(municipio);
  }

  protected override onSubmit(formulario: NgForm): void {
    if (formulario.valid) {
      this.entidad.idEstado = this.idEstado!;
    }

    super.onSubmit(formulario);
  }

  private consultarPaises(): void {
    this.paises$ = this.paisService.consultarGeneral();
  }

  protected consultarEstados(idPais?: number): void {
    if (!idPais) {
      this.estados$ = new Observable<EstadoSelectorDto[]>();
      this.idEstado = undefined;
    }
    else {
      this.estados$ = this.estadoService.consultarPorPaisParaSelector(idPais)
        .pipe(tap((estados) => {}));
    }
  }
}
