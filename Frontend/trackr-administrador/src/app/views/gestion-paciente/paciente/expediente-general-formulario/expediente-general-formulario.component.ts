import { EntidadEstructuraService } from '../../../../shared/http/gestion-entidad/entidad-estructura.service';
import { BusquedaExpedienteComponent } from '../busqueda-expediente/busqueda-expediente.component';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpedientePadecimientoDTO } from '@dtos/seguridad/expediente-padecimiento-dto';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { ExpedienteWrapper } from '@dtos/seguridad/expediente-wrapper';
import { ExpedientePadecimientoService } from '@http/seguridad/expediente-padecimiento.service';
import { ExpedienteTrackrService } from '@http/seguridad/expediente-trackr.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Domicilio } from '@models/seguridad/domicilio';
import { ExpedienteTrackR } from '@models/seguridad/expediente-trackr';
import { Usuario } from '@models/seguridad/usuario';
import { EncryptionService } from '@services/encryption.service';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import * as Utileria from '@utils/utileria';
import { BsModalService } from 'ngx-bootstrap/modal';
import { from, lastValueFrom, of } from 'rxjs';
import { first } from 'rxjs/operators';
import { GeneroSelectorDTO } from './genero-selector';
import { UsuarioFormularioComponent } from 'src/app/views/configuracion-general/catalogo/usuario/usuario-formulario/usuario-formulario.component';

/**
 * Componente de formulario para el manejo de expedientes.
 * Permite agregar, editar y eliminar expedientes.
 */
@Component({
  selector: 'app-expediente-general-formulario',
  templateUrl: './expediente-general-formulario.component.html',
  styleUrls: ['./expediente-general-formulario.component.scss']
})
export class ExpedienteGeneralFormularioComponent implements OnInit {

  // Modelos
  public expedienteWrapper: ExpedienteWrapper = new ExpedienteWrapper();

  public expediente: ExpedienteTrackR = new ExpedienteTrackR();
  public domicilio: Domicilio = new Domicilio();
  public padecimientos: ExpedientePadecimientoDTO[] = [];
  public paciente: Usuario = new Usuario();
  // Variables
  public btnSubmit: boolean = false;
  public accion: string;
  public idUsuario: number;

  // Selectores
  public padecimientoList: ExpedientePadecimientoSelectorDTO[] = [];
  public generoList: GeneroSelectorDTO[] = [];

  // Mensajes
  public MENSAJE_AGREGAR = 'El expediente ha sido agregado';
  public MENSAJE_EDITAR = 'El expediente ha sido modificado';
  
  // Configuraciones
  public placeHolderSelect = GeneralConstant.PLACEHOLDER_DROPDOWN;
  public placeHolderNoOptions = GeneralConstant.PLACEHOLDER_DROPDOWN_NO_OPTIONS;

  public filtro: string;
  public btnSubmitBusqueda = false;


  constructor(
    private expedienteTrackrService: ExpedienteTrackrService,
    private entidadEstructuraService: EntidadEstructuraService,
    private usuarioService: UsuarioService,
    private route: ActivatedRoute,
    private router: Router,
    private encryptionService: EncryptionService,
    private formularioService: FormularioService,
    private modalService: BsModalService,
    private modalMensajeService: MensajeService,
    public bsModalRef: BsModalRef,
  ) { }
  /**
   * Inicializa el componente, configura los parámetros de la URL y 
   * verifica si hay un usuario existente (si es Editar).
   * Si existe un usuario, consulta su expediente.
   */
  public async ngOnInit(): Promise<void> { 
    await Promise.all([
      this.consultarGeneros(),
      this.consultarPadecimientos(),
    ]);

    await this.obtenerParametrosURL();
  }

  /**
   * Obtiene los parámetros de la URL y los asigna a las variables del componente.
   */
  private async obtenerParametrosURL(): Promise<void> {
    const queryParams = await lastValueFrom(this.route.queryParams.pipe(first()));
    const params = this.encryptionService.readUrlParams(queryParams);
    this.idUsuario = Number(params.i);
    if(this.idUsuario > 0){
      this.consultarExpedienteWrapper()
      this.accion = GeneralConstant.MODAL_ACCION_EDITAR;
    }
    else{
      this.agregarPadecimiento()
      this.accion = GeneralConstant.MODAL_ACCION_AGREGAR
    }
  }

