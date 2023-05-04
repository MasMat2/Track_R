import { Location } from "@angular/common";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";
import * as Utileria from '@utils/utileria';
import { ICrudService } from "../services/crud-service";
import { MensajeService } from "@sharedComponents/mensaje/mensaje.service";
import { GeneralConstant } from "@utils/general-constant";

/**
 * Clase que define la lógica básica para realizar operaciones CRUD en un formulario. Para utilizar esta clase se debe generar
 * un componente formulario que herede de ella.
 */
export abstract class CrudFormularioBase<Modelo> {
    public accion: string;
    public onClose: (refrescarGrid: boolean) => void;
    public nextUrl: string;
    public idEntidad: number;
    public nombreEntidad: string;
    public generoGramatical: 'masc' | 'fem';

    public disableSubmit: boolean = false;
    public entidad: Modelo = {} as Modelo;

    constructor(
        private crudService: ICrudService,
        protected mensajeService: MensajeService,
        protected router: Router,
        protected location: Location,
    ) {
    }

    async onInit(): Promise<void> {
        if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
            await this.consultarEntidad(this.idEntidad);
        }
    }

    /**
     * Utiliza el método consultar del CrudService para obtener la información de la entidad que se muestra en el formulario.
     */
    protected async consultarEntidad(idEntidad: number): Promise<void> {
        return this.crudService
            .consultar(idEntidad)
            .toPromise()
            .then((entidad: Modelo) => {
                this.entidad = entidad;
            });
    };

    /**
     * Este método se debe mandar llamar cuando se da click en el botón principal del formulario.
     * Este método valida los campos del formulario y manda llamar al método correspondiente del CrudService
     * dependiendo de la acción del formulario.
     */
    public async enviarFormulario(formulario: NgForm): Promise<void> {
        this.disableSubmit = true;

        if (!formulario.valid) {
            Utileria.validarCamposRequeridos(formulario);
            this.disableSubmit = false;
            return;
        }

        let exito: boolean = false;

        const [articulo, accion]: [string, string] = this.generoGramatical === 'masc'
            ? ['El', 'o']
            : ['La', 'a'];

        let mensajeExito: string = `${articulo} ${this.nombreEntidad.toLocaleLowerCase()} ha sido `;
        if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
            exito = await this.agregar();
            mensajeExito += `agregad${accion}`;
        }
        else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
            exito = await this.editar();
            mensajeExito += `modificad${accion}`;
        }

        if (exito) {
            this.mensajeService.modalExito(mensajeExito);

            // TODO: 2022-12-16 -> Agregar configuración para definir si el formulario se debe cerrrar al enviarse
            if (this.onClose) {
                this.onClose(true);
            }
            else {
                this.cancelar();
            }
        }
        else {
            this.disableSubmit = false;
        }
    }

    protected async agregar(): Promise<boolean> {
        return this.crudService
            .agregar(this.entidad)
            .toPromise()
            .then(() => true)
            .catch(() => false);
    }

    protected async editar(): Promise<boolean> {
        return this.crudService
            .editar(this.entidad)
            .toPromise()
            .then(() => true)
            .catch(() => false);
    }

    /**
     * Cierra el formulario. Si es un modal lo cierra, si no, redirecciona al usuario a
     * la ruta especificada, o a la ruta anterior si no se definió.
     */
    public cancelar(): void {
        if (this.onClose) {
            this.onClose(false);
        }
        else if (this.nextUrl) {
            this.router.navigate([this.nextUrl]);
        }
        else {
            this.location.back();
        }
    }

}
