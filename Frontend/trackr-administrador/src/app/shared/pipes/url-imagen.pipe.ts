import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Pipe({
  name: 'urlImagen'
})
export class UrlImagenPipe implements PipeTransform {

  constructor(
    private sanitizer: DomSanitizer
  ) { }

  transform(stringBase64?: string, tipoMime?: string): SafeUrl {
    if (!stringBase64) {
      return './assets/img/default.png';
    }

    if (tipoMime === undefined) {
      tipoMime = 'image/jpeg';
    }

    const url = `data:${tipoMime};base64,${stringBase64}`;
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

}