  protected agregarUsuario(){
    this.bsModalRef = this.modalService.show(UsuarioFormularioComponent, {
			...GeneralConstant.CONFIG_MODAL_LARGE,
		});
    this.bsModalRef.content.accion = GeneralConstant.MODAL_ACCION_AGREGAR;
    this.bsModalRef.content.onClose = (idUsuario: number) => {
      this.bsModalRef.hide();
      if (typeof (idUsuario) == 'number' && idUsuario) {
        this.idUsuario = idUsuario;
        this.consultarExpedienteWrapper();
      }
    };
  }
  
  /**
   * Envía el formulario, verifica si es válido y si no, valida los campos requeridos.
   * @param {NgForm} formulario - El formulario que se va a enviar.
   */
  protected enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;

    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    this.expedienteWrapper.expediente = this.expediente;

    this.expedienteWrapper.padecimientos = this.padecimientos;
    this.expedienteWrapper.paciente.idUsuario = this.paciente.idUsuario;
    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      if(this.expedienteWrapper.expediente.idExpediente > 0){
        this.editar();
      }
      else{
        this.agregar();
      }
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      this.editar();
    }
    this.consultarExpedienteWrapper();
  }

  /**
   * Envía una solicitud para agregar un nuevo expediente.
   */
  protected agregar(): void {
    lastValueFrom(this.expedienteTrackrService.agregarWrapper(this.expedienteWrapper))
    .then((response) => {
      if (response) {
        this.modalMensajeService.modalExito(this.MENSAJE_AGREGAR);
        this.btnSubmit = false;
      }
    });
    this.btnSubmit = false;
  }
  
  /**
   * Envía una solicitud para editar un expediente existente.
   */
  protected editar(): void {
    lastValueFrom(this.expedienteTrackrService.editarWrapper(this.expedienteWrapper))
    .then((response) => {
      if (response) {
        this.modalMensajeService.modalExito(this.MENSAJE_EDITAR);
        this.btnSubmit = false;
      }
      
    })
    .catch((error) => {  });
  }

  /**
   * Cancela la acción actual y redirige al usuario a la página de gestión del paciente.
   */
  protected cancelar(): void {
    this.router.navigate(['/administrador/gestion-paciente/paciente']);
  }

  /**
   * Agrega un nuevo padecimiento al expediente.
   */
  protected agregarPadecimiento(){
    const padecimiento = new ExpedientePadecimientoDTO();
    padecimiento.idPadecimiento = 0;
    //padecimiento.fechaDiagnostico = new Date();
    this.padecimientos = [...this.padecimientos, padecimiento ];
    // this.padecimientos.push(padecimiento); NO FUNCIONA
  }

  trackByFunction(index: number, item: ExpedientePadecimientoDTO): number {
    return index; // o item.id si existe tal propiedad
  }
  
  /**
   * Elimina un padecimiento específico del expediente.
   * @param {number} index - El índice del padecimiento a eliminar.
   */
  protected eliminarPadecimiento(index: number){
    if(index < 0 || index >= this.padecimientos.length){
      return;
    }
    this.padecimientos.splice(index, 1);

    if(this.padecimientos.length === 0){
      this.agregarPadecimiento();
    }
  }

  /**
   * Consulta y asigna los datos de expediente del usuario actual.
   */
  private async consultarExpedienteWrapper() {
    lastValueFrom(this.expedienteTrackrService.consultarWrapperPorUsuario(this.idUsuario))
    .then((expedienteWrapper: ExpedienteWrapper) => {

      // Asigna el objeto al wrapper
      this.paciente = expedienteWrapper.paciente || new Usuario();
      this.domicilio = this.obtenerPacienteDomicilio(this.paciente);
      this.expediente = this.obtenerExpediente(expedienteWrapper.expediente);
      

      this.padecimientos = expedienteWrapper.padecimientos.map(padecimiento => {
        let padecimientoDTO = new ExpedientePadecimientoDTO();
        padecimientoDTO.idExpedientePadecimiento = padecimiento.idExpedientePadecimiento;
        padecimientoDTO.idPadecimiento = padecimiento.idPadecimiento;
        padecimientoDTO.fechaDiagnostico = new Date(padecimiento.fechaDiagnostico);
        
        return padecimientoDTO;
      });


      // Calcular el campo edad
      this.calcularEdad();

      // Comprobar si tiene padecimientos, si no hay, agregar uno por default
      if(this.padecimientos == null || this.padecimientos.length === 0){
        this.agregarPadecimiento();
      }
    })
  }

  private obtenerExpediente(expediente: ExpedienteTrackR): ExpedienteTrackR{
    if(expediente == null){
      expediente = new ExpedienteTrackR();
    }
    let expedienteTrackR = expediente;
    if(expediente.fechaNacimiento != null){
      expedienteTrackR.fechaNacimiento = new Date(expediente.fechaNacimiento);
    }
    else{
      expedienteTrackR.fechaNacimiento = new Date();
    }
    if(expediente.fechaAlta != null){
      expedienteTrackR.fechaAlta = new Date(expediente.fechaAlta);
    }
    else{
      expedienteTrackR.fechaAlta = new Date();
    }

    return expedienteTrackR;
  }

  private obtenerPacienteDomicilio(paciente: Usuario): Domicilio{
    let domicilio = new Domicilio();
    domicilio.idPais = paciente.idPais;
    domicilio.codigoPostal = paciente.codigoPostal;
    domicilio.idEstado = paciente.idEstado;
    domicilio.idMunicipio = paciente.idMunicipio;
    domicilio.idLocalidad = paciente.idLocalidad;
    domicilio.idColonia = paciente.idColonia;
    domicilio.calle = paciente.calle;
    domicilio.numeroExterior = paciente.numeroExterior;
    domicilio.numeroInterior = paciente.numeroInterior;
    // TODO: Implementar EntreCalles
    return domicilio;
  }

  
  protected calcularEdad(){
    let fechaNacimiento = new Date(this.expediente.fechaNacimiento);
    let edadObject = Utileria.diferenciaFechas(fechaNacimiento, new Date());
    let edadString = edadObject.years + ' años, ' + edadObject.months + ' meses, ' + edadObject.days + ' días';
    this.expediente.edad = edadString;
  }

  /**
   * Consulta los padecimientos para el selector. 
   */
  private consultarPadecimientos(){
    return lastValueFrom(this.entidadEstructuraService.consultarPadecimientosParaSelector())
    .then((padecimientos: ExpedientePadecimientoSelectorDTO[]) => {
      this.padecimientoList = padecimientos;
    })
  }

  /**
   * Consulta los géneros para el selector
   */
  private consultarGeneros() {
    this.generoList = [
      {
        idGenero: 1,
        nombre: "Hombre"
      },
      {
        idGenero: 2,
        nombre: "Mujer"
      }
    ];

    // NOTA: temporal para simular asincronidad mientras se implementa el catálogo
    return lastValueFrom(of(this.generoList));
  }

  
  public async buscar(): Promise<void> {
    let resultadoUsuarios: Usuario[] = [];

    this.btnSubmitBusqueda = true;

    await lastValueFrom(
    this.usuarioService.consultarPorNombre(this.filtro)).then(
      (data) => { 
        resultadoUsuarios = data != null ? data : [];
        }
    )
    .catch(
      (error) => {
        this.btnSubmitBusqueda = false;
      }
    );

    if (resultadoUsuarios.length == 0) {
      this.modalMensajeService.modalError("No se encontraron resultados");
      this.btnSubmitBusqueda = false;
    }
    else if (resultadoUsuarios.length == 1) {
      this.paciente = resultadoUsuarios[0];
      this.idUsuario = this.paciente.idUsuario;
      this.btnSubmitBusqueda = false;
      this.filtro = "";
    }
    else {
      let elementosBusqueda: Usuario[] = resultadoUsuarios.map((elem) => {
        let item = new Usuario();

        item.idUsuario = elem.idUsuario;
        item.correo = elem.correo;
        item.nombreCompleto = elem.nombreCompleto;

        return item;
      });

      const initialState = {
        accion: GeneralConstant.MODAL_ACCION_EDITAR,
        elementos: elementosBusqueda
      };

      this.bsModalRef = this.modalService.show(
        BusquedaExpedienteComponent,
        {
          initialState,
          ...GeneralConstant.CONFIG_MODAL_DEFAULT,
        }
      );

      this.bsModalRef.content.onClose = (seleccionado: Usuario) => {
        if (seleccionado) {
          this.paciente = resultadoUsuarios.find((elem) => elem.idUsuario == seleccionado.idUsuario)!;
          this.idUsuario = this.paciente.idUsuario;
          this.consultarExpedienteWrapper();
          this.filtro = "";
        }

        this.btnSubmitBusqueda = false;
        this.bsModalRef.hide();
      };
    }
  }

}
