import { Component, OnInit } from '@angular/core';

interface Paciente {
  idPaciente: number;
  nombreCompleto: string;
  imagenBase64?: string;
  tipoMime?: string;
  patologias: string[];
  glucosa: number;
  presionSistolica: number;
  presionAsistolica: number;
}

@Component({
  selector: 'app-paciente',
  templateUrl: './paciente.component.html',
  styleUrls: ['./paciente.component.scss']
})
export class PacienteComponent implements OnInit {

  protected pacientes: Paciente[] = [];

  constructor() { }

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

  protected ver(paciente: Paciente): void {
  }

  protected editar(paciente: Paciente): void {
  }

  protected eliminar(paciente: Paciente): void {
  }

}
