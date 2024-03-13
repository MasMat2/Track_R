import { NgModule } from '@angular/core';
import { PermitirSoloNumerosDirective } from './permitir-solo-numero.directive';
import { FormatoCodigoPostalDirective } from './formato-codigo-postal.directive';

@NgModule({
  declarations: [
    PermitirSoloNumerosDirective,
    FormatoCodigoPostalDirective,
  ],
  exports: [
    PermitirSoloNumerosDirective,
    FormatoCodigoPostalDirective,
  ]
})
export class DirectiveModule {}
