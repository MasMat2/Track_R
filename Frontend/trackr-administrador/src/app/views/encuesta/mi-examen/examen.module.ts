import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { GridGeneralModule } from 'src/app/shared/components/grid-general/grid-general.module';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { ExamenComponent } from './examen.component';
import { ExamenRoutingModule } from './examen.routing.module';

@NgModule({
    imports: [
      CommonModule,
      GridGeneralModule,
      DirectiveModule,
      ModalModule.forChild(),
      ExamenRoutingModule,
    ],
    declarations: [ExamenComponent],
    providers: []
  })
  export class ExamenModule {}
