import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-image-only-modal',
  templateUrl: './image-only-modal.component.html',
  styleUrls: ['./image-only-modal.component.scss'],
   standalone: true,
   imports: [
    IonicModule,
    CommonModule,
    FormsModule
   ]
})
export class ImageOnlyModalComponent  implements OnInit {

  @Input() nombreImagen:string='';
  @Input() archivo: string='';
  @Input() archivoTipoMime:string='';
  protected imageSrc: SafeResourceUrl;
  
  constructor(
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    if(this.archivo || this.archivo != ""){
      this.imageSrc = `data:image/jpeg;base64,${this.archivo}`;
    }
  }

}
