import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { IonicModule, ModalController } from '@ionic/angular';

@Component({
  selector: 'app-pdf-visor',
  templateUrl: './pdf-visor.component.html',
  styleUrls: ['./pdf-visor.component.scss'],
  standalone:true,
  imports:[CommonModule,
    NgxExtendedPdfViewerModule,IonicModule]
})
export class PdfVisorComponent implements OnInit {
  @Input() archivo: string='';
  @Input() nombre: string='';
  @Input() archivoNombre: string='';
  @Input() height: string = "80vh";

  public onClose: any;
  
  constructor(private modalController:ModalController) { }

  ngOnInit() {
  }

  cancelar() {
    this.modalController.dismiss();
  }

}
