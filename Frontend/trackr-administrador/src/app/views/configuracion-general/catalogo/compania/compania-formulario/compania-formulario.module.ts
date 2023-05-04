import { NgModule } from '@angular/core';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CompaniaFormularioComponent } from './compania-formulario.component';
import { CompaniaFormularioRoutingModule } from './compania-formulario.routing.module';
import { CompaniaInformacionFormularioModule } from './compania-informacion-formulario/compania-informacion-formulario.module';
// import { ConfiguracionVigenciaModule } from './configuracion-vigencia/configuracion-vigencia.module';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    // SharedModule,
    CommonModule,
    ModalModule.forChild(),
    TabsModule,
    CompaniaFormularioRoutingModule,
    CompaniaInformacionFormularioModule,
    // ConfiguracionVigenciaModule
  ],
  declarations: [CompaniaFormularioComponent],
  providers: []
})
export class CompaniaFormularioModule {}
