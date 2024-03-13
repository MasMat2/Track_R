import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pdf-visor',
  templateUrl: './pdf-visor.component.html',
  styleUrls: ['./pdf-visor.component.scss']
})
export class PdfVisorComponent implements OnInit {
  @Input() archivo: string;
  @Input() nombre: string;
  @Input() archivoNombre: string;
  @Input() height: string = "80vh";

  public onClose: any;
  
  constructor() { }

  ngOnInit() {
  }

  cancelar() {
    this.onClose(true);
  }

}
