import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
import { NgxExtendedPdfViewerComponent, NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { ArchivoGetDTO } from 'src/app/shared/Dtos/archivos/archivo-get-dto';

@Component({
  selector: 'app-archivo-previsualizar',
  templateUrl: './archivo-previsualizar.component.html',
  styleUrls: ['./archivo-previsualizar.component.scss'],
  standalone: true,
  imports: [CommonModule,IonicModule, NgxExtendedPdfViewerModule],
})
export class ArchivoPrevisualizarComponent  implements OnInit {
  protected archivo:ArchivoGetDTO;
  protected type:string;
  protected archivoBase64:string;

  constructor(private modalController:ModalController) { }

  ngOnInit() {
    this.type = this.archivo.archivoMime.split('/')[0]
    this.archivoBase64 = 'data:' + this.archivo.archivoMime + ';base64,' + this.archivo.archivo
  }

  regresarBtn(){
    this.modalController.dismiss();
  }

}
