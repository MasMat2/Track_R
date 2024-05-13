import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AlertController, IonicModule } from '@ionic/angular';

import { Observable } from 'rxjs';

import { PerfilTratamientoDto } from '@dtos/gestion-perfil/perfil-tratamiento-dto';
import { SelectorDto } from '@dtos/gestion-perfil/selector-dto';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { HeaderComponent } from '@pages/home/layout/header/header.component';

import { Photo } from '@capacitor/camera';
import { PhotoService } from '@services/photo.service';
import {  ModalController } from '@ionic/angular/standalone';

import { addIcons } from 'ionicons';

//TODO:Definir cantidad máxima de fármaco
const CANTIDAD_MAXIMA = 99;


@Component({
  selector: 'app-agregar-tratamiento',
  templateUrl: './agregar-tratamiento.page.html',
  styleUrls: ['./agregar-tratamiento.page.scss'],
  standalone: true,
  imports: [
    IonicModule, 
    CommonModule, 
    FormsModule, 
    ReactiveFormsModule, 
    HeaderComponent, 
  ]
})
export class AgregarTratamientoPage implements OnInit {

  protected formTratamiento: FormGroup;
  protected perfilTratamientoDto: PerfilTratamientoDto;

  private cantidadFarmaco: number = 1;
  protected isModalRecordatorioOpen: boolean = false;
  protected btnSubmit: boolean = false;

  protected now = new Date();
  protected localOffset = this.now.getTimezoneOffset() * 60000;
  protected localISOTime = (new Date(this.now.getTime() - this.localOffset)).toISOString().slice(0,-1);
  protected dateToday: string = this.localISOTime;
  protected fechaSeleccionada: string = this.dateToday;
  protected photo?: Photo;

  protected weekDays = [
     {id: 0 , name: 'L'},
     {id: 1 , name: 'M'},
     {id: 2 , name: 'M'},
     {id: 3 , name: 'J'},
     {id: 4 , name: 'V'},
     {id: 5 , name: 'S'},
     {id: 6 , name: 'D'},
  ];

  // Selectores
  protected padecimientos$: Observable<SelectorDto[]>;
  protected doctores$: Observable<SelectorDto[]>;

  //TODO: Definir tabla de unidades en la BD
  protected unidades = [
    {id: 1, nombre: 'mcg'},
    {id: 2, nombre: 'mg'}, 
    {id: 3, nombre: 'g'},
    {id: 4, nombre: 'ml'},
    {id: 5, nombre: 'tabletas'},
    {id: 6, nombre: 'cucharadas'}
  ]

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    private fb: FormBuilder,
    private photoService: PhotoService,
    private _modalCtrl: ModalController,
    private alertController: AlertController
  ) { 
    addIcons({
    'chevron-left': 'assets/img/svg/chevron-left.svg',
    'chevron-right': 'assets/img/svg/chevron-right.svg',
    'chevron-up': 'assets/img/svg/chevron-up.svg',
    'chevron-down': 'assets/img/svg/chevron-down.svg',
    'calendar': 'assets/img/svg/calendar.svg',
    'clock': 'assets/img/svg/clock-2.svg',
    'info': 'assets/img/svg/info.svg',
    'camera': 'assets/img/svg/camera.svg',
    'minus': 'assets/img/svg/minus.svg',
    'plus': 'assets/img/svg/plus.svg',
    'x': 'assets/img/svg/x.svg',
    'trash-2': 'assets/img/svg/trash-2.svg'
    })  
  }

  public ngOnInit() {
    this.formTratamiento = this.fb.group({
      fechaRegistro: [(new Date()).toISOString(), Validators.required],
      farmaco: ['', Validators.required],
      cantidad: ['1', Validators.required],
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
      horas: this.fb.array([])
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
    console.log(this.photo);
  }

  // Recordatorios Horas
  protected removeHour(index: number) {
    this.horas.removeAt(index);
    //Al eliminar la ultima hora, se desactiva el recordatorio
    if(this.horas.length === 0){
      this.formTratamiento.patchValue({ recordatorioActivo: false });
    }
  };

  protected addHour() {
    //Al agregar la primer hora, se activa el recordatorio
    if(this.horas.length === 0){
      this.formTratamiento.patchValue({ recordatorioActivo: true });
    }
    const horaSeleccionada = this.fechaSeleccionada;
    this.horas.push(this.fb.control(horaSeleccionada));
    this.fechaSeleccionada = this.dateToday;
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
    this.btnSubmit = true;
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
  };

  protected agregar(perfilTratamientoDto: PerfilTratamientoDto) {
    this.perfilTratamientoService.agregar(perfilTratamientoDto).subscribe({
      next: ()=> {

      }, 
      error: () => {
        this.btnSubmit = false;
      },
      complete: ()=> {
        this.btnSubmit = false;
        this.presentarAlertaSuccess();
      }
    })
  }

  protected cerrarModal(rol: string){
    this._modalCtrl.dismiss(null, rol);
  }

  protected seleccionarTratamientoPermanente(seleccion: boolean){
    this.formTratamiento.patchValue({ tratamientoPermanente: seleccion });
  }

  protected incrementarCantidad(){
    if(this.cantidadFarmaco < CANTIDAD_MAXIMA){
      this.cantidadFarmaco += 1;
      this.formTratamiento.patchValue({ cantidad: (this.cantidadFarmaco) });
    }
  }

  protected decrementarCantidad(){
    if(this.cantidadFarmaco > 0){
      this.cantidadFarmaco -= 1;
      this.formTratamiento.patchValue({ cantidad: (this.cantidadFarmaco) });
    }
  }

  protected setOpen(isOpen: boolean) {
    this.isModalRecordatorioOpen = isOpen;
  }

  protected esDiaSemanaSeleccionado(index: number){
    const diaSemanaControl = this.formTratamiento.get('diaSemana') as FormArray;
    const controlEnIndice = diaSemanaControl.at(index);

    return controlEnIndice.value;
  }

  protected seleccionarDiaRecordatorio(index: number){
    const diaSemanaControl = this.formTratamiento.get('diaSemana') as FormArray;
    const controlEnIndice = diaSemanaControl.at(index);
    controlEnIndice.patchValue(!controlEnIndice.value);
  }

  protected eliminarAdjunto(){
    this.photo = undefined;
  }

  protected async presentarAlertaSuccess() {

    const alertSuccess = await this.alertController.create({
      header: 'Tratamiento agregado exitosamente',
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.cerrarModal('confirm');
        }
      }],
      cssClass: 'custom-alert-success',
    });

    await alertSuccess.present();
  }

}
