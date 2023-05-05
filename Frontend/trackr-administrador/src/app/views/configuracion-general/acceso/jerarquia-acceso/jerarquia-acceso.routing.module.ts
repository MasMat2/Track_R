import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdministradorAuthGuard } from "src/app/auth/administrador-auth-guard.service";
import { JerarquiaAccesoComponent } from "./jerarquia-acceso.component";

const routes: Routes = [
    {
        path: '',
        component: JerarquiaAccesoComponent,
        canActivate: [AdministradorAuthGuard],
        data: {
            title: 'Jerarquía Acceso'
            // acceso: CodigoAcceso.
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class JerarquiaAccesoRoutingModule {}
