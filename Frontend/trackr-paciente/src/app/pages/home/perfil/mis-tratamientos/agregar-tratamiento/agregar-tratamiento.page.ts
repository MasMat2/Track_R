import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AlertController, IonicModule } from '@ionic/angular';
import { PerfilTratamientoService } from '@http/gestion-perfil/perfil-tratamiento.service';
import { HeaderComponent } from '@pages/home/layout/header/header.component';
import {  ModalController } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { ExpedienteTratamientoDetalleDto } from 'src/app/shared/Dtos/gestion-perfil/expediente-tratamiento-detalle-dto';
import { MisDoctoresService } from '@http/gestion-expediente/mis-doctores.service';
import { UsuarioDoctoresSelectorDto } from 'src/app/shared/Dtos/usuario-doctores-selector-dto';
import { ExpedientePadecimientoSelectorDTO } from '@dtos/seguridad/expediente-padecimiento-selector-dto';
import { ExpedientePadecimientoService } from '@http/gestion-expediente/expediente-padecimiento.service';

import { CapacitorUtils } from '@utils/capacitor-utils';
import { UnidadMedidaService } from 'src/app/services/dashboard/unidad-medida.service';
import { UnidadMedidaGridDto } from 'src/app/shared/Dtos/catalogo/unidad-medida-grid-dto';
import { FechaService } from '@services/fecha.service';

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
  ],
  providers: [CapacitorUtils, UnidadMedidaService]
})
export class AgregarTratamientoPage implements OnInit {

  protected readonly dateToday = this.fechaService.obtenerFechaActualISOString();

  protected accion: string;
  protected formTratamiento: FormGroup;
  protected perfilTratamientoDto: ExpedienteTratamientoDetalleDto;
  protected isFormModified: boolean = false;
  protected cantidadFarmaco: number = 1;
  protected isModalRecordatorioOpen: boolean = false;
  protected btnSubmit: boolean = false;
  protected fechaSeleccionada: string = this.dateToday;
  protected doctoresSelector: UsuarioDoctoresSelectorDto[];
  protected padecimientosSelector: ExpedientePadecimientoSelectorDTO[];

  protected isPictureTaken: boolean = false;
  protected archivo: any;
  protected archivoTipoMime: string;
  protected archivoNombre: string;
  private mimeType: string = '';

  protected tituloAccion: string = "Agregar";

  protected weekDays = [
     {id: 0 , name: 'L'},
     {id: 1 , name: 'M'},
     {id: 2 , name: 'M'},
     {id: 3 , name: 'J'},
     {id: 4 , name: 'V'},
     {id: 5 , name: 'S'},
     {id: 6 , name: 'D'},
  ];

  protected unidades : UnidadMedidaGridDto[];

