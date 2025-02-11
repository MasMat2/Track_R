import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AsistenteDoctorDto } from '@dtos/seguridad/asistente-doctor-dto';
import { UsuarioExpedienteGridDTO } from '@dtos/seguridad/usuario-expediente-grid-dto';
import { ExpedienteTrackR } from '@models/seguridad/expediente-trackr';
import { Usuario } from '@models/seguridad/usuario';
import { UsuarioDomicilio } from '@models/seguridad/usuario-domicilio';
import { UsuarioEncabezado } from '@models/seguridad/usuario-encabezado';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private url = 'usuario/';

  constructor(public http: HttpClient) {}

  procesarUsuario(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(this.url + 'procesarUsuario', usuario);
  }

  consultarEncabezado() {
    return this.http.get<UsuarioEncabezado>(this.url + `consultarEncabezado`);
  }

  consultarExistencia(correo: string) {
    return this.http.post<boolean>(this.url + `consultarExistencia`, { correo });
  }

  consultarPorRol(claveRol: string) {
    return this.http.get<Usuario[]>(this.url + `consultarPorRol/${claveRol}`);
  }

  consultarExistenciaAdministrador(correo: string) {
    return this.http.post<boolean>(this.url + `consultarExistenciaAdministrador`, { correo });
  }

  editarMiPerfil(usuario: Usuario) {
    return this.http.put<void>(this.url + 'editarMiUsuario', usuario);
  }

  editarLocacionAdministrador(usuario: Usuario) {
    return this.http.put<void>(this.url + 'editarLocacionAdministrador', usuario);
  }

  consultarMiPerfil() {
    return this.http.get<Usuario>(this.url + `consultarMiUsuario`);
  }

  consultarTipoDeUsuarioEnSesion() {
    return this.http.get(this.url + `consultarTipoDeUsuarioEnSesion`, { responseType: 'text' });
  }

  consultar(idUsuario: number) {
    return this.http.get<Usuario>(this.url + `consultar/${idUsuario}`);
  }
  consultarPorRFC(rfc: string) {
    return this.http.get<Usuario>(this.url + `consultarPorRFC/${rfc}`);
  }

  consultarPorTipoUsuario(claveTipoUsuario: string) {
    return this.http.get<Usuario[]>(this.url + `consultarPorTipoUsuario/${claveTipoUsuario}`);
  }

  // consultarClinicosActivos(claveTipoUsuario: string) {
  //   return this.http.get<Usuario[]>(this.url + `consultarClinicosActivos/${claveTipoUsuario}`);
  // }

  agregar(usuario: Usuario) {
    return this.http.post<number>(this.url + 'agregar', usuario);
  }

  editar(usuario: Usuario) {
    return this.http.put<void>(this.url + 'editar', usuario);
  }

  editarAdministrador(usuario: Usuario) {
    return this.http.put<void>(this.url + 'editarAdministrador', usuario);
  }

  eliminar(idUsuario: number) {
    return this.http.delete<void>(this.url + `eliminar/${idUsuario}`);
  }

  consultarGeneral() {
    return this.http.get<Usuario[]>(this.url + 'consultarGeneral');
  }
  consultarParaPuntoVenta() {
    return this.http.get<Usuario[]>(this.url + 'consultarParaPuntoVenta');
  }

  // ConsultarPorRolActivosParaSelector(roles: number[]) {
  //   return this.http.post<Usuario[]>(this.url + `ConsultarPorRolActivosParaSelector`, roles);
  // }

  ConsultarPorRolCompaniaParaSelector(roles: number[]) {
    return this.http.post<Usuario[]>(this.url + `ConsultarPorRolCompaniaParaSelector`, roles);
  }

  ConsultarPorRolCompaniaParaSelectorDomicilio(roles: number[]) {
    return this.http.post<Usuario[]>(this.url + `ConsultarPorRolCompaniaParaSelectorDomicilio`, roles);
  }

  // consultarUsuariosParaRegistrarEntrada() {
  //   return this.http.get<Usuario[]>(this.url + 'consultarUsuariosParaRegistrarEntrada');
  // }

  consultarMedico(cedula: string) {
    return this.http.get<Usuario>(this.url + `consultarMedico/${cedula}`);
  }
  consultarMedicoId(idUsuario: number) {
    return this.http.get<Usuario>(this.url + `consultarMedicoId/${idUsuario}`);
  }

  agregarMedico(usuario: Usuario) {
    return this.http.post<number>(this.url + 'agregarMedico', usuario);
  }

  consultarBusquedaGridFiltro(usuario: Usuario) {
    return this.http.post<Usuario[]>(this.url + 'consultarBusquedaGridFiltro', usuario);
  }

  consultarPorLocacionParaSelector() {
    return this.http.get<Usuario[]>(this.url + `consultarPorLocacionParaSelector`);
  }

  consultarPorLocacionSeleccionadaParaSelector(idLocacion: number) {
    return this.http.get<Usuario[]>(this.url + `consultarPorLocacionSeleccionadaParaSelector/${idLocacion}`);
  }

  consultarParaSelector() {
    return this.http.get<Usuario[]>(this.url + `consultarParaSelector`);
  }


  consultarPorNombre(nombre: string) {
    return this.http.get<Usuario[]>(this.url + `consultarPorNombre/${nombre}`);
  }

  consultaDomicilioPorId(idUsuario: number){
    return  this.http.get<UsuarioDomicilio>(this.url + `consultaDomicilioPorId/${idUsuario}`);
  }

  consultarAsistentes(){
    return this.http.get<Usuario[]>(this.url + `consultarAsistentes`);
  }

  misAsistentes()
  {
    return this.http.get<AsistenteDoctorDto[]>(this.url + `asistentesPorDoctor`);
  }

  misDoctores()
  {
    return this.http.get<AsistenteDoctorDto[]>(this.url + `misDoctores`);
  }

  agregarAsistente(idAsistentes : number[])
  {
    return this.http.post<void>(this.url + `asistente/agregar` , idAsistentes);
  }
  
  eliminarAsistente(idAsistentes : number[])
  {
    return this.http.post<void>(this.url + `asistente/eliminar`, idAsistentes);
  }

  esAsistente()
  {
    return this.http.get<boolean>(this.url + `esAsistente`);
  }

  esMedico()
  {
    return this.http.get<boolean>(this.url + `esMedico`);
  }

  consultarPersonalParaSelector() {
    return this.http.get<Usuario[]>(this.url + 'consultarPersonalParaSelector');
  }

  consultarPacientesParaSelector() {
    return this.http.get<Usuario[]>(this.url + 'consultarPacientesParaSelector');
  }
}
