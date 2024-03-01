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

@Component({
  selector: 'app-seccion-tabla',
  templateUrl: './seccion-tabla.component.html',
  styleUrls: ['./seccion-tabla.component.scss']
})
export class SeccionTablaComponent implements OnInit {
  @Input() public entidadEstructuraSeccion: EntidadEstructura;
  @Input() public idTabla: number;
 
 public campos: SeccionCampo = new SeccionCampo();
 public muestras : ExpedienteMuestrasGridDTO[];
  public readonly EDITAR: string = "Editar";
  public readonly AGREGAR: string = "Agregar";

  public seleccionado?: ExpedienteMuestrasGridDTO;

  constructor(
    private modalService: BsModalService,
    private mensajeService: MensajeService,
    private entidadEstructuraTablaValorService: EntidadEstructuraTablaValorService,
  ) { }

  ngOnInit() {
    this.entidadEstructuraSeccion.campos.sort((a, b) => a.orden - b.orden);
    this.obtenerMuestrasGrid();
    this.campos.descripcion = 'Fuera De Rango';
    this.campos.clave = 'F-Rango';
    this.entidadEstructuraSeccion.campos.push(this.campos);
  }

  private obtenerMuestrasGrid() {
    this.entidadEstructuraTablaValorService.consultarGridMuestras(this.idTabla).subscribe((data) => {
      this.muestras = data;
    })
  }

  public seleccionar(registro: ExpedienteMuestrasGridDTO): void {
    this.seleccionado = registro;
  }

  private actualizarGrid(): void {
    const subscription = this.entidadEstructuraTablaValorService
      .consultarPorPestanaSeccion(this.entidadEstructuraSeccion.idEntidadEstructura, this.idTabla)
      .subscribe({
        next: (registros: RegistroTabla[]) => {
          subscription.unsubscribe();
          this.entidadEstructuraSeccion.registrosTabla = registros;
        }
      });
  }


  public obtenerValor(claveColumna: string, valores: ExpedienteMuestrasRegistroDTO , firstRegistro : boolean , lastColumna : boolean) : string  {

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

    if(claveColumna == valores.claveCampo)
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
      idPestanaSeccion: this.entidadEstructuraSeccion.idEntidadEstructura,
      nombreSeccion: this.entidadEstructuraSeccion.nombre,
      campos: this.entidadEstructuraSeccion.campos,
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


        const v = this.entidadEstructuraSeccion.campos.find(c => c.clave === campo.claveCampo);
        const fecha = this.entidadEstructuraSeccion.campos.find(c => c.clave === 'ME-fecha');
        const hora = this.entidadEstructuraSeccion.campos.find(c => c.clave === 'ME-hora');

        
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
      idPestanaSeccion: this.entidadEstructuraSeccion.idEntidadEstructura,
      nombreSeccion: this.entidadEstructuraSeccion.nombre,
      campos: this.entidadEstructuraSeccion.campos,
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
    this.entidadEstructuraSeccion.campos.forEach(c => {
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


      const v = this.entidadEstructuraSeccion.campos.find(c => c.clave === campo.claveCampo);

      if(v){
      
        v.idEntidadEstructuraValor = campo.idEntidadEstructuraTablaValor;
        v.valor = v ? campo.valor : '';
        v.habilitado = v === undefined ? false : true;
      }

    }

    const campos = this.entidadEstructuraSeccion.campos.filter(
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
        claveCampo: campo.clave,
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
