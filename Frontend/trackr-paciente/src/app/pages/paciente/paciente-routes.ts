import { Route } from "@angular/router";
import { DashboardPage } from "./dashboard/dashboard.page";
import { RegistroPage } from "./registro/registro.page";

export default [
    { path: '', component: DashboardPage },
    { path: 'registro', component: RegistroPage }
] as Route[];
