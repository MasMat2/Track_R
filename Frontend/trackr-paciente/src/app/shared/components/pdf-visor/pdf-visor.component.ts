import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { PdfViewerModule } from 'ng2-pdf-viewer';

@Component({
  selector: 'app-pdf-visor',
  templateUrl: './pdf-visor.component.html',
  styleUrls: ['./pdf-visor.component.scss'],
  standalone: true,
  imports: [
    IonicModule,
    CommonModule,
    PdfViewerModule
  ]
})
export class PdfVisorComponent  implements OnInit {

  @Input()
  Base64Src: string;
  
  protected zoom: number = 1;
  protected pagina: number = 1;
  protected paginasCargadas: boolean = false;
  protected documentoCargado: boolean = false;

  //pdfSrc = "https://vadimdez.github.io/ng2-pdf-viewer/assets/pdf-test.pdf"; //pdf de prueba


  
  
  constructor() { 
    addIcons({
      'plus': 'assets/img/svg/plus.svg',
      'minus': 'assets/img/svg/minus.svg',
      'chevron-right': 'assets/img/svg/chevron-right.svg',
      'chevron-left': 'assets/img/svg/chevron-left.svg',
    })
  }

  ngOnInit() {
  }


  protected loadcomplete(event: any){
    this.documentoCargado = true;
  }

  protected pageInitialized(e: CustomEvent) {
    this.paginasCargadas = true;
  }

  protected onError(error: any) {
    console.error(error);
    //alert de error
  }

  protected masZoom(){
    this.zoom += 0.2;
  }

  protected menosZoom(){
    if(this.zoom > 0.6){
      this.zoom -= 0.2;
    }
  }
  
  protected avanzarPagina(){
    this.pagina += 1;
  }

  protected retrocederPagina(){
    if(this.pagina > 1){
      this.pagina -= 1;
    }
  }

}
