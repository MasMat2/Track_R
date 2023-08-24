import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule } from '@ionic/angular';

import { Observable } from 'rxjs';

import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { SelectorDto } from '@dtos/gestion-perfil/selector-dto';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { HeaderComponent } from '@pages/home/layout/header/header.component';

import { Photo } from '@capacitor/camera';
import { PhotoService } from '@services/photo.service';


@Component({
  selector: 'app-agregar-tratamiento',
  templateUrl: './agregar-tratamiento.page.html',
  styleUrls: ['./agregar-tratamiento.page.scss'],
  standalone: true,
  imports: [IonicModule, CommonModule, FormsModule, ReactiveFormsModule, HeaderComponent]
})
export class AgregarTratamientoPage implements OnInit {

  protected formTratamiento: FormGroup;
  protected perfilTratamientoDto: PerfilTratamientoDto;

  protected weekDays: string[] = ['Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá', 'Do'];
  protected photo?: Photo;

  // Helpers - DateTime modal
  protected selectedDate = new Date().toISOString();
  protected dateIndex: string;
  protected showDateModal: boolean = false;
  protected hourIndex: number;
  protected showTimeModal: boolean = false;

  // Selectores
  protected padecimientos$: Observable<SelectorDto[]>;
  protected doctores$: Observable<SelectorDto[]>;

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    private fb: FormBuilder,
    private photoService: PhotoService,
    private router: Router) { }

  public ngOnInit() {
    this.formTratamiento = this.fb.group({
      fechaRegistro: [(new Date()).toISOString(), Validators.required],
      farmaco: ['', Validators.required],
      cantidad: ['', Validators.required],
      unidad: ['', Validators.required],
      indicaciones: ['', Validators.required],
      idPadecimiento: ['', Validators.required],
      idUsuarioDoctor: ['', Validators.required],
      tratamientoPermanente: [false],
      fechaInicio: [(new Date()).toISOString(), Validators.required],
      fechaFin: [(new Date()).toISOString()],
      imagenBase64: [''],
      recordatorioActivo: [false],
      diaSemana: this.fb.array([false, false, false, false, false, false, false]),
      horas: this.fb.array([new Date().toISOString()])
    },
      { validators: [this.validateDiaSemana(), this.compareDates()] });

    this.selectorPadecimeintos();
    this.selectorDoctor();
  }

  // Getter para el form array horas
  get horas(): FormArray {
    return this.formTratamiento.get('horas') as FormArray;
  }

  // Selectores
  protected selectorPadecimeintos(): void {
    this.padecimientos$ = this.perfilTratamientoService.selectorPadecimeintos();
  }

  protected selectorDoctor(): void {
    this.doctores$ = this.perfilTratamientoService.selectorDeDoctor();
  }

  // Camara
  protected async addPhotoToGallery() {
    this.photo = await this.photoService.takePicture();
  }

  // DateTime Modals
  protected openTimeModal(n: number) {
    this.hourIndex = n;
    this.showTimeModal = true;
  }

  protected closeTimeModal() {
    this.horas.at(this.hourIndex).setValue(this.selectedDate);
    this.showTimeModal = false;
  }

  protected openDateModal(dateIndex: string) {
    this.dateIndex = dateIndex;
    this.showDateModal = true;
  }

  protected closeDateModal() {
    this.formTratamiento.controls[this.dateIndex].setValue(this.selectedDate);
    this.showDateModal = false;
  }

  // Recordatorios Horas
  protected removeHour(index: number) {
    this.horas.removeAt(index);
  };

  protected addHour() {
    this.horas.push(this.fb.control(new Date().toISOString()));
  };

  // Validaciones
  // Valida que la fechaFin sea mayor o igual a fechaInicio
  protected compareDates(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const tratamientoPermanente = control.get('tratamientoPermanente')?.value;
      const fechaInicio = new Date(control.get('fechaInicio')?.value);
      const fechaFin = new Date(control.get('fechaFin')?.value);
    
      if (!tratamientoPermanente && fechaInicio && fechaFin && fechaInicio > fechaFin) {
        return { 'fechaFinLessThanFechaInicio': true };
      }
    
      return null;
    }
  }

  // Valida que se seleccione al menos un dia para recordatorio
  protected validateDiaSemana(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const recordatorioActivo = control.get('recordatorioActivo')?.value;
      const diaSemana = control.get('diaSemana')?.value;
      if (recordatorioActivo && !diaSemana.some((day: boolean) => day)) {
        return { 'sinDiaSelecionado': true };
      }
      return null;
    };
  }


  // Enviar
  protected submitForm() {
    const formValues = this.formTratamiento.value;

    const horasTiempos = formValues.horas.map((hora: string) => {
      return hora.split('T')[1].split('.')[0];
    });

    const tratamientoDto: PerfilTratamientoDto = {
      farmaco: formValues.farmaco,
      fechaRegistro: new Date(formValues.fechaRegistro), // Convertir string a Date
      cantidad: formValues.cantidad,
      unidad: formValues.unidad,
      indicaciones: formValues.indicaciones,
      padecimiento: formValues.padecimiento,
      idPadecimiento: formValues.idPadecimiento,
      idUsuarioDoctor: formValues.idUsuarioDoctor,
      imagenBase64: this.photo?.base64String || "",
      recordatorioActivo: formValues.recordatorioActivo,
      diaSemana: formValues.recordatorioActivo ? formValues.diaSemana : null,
      horas: formValues.recordatorioActivo ? horasTiempos : null

    };

    this.perfilTratamientoDto = tratamientoDto;

    this.agregar(this.perfilTratamientoDto);

    this.router.navigateByUrl('/home/perfil/mis-tratamientos');
  };

  protected agregar(perfilTratamientoDto: PerfilTratamientoDto): Promise<boolean> {
    return this.perfilTratamientoService
      .agregar(perfilTratamientoDto)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }

}
