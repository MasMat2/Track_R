import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from '@models/seguridad/usuario';
import { GeneralConstant } from '@utils/general-constant';
import { Paciente } from './paciente';

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrls: ['./paciente.component.scss']
})
export class PacienteComponent implements OnInit {

  protected pacientes: Paciente[] = [];
  protected isVistaCuadricula: boolean = true;

  // App Grid View
  public HEADER_GRID = 'Pacientes';
  public columnaEditar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_EDITAR,
      title: 'Editar',
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );

  public columnaEliminar = Object.assign(
    {
      action: GeneralConstant.GRID_ACCION_ELIMINAR,
      title: 'Eliminar',
      cellRendererSelector: (params: any) => {
        const component = { component: 'actionButton', params: { disabled: false } };
        return component;
      },
    },
    GeneralConstant.CONFIG_COLUMN_ACTION
  );


  public columns = [
    { headerName: 'Paciente', field: 'nombreCompleto', minWidth: 150 },
    this.columnaEditar,
    this.columnaEliminar
  ];
  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.pacientes = [
      { idPaciente: 1, nombreCompleto: 'Paciente 1', imagenBase64: undefined, tipoMime: undefined, patologias: ['Patologia 1', 'Patologia 2'], glucosa: 100, presionSistolica: 120, presionAsistolica: 80 },
      { idPaciente: 2, nombreCompleto: 'Paciente 2', imagenBase64: undefined, tipoMime: undefined, patologias: ['Patologia 1', 'Patologia 2'], glucosa: 100, presionSistolica: 120, presionAsistolica: 80 },
      { idPaciente: 3, nombreCompleto: 'Paciente 3', imagenBase64: undefined, tipoMime: undefined, patologias: ['Patologia 1', 'Patologia 2'], glucosa: 100, presionSistolica: 120, presionAsistolica: 80 },
      { idPaciente: 4, nombreCompleto: 'Paciente 4', imagenBase64: undefined, tipoMime: undefined, patologias: ['Patologia 1', 'Patologia 2'], glucosa: 100, presionSistolica: 120, presionAsistolica: 80 },
      { idPaciente: 5, nombreCompleto: 'Paciente 5', imagenBase64: undefined, tipoMime: undefined, patologias: ['Patologia 1', 'Patologia 2'], glucosa: 100, presionSistolica: 120, presionAsistolica: 80 },
    ]
  }

  protected descargarExcel(): void {
  }

  protected ver(event: Paciente): void {
    console.log(event)
  }

  protected editar(): void {
    this.router.navigate(['/administrador/gestion-paciente/paciente/paciente-formulario'], {
      // queryParams: this.encryptionService.generateURL({
      //   i: idPerfil.toString(),
      //   ij: data.idJerarquiaAcceso > 0 ? data.idJerarquiaAcceso.toString() : 0
      // })
    });
  }

  /**
   * Evento que se ejecuta al dar clic en algun boton del grid.
   */
  public onGridClick(gridData: { accion: string; data: Paciente }) {
    if (gridData.accion === GeneralConstant.GRID_ACCION_EDITAR) {
      this.editar();
    } else if (gridData.accion === GeneralConstant.GRID_ACCION_ELIMINAR) {
      // this.eliminar(gridData.data);
    }
  }

}
