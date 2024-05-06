import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';


@Component({
  selector: 'app-img-visor',
  templateUrl: './img-visor.component.html',
  styleUrls: ['./img-visor.component.scss']
})
export class ImgVisorComponent implements OnInit {

  @Input() nombreEstudio:string='';
  @Input() archivo: string='';
  @Input() archivoTipoMime:string='';
  @Input() nombre: string;

  public onClose: any;
  protected imageSrc: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer) {

  }

  ngOnInit() {
    this.imageSrc = this.sanitizer.bypassSecurityTrustResourceUrl(`data:${this.archivoTipoMime};base64,${this.archivo}`);
  }

  cancelar() {
    this.onClose(true);
  }
}