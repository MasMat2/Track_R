import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { IonicModule, ModalController } from '@ionic/angular';

@Component({
  selector: 'app-imagen-visor',
  templateUrl: './imagen-visor.component.html',
  styleUrls: ['./imagen-visor.component.scss'],
  standalone:true,
  imports:[
    IonicModule
  ],
})
export class ImagenVisorComponent  implements OnInit {
  @Input() nombreEstudio:string='';
  @Input() archivo: string='';
  @Input() archivoTipoMime:string='';
  imageSrc: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer,private modalController:ModalController) {
  }

  ngOnInit(
  ) {
    this.imageSrc = this.sanitizer.bypassSecurityTrustResourceUrl(`data:${this.archivoTipoMime};base64,${this.archivo}`);

  }
  cancelar() {
    this.modalController.dismiss();
  }

}
