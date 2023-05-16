import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CodigoAcceso } from "@utils/codigo-acceso";
import { AdministradorAuthGuard } from "src/app/auth/administrador-auth-guard.service";
import { ConfiguracionEntidadComponent } from "./configuracion-entidad.component";

const routes: Routes = [
    {
        path: '',
        component: ConfiguracionEntidadComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
            title: 'Entidad',
            acceso: CodigoAcceso.CONFIGURADOR_ENTIDAD
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ConfiguracionEntidadRoutingModule {}
