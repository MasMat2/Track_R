import { NgModule } from '@angular/core';
import { GeneroService } from '@http/catalogo/genero.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DirectiveModule } from 'src/app/shared/directives/directive.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { GeneroFormularioComponent } from './genero-formulario/genero-formulario.component';
import { GeneroComponent } from './genero.component';
import { GeneroRoutingModule } from './genero.routing.module';
import { LucideAngularModule, X } from 'lucide-angular';
@NgModule({
    imports: [
        GeneroRoutingModule,
        SharedModule,
        DirectiveModule,
        ModalModule.forRoot(),
        LucideAngularModule.pick({X}),

    ],
    declarations: [
    GeneroComponent,
    GeneroFormularioComponent
    ],
    entryComponents: [GeneroFormularioComponent],
    providers:[
        GeneroService
    ]

})
export class GeneroModule {}