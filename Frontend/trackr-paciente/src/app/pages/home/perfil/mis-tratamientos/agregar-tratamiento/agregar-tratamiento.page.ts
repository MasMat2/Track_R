import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, FormGroup, FormArray, FormBuilder, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { ReactiveFormsModule } from '@angular/forms';

import { Observable } from 'rxjs';

import { HeaderComponent } from '@pages/home/layout/header/header.component';
import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { SelectorDto } from '@dtos/gestion-perfil/selector-dto';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';

import { PhotoService } from '@services/photo.service';
import { Photo } from '@capacitor/camera';


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
  photo: Photo;

  // Helpers - DateTime modal
  selectedDate = new Date().toISOString();
  dateIndex: string;
  showDateModal: boolean = false;
  hourIndex: number;
  showTimeModal: boolean = false;

  // Selectores
  protected padecimientos$: Observable<SelectorDto[]>;
  protected doctores$: Observable<SelectorDto[]>;

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    private fb: FormBuilder,
    public photoService: PhotoService) { }

  ngOnInit() {
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
      recordatorioActivo: [true],
      diaSemana: this.fb.array([true, true, false, false, false, false, false]),
      horas: this.fb.array([new Date().toISOString()])
    },
      { validators: this.validateDiaSemana() });

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
      idExpediente: 9,  // Obtener idExpediente
      farmaco: formValues.farmaco,
      fechaRegistro: new Date(formValues.fechaRegistro), // Convertir string a Date
      cantidad: formValues.cantidad,
      unidad: formValues.unidad,
      indicaciones: formValues.indicaciones,
      padecimiento: formValues.padecimiento,
      idPadecimiento: formValues.idPadecimiento,
      idUsuarioDoctor: formValues.idUsuarioDoctor,
      imagenBase64: this.photo.base64String || "",
      recordatorioActivo: formValues.recordatorioActivo,
      diaSemana: formValues.diaSemana,
      horas: horasTiempos

    };

    this.perfilTratamientoDto = tratamientoDto;


    this.agregar(this.perfilTratamientoDto);
  };

  protected agregar(perfilTratamientoDto: PerfilTratamientoDto): Promise<boolean> {
    return this.perfilTratamientoService
      .agregar(perfilTratamientoDto)
      .toPromise()
      .then(() => true)
      .catch(() => false);
  }

}
