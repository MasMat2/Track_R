import { NgModule } from '@angular/core';
import { CatalogoBaseComponent } from './catalogo-base.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SharedModule } from 'src/app/shared/shared.module';
import { GridGeneralModule } from '@sharedComponents/grid-general/grid-general.module';

@NgModule({
    declarations: [CatalogoBaseComponent],
    exports: [CatalogoBaseComponent],
    imports: [
        SharedModule,
        GridGeneralModule,
        ModalModule.forChild()
    ],
    providers: []
})
export class CatalogoBaseModule {}
