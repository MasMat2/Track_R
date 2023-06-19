
import { NgModule } from '@angular/core';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ContenidoExamenFormularioModule } from './contenido-examen-formulario/contenido-examen-formulario.module';
import { ContenidoExamenComponent } from './contenido-examen.component';
import { ContenidoExamenRoutingModule } from './contenido-examen.routing.module';

@NgModule({
    imports: [
      SharedModule,
      ContenidoExamenRoutingModule,
      GridGeneralModule,
      ContenidoExamenFormularioModule
    ],
    declarations: [ContenidoExamenComponent],
    providers: []
  })
  export class ContenidoExamenModule {}
