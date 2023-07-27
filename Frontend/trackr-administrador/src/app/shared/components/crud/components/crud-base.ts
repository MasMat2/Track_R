import { Router } from "@angular/router";
import { AccesoService } from "@http/seguridad/acceso.service";
import { EncryptionService } from "@services/encryption.service";
import { MensajeService } from "@sharedComponents/mensaje/mensaje.service";
import { KeyValueCollection } from "@utils/constants/types";
import { GeneralConstant } from "@utils/general-constant";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { ICrudConfig } from "../crud-config";
import { ICrudService } from "../services/crud-service";

/**
 * Clase abstracta que define la lógica básica para realizar operaciones CRUD. Para utilizar esta clase se debe generar
 * un componente que herede de ella.
 * Es recomendable utilizar el componente @see CatalogoBaseComponent en el template del componente para simplificar
 * el HTML.
 */
export abstract class CrudBase<Modelo> {
	// Variables de Entrada
	public crudConfig: ICrudConfig;

	// Variables
	public elementos: Modelo[] = [];
	public tieneAccesoAgregar: boolean = false;

	constructor(
        private crudService: ICrudService,
		protected accesoService: AccesoService,
		protected bsModalRef: BsModalRef,
		protected bsModalService: BsModalService,
		protected encryptionService: EncryptionService,
		protected mensajeService: MensajeService,
		protected router: Router,
	) {
    }

    onInit(): void {
      this.consultarGrid();
		  this.consultarAccesoAgregar();
    }

	protected consultarAccesoAgregar(): void {
		// Revisar que exista la configuración del formularios
		if (!this.crudConfig.formConfig) {
			// throw new Error('No se configuró la propiedad "formConfig" en el objeto "crudConfig"');
			return;
		}

		// Obtener el objeto de configuración dependiendo de la acción
		const configAgregar = this.crudConfig.formConfig.configAgregar;

		// Revisar que exista la configuración de la acción
		if (!configAgregar) {
			// throw new Error(`No se configuró la propiedad "configAgregar" en el objeto "crudConfig"`);
			return;
		}

		this.accesoService
			.tieneAcceso(configAgregar.acceso)
			.subscribe((tieneAcceso) => {
				this.tieneAccesoAgregar = tieneAcceso;
			});
	}

	/**
	 * Método abstracto que define la forma en la que se recuperan los registros que se muestran en el DataGrid.
	 */
	protected abstract consultarGrid(): void;

  public onGridClick(gridData: any): void {
    const nombrePropiedadId = this.crudConfig.nombrePropiedadId as keyof Modelo;
    const id = gridData.data[nombrePropiedadId];

    if (typeof id !== 'number') {
      throw new Error(`No se pudo obtener el id del elemento. La propiedad "${this.crudConfig.nombrePropiedadId}" no es un número.`);
    }

		switch (gridData.accion) {
			case GeneralConstant.GRID_ACCION_EDITAR:
        this.editar(id);
				break;
			case GeneralConstant.GRID_ACCION_ELIMINAR:
				this.eliminar(gridData.data, id);
				break;
		}
  }

	// public onGridClick(gridData: { accion: string; data: Modelo; }): void {
  //   const nombrePropiedadId = this.crudConfig.nombrePropiedadId as keyof Modelo;
  //   const id = gridData.data[nombrePropiedadId];

  //   if (typeof id !== 'number') {
  //     throw new Error(`No se pudo obtener el id del elemento. La propiedad "${this.crudConfig.nombrePropiedadId}" no es un número.`);
  //   }

	// 	switch (gridData.accion) {
	// 		case GeneralConstant.GRID_ACCION_EDITAR:
  //       this.editar(id);
	// 			break;
	// 		case GeneralConstant.GRID_ACCION_ELIMINAR:
	// 			this.eliminar(gridData.data, id);
	// 			break;
	// 	}
	// }

