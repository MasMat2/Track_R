import { Type } from '@angular/core';
import { KeyValueCollection } from '@utils/constants/types';
import { ModalOptions } from 'ngx-bootstrap/modal';
import { CrudFormularioBase } from './crud-formulario-base';

/** Define la configuración necesaria para efectuar una acción en el formulario. */
type FormActionConfig = {
  /** Código de acceso que identifica si el usuario puede ejecutar la acción correspondiente. */
  acceso: string;

  /** Parametros adicionales que se envían al componente del formulario cuando se efectúa la acción correspondiente. */
  params?: KeyValueCollection;
};

/**
 * Identifica las acciones que se pueden realizar en un formulario (agregar y editar).
 * Para que se habilite una acción en el componente @see CrudBase es necesario configurar su propiedad correspondiente.
 */
type FormActions = {
  configAgregar?: FormActionConfig;
  configEditar?: FormActionConfig;
  configConsultar?: FormActionConfig;
};

/** Configuraciones necesarias para un formulario que se muestra en un modal. */
type ModalConfig = {
  /** Referencia al componente que se utiliza como formulario. El componente debe heredar de la clase @see CrudFormularioBase */
  ComponenteFormulario: Type<CrudFormularioBase<any>>;

  /** Configuración del modal. Se pueden utilizar las propiedades definidas en @see GeneralConstant */
  modalConfig: ModalOptions;
};

// Unión de la configuración del formulario y de sus acciones (agregar y editar)
type FormModalConfig = ModalConfig & FormActions;
// type FormScreenConfig = ScreenConfig & FormActions;

/** Interfaz utilizada para definir la configuración de los componentes que hereden de la clase @see CrudBase */
export interface ICrudConfig {
  /**
   * Nombre de la entidad utilizando lenguaje natural (por ejemplo: Punto de Venta en lugar de PuntoVenta).
   * Esta propiedad se utiliza para mostrar títulos, mensajes de confirmación y otros textos relacionados a la entidad.
   */
  nombreEntidad: string;

  /**
   * Artículo utilizado para referirse la entidad.
   * Se utiliza para mostrar mensajes como "¿Está seguro que desea eliminar 'el domicilio' / 'la licencia'?"
   */
  generoGramatical: 'masc' | 'fem';

  /** Nombre de la propiedad que contiene al identificador de la entidad (por ejemplo, 'idDomicilio'). */
  nombrePropiedadId: string;

  /**
   * Configuraciones relacionadas a la eliminación de registros.
   * Si esta propiedad se define, se habilita la opción de eliminar elementos.
   */
  configEliminar?: {
    /** Código de acceso que identifica si el usuario puede eliminar registros de la entidad. */
    acceso: string;

    /**
     * Función que recibe una entidad y retorna un string que representa a dicha entidad.
     * Principalmente utilizado en los modales de confirmación de eliminar.
     */
    elementToString: (element: any) => string;
  };

  formConfig?: FormModalConfig;
}
