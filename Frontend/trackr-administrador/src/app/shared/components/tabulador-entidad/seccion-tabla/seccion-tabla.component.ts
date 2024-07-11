import { Component, Input, OnInit } from '@angular/core';
import { EntidadTablaRegistroDto, TablaValorDto } from '@dtos/gestion-entidades/entidad-tabla-registro-dto';
import { EntidadEstructuraTablaValorService } from '@http/gestion-entidad/entidad-estructura-tabla-valor.service';
import { EntidadEstructura } from '@models/gestion-entidad/entidad-estructura';
import { EntidadEstructuraTablaValor } from '@models/gestion-entidad/entidad-estructura-tabla-valor';
import { RegistroTabla } from '@models/gestion-entidad/registro-tabla';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { GeneralConstant } from '@utils/general-constant';
import { BsModalService } from 'ngx-bootstrap/modal';
import { SeccionTablaFormularioComponent } from '../seccion-tabla-formulario/seccion-tabla-formulario.component';
import * as moment from 'moment';
import { ExpedienteMuestrasGridDTO } from '@dtos/gestion-expediente/expediente-muestras-grid-dto';
import { ExpedienteMuestrasRegistroDTO } from '@dtos/gestion-expediente/expediente-muestras-registros-dto';
import { toDateString } from '../../../utils/utileria';
import { SeccionCampo } from '@models/gestion-entidad/seccion-campo';
import { first } from 'rxjs/operators';
import { sum } from 'lodash';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { Dominio } from '@models/catalogo/dominio';

@Component({
  selector: 'app-seccion-tabla',
  templateUrl: './seccion-tabla.component.html',
  styleUrls: ['./seccion-tabla.component.scss']
})
export class SeccionTablaComponent implements OnInit {
  @Input() public idTabla: number;
 
 public campos: SeccionCampo[] = [];
 public muestras : ExpedienteMuestrasGridDTO[];
 public seccionCampoList : SeccionCampo[] = [];
  public readonly EDITAR: string = "Editar";
  public readonly AGREGAR: string = "Agregar";

  public seleccionado?: ExpedienteMuestrasGridDTO;

  constructor(
    private modalService: BsModalService,
    private mensajeService: MensajeService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
    private entidadEstructuraService : EntidadEstructuraService
  ) { }

  ngOnInit() {
    this.valoresVariablesPadecimiento();
  }

  //Determina las variables de los padecimientos que tiene el usuario
  private valoresVariablesPadecimiento() {
    this.entidadEstructuraService.valoresVariablesPadecimiento(this.idTabla).subscribe((data) => {
  
        this.agregarFechaHora();
        this.seccionCampoList = this.seccionCampoList.concat(data);
        this.campos = this.campos.concat(data);
        this.agregarFueraDeRango();
        this.obtenerMuestrasGrid();
    });
  }
    private agregarFechaHora() {
        var fecha = new SeccionCampo();
        fecha.idDominioNavigation = new Dominio();
        fecha.clave = 'ME-fecha';
        fecha.descripcion = 'Fecha de Muestra';
        fecha.grupo = 'Fecha';
        fecha.idDominioNavigation.tipoCampo = 'Date';
        fecha.idDominioNavigation.longitudMaxima = 10;
        fecha.requerido = true;
        fecha.habilitado = true;

        fecha.fila = 1;
        fecha.orden = 1;

        var hora = new SeccionCampo();
        hora.idDominioNavigation = new Dominio();
        hora.clave = 'ME-hora';
        hora.descripcion = 'Hora de Muestra';
        hora.grupo = 'Fecha';
        hora.idDominioNavigation.tipoCampo = 'Time';
        hora.idDominioNavigation.longitudMaxima = 5;
        hora.fila = 1;
        hora.orden = 1;
        hora.requerido = true;
        

        this.seccionCampoList.unshift(fecha, hora);
        this.campos.unshift(fecha, hora);
    }
    
  private agregarFueraDeRango() {
    var fueraDeRango = new SeccionCampo();
    fueraDeRango.clave = 'F-Rango';
    fueraDeRango.descripcion = 'Fuera de Rango';
    fueraDeRango.orden = 1000;
    this.seccionCampoList.push(fueraDeRango);
  }

  private obtenerMuestrasGrid() {
    this.entidadEstructuraTablaValorService.consultarGridMuestras(this.idTabla).subscribe((data) => {
      this.muestras = data;
    })
  }

  public seleccionar(registro: ExpedienteMuestrasGridDTO): void {
    this.seleccionado = registro;
  }


