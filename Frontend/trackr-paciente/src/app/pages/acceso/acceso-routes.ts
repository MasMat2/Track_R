import { Route } from "@angular/router";
import { RegistroPage } from "./registro/registro.page";
import { LoginPage } from "./login/login.page";
import { AccesoPage } from "./acceso.page";

export default [
    { path: '', component: AccesoPage },
    { path: 'login', component: LoginPage },
    { path: 'registro', component: RegistroPage }
] as Route[];
