import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { IonicModule, ModalController } from '@ionic/angular';
import { chevronBack } from 'ionicons/icons';
import { addIcons } from 'ionicons';

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
  protected imageSrc: SafeResourceUrl;

  constructor(private sanitizer: DomSanitizer,private modalController:ModalController) {
    addIcons({chevronBack})
  }

  ngOnInit(
  ) {
    this.imageSrc = this.sanitizer.bypassSecurityTrustResourceUrl(`data:${this.archivoTipoMime};base64,${this.archivo}`);

  }
  protected cancelar() {
    this.modalController.dismiss();
  }

}