  public obtenerValor(claveColumna: string, valores: ExpedienteMuestrasRegistroDTO , firstRegistro : boolean , lastColumna : boolean, idSeccionValor : number) : string  {
    if(lastColumna && firstRegistro)
      return ' _ ';

    if(claveColumna == 'ME-fecha' && valores.fechaMuestra != undefined && firstRegistro)
    {
      
      const fechaMuestra = new Date(valores.fechaMuestra);
      const fechaFormateada = fechaMuestra.toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });
      return fechaFormateada ?? '';
    }
    
    if(claveColumna == 'ME-hora' && firstRegistro)
    {
      const fechaMuestra = new Date(valores.fechaMuestra);

      const horas = fechaMuestra.getHours().toString().padStart(2, '0');
      const minutos = fechaMuestra.getMinutes().toString().padStart(2, '0');

      const horaYMinuto = `${horas}:${minutos}`;
      return horaYMinuto;
    }

    if(idSeccionValor == valores.idSeccionVariable)
            return valores.valor;
      
    return '';        

  }

  public obtenerValorEstilo(valor: ExpedienteMuestrasRegistroDTO , first : boolean) : string
  {   
    if(first)
     return valor.fueraDeRango ? 'fuera-de-rango' : 'no-fuera-de-rango';
    else
      return '';
  }

  public agregar(): void {
    this.limpiarCampos(this.AGREGAR);
    const initialState = {
      accion: 'Agregar',
      idTabla: this.idTabla,
      nombreSeccion: 'Agregar muestra',
      campos: this.campos,
    };

    const modalRef = this.modalService.show(
      SeccionTablaFormularioComponent,
      {
        initialState,
        ...GeneralConstant.CONFIG_MODAL_DEFAULT
      }
    );

    modalRef.content?.cerrar.subscribe(() => {
      modalRef.hide();
      this.obtenerMuestrasGrid();
    });
  }

  public editar(): void {
    
    if (this.seleccionado && this.seleccionado.registro) {

      this.limpiarCampos(this.EDITAR);


      for (const campo of this.seleccionado.registro) {


        const v = this.seccionCampoList.find(c => c.idSeccionCampo === campo.idSeccionVariable);
        const fecha = this.seccionCampoList.find(c => c.clave === 'ME-fecha');
        const hora = this.seccionCampoList.find(c => c.clave === 'ME-hora');

        
        if(v){
        
          v.idEntidadEstructuraValor = campo.idEntidadEstructuraTablaValor;
          v.valor = v ? campo.valor : '';
          v.habilitado = v === undefined ? false : true;
        }else{

        }
        if(fecha) {
            let fechaMuestra = new Date(campo.fechaMuestra);
            let fechaFormato = fechaMuestra.toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });
            let horaFormato = fechaMuestra.toLocaleTimeString('es-ES', { hour: '2-digit', minute: '2-digit' });
            fecha.valor = hora ? `${fechaFormato} ${horaFormato}` : '';
        }
        if(hora) {
            let fechaMuestra = new Date(campo.fechaMuestra);
            let horaFormato = fechaMuestra.toLocaleTimeString('es-ES', { hour: '2-digit', minute: '2-digit' });
            hora.valor = fecha ? horaFormato : '';
        }
      
      }


    const initialState = {
      accion: 'Editar',
      numeroRegistro: this.seleccionado.idEntidadEstructuraTablaValor,
      idTabla: this.idTabla,
      idPestanaSeccion: this.seleccionado.idEntidadEstructura,
      nombreSeccion: 'Editar registro',
      campos: this.campos,
    };

    const modalRef = this.modalService.show(
      SeccionTablaFormularioComponent,
      {
        initialState,
        ...GeneralConstant.CONFIG_MODAL_DEFAULT
      }
    );

    modalRef.content?.cerrar.subscribe(() => {
      modalRef.hide();
      this.obtenerMuestrasGrid();
    });
  }else{
    return;
  }

  }

  private limpiarCampos(accion : string): void {
    this.campos.forEach(c => {
      c.valor = '';
      c.habilitado = accion == "Editar"? false : true;
      if(accion == "Agregar")
        c.requerido = true;
    });
  }

  public async eliminar(): Promise<void> {
    if (!this.seleccionado) {
      return;
    }

    const confirmacion = await this.mensajeService
      .modalConfirmacion(
        '¿Desea eliminar el registro <strong>' + this.seleccionado.idEntidadEstructuraTablaValor + '</strong>?',
        'Eliminar Registro',
        GeneralConstant.ICONO_CRUZ
      )
      .then(_ => true)
      .catch(_ => false);

    if (!confirmacion) {
      return;
    }

    for (const campo of this.seleccionado.registro) {


      const v = this.seccionCampoList.find(c => c.idSeccionCampo === campo.idSeccionVariable);

      if(v){
      
        v.idEntidadEstructuraValor = campo.idEntidadEstructuraTablaValor;
        v.valor = v ? campo.valor : '';
        v.habilitado = v === undefined ? false : true;
      }

    }

    const campos = this.seccionCampoList.filter(
      (c) => c.idDominioNavigation?.tipoCampo !== 'Select Múltiple' &&
        c.valor && c.valor !== '' && c.idEntidadEstructuraValor > 0
    );


    const registro: EntidadTablaRegistroDto = {
      numero: this.seleccionado.idEntidadEstructuraTablaValor,
      idEntidadEstructura: this.seleccionado.idEntidadEstructura,
      idTabla: this.idTabla,
      valores: [],
    };

    registro.valores = campos.map((campo: SeccionCampo) => {
      const valor: TablaValorDto = {
        idEntidadEstructuraTablaValor: campo.idEntidadEstructuraValor,
        idSeccionVariable: campo.idSeccionCampo,
        valor: campo.valor?.toString() ?? '',
        fueraDeRango: false,
        fechaMuestra: new Date()
      };
    
      return valor;
    });



    const subscription = this.entidadEstructuraTablaValorService
      .eliminar(registro)
      .subscribe({
        next: () => {
          this.mensajeService.modalExito('Se eliminó el registro correctamente');
          this.obtenerMuestrasGrid();
          subscription.unsubscribe();
        }
      });
  }

}
