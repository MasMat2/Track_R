import { NgModule } from '@angular/core';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CompaniaFormularioComponent } from './compania-formulario.component';
import { CompaniaFormularioRoutingModule } from './compania-formulario.routing.module';
import { CompaniaInformacionFormularioModule } from './compania-informacion-formulario/compania-informacion-formulario.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    ModalModule.forChild(),
    TabsModule,
    CompaniaFormularioRoutingModule,
    CompaniaInformacionFormularioModule,
  ],
  declarations: [CompaniaFormularioComponent],
  providers: []
})
export class CompaniaFormularioModule {}
