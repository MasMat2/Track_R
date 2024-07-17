import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ExamenService } from '@http/examen/examen.service';
import { ProgramacionExamenService } from '@http/examen/programacion-examen.service';
import { TipoExamenService } from '@http/examen/tipo-examen.service';
import { UsuarioService } from '@http/seguridad/usuario.service';
import { Examen } from '@models/examen/examen';
import { ProgramacionExamen } from '@models/examen/programacion-examen';
import { TipoExamen } from '@models/examen/tipo-examen';
import { Usuario } from '@models/seguridad/usuario';
import { FormularioService } from '@services/formulario.service';
import { MensajeService } from '@sharedComponents/mensaje/mensaje.service';
import {
  DROPDOWN_NO_OPTIONS,
  DROPDOWN_PLACEHOLDER,
  FORM_ACTION,
} from '@utils/constants/constants';
import { CONFIG_COLUMN_ACTION, GRID_ACTION } from '@utils/constants/grid';
import { ColDef, ICellRendererParams } from 'ag-grid-community';
import { flatMap } from 'lodash';
import { combineLatestWith, mergeMap } from 'rxjs';
import { EncryptionService } from 'src/app/shared/services/encryption.service';
import * as Utileria from '@utils/utileria';

@Component({
  selector: 'app-programacionExamen-formulario',
  templateUrl: './programacion-examen-formulario.component.html',
  styleUrls: ['./programacion-examen-formulario.component.scss'],
})
export class ProgramacionExamenFormularioComponent implements OnInit {

  protected programacionExamen = new ProgramacionExamen();
  protected submitting = false;

  // Selectores
  protected readonly DROPDOWN_PLACEHOLDER = DROPDOWN_PLACEHOLDER;
  protected readonly DROPDOWN_NO_OPTIONS = DROPDOWN_NO_OPTIONS;

  public pacienteList: Usuario[] = [];
  public personalList: Usuario[] = [];
  public tipoExamenList: TipoExamen[] = [];
  public examenList: Examen[] = [];

  // Inputs
  public accion: string;
  public esEdicion = false;
  public idProgramacionExamen?: number;
  public onClose: (actualizar: boolean) => void;

  // Grid
  protected readonly HEADER_GRID: string = 'Cuestionarios';

  private readonly COLUMNA_DETALLE: ColDef = Object.assign(
    {
      action: GRID_ACTION.Ver,
      cellRendererSelector: (params: ICellRendererParams) => {
        const component = {
          component: 'actionButton',
          params: { disabled: false },
        };
        return params.data.resultado != null ? component : '';
      },
      minWidth: 44,
      maxWidth: 44,
    },
    CONFIG_COLUMN_ACTION
  );

  private readonly COLUMNA_DESCARGAR_PDF: ColDef = Object.assign(
    {
      action: GRID_ACTION.DescargarPdf,
      cellRendererSelector: (params: ICellRendererParams) => {
        const component = {
          component: 'actionButton',
          params: { disabled: false },
        };
        return params.data.resultado != null ? component : '';
      },
      minWidth: 44,
      maxWidth: 44,
    },
    CONFIG_COLUMN_ACTION
  );

  protected columns: ColDef[] = [
    {
      headerName: 'Participantes',
      field: 'nombreUsuario',
      minWidth: 200,
      width: 200,
    },
    {
      headerName: 'Resultado',
      field: 'resultado',
      minWidth: 70,
      width: 70
    },
    {
      headerName: 'Preguntas Correctas',
      field: 'preguntasCorrectas',
      minWidth: 70,
      width: 70,
    },
    this.COLUMNA_DETALLE,
    this.COLUMNA_DESCARGAR_PDF,
  ];

  constructor(
    private encryptionService: EncryptionService,
    private examenService: ExamenService,
    private formularioService: FormularioService,
    private mensajeService: MensajeService,
    private programacionExamenService: ProgramacionExamenService,
    private router: Router,
    private tipoExamenService: TipoExamenService,
    private usuarioService: UsuarioService,
  ) {}

  public ngOnInit(): void {

    this.consultarPacientes();
    this.consultarPersonal();
    this.consultarTipoExamen();

    this.programacionExamen.participantes = [];

    if (this.accion === FORM_ACTION.Agregar) {
      this.programacionExamen.fechaAlta = new Date();
      this.programacionExamen.estatus = true;
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.consultarProgramacionExamen(this.idProgramacionExamen!);
    }
  }

  private consultarProgramacionExamen(idProgramacionExamen: number): void {
    const programacionExamen$ = this.programacionExamenService.consultar(idProgramacionExamen);
    const examenes$ = this.examenService.consultarCalificaciones(idProgramacionExamen);

    programacionExamen$
      .pipe(combineLatestWith(examenes$))
      .subscribe(
        ([programacionExamen, examenes]: [ProgramacionExamen, Examen[]]) => {
          const participantes = examenes.map(
            (element) => element.idUsuarioParticipante
          );

          programacionExamen.participantes = participantes;
          programacionExamen.fechaExamen = new Date(programacionExamen.fechaExamen);
          programacionExamen.fechaAlta = new Date(programacionExamen.fechaAlta);

          this.examenList = examenes;
          this.programacionExamen = programacionExamen;
        }
      );
  }

