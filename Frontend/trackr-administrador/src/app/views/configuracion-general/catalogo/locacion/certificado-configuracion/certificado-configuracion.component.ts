import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CertificadoLocacionService } from '@http/seguridad/certificado-locacion.service';
import { CertificadoLocacion } from '@models/catalogo/certificado-locacion';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';

@Component({
  selector: 'app-certificado-configuracion',
  templateUrl: './certificado-configuracion.component.html'
})
export class CertificadoConfiguracionComponent implements OnInit {

  // Constantes
  private readonly tipoCertificado: string = 'cer';
  private readonly tipoClave: string = 'key';

  // Configuración
  public onClose: any;
  public btnSubmit: boolean = false;
  public idLocacion: number;

  // Gestión de certificados
  public certificadoLocacion = new CertificadoLocacion();
  public clavePrivadaLocacion = new CertificadoLocacion();
  public certificados: CertificadoLocacion[] = [];

  constructor(
    private certificadoLocacionService: CertificadoLocacionService,
    private formularioService: FormularioService,
    private mensajeService: MensajeService
  ) {}

  ngOnInit() {
  }

  public enviarFormulario(formulario: NgForm): void {
    this.btnSubmit = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.btnSubmit = false;
      return;
    }

    this.validarCertificados();
    this.certificados.push(this.certificadoLocacion, this.clavePrivadaLocacion);

    this.agregar();
  }

  public fileChangeCertificado(event: any, tipo: string): void {
    if (event.target.files && event.target.files[0]) {

      const certificado = new CertificadoLocacion();
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);

      certificado.idLocacion = this.idLocacion;
      certificado.nombre = event.target.files[0].name;
      certificado.tipoMime = event.target.files[0].type;

      reader.onload = (event: Event) => {
        const result = reader.result;
        certificado.archivoBase64 = result
          ? result.toString().split(',')[1]
          : '';
      };

      switch (tipo) {
        case this.tipoCertificado:
            this.certificadoLocacion = certificado;
        break;

        case this.tipoClave:
            certificado.tipoMime = 'application/pkcs8';
            this.clavePrivadaLocacion = certificado;
        break;

        default:
          break;
      }
    }
  }

  private agregar() {
    this.certificadoLocacionService.agregar(this.certificados).subscribe((success) => {
      this.mensajeService.modalExito("Los certificados han sido configurados exitosamente");
      this.onClose(true);
    });
  }

  private validarCertificados() {
    if (!(this.certificadoLocacion.archivoBase64))  {
      this.mensajeService.modalExito("El certificado es requerido");
    }

    if (!(this.clavePrivadaLocacion.archivoBase64))  {
      this.mensajeService.modalExito("La clave privada es requerida");
    }
  }

  public cancelar(): void {
    this.onClose(true);
  }

}
