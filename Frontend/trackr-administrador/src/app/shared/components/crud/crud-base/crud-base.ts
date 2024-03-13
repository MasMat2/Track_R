import { AccesoService } from '@http/seguridad/acceso.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { KeyValueCollection } from '@utils/constants/types';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ICrudConfig } from './crud-config';
import { Observable } from 'rxjs';
import { GRID_ACTION } from '@utils/constants/grid';
import { FORM_ACTION } from '@utils/constants/constants';
import { ICONO } from '@utils/constants/font-awesome-icons';

/**
 * Clase abstracta que define la lógica básica para realizar operaciones CRUD. Para utilizar esta clase se debe generar
 * un componente que herede de ella.
 * Es recomendable utilizar el componente @see CatalogoBaseComponent en el template del componente para simplificar
 * el HTML.
 */
export abstract class CrudBase<GridDto> {
  // Variables de Entrada
  protected crudConfig: ICrudConfig;

  // Variables
  protected registros$: Observable<GridDto[]>;
  protected tieneAccesoAgregar$: Observable<boolean>;

  protected abstract consultarGrid(): Observable<GridDto[]>;
  protected abstract eliminar(id: number): Observable<void>;

  constructor(
    protected accesoService: AccesoService,
    protected bsModalService: BsModalService,
    protected mensajeService: MensajeService
  ) {}

  onInit(): void {
    this.actualizarGrid();
    this.consultarAccesoAgregar();
  }

  private actualizarGrid(): void {
    this.registros$ = this.consultarGrid();
  }

  protected consultarAccesoAgregar(): void {
    // Revisar que exista la configuración del formularios
    if (!this.crudConfig.formConfig) {
      return;
    }

    // Obtener el objeto de configuración dependiendo de la acción
    const configAgregar = this.crudConfig.formConfig.configAgregar;

    // Revisar que exista la configuración de la acción
    if (!configAgregar) {
      // throw new Error(`No se configuró la propiedad "configAgregar" en el objeto "crudConfig"`);
      return;
    }

    this.tieneAccesoAgregar$ = this.accesoService.tieneAcceso(configAgregar.acceso);
  }

  protected onGridClick(gridData: any): void {
    const nombrePropiedadId = this.crudConfig.nombrePropiedadId as keyof GridDto;
    const id = gridData.data[nombrePropiedadId];

    if (typeof id !== 'number') {
      throw new Error(
        `No se pudo obtener el id del elemento. La propiedad "${this.crudConfig.nombrePropiedadId}" no es un número.`
      );
    }

    const actions = {
      [GRID_ACTION.Editar as string]: () => this.editar(id),
      [GRID_ACTION.Eliminar as string]: () => this.confirmarEliminacion(gridData.data, id),
    }

    actions[gridData.accion]();
  }

  /**
   * Muestra el formulario. Dependiendo de la configuración especificada, el formulario se mostrará en un modal
   * o se redireccionará al usuario a la ruta correspondiente del modal.
   * Al formulario se le agregan algunos atributos por defecto (acción, id, nombre de la entidad) y los parámetros
   * definidos en @see ICrudConfig.
   */
  private openForm(accion: string, idElemento?: number): void {
    // Revisar que exista la configuración del formularios
    if (!this.crudConfig.formConfig) {
      throw new Error(
        'No se configuró la propiedad "formConfig" en el objeto "crudConfig"'
      );
    }

    // Obtener el objeto de configuración dependiendo de la acción
    const actionConfig =
      accion === FORM_ACTION.Agregar
        ? this.crudConfig.formConfig.configAgregar
        : this.crudConfig.formConfig.configEditar;

    // Revisar que exista la configuración de la acción
    if (!actionConfig) {
      const propiedad: string =
        accion === FORM_ACTION.Editar
          ? 'configEditar'
          : 'configAgregar';

      throw new Error(
        `No se configuró la propiedad "${propiedad}" en el objeto "crudConfig"`
      );
    }

    // Agregar parámetros por defecto y parámetros definidos por el usuario
    let params: KeyValueCollection = {
      accion: accion,
      idEntidad: idElemento,
      nombreEntidad: this.crudConfig.nombreEntidad,
      generoGramatical: this.crudConfig.generoGramatical,
      ...actionConfig.params,
    };

    // Agregar la propiedad onClose para cerrar el modal
    params['onClose'] = (refrescarGrid: boolean) => {
      if (refrescarGrid) {
        this.actualizarGrid();
      }

      bsModalRef.hide();
    };

    // Abrir el formulario en un modal
    const bsModalRef = this.bsModalService.show(
      this.crudConfig.formConfig.ComponenteFormulario,
      {
        initialState: params,
        ...this.crudConfig.formConfig.modalConfig,
      }
    );
  }

  /** Abre el formulario en modo Agregar */
  protected agregar(): void {
    this.openForm(FORM_ACTION.Agregar);
  }

  /** Abre el formulario en modo Editar */
  private editar(idElemento: number): void {
    this.openForm(FORM_ACTION.Editar, idElemento);
  }

  private confirmarEliminacion(elemento: GridDto, id: number): void {
    if (!this.crudConfig.configEliminar) {
      throw new Error(
        `No se configuró la propiedad "configEliminar" en el objeto "crudConfig"`
      );
    }

    const tituloModal: string = `Eliminar ${this.crudConfig.nombreEntidad}`;

    const [articulo, accion]: [string, string] =
      this.crudConfig.generoGramatical === 'masc'
        ? ['El', 'eliminado']
        : ['La', 'eliminada'];

    const nombreEntidad: string = `${articulo.toLowerCase()} ${this.crudConfig.nombreEntidad.toLowerCase()}`;
    const descripcion: string = this.crudConfig.configEliminar.elementToString(elemento);
    const mensajeConfirmacion: string = `¿Desea eliminar ${nombreEntidad}: <strong>${descripcion}</strong>?`;

    this.mensajeService
      .modalConfirmacion(
        mensajeConfirmacion,
        tituloModal,
        ICONO.Cruz
      )
      .then(() => {
        const mensajeExito: string = `${articulo} ${this.crudConfig.nombreEntidad.toLowerCase()} ha sido ${accion}`;

        this.eliminar(id)
          .subscribe(() => {
            this.mensajeService.modalExito(mensajeExito);
            this.actualizarGrid();
          });
      })
      .catch(() => {});
  }
}