  private consultarPacientes(): void {
    this.usuarioService
      .consultarPacientesParaSelector()
      .subscribe((usuarios: Usuario[]) => {
        this.pacienteList = usuarios;
      });
  }

  private consultarPersonal(): void {
    this.usuarioService
      .consultarPersonalParaSelector()
      .subscribe((usuarios: Usuario[]) => {
        this.personalList = usuarios;
      });
  }

  private consultarTipoExamen(): void {
    this.tipoExamenService
      .consultarTodosParaSelector()
      .subscribe((tiposExamen: TipoExamen[]) => {
        this.tipoExamenList = tiposExamen;
      });
  }

  protected onGridClick(gridData: { accion: string; data: Examen }): void {
    if (gridData.accion === GRID_ACTION.Ver) {
      this.revisar(gridData.data.idExamen);
    }
    if(gridData.accion === GRID_ACTION.DescargarPdf){
      this.descargarRespuestasPdf(
        gridData.data.idExamen,
        this.programacionExamen.clave,
        gridData.data.nombreUsuario
      );
    }
  }

  protected enviarFormulario(formulario: NgForm): void {
    this.submitting = true;

    if (!formulario.valid) {
      this.formularioService.validarCamposRequeridos(formulario);
      this.submitting = false;
      return;
    }

    if (this.accion === FORM_ACTION.Agregar) {
      this.agregar();
    }
    else if (this.accion === FORM_ACTION.Editar) {
      this.editar();
    }
  }

  private agregar(): void {
    const MENSAJE_AGREGAR: string = 'La programación del Cuestionario ha sido agregada';

    this.programacionExamenService
      .agregar(this.programacionExamen)
      .pipe(
        mergeMap((data) => {
          this.programacionExamen.idProgramacionExamen = data
          this.passToList();
          return this.examenService.actualizar(this.examenList);
        })
      )
      .subscribe({
        next: (data) => {
          this.mensajeService.modalExito(MENSAJE_AGREGAR);
          this.onClose(true);
        },
        error: (error) => {
          this.submitting = true;
        }
      });
  }

  private editar(): void {
    const MENSAJE_EDITAR: string = 'La programación de Cuestionario ha sido modificada';

    this.programacionExamenService
      .editar(this.programacionExamen)
      .pipe(
        mergeMap((data) => {
          this.passToList();
          return this.examenService.actualizar(this.examenList);
        })
      )
      .subscribe({
        next: (data) => {
          this.mensajeService.modalExito(MENSAJE_EDITAR);
          this.onClose(true);
        },
        error: (error) => {
          this.submitting = true;
        }
      });
  }

  protected cancelar(): void {
    this.onClose(true);
  }

  private passToList(): void {
    const participantes: number[] = this.examenList.map(e => e.idUsuarioParticipante);

    for (const participante of this.programacionExamen.participantes) {
      const examenExistente = this.examenList.findIndex(e => e.idUsuarioParticipante === participante);
      if (examenExistente >= 0) {
        this.examenList[examenExistente].fechaAlta = this.programacionExamen.fechaExamen;
        continue;
      }

      const examen = new Examen();
      examen.idProgramacionExamen = this.programacionExamen.idProgramacionExamen;
      examen.idUsuarioParticipante = participante;
      examen.idEstatusExamen = 1;
      examen.fechaAlta = new Date();
      examen.estatus = true;

      this.examenList.push(examen);
    }
  }

  protected actualizarDuracion(): void {
    if (
      this.programacionExamen.idTipoExamen != null &&
      this.programacionExamen.idTipoExamen != 0
    ) {
      this.tipoExamenService
        .consultar(this.programacionExamen.idTipoExamen)
        .subscribe((data) => {
          this.programacionExamen.duracion = data.duracion;
        });
    }
  }

  private revisar(idExamen: number): void {
    this.cancelar();
    this.examenService
      .consultarMiExamenIndividual(idExamen)
      .subscribe((data) => {
        this.router.navigate(['/administrador/examen/examen/presentar'], {
          queryParams: this.encryptionService.generateURL({
            i: idExamen.toString(),
            revision: '1',
          }),
        });
      });
  }

  private descargarRespuestasPdf(idExamen: number, claveExamen: string, nombreUsuario: string){
    this.examenService.descargarRespuestasPDF(idExamen).subscribe(
      (data) => {
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(data);
        a.href = objectUrl;
        a.download = Utileria.obtenerFormatoNombreArchivo(
          'Respuestas_' + claveExamen + '_' + nombreUsuario,
          'pdf'
        );
        a.click();

        URL.revokeObjectURL(objectUrl);
      }
    )
  }


}