	/**
	 * Muestra el formulario. Dependiendo de la configuración especificada, el formulario se mostrará en un modal
	 * o se redireccionará al usuario a la ruta correspondiente del modal.
	 * Al formulario se le agregan algunos atributos por defecto (acción, id, nombre de la entidad) y los parámetros
	 * definidos en @see ICrudConfig.
	 */
	private openForm(accion: string, idElemento?: number): void {
		// Revisar que exista la configuración del formularios
		if (!this.crudConfig.formConfig) {
			throw new Error('No se configuró la propiedad "formConfig" en el objeto "crudConfig"');
			return;
		}

		// Obtener el objeto de configuración dependiendo de la acción
		const actionConfig = accion === GeneralConstant.MODAL_ACCION_AGREGAR
			? this.crudConfig.formConfig.configAgregar
			: this.crudConfig.formConfig.configEditar;

		// Revisar que exista la configuración de la acción
		if (!actionConfig) {
			const propiedad: string = accion === GeneralConstant.MODAL_ACCION_EDITAR
				? 'configEditar'
				: 'configAgregar';

			throw new Error(`No se configuró la propiedad "${propiedad}" en el objeto "crudConfig"`);
			return;
		}

		// Agregar parámetros por defecto y parámetros definidos por el usuario
		let params: KeyValueCollection = {
			accion: accion,
			idEntidad: idElemento,
			nombreEntidad: this.crudConfig.nombreEntidad,
			generoGramatical: this.crudConfig.generoGramatical,
			...actionConfig.params
		};

		// Abrir el formulario en un modal
		if (this.crudConfig.formConfig.type === 'modal') {
			// Agregar la propiedad onClose para cerrar el modal
			params['onClose'] = (refrescarGrid: boolean) => {
				if (refrescarGrid) {
					this.consultarGrid();
				}

				this.bsModalRef.hide();
			};

			// Abrir el modal
			this.bsModalRef = this.bsModalService.show(
				this.crudConfig.formConfig.ComponenteFormulario,
				{
					initialState: params,
					...this.crudConfig.formConfig.modalConfig
				}
			);
		}
		// Abrir el formulario en una ruta
		else {
			// Agregar el atributo nextUrl
			params['nextUrl'] = this.router.url;

			this.router.navigate(
				[this.crudConfig.formConfig.url],
				this.encryptionService.generateURL(params)
			);
		}
	}

	/** Abre el formulario en modo Agregar */
	public agregar(): void {
        this.openForm(GeneralConstant.MODAL_ACCION_AGREGAR);
	}

	/** Abre el formulario en modo Editar */
	private editar(idElemento: number): void {
        this.openForm(GeneralConstant.MODAL_ACCION_EDITAR, idElemento);
	}

	private eliminar(elemento: Modelo, id: number): void {
		if (!this.crudConfig.configEliminar) {
			throw new Error(`No se configuró la propiedad "configEliminar" en el objeto "crudConfig"`);
			return;
		}

		const tituloModal: string = `Eliminar ${this.crudConfig.nombreEntidad}`;

		const [articulo, accion]: [string, string] = this.crudConfig.generoGramatical === 'masc'
			? ['El', 'eliminado']
			: ['La', 'eliminada'];

		this.mensajeService
			.modalConfirmacion(
				`¿Desea eliminar ${articulo.toLowerCase()} ${this.crudConfig.nombreEntidad.toLowerCase()}: ` +
				`<strong>${this.crudConfig.configEliminar.elementToString(elemento)}</strong>?`,
				tituloModal,
				GeneralConstant.ICONO_CRUZ
			)
			.then(() => {
				const mensajeExito: string = `${articulo} ${this.crudConfig.nombreEntidad.toLowerCase()} ha sido ${accion}`;



				this.crudService
					.eliminar(id)
					.subscribe(() => {
						this.mensajeService.modalExito(mensajeExito);
						this.consultarGrid();
					});
			})
			.catch(() => { });
	}
}
