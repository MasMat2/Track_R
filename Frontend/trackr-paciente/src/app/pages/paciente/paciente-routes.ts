import { Route } from "@angular/router";
import { DashboardPage } from "./dashboard/dashboard.page";
import { ConfiguracionDashboardPage } from "./configuracion-dashboard/configuracion-dashboard.page";

export default [
    { path: '', component: DashboardPage },
    { path: 'configuracion', component: ConfiguracionDashboardPage }

] as Route[];