  constructor(
    private perfilTratamientoService: PerfilTratamientoService,
    private doctoresService: MisDoctoresService,
    private expedientePadecimientoService: ExpedientePadecimientoService,
    private fb: FormBuilder,
    private _modalCtrl: ModalController,
    private alertController: AlertController,
    private capacitorUtils: CapacitorUtils,
    private unidadMedidaService: UnidadMedidaService,
    private fechaService: FechaService
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
      farmaco: ['', Validators.required],
      cantidad: ['1', Validators.required],
      unidad: ['', Validators.required],
      indicaciones: ['', Validators.required],
      idPadecimiento: ['', Validators.required],
      idUsuarioDoctor: ['', Validators.required],
      tratamientoPermanente: [false],
      fechaInicio: [(new Date()).toISOString(), Validators.required],
      fechaFin: [(new Date()).toISOString()],
      recordatorioActivo: [false],
      diaSemana: this.fb.array([false, false, false, false, false, false, false]),
      horas: this.fb.array([])
    },
      { validators: [this.validateDiaSemana(), this.compareDates()] });

    this.consutarUnidadesMedida();
    this.selectorPadecimientos();
    this.selectorDoctor();
    this.verificarAccionFormulario();
  }

  private verificarAccionFormulario(){
    if(this.accion == "editar"){
      this.tituloAccion = "Editar";
      this.rellenarValoresEditar();
    }
    else{
      this.tituloAccion = "Añadir";
      return
    }
  }

  private consutarUnidadesMedida(){
    this.unidadMedidaService.consultarParaGrid().subscribe({
      next: (data) => {
        this.unidades = data;
      }
    })
  }

  private rellenarValoresEditar(){
    this.formTratamiento.patchValue({
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
      horasFormArray.push(new FormControl(hora));
    });

    //asignar la imagen del medicamento
    if(this.perfilTratamientoDto.imagenBase64 != ""){
      this.archivo= `${this.perfilTratamientoDto.imagenBase64}`;
    }
    this.archivoNombre = this.perfilTratamientoDto.archivoNombre;
    this.archivoTipoMime = this.perfilTratamientoDto.archivoTipoMime;

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
    const fechalocalString = this.fechaService.fechaUTCAFechaLocal(fecha.toISOString());

    return fechalocalString;
  }

  // Selectores
  protected selectorPadecimientos(): void {
    this.expedientePadecimientoService.consultarPorUsuarioParaSelector().subscribe({
      next: (data) => {
        this.padecimientosSelector = data;
      }
    })
  }

  protected selectorDoctor(): void {
    //this.doctores$ = this.perfilTratamientoService.selectorDeDoctor();
    this.doctoresService.consultarPorUsuarioParaSelector().subscribe({
      next: (data) => {
        this.doctoresSelector = data;
      }
    })
  }

  // Camara
  protected async takePicture() {
    const image_src = await this.capacitorUtils.takePicture();

    const [, data] = image_src.split(',');
    const mimeType = image_src.split(':')[1].split(';')[0];

    this.archivo = data;
    this.archivoTipoMime = mimeType;
    this.archivoNombre = this.generateFileName();

    this.isPictureTaken = true;
    this.isFormModified= true;
  }

  // Recordatorios Horas
  protected removeHour(index: number) {
    this.isFormModified = true;
    this.horas.removeAt(index);
    //Al eliminar la ultima hora, se desactiva el recordatorio
    if(this.horas.length === 0){
      this.formTratamiento.patchValue({ recordatorioActivo: false });
    }
  };

  protected addHour() {
    this.isFormModified = true;
    //Al agregar la primer hora, se activa el recordatorio
    if(this.horas.length === 0){
      this.formTratamiento.patchValue({ recordatorioActivo: true });
    }
    const horaSeleccionada = this.fechaSeleccionada;
    const horaRepetida = this.validarHoraRepetida(horaSeleccionada, this.horas.value);

    if(horaRepetida){
      this.fechaSeleccionada = this.dateToday; //reiniciar selector de hora
      return
    }
    else{
      this.horas.push(this.fb.control(horaSeleccionada));
    }
    this.fechaSeleccionada = this.dateToday; //reiniciar selector de hora
  };

  // Validaciones

  protected validarHoraRepetida(horaSeleccionada: string, horasArray: string[]){
    // Extraer solo la parte de la hora y los minutos de la hora seleccionada
    const horaSeleccionadaHoraMinutos = horaSeleccionada.substring(11, 16);

    const horaRepetida = horasArray.some(hora => hora.substring(11, 16) === horaSeleccionadaHoraMinutos);

    return horaRepetida; // Retorna true si hay una hora repetida, false en caso contrario
  }

  // Valida que la fechaFin sea mayor o igual a fechaInicio
  protected compareDates(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const tratamientoPermanente = control.get('tratamientoPermanente')?.value;
      const fechaInicio = new Date(control.get('fechaInicio')?.value);
      const fechaFin = new Date(control.get('fechaFin')?.value);
    
      if (!tratamientoPermanente && fechaInicio && fechaFin && (fechaInicio > fechaFin)) {
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
      const nuevaHora = this.fechaService.fechaLocalAFechaUTC(hora); //pasar a UTC
      return nuevaHora.split('T')[1].split('.')[0]; 
    });

    const tratamientoDto: ExpedienteTratamientoDetalleDto = {
      idExpedienteTratamiento: this.perfilTratamientoDto?.idExpedienteTratamiento,//tendra valor solo cuando la accion sea editar
      farmaco: formValues.farmaco,
      fechaInicio: this.setHora(formValues.fechaInicio, "00:00:00"),
      fechaFin: !formValues.tratamientoPermanente ? (this.setHora(formValues.fechaFin, "00:00:00")) : undefined,
      cantidad: formValues.cantidad,
      unidad: formValues.unidad,
      indicaciones: formValues.indicaciones,
      padecimiento: formValues.padecimiento,
      idPadecimiento: formValues.idPadecimiento,
      idUsuarioDoctor: formValues.idUsuarioDoctor,
      imagenBase64: "",
      archivo: this.archivo,
      archivoNombre: this.archivoNombre,
      archivoTipoMime: this.archivoTipoMime,
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
    this.isFormModified = true;
    if(this.cantidadFarmaco < CANTIDAD_MAXIMA){
      this.cantidadFarmaco += 1;
      this.formTratamiento.patchValue({ cantidad: (this.cantidadFarmaco) });
    }
  }

  protected decrementarCantidad(){
    this.isFormModified = true;
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

  protected hayDiaSeleccionado(){
    const diaSemanaControl = this.formTratamiento.get('diaSemana') as FormArray;
    return diaSemanaControl.value.indexOf(true) !== -1; //true si hay al menos un dia seleccionado
  }

  protected setHora(fecha: string, nuevaHora: string){
    const date = new Date(fecha);
    const [horas, minutos, segundos] = nuevaHora.split(':').map(Number);
    date.setHours(horas, minutos, segundos);

    return date.toISOString().slice(0,-1);
  }

  protected eliminarAdjunto(){
    this.archivo = undefined;
    this.archivoNombre = "";
    this.archivoTipoMime = "";
    this.isFormModified = true;
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

  protected setOpenModalRecordatorio(isOpen: boolean) {
    this.isModalRecordatorioOpen = isOpen;
  }

  private generateFileName(): string {
    const date = new Date();
    const formattedDate = date.toISOString().split('T')[0];
    const timestamp = date.getTime();
    const extension = this.mimeType === 'image/png' ? 'png' : 'jpg';

    return `TrackrImage_${formattedDate}_${timestamp}.${extension}`;
  }

}
