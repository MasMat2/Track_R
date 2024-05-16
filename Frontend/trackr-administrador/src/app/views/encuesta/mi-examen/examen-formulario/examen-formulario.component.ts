import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExamenReactivoService } from '@http/examen/examen-reactivo.service';
import { ExamenService } from '@http/examen/examen.service';
import { Examen } from '@models/examen/examen';
import { ExamenReactivo } from '@models/examen/examen-reactivo';
import { Reactivo } from '@models/examen/reactivo';
import { EncryptionService } from '@services/encryption.service';
import { GeneralConstant } from '@utils/general-constant';
import { MensajeService } from 'src/app/shared/components/mensaje/mensaje.service';
import { FormularioService } from 'src/app/shared/services/formulario.service';
import * as Utileria from '@utils/utileria';
import {
  DROPDOWN_NO_OPTIONS,
  DROPDOWN_PLACEHOLDER,
} from '../../../../shared/utils/constants/constants';

@Component({
  selector: 'app-reactivo-formulario',
  templateUrl: './examen-formulario.component.html',
  styleUrls: ['./examen-formulario.component.scss'],
})
export class ExamenFormularioComponent implements OnInit {
  protected readonly DROPDOWN_PLACEHOLDER: string = DROPDOWN_PLACEHOLDER;
  protected readonly DROPDOWN_NO_OPTIONS: string = DROPDOWN_NO_OPTIONS;

  private MENSAJE_ULTIMO_MINUTO: string = 'Queda un minuto para finalizar el Cuestionario';
  private MENSAJE_NO_SUFICIENTES_REACTIVOS: string =
    'No hay suficientes reactivos para aplicar este cuestionario';

  private TITULO_MODAL_TERMINAR: string = 'Terminar Cuestionario';

  // Inputs
  public accion: string;
  public esEdicion = false;
  public onClose: any;

  // Form
  protected submitting = false;
  protected presentando = false;
  protected permitirDescargar = false;

  protected reactivo = new Reactivo();
  protected examen = new Examen();
  protected examenTerminado = false;
  protected blockQuestions = false;

  protected reactivoList: ExamenReactivo[] = [];

  protected respuestas: { nombre: string }[] = [
    { nombre: 'a' },
    { nombre: 'b' },
    { nombre: 'c' },
    { nombre: 'd' },
    { nombre: 'e' },
  ];

  protected indice = 0;

  protected segundosRestantes = 0;
  protected lastMinute = false;

  private interval: NodeJS.Timer;

  constructor(
    private encryptionService: EncryptionService,
    private examenReactivoService: ExamenReactivoService,
    private examenService: ExamenService,
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.submitting = true;

    this.route.queryParams.subscribe((params) => {
      this.examen.idExamen = this.encryptionService.readUrlParams(params).i;
      const revision = this.encryptionService.readUrlParams(params).revision;

      if (revision != null && revision != '1') {
        if (this.examen.idExamen > 0) {
          this.consultarExamen(this.examen.idExamen);
        }
      } else {
        this.blockQuestions = true;
        this.consultarReactivosParaRevision();
      }
    });
  }

  consultarExamen(idExamen: number) {
    this.examenService.consultarMiExamen(idExamen).subscribe((data) => {
      this.examen = data;
      this.segundosRestantes = this.examen.duracion * 60;

      this.submitting = false;
    });
  }

  consultarReactivos() {
    this.submitting = true;
    this.examenReactivoService
      .consultarReactivosExamen(this.examen.idExamen)
      .subscribe((data) => {
        if (data.length != this.examen.totalPreguntas) {
          this.mensajeService.modalError(this.MENSAJE_NO_SUFICIENTES_REACTIVOS);
          this.cancelar();
        } else {
          this.reactivoList = data;

          this.submitting = false;

          this.interval = setInterval(() => this.updateTimer(), 1000);
          setTimeout(() => this.terminarExamenTiempo(), this.segundosRestantes * 1000);

          this.presentando = true;
        }
      });
  }

  consultarReactivosParaRevision() {
    this.permitirDescargar = true;

    this.examenService.consultarMiExamen(this.examen.idExamen)
      .subscribe((data) => {
        this.examen = data;
      });
    this.examenReactivoService
      .consultarReactivosExamen(this.examen.idExamen)
      .subscribe((data) => {
        this.reactivoList = data;
        this.submitting = false;
      });
  }

  public updateTimer() {
    // var minutes = Math.floor(this.segundosRestantes / 60);
    // var seconds = this.segundosRestantes % 60;

    // var doubleDigit = seconds < 10 ? '0' : '';

    // var timerElement = document.getElementById('timer');
    // timerElement.innerHTML = `${minutes}:${doubleDigit}${seconds}`;
    this.segundosRestantes--;

    if (this.segundosRestantes == 0) {
      clearInterval(this.interval);
    } else if (this.segundosRestantes < 60 && !this.lastMinute) {
      this.lastMinute = true;
      this.mensajeService.modalError(this.MENSAJE_ULTIMO_MINUTO);
    }
  }

  public enviarFormulario(formulario: NgForm): void {
    this.submitting = true;
    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    if (this.accion === GeneralConstant.MODAL_ACCION_AGREGAR) {
      //this.agregarReactivo(formulario);
    } else if (this.accion === GeneralConstant.MODAL_ACCION_EDITAR) {
      //this.editarReactivo(formulario);
    }
  }

  public cancelar(): void {
    if (!this.blockQuestions) {
      this.router.navigate(['/administrador/examen/examen'], {});
    } else {
      this.router.navigate(['/administrador/examen/programacion-examen'], {});
    }
  }

  public siguiente() {
    if (this.indice + 1 < this.reactivoList.length) {
      this.indice++;
    }
  }

  public anterior() {
    if (this.indice > 0) {
      this.indice--;
    }
  }

  public terminarExamen() {
    this.mensajeService
      .modalConfirmacion(
        '¿Seguro que deseas terminar el Cuestionario?',
        this.TITULO_MODAL_TERMINAR,
        GeneralConstant.ICONO_CRUZ
      )
      .then((aceptar) => {
        clearInterval(this.interval);
        this.submitting = true;
        this.examenReactivoService
          .revisar(this.reactivoList)
          .subscribe((data) => {
            this.examen.resultado = data;
            this.examenTerminado = true;

            this.submitting = false;
          });
      });
  }

  public terminarExamenTiempo() {
    this.submitting = true;
    this.examenReactivoService.revisar(this.reactivoList).subscribe((data) => {
      this.examen.resultado = data;
      this.examenTerminado = true;

      this.submitting = false;
    });
  }

  protected descargarRespuestas(idExamen: number){
    this.examenService.descargarRespuestasPDF(idExamen).subscribe(
      (data) => {
        console.log(this.examen);
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(data);
        a.href = objectUrl;
        a.download = Utileria.obtenerFormatoNombreArchivo(
          'Respuestas_' + this.examen.clave + '_' + this.examen.nombreUsuario,
          'pdf'
        );
        a.click();

        URL.revokeObjectURL(objectUrl);
      }
    )
  }
}
