import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UnidadMedidaComponent } from './unidad-medida.component';
import { UnidadMedidaRoutingModule } from './unidad-medida-routing.module';
import { CatalogoBaseModule } from '@sharedComponents/crud/catalogo-base/catalogo-base.module';
import { UnidadMedidaFormularioComponent } from './unidad-medida-formulario/unidad-medida-formulario.component';
import { UnidadMedidaFormularioModule } from './unidad-medida-formulario/unidad-medida-formulario.module';

@NgModule({
  imports: [
    CommonModule,
    UnidadMedidaRoutingModule,
    CatalogoBaseModule,
    UnidadMedidaFormularioModule
  ],
  declarations: [UnidadMedidaComponent]
})
export class UnidadMedidaModule { }
