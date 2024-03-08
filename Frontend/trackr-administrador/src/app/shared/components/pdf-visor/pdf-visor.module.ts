import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PdfVisorComponent } from './pdf-visor.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';

@NgModule({
  imports: [
    CommonModule,
    NgxExtendedPdfViewerModule
  ],
  declarations: [PdfVisorComponent],
  exports: [PdfVisorComponent]
})
export class PdfVisorModule { }
