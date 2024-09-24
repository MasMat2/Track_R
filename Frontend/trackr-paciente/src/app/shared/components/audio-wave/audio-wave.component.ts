import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { ArchivoService } from '@http/archivo/archivo.service';
import WaveSurfer from 'wavesurfer.js';

@Component({
  selector: 'app-audio-wave',
  templateUrl: './audio-wave.component.html',
  styleUrls: ['./audio-wave.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class AudioWaveComponent implements AfterViewInit, OnInit {
  @Input() height: number;
  @Input() width: number;
  @Input() idArchivo: number;
  @Input() color: 'light' | 'dark';
  @Output() isAudioPlaying: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected isPlaying: boolean = false;
  protected duracionAudio: any;
  //remainingTime: any;
  protected archivo: any;

  @ViewChild('waveform', { static: false })
  waveformEl: ElementRef<any>;

  private waveform: WaveSurfer;

  constructor(private archivoService: ArchivoService) {}

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.waveform = WaveSurfer.create({
      container: this.waveformEl.nativeElement,
      barWidth: 2,
      barGap: 2,
      height: this.height,
      width: this.width,
      waveColor: '#FFFFFF',
      progressColor: '#A8A6D3',
      //waveColor: (this.color == 'dark' ? '#FFFFFF' : '#958FC5'),
      //progressColor: (this.color == 'dark' ? '#7C7C7C' : '#695e93'),
      cursorWidth: 0,
    });
    this.events();
    this.descargarArchivo(this.idArchivo);
  }

  events() {
    // this.waveform.on('audioprocess', () => {
    //   let totalTime = this.waveform.getDuration();
    //   let currentTime = this.waveform.getCurrentTime();
    //   this.remainingTime = totalTime - currentTime;
    //   //console.log(this.remainingTime);
    // })

    this.waveform.on('ready', () => {
      this.duracionAudio = this.calcularDuracion(this.waveform.getDuration());
    });

    this.waveform.on('interaction', () => {
      this.waveform.play();
    });
    this.waveform.on('play', () => {
      this.isPlaying = true;
      this.isAudioPlayingEvent(true);
    });
    this.waveform.on('pause', () => {
      this.isPlaying = false;
      this.isAudioPlayingEvent(false);
    });
  }

  protected playAudio() {
    this.waveform.play();
  }

  protected pauseAudio() {
    this.waveform.pause();
  }

  protected switchPlay() {
    if (!this.isPlaying) this.playAudio();
    else {
      this.pauseAudio();
    }
  }

  protected calcularDuracion(duracionSegundos: number) {
    const minutos = Math.floor(duracionSegundos / 60);
    const segundos = (duracionSegundos % 60).toString().split('.')[0].padStart(2, '0');
    const duracionAudio = `${minutos}:${segundos}`;

    return duracionAudio;
  }

  descargarArchivo(idArchivo: number){
    this.archivoService.getArchivo(idArchivo).subscribe({
      next: (res) => {
        this.archivo = `data:${res.archivoMime};base64,${res.archivo}`;
      },
      error: () => {
        console.error("Error al descargar el archivo");
      },
      complete: () => {
        fetch(this.archivo).then(res => res.blob()).then((blob) => {
          this.waveform.loadBlob(blob);
        });
      },
    });
  }

  private isAudioPlayingEvent(option: boolean) {
    this.isAudioPlaying.emit(option);
  }
}
