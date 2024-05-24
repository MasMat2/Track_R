import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AlertController, IonicModule } from '@ionic/angular';

import { Observable } from 'rxjs';

import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { HeaderComponent } from '@pages/home/layout/header/header.component';

import { Photo } from '@capacitor/camera';
import { PhotoService } from '@services/photo.service';
import {  ModalController } from '@ionic/angular/standalone';

import { addIcons } from 'ionicons';
import { ExpedienteTratamientoDetalleDto } from 'src/app/shared/Dtos/gestion-perfil/expediente-tratamiento-detalle-dto';
import { SelectorDto } from 'src/app/shared/Dtos/gestion-perfil/selector-dto';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { EntidadEstructuraService } from '@http/gestion-entidad/entidad-estructura.service';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { ExpedientePadecimientoService } from '@http/gestion-expediente/expediente-padecimiento.service';

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

  protected readonly now = new Date();
  protected readonly localOffset = this.now.getTimezoneOffset() * 60000;
  protected readonly localISOTime = (new Date(this.now.getTime() - this.localOffset)).toISOString().slice(0,-1);
  protected readonly dateToday: string = this.localISOTime;

  protected accion: string;
  protected formTratamiento: FormGroup;
  protected perfilTratamientoDto: ExpedienteTratamientoDetalleDto;
  private cantidadFarmaco: number = 1;
  protected isModalRecordatorioOpen: boolean = false;
  protected btnSubmit: boolean = false;
  protected fechaSeleccionada: string = this.dateToday;
  protected photo: Photo | undefined;
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];
  protected padecimientosSelector: ExpedientePadecimientoSelectorDTO[];

  protected weekDays = [
     {id: 0 , name: 'L'},
     {id: 1 , name: 'M'},
     {id: 2 , name: 'M'},
     {id: 3 , name: 'J'},
     {id: 4 , name: 'V'},
     {id: 5 , name: 'S'},
     {id: 6 , name: 'D'},
  ];
  //TODO: Definir tabla de unidades en la BD
  protected unidades = [
    {id: 1, nombre: 'mcg'},
    {id: 2, nombre: 'mg'}, 
    {id: 3, nombre: 'g'},
    {id: 4, nombre: 'ml'},
    {id: 5, nombre: 'tabletas'},
    {id: 6, nombre: 'cucharadas'}
  ]

  // Selectores
  //protected padecimientos$: Observable<SelectorDto[]>;
  //protected doctores$: Observable<SelectorDto[]>;

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    private doctoresService: MisDoctoresService,
    private expedientePadecimientoService: ExpedientePadecimientoService,
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
    this.verificarAccionFormulario();
  }

  private verificarAccionFormulario(){
    if(this.accion == "editar"){
      this.rellenarValoresEditar();
    }
    else{
      return
    }
  }

  private rellenarValoresEditar(){
    this.formTratamiento.patchValue({
      fechaRegistro: this.perfilTratamientoDto.fechaRegistro,
      farmaco: this.perfilTratamientoDto.farmaco,
      cantidad: this.perfilTratamientoDto.cantidad,
      unidad: this.perfilTratamientoDto.unidad,
      indicaciones: this.perfilTratamientoDto.indicaciones,
      idPadecimiento: this.perfilTratamientoDto.idPadecimiento,
      idUsuarioDoctor: this.perfilTratamientoDto.idUsuarioDoctor,
      tratamientoPermanente: this.perfilTratamientoDto.fechaFin == null,
      fechaInicio: this.perfilTratamientoDto.fechaInicio,
      fechaFin: this.perfilTratamientoDto.fechaFin,
      imagenBase64: this.perfilTratamientoDto.imagenBase64,
      recordatorioActivo: this.perfilTratamientoDto.recordatorioActivo,
    });
    
    // Llenar el array 'diaSemana' del FormGroup
    const diaSemanaFormArray = this.formTratamiento.get('diaSemana') as FormArray;
    diaSemanaFormArray.patchValue(this.perfilTratamientoDto.diaSemana);

    // Llenar el array 'horas' del FormGroup
    const horasFormArray = this.formTratamiento.get('horas') as FormArray;
    horasFormArray.clear();
    this.perfilTratamientoDto.horas.forEach(hora => {
      const fechaLocal = this.formatearHoraAFechaLocal(hora);
      horasFormArray.push(new FormControl(fechaLocal));
    });

    //asignar la imagen del medicamento
    if(this.perfilTratamientoDto.imagenBase64 != ""){
      this.photo = {format: 'jpeg', saved: false, base64String: this.perfilTratamientoDto.imagenBase64};
    }

    //ajustar el selector de cantidad
    this.cantidadFarmaco = this.perfilTratamientoDto.cantidad;
  }

  // Getter para el form array horas
  get horas(): FormArray {
    return this.formTratamiento.get('horas') as FormArray;
  }

  //Dar formato a las horas que vienen al editar tratamiento para poder usarlas en los datepickers
  protected formatearHoraAFechaLocal(hora: string){
    const dateTodayString = this.dateToday.split('T')[0];
    const fecha = new Date(`${dateTodayString}T${hora}`);
    const fechalocalString = new Date(fecha.getTime() - this.localOffset).toISOString().slice(0,-1);

    return fechalocalString;
  }

  // Selectores
  protected selectorPadecimeintos(): void {
    this.expedientePadecimientoService.consultarPorUsuarioParaSelector().subscribe({
      next: (data) => {
        console.log(data);
        this.padecimientosSelector = data;
      }
    })
  }

  protected selectorDoctor(): void {
    //this.doctores$ = this.perfilTratamientoService.selectorDeDoctor();
    this.doctoresService.consultarPorUsuarioParaSelector().subscribe({
      next: (data) => {
        console.log(data);
        this.doctoresSelector = data;
      }
    })
  }

  // Camara
  protected async addPhotoToGallery() {
    this.photo = await this.photoService.takePicture();
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
    this.fechaSeleccionada = this.dateToday; //reiniciar selector de hora
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

    const horasTiempos= formValues.horas.map((hora: string) => {
      return hora.split('T')[1].split('.')[0]; 
    });

    const tratamientoDto: ExpedienteTratamientoDetalleDto = {
      idExpedienteTratamiento: this.perfilTratamientoDto.idExpedienteTratamiento,//tendra valor solo cuando la accion sea editar
      farmaco: formValues.farmaco,
      fechaRegistro: new Date(formValues.fechaRegistro),
      fechaInicio: new Date(formValues.fechaInicio),
      fechaFin: !formValues.tratamientoPermanente ? new Date(formValues.fechaFin) : undefined,
      cantidad: formValues.cantidad,
      unidad: formValues.unidad,
      indicaciones: formValues.indicaciones,
      padecimiento: formValues.padecimiento,
      idPadecimiento: formValues.idPadecimiento,
      idUsuarioDoctor: formValues.idUsuarioDoctor,
      imagenBase64: this.photo?.base64String || "",
      recordatorioActivo: formValues.recordatorioActivo,
      diaSemana: formValues.recordatorioActivo ? formValues.diaSemana : null,
      horas: formValues.recordatorioActivo ? horasTiempos : null,
    };
    this.perfilTratamientoDto = tratamientoDto;

    if(this.accion == "editar"){
      this.editar(this.perfilTratamientoDto);
    }
    else{
      this.agregar(this.perfilTratamientoDto);
    }
  };

  protected agregar(perfilTratamientoDto: ExpedienteTratamientoDetalleDto) {
    this.perfilTratamientoService.agregar(perfilTratamientoDto).subscribe({
      next: ()=> {

      }, 
      error: () => {
        this.btnSubmit = false;
      },
      complete: ()=> {
        this.btnSubmit = false;
        this.presentarAlertaSuccess("agregado");
      }
    })
  }

  protected editar(perfilTratamientoDto: ExpedienteTratamientoDetalleDto) {
    this.perfilTratamientoService.editarTratamiento(perfilTratamientoDto).subscribe({
      next: ()=> {

      }, 
      error: () => {
        this.btnSubmit = false;
      },
      complete: ()=> {
        this.btnSubmit = false;
        this.presentarAlertaSuccess("editado");
      }
    })
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

  protected async presentarAlertaSuccess(accion: string) {

    const alertSuccess = await this.alertController.create({
      header: `Tratamiento ${accion} exitosamente`,
      buttons: [{
        text: 'De acuerdo',
        role: 'confirm',
        handler: () => {
          this.cerrarModal('confirm');
        }
      }],
      cssClass: 'custom-alert color-primary icon-check',
    });

    await alertSuccess.present();
  }

  protected cerrarModal(rol: string){
    this._modalCtrl.dismiss(null, rol);
  }

  protected setOpen(isOpen: boolean) {
    this.isModalRecordatorioOpen = isOpen;
  }

}
