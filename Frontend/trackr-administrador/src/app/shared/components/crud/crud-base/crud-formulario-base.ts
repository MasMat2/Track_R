import { NgForm } from '@angular/forms';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import { FORM_ACTION } from '@utils/constants/constants';
import * as Utileria from '@utils/utileria';
import { Observable } from 'rxjs';

/**
 * Clase que define la lógica básica para realizar operaciones CRUD en un formulario. Para utilizar esta clase se debe generar
 * un componente formulario que herede de ella.
 */
export abstract class CrudFormularioBase<FormularioCapturaDto> {
  public accion: string;
  public onClose: (refrescarGrid: boolean) => void;
  public nextUrl: string;
  public idEntidad: number;
  public nombreEntidad: string;
  public generoGramatical: 'masc' | 'fem';

  protected submitting: boolean = false;
  protected entidad: FormularioCapturaDto = {} as FormularioCapturaDto;

  protected abstract consultar(idEntidad: number): Observable<FormularioCapturaDto>;
  protected abstract agregar(entidad: FormularioCapturaDto): Observable<void>;
  protected abstract editar(entidad: FormularioCapturaDto): Observable<void>;

  constructor(
    protected mensajeService: MensajeService,
  ) {}

  onInit(): void {
    if (this.accion === FORM_ACTION.Editar && this.idEntidad) {
      this.consultar(this.idEntidad).subscribe({
        next: (entidad: FormularioCapturaDto) => {
          this.entidad = entidad;
        }
      });
    }
  }

  /**
   * Este método se debe mandar llamar cuando se da click en el botón principal del formulario.
   * Este método valida los campos del formulario y manda llamar al método correspondiente del CrudService
   * dependiendo de la acción del formulario.
   */
  protected onSubmit(formulario: NgForm): void {
    this.submitting = true;

    if (!formulario.valid) {
      Utileria.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    const [articulo, letra]: [string, string] =
      this.generoGramatical === 'masc' ? ['El', 'o'] : ['La', 'a'];

    const accion = this.accion === FORM_ACTION.Agregar
      ? `agregad${letra}`
      : `modificad${letra}`;

    const mensajeExito: string = `${articulo} ${this.nombreEntidad.toLocaleLowerCase()} ha sido ${accion}`;

    const observable = this.accion === FORM_ACTION.Agregar
      ? this.agregar(this.entidad)
      : this.editar(this.entidad);

    observable.subscribe({
      next: () => {
        this.mensajeService.modalExito(mensajeExito);
        this.onClose(true);
      },
      error: () => {
        this.submitting = false;
      }
    });
  }

  /**
   * Cierra el formulario. Si es un modal lo cierra, si no, redirecciona al usuario a
   * la ruta especificada, o a la ruta anterior si no se definió.
   */
  protected cancelar(): void {
    this.onClose(false);
  }
}
