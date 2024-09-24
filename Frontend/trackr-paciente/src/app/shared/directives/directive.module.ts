import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormatoCorreoDirective } from './formato-correo.directive';
import { FormatoContrasenaDirective } from './formato-contrasena.directive';
import { ConfirmacionContrasenaDirective } from './confirmacion-contrasena.directive';
import { FormatoNombreDirective } from './formato-nombre.directive';
import { FormatoNumericoDirective } from './formato-numerico.directive';
import { FormatoRfcDirective } from './formato-rfc.directive';
import { PermitirSoloNumerosDirective } from './permitir-solo-numero.directive';
import { MaxDigitsDirective } from './max-digits.directive';



@NgModule({
  declarations: [
    FormatoCorreoDirective,
    FormatoContrasenaDirective,
    ConfirmacionContrasenaDirective,
    FormatoNombreDirective,
    FormatoNumericoDirective,
    FormatoRfcDirective,
    PermitirSoloNumerosDirective,
    MaxDigitsDirective
  ],
  imports: [
    CommonModule
  ],
  exports: [
    FormatoCorreoDirective,
    FormatoContrasenaDirective,
    ConfirmacionContrasenaDirective,
    FormatoNombreDirective,
    FormatoNumericoDirective,
    FormatoRfcDirective,
    PermitirSoloNumerosDirective,
    MaxDigitsDirective
  ]
})
export class DirectiveModule { }
