"use strict";(self.webpackChunkapp=self.webpackChunkapp||[]).push([[6472],{6472:(en,P,c)=>{c.r(P),c.d(P,{InformacionGeneralComponent:()=>$});var d=c(5861),_=c(6895),g=c(433),l=c(9149),M=c(429),v=c(9808),x=c(8505),m=c(1241),y=c(2583),C=c(1297),O=c(4384),Z=c(8508),n=c(1571),A=c(6558),U=c(6101),G=c(6441),I=c(1757),T=c(6902),E=c(9231),w=c(5302),D=c(6958),k=c(226),b=c(529);let F=(()=>{var r;class s{constructor(o){this.http=o,this.dataUrl="confirmacionCorreo/"}enviarCorreoConfirmacion(o){return this.http.post(this.dataUrl+"enviarCorreoConfirmacion",o)}}return(r=s).\u0275fac=function(o){return new(o||r)(n.LFG(b.eN))},r.\u0275prov=n.Yz7({token:r,factory:r.\u0275fac,providedIn:"root"}),s})(),q=(()=>{var r;class s{constructor(o){this.http=o,this.dataUrl="genero/"}consultarGeneros(){return this.http.get(this.dataUrl)}}return(r=s).\u0275fac=function(o){return new(o||r)(n.LFG(b.eN))},r.\u0275prov=n.Yz7({token:r,factory:r.\u0275fac,providedIn:"root"}),s})();const S=["formulario"];function J(r,s){if(1&r){const i=n.EpF();n.TgZ(0,"div",40)(1,"p"),n._uU(2,"Tu correo a\xfan no est\xe1 verificado"),n.qZA(),n.TgZ(3,"ion-button",41),n.NdJ("click",function(){n.CHM(i);const t=n.oxw(2);return n.KtG(t.reenviarConfirmacionCorreo(t.infoUsuario.correo))}),n._uU(4," Enviar correo de verificaci\xf3n "),n.qZA()()}if(2&r){const i=n.oxw(2);n.xp6(3),n.Q6J("disabled",i.emailsubmiting)}}function N(r,s){1&r&&(n.TgZ(0,"div",42),n._UZ(1,"ion-icon",43),n._uU(2," El nombre es requerido "),n.qZA())}function L(r,s){1&r&&(n.TgZ(0,"div",42),n._uU(1," El apellido paterno es requerido "),n.qZA())}function Y(r,s){1&r&&(n.TgZ(0,"div",42),n._uU(1," El apellido materno es requerido "),n.qZA())}function Q(r,s){if(1&r){const i=n.EpF();n.TgZ(0,"ion-datetime",44,45),n.NdJ("ionChange",function(){n.CHM(i);const t=n.oxw(2);return n.KtG(t.calcularEdad())})("ngModelChange",function(t){n.CHM(i);const e=n.oxw().ngIf;return n.KtG(e.fechaNacimiento=t)}),n.qZA()}if(2&r){const i=n.oxw().ngIf;n.Q6J("ngModel",i.fechaNacimiento)}}function z(r,s){if(1&r){const i=n.EpF();n.TgZ(0,"ion-icon",46),n.NdJ("click",function(){n.CHM(i);const t=n.oxw(2);return n.KtG(t.openGeneroModal())}),n.qZA()}}function K(r,s){1&r&&n._UZ(0,"ion-icon",47)}function B(r,s){1&r&&(n.TgZ(0,"div",42),n._uU(1," El peso es requerido "),n.qZA())}function H(r,s){1&r&&(n.TgZ(0,"div",42),n._uU(1," La medida de estatura es requerida "),n.qZA())}function j(r,s){1&r&&(n.TgZ(0,"div",42),n._uU(1," El n\xfamero de tel\xe9fono es requerido "),n.qZA())}function W(r,s){1&r&&(n.TgZ(0,"div",42),n._uU(1," El correo electr\xf3nico es requerido "),n.qZA())}function V(r,s){if(1&r){const i=n.EpF();n.TgZ(0,"div")(1,"form",8,9),n.NdJ("ngSubmit",function(){n.CHM(i);const t=n.MAs(2),e=n.oxw();return n.KtG(e.enviarFormulario(t))}),n.TgZ(3,"ion-grid"),n.YNc(4,J,5,1,"div",10),n.TgZ(5,"div",11)(6,"ion-row")(7,"ion-col",12)(8,"div",13)(9,"ion-label"),n._uU(10," Nombre(s) "),n.qZA(),n.TgZ(11,"ion-input",14,15),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.nombre=t)}),n.qZA(),n.YNc(13,N,3,0,"div",16),n.qZA()()(),n.TgZ(14,"ion-row")(15,"ion-col",17)(16,"div",13)(17,"ion-label"),n._uU(18," Apellido Paterno "),n.qZA(),n.TgZ(19,"ion-input",18,19),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.apellidoPaterno=t)}),n.qZA(),n.YNc(21,L,2,0,"div",16),n.qZA()(),n.TgZ(22,"ion-col",17)(23,"div",13)(24,"ion-label"),n._uU(25," Apellido Materno "),n.qZA(),n.TgZ(26,"ion-input",20,21),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.apellidoMaterno=t)}),n.qZA(),n.YNc(28,Y,2,0,"div",16),n.qZA()()(),n.TgZ(29,"ion-row")(30,"ion-col",12)(31,"div",13)(32,"ion-label"),n._uU(33," Fecha de Nacimiento "),n.qZA(),n.TgZ(34,"ion-item",22),n._UZ(35,"ion-datetime-button",23),n.TgZ(36,"ion-modal",24),n.YNc(37,Q,2,1,"ng-template"),n.qZA()()()()(),n.TgZ(38,"ion-row")(39,"ion-col",12)(40,"div",13)(41,"ion-label"),n._uU(42," Edad "),n.qZA(),n.TgZ(43,"ion-input",25),n.NdJ("ngModelChange",function(t){n.CHM(i);const e=n.oxw();return n.KtG(e.edadUsuario=t)}),n.qZA()()()(),n.TgZ(44,"ion-row")(45,"ion-col",12)(46,"div",13)(47,"ion-label"),n._uU(48," G\xe9nero "),n.qZA(),n.TgZ(49,"ion-input",26),n.YNc(50,z,1,0,"ion-icon",27),n.YNc(51,K,1,0,"ion-icon",28),n.qZA()()()(),n.TgZ(52,"ion-row")(53,"ion-col",17)(54,"div",13)(55,"ion-label"),n._uU(56," Peso (kg) "),n.qZA(),n.TgZ(57,"ion-input",29,30),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.peso=t)}),n.TgZ(59,"ion-label",31),n._uU(60," Kg "),n.qZA()(),n.YNc(61,B,2,0,"div",16),n.qZA()(),n.TgZ(62,"ion-col",17)(63,"div",13)(64,"ion-label"),n._uU(65," Estatura "),n.qZA(),n.TgZ(66,"ion-input",32,33),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.estatura=t)}),n.TgZ(68,"ion-label",31),n._uU(69," cm "),n.qZA()(),n.YNc(70,H,2,0,"div",16),n.qZA()()(),n.TgZ(71,"ion-row")(72,"ion-col",12)(73,"div",13)(74,"ion-label"),n._uU(75," Tel\xe9fono m\xf3vil "),n.qZA(),n.TgZ(76,"ion-input",34,35),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.telefonoMovil=t)}),n.qZA(),n.YNc(78,j,2,0,"div",16),n.qZA()()(),n.TgZ(79,"ion-row")(80,"ion-col",12)(81,"div",13)(82,"ion-label"),n._uU(83," Correo electr\xf3nico "),n.qZA(),n.TgZ(84,"ion-input",36,37),n.NdJ("ngModelChange",function(t){const a=n.CHM(i).ngIf;return n.KtG(a.correoPersonal=t)}),n.qZA(),n.YNc(86,W,2,0,"div",16),n.qZA()()()(),n.TgZ(87,"ion-row",38)(88,"ion-col")(89,"ion-button",39),n._uU(90,"Guardar"),n.qZA()()()()()()}if(2&r){const i=s.ngIf,o=n.MAs(12),t=n.MAs(20),e=n.MAs(27),a=n.MAs(58),u=n.MAs(67),f=n.MAs(77),h=n.MAs(85),p=n.oxw();n.xp6(4),n.Q6J("ngIf",!i.correoConfirmado),n.xp6(7),n.Q6J("ngModel",i.nombre),n.xp6(2),n.Q6J("ngIf",(null==o.errors?null:o.errors.required)&&(o.dirty||o.touched)),n.xp6(6),n.Q6J("ngModel",i.apellidoPaterno),n.xp6(2),n.Q6J("ngIf",(null==t.errors?null:t.errors.required)&&(t.dirty||t.touched)),n.xp6(5),n.Q6J("ngModel",i.apellidoMaterno),n.xp6(2),n.Q6J("ngIf",(null==e.errors?null:e.errors.required)&&(e.dirty||e.touched)),n.xp6(8),n.Q6J("keepContentsMounted",!0),n.xp6(7),n.Q6J("ngModel",p.edadUsuario),n.xp6(6),n.Q6J("ngModel",p.nombreGenero),n.xp6(1),n.Q6J("ngIf",!p.modalGeneroAbierto),n.xp6(1),n.Q6J("ngIf",p.modalGeneroAbierto),n.xp6(6),n.Q6J("ngModel",i.peso),n.xp6(4),n.Q6J("ngIf",(null==a.errors?null:a.errors.required)&&(a.dirty||a.touched)),n.xp6(5),n.Q6J("ngModel",i.estatura),n.xp6(4),n.Q6J("ngIf",(null==u.errors?null:u.errors.required)&&(u.dirty||u.touched)),n.xp6(6),n.Q6J("ngModel",i.telefonoMovil),n.xp6(2),n.Q6J("ngIf",(null==f.errors?null:f.errors.required)&&(f.dirty||f.touched)),n.xp6(6),n.Q6J("ngModel",i.correoPersonal),n.xp6(2),n.Q6J("ngIf",(null==h.errors?null:h.errors.required)&&(h.dirty||h.touched)),n.xp6(3),n.Q6J("disabled",p.submiting)}}let $=(()=>{var r;class s{constructor(o,t,e,a,u,f,h,p,R,X,nn,on,tn){this.usuarioService=o,this.paisService=t,this.estadoService=e,this.municipioService=a,this.localidadService=u,this.coloniaService=f,this.codigoPostalService=h,this.entidadEstructuraService=p,this.doctoresService=R,this.alertController=X,this.confirmacionCorreoService=nn,this.generoService=on,this.modalController=tn,this.submiting=!1,this.emailsubmiting=!1,this.esPaisExtranjero=!1,this.nuevoPadecimiento=new m.h,this.nuevoAntecedente=new m.h,this.nuevoDiagnostico=new m.h,this.nuevoAntecedenteInvalido=!1,this.nuevoDiagnosticoInvalido=!1,this.modalGeneroAbierto=!1,this.paisList=[],this.estadoList=[],this.municipioList=[],this.localidadList=[],this.coloniaList=[],this.antecedenteList=[],this.diagnosticoList=[],this.antecedenteFiltradoList=[],this.diagnosticoFiltradoList=[],this.customAlertOptions={cssClass:"custom-select-alert"},(0,y.a)({addCircleOutline:C.hd1,closeCircleOutline:C.UMR,chevronBack:C.Mny,chevronDown:C.Dd1,chevronUp:C.m5K,informacion:"assets/img/svg/info.svg"})}ngOnInit(){var o=this;return(0,d.Z)(function*(){yield o.consultarGeneros(),o.obtenerUsuario(),o.consultarPaises()})()}ionViewWillEnter(){this.consultarDoctores()}openGeneroModal(){var o=this;return(0,d.Z)(function*(){const t=yield o.modalController.create({component:Z.Q,breakpoints:[0,1],initialBreakpoint:1,cssClass:"custom-sheet-modal",componentProps:{generoList:o.generoList}});yield t.present(),o.modalGeneroAbierto=!0;const{data:e}=yield t.onWillDismiss();e&&(o.infoUsuario.idGenero=e.id,o.nombreGenero=e.descripcion),o.modalGeneroAbierto=!1})()}consultarPaises(){var o=this;return(0,d.Z)(function*(){const t=yield o.paisService.consultarTodosParaSelector().toPromise();o.paisList=null!=t?t:[]})()}consultarEstados(o){var t=this;return(0,d.Z)(function*(){const e=o>0?yield t.estadoService.consultarPorPaisParaSelector(o).toPromise():[];t.estadoList=null!=e?e:[]})()}consultarMunicipios(o){var t=this;return(0,d.Z)(function*(){const e=o>0?yield t.municipioService.consultarPorEstadoParaSelector(o).toPromise():[];t.municipioList=null!=e?e:[]})()}consultarLocalidades(o){var t=this;return(0,d.Z)(function*(){const e=o>0?yield t.localidadService.consultarPorEstado(o).toPromise():[];t.localidadList=null!=e?e:[]})()}consultarColonias(o){var t=this;return(0,d.Z)(function*(){const e=o&&5===o.length?yield t.coloniaService.consultarPorCodigoParaSelector(o).toPromise():[];t.coloniaList=null!=e?e:[]})()}onChangePais(){var o=this;return(0,d.Z)(function*(){o.esPaisExtranjero=o.infoUsuario.idPais!==o.idPaisMexico,o.infoUsuario.idEstado=0,yield o.consultarEstados(o.infoUsuario.idPais),o.onChangeEstado()})()}onChangeEstado(){var o=this;return(0,d.Z)(function*(){o.infoUsuario.idMunicipio=0,o.infoUsuario.idLocalidad=0,yield Promise.all([o.consultarMunicipios(o.infoUsuario.idEstado),o.consultarLocalidades(o.infoUsuario.idEstado)])})()}onChangeCodigoPostal(){var o=this;return(0,d.Z)(function*(){o.infoUsuario.idColonia=0;const t=o.infoUsuario.codigoPostal;5===t.length&&(yield o.asignarValoresDeCodigoPostal(t))})()}asignarValoresDeCodigoPostal(o){var t=this;return(0,d.Z)(function*(){const e=yield t.codigoPostalService.consultarPorCodigoPostal(o).toPromise().then(u=>u&&u.length>0?u[0]:null);if(!e)return;t.infoUsuario.idEstado=e.idEstado,yield Promise.all([t.consultarMunicipios(e.idEstado),t.consultarLocalidades(e.idEstado)]),t.infoUsuario.idMunicipio=e.idMunicipio,t.infoUsuario.idLocalidad=0,yield t.consultarColonias(o);const a=t.coloniaList.find(u=>M.q1(u.nombre,e.colonia));a&&(t.infoUsuario.idColonia=a.idColonia)})()}consultarAntecedentes(){var o=this;return(0,d.Z)(function*(){return(0,v.n)(o.entidadEstructuraService.consultarAntecedentesParaSelector()).then(t=>{o.antecedenteList=t,o.antecedenteFiltradoList=t.filter(e=>!o.infoUsuario.padecimientos.some(a=>a.idPadecimiento===e.idPadecimiento))})})()}consultarDiagnosticos(){var o=this;return(0,d.Z)(function*(){return(0,v.n)(o.entidadEstructuraService.consultarDiagnosticosParaSelector()).then(t=>{o.diagnosticoList=t,o.diagnosticoFiltradoList=t.filter(e=>!o.infoUsuario.padecimientos.some(a=>a.idPadecimiento===e.idPadecimiento))})})()}consultarDoctores(){this.doctoresService.consultarExpediente().subscribe(o=>{this.misDoctores=o})}consultarGeneros(){var o=this;return(0,d.Z)(function*(){o.generoService.consultarGeneros().subscribe(t=>{o.generoList=t})})()}obtenerUsuario(){this.informacionUsuario$=this.usuarioService.consultarInformacionGeneral().pipe((0,x.b)(o=>{this.infoUsuario=o;const t=this.generoList.find(e=>e.idGenero===this.infoUsuario.idGenero);this.nombreGenero=t?t.descripcion:"",this.consultarEstados(this.infoUsuario.idPais),this.consultarMunicipios(this.infoUsuario.idEstado),this.consultarLocalidades(this.infoUsuario.idEstado),this.consultarColonias(this.infoUsuario.codigoPostal),this.consultarAntecedentes(),this.consultarDiagnosticos(),this.calcularEdad()}))}actualizarInformacionUsuario(o){this.usuarioService.actualizarInformacionGeneral(o).subscribe({next:()=>{}})}calcularEdad(){let o=new Date(this.infoUsuario.fechaNacimiento),e=M.KP(o,new Date).years+" a\xf1os ";this.edadUsuario=e}enviarFormulario(o){var t=this;return(0,d.Z)(function*(){if(t.submiting=!0,o.invalid)return M.CW(o),t.presentAlertError(),void(t.submiting=!1);t.presentAlert(),t.actualizarInformacionUsuario(t.infoUsuario),t.submiting=!1})()}eliminarPadecimiento(o){o<0||o>=this.infoUsuario.padecimientos.length||(this.infoUsuario.padecimientos.splice(o,1),0===this.infoUsuario.padecimientos.length&&this.agregarPadecimiento())}agregarPadecimiento(){const o=new m.h;o.idPadecimiento=0,this.infoUsuario.padecimientos=[...this.infoUsuario.padecimientos,o]}agregarAntecedente(){if(null==this.nuevoAntecedente.idPadecimiento||null==this.nuevoAntecedente.idUsuarioDoctor)return void(this.nuevoAntecedenteInvalido=!0);const o=new m.h;o.idPadecimiento=this.nuevoAntecedente.idPadecimiento,o.idUsuarioDoctor=this.nuevoAntecedente.idUsuarioDoctor,o.fechaDiagnostico=this.nuevoAntecedente.fechaDiagnostico,o.esAntecedente=!0,this.infoUsuario.padecimientos=[...this.infoUsuario.padecimientos,o],this.nuevoAntecedenteInvalido=!1,this.nuevoAntecedente=new m.h,this.consultarAntecedentes()}agregarDiagnostico(){if(null==this.nuevoDiagnostico.idPadecimiento||null==this.nuevoDiagnostico.idUsuarioDoctor)return void(this.nuevoDiagnosticoInvalido=!0);const o=new m.h;o.idPadecimiento=this.nuevoDiagnostico.idPadecimiento,o.idUsuarioDoctor=this.nuevoDiagnostico.idUsuarioDoctor,o.fechaDiagnostico=this.nuevoDiagnostico.fechaDiagnostico,o.esAntecedente=!1,this.infoUsuario.padecimientos=[...this.infoUsuario.padecimientos,o],this.nuevoDiagnosticoInvalido=!1,this.nuevoDiagnostico=new m.h,this.consultarDiagnosticos()}reenviarConfirmacionCorreo(o){this.emailsubmiting=!0,this.confirmacionCorreoService.enviarCorreoConfirmacion({correo:o,token:""}).subscribe({next:()=>{}})}presentAlertSalir(){var o=this;return(0,d.Z)(function*(){const t=yield o.alertController.create({header:"\xbfEst\xe1 seguro que desea salir?",message:"Si sale, perder\xe1 los cambios que no haya guardado",buttons:[{text:"Cancelar",role:"cancel",cssClass:"secondary",handler:()=>!1},{text:"Salir",handler:()=>!0}],cssClass:"custom-alert"});return t.present(),"cancel"!==(yield t.onDidDismiss()).role})()}presentAlert(){var o=this;return(0,d.Z)(function*(){yield(yield o.alertController.create({header:"Informaci\xf3n actualizada",message:"La informaci\xf3n se actualiz\xf3 correctamente",buttons:["OK"],cssClass:"custom-alert"})).present()})()}presentAlertError(){var o=this;return(0,d.Z)(function*(){yield(yield o.alertController.create({header:"Campos requeridos",message:"Llene todos los campos requeridos",buttons:["OK"],cssClass:"custom-alert"})).present()})()}onExit(){return!this.formulario.dirty||this.presentAlertSalir()}}return(r=s).\u0275fac=function(o){return new(o||r)(n.Y36(A.i),n.Y36(U.$),n.Y36(G.s),n.Y36(I.G),n.Y36(T.b),n.Y36(E.T),n.Y36(w.R),n.Y36(D.B),n.Y36(k.q),n.Y36(l.Br),n.Y36(F),n.Y36(q),n.Y36(l.IN))},r.\u0275cmp=n.Xpm({type:r,selectors:[["app-informacion-general"]],viewQuery:function(o,t){if(1&o&&n.Gf(S,5),2&o){let e;n.iGM(e=n.CRH())&&(t.formulario=e.first)}},standalone:!0,features:[n.jDz],decls:15,vars:3,consts:[[1,"ion-no-border"],[1,"contenedor-header"],["slot","start"],["routerLink","inicio-perfil"],["slot","start","name","chevron-back"],[1,"contenedor-principal"],[1,"titulo"],[4,"ngIf"],["novalidate","novalidate",3,"ngSubmit"],["formulario","ngForm"],["class","correoConfirmado",4,"ngIf"],[1,"seccion"],["size","12"],[1,"input"],["id","nombre","name","nombre","fill","outline","type","text","required","true","maxlength","50","autocomplete","off","placeholder","Nombres",3,"ngModel","ngModelChange"],["nombres","ngModel"],["class","error",4,"ngIf"],["size","6"],["id","apePaterno","name","apellidoPaterno","fill","outline","type","text","required","true","maxlength","50","autocomplete","off","placeholder","Apellido Paterno",3,"ngModel","ngModelChange"],["apellidoPaterno","ngModel"],["id","apeMaterno","name","apellidoMaterno","fill","outline","type","text","required","true","maxlength","50","autocomplete","off","placeholder","Apellido Materno",3,"ngModel","ngModelChange"],["apellidoMaterno","ngModel"],["lines","none",1,"custom-datetime"],["datetime","fechaNacimiento"],[3,"keepContentsMounted"],["id","edad","name","edad","fill","outline","shape","round","type","text","readonly","true","disabled","true","required","true",3,"ngModel","ngModelChange"],["id","genero1","name","genero1","fill","outline","shape","round","type","text","readonly","true",3,"ngModel"],["slot","end","name","chevron-down",3,"click",4,"ngIf"],["slot","end","name","chevron-up",4,"ngIf"],["id","peso","name","peso","fill","outline","shape","round","type","text","required","true","autocomplete","off","placeholder","Peso kg",3,"ngModel","ngModelChange"],["peso","ngModel"],["id","unidadMedida","slot","end"],["id","estatura","name","estatura","fill","outline","shape","round","type","text","required","true","autocomplete","off","placeholder","Estatura cm",3,"ngModel","ngModelChange"],["estatura","ngModel"],["id","telefono","name","telefono","fill","outline","shape","round","type","text","required","true","maxlength","10","autocomplete","off","placeholder","Tel\xe9fono m\xf3vil",3,"ngModel","ngModelChange"],["telefono","ngModel"],["id","correo","name","correo","fill","outline","shape","round","type","text","required","true","maxlength","50","autocomplete","off","placeholder","Correo electr\xf3nico",3,"ngModel","ngModelChange"],["correo","ngModel"],[1,"mt-4"],["type","submit",1,"btn-enviar",3,"disabled"],[1,"correoConfirmado"],[3,"disabled","click"],[1,"error"],["slot","start","name","informacion"],["color","morado-custom","name","fechaNacimiento","id","fechaNacimiento","presentation","date","showDefaultButtons","true","locale","es-ES","required","true",3,"ngModel","ionChange","ngModelChange"],["fechaNacimiento","ngModel"],["slot","end","name","chevron-down",3,"click"],["slot","end","name","chevron-up"]],template:function(o,t){1&o&&(n.TgZ(0,"ion-header",0)(1,"div",1)(2,"ion-toolbar")(3,"ion-buttons",2)(4,"ion-button",3),n._UZ(5,"ion-icon",4),n._uU(6," Atr\xe1s "),n.qZA()()()()(),n.TgZ(7,"ion-content")(8,"div",5)(9,"div",6)(10,"ion-text")(11,"h3"),n._uU(12,"General"),n.qZA()()(),n.YNc(13,V,91,21,"div",7),n.ALo(14,"async"),n.qZA()()),2&o&&(n.xp6(13),n.Q6J("ngIf",n.lcZ(14,1,t.informacionUsuario$)))},dependencies:[_.ez,_.O5,_.Ov,g.u5,g._Y,g.JJ,g.JL,g.Q7,g.nD,g.On,g.F,O.Bz,O.rH,g.UX,l.Pc,l.YG,l.Sm,l.wI,l.W2,l.x4,l.Mj,l.jY,l.Gu,l.gu,l.pK,l.Ie,l.Q$,l.Nd,l.yW,l.sr,l.ki,l.QI,l.j9,l.YI],styles:['[_ngcontent-%COMP%]:root{--color-primario: #9BD8AC;--color-secundario: #9ED1FE;--color-terceario: #671E75;--color-fuente-secundario: #7F7F7F;--background-primario: #0278E4;--background-secundario: #11B2A9;--background-terceario: #671E75;--ion-color-primary: #9BD8AC;--ion-color-primary-rgb: 155, 216, 172;--ion-color-primary-contrast: #000000;--ion-color-primary-contrast-rgb: 0, 0, 0;--ion-color-primary-shade: #6CC686;--ion-color-primary-tint: #B6E2C2;--ion-color-secondary: #9ED1FE;--ion-color-secondary-rgb: 158, 209, 254;--ion-color-secondary-contrast: #000000;--ion-color-secondary-contrast-rgb: 0, 0, 0;--ion-color-secondary-shade: #86C6FE;--ion-color-secondary-tint: #C2E2FE;--ion-color-tertiary: #671E75;--ion-color-tertiary-rgb: 103, 30, 117;--ion-color-tertiary-contrast: #ffffff;--ion-color-tertiary-contrast-rgb: 255, 255, 255;--ion-color-tertiary-shade: #471551;--ion-color-tertiary-tint: #802692;--ion-color-success: #2dd36f;--ion-color-success-rgb: 45, 211, 111;--ion-color-success-contrast: #ffffff;--ion-color-success-contrast-rgb: 255, 255, 255;--ion-color-success-shade: #28ba62;--ion-color-success-tint: #42d77d;--ion-color-warning: #ffc409;--ion-color-warning-rgb: 255, 196, 9;--ion-color-warning-contrast: #000000;--ion-color-warning-contrast-rgb: 0, 0, 0;--ion-color-warning-shade: #e0ac08;--ion-color-warning-tint: #ffca22;--ion-color-danger: #eb445a;--ion-color-danger-rgb: 235, 68, 90;--ion-color-danger-contrast: #ffffff;--ion-color-danger-contrast-rgb: 255, 255, 255;--ion-color-danger-shade: #cf3c4f;--ion-color-danger-tint: #ed576b;--ion-color-dark: #222428;--ion-color-dark-rgb: 34, 36, 40;--ion-color-dark-contrast: #ffffff;--ion-color-dark-contrast-rgb: 255, 255, 255;--ion-color-dark-shade: #1e2023;--ion-color-dark-tint: #383a3e;--ion-color-medium: #92949c;--ion-color-medium-rgb: 146, 148, 156;--ion-color-medium-contrast: #ffffff;--ion-color-medium-contrast-rgb: 255, 255, 255;--ion-color-medium-shade: #808289;--ion-color-medium-tint: #9d9fa6;--ion-color-light: #f4f5f8;--ion-color-light-rgb: 244, 245, 248;--ion-color-light-contrast: #000000;--ion-color-light-contrast-rgb: 0, 0, 0;--ion-color-light-shade: #d7d8da;--ion-color-light-tint: #f5f6f9;--accent-color-green: #9bd8ac;--primary-50: #F3F4FA;--primary-100: #EAEBF5;--primary-200: #D8D9ED;--primary-300: #C0C2E1;--primary-400: #A8A6D3;--primary-500: #958FC5;--primary-600: #8377B4;--primary-700: #695e93;--primary-800: #5C547F;--primary-900: #4D4867;--primary-950: #2D2A3C;--neutral-50: #FFFFFF;--neutral-100: #F4F4F4;--neutral-200: #DCDCDC;--neutral-300: #BDBDBD;--neutral-400: #989898;--neutral-500: #7C7C7C;--neutral-600: #656565;--neutral-700: #525252;--neutral-800: #464646;--neutral-900: #292929;--neutral-950: #000000;--error-100: #F9DEDC;--error-200: #B3261E;--error-300: #410E0B;--font-display: "Gayathri", sans-serif;--font-body: "Inter", sans-serif}[_ngcontent-%COMP%]:root   .ion-color-morado-custom[_ngcontent-%COMP%]{--ion-color-base: var(--primary-700);--ion-color-base-rgb: var(--primary-700);--ion-color-contrast: var(--neutral-50);--ion-color-contrast-rgb: var(--neutral-50);--ion-color-shade: var(--primary-950);--ion-color-tint: var(--primary-300)}ion-content[_ngcontent-%COMP%]{--background: var(--neutral-100)}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-loading[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%]{border-radius:16px!important;min-width:327px!important}@media (max-width: 350px){ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-loading[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%]{min-width:80vw!important;max-width:80vw!important;font-size:7vw!important}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-loading[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%]{font-size:1em!important;line-height:1.2em!important}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-loading[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%]{font-size:.6em!important;line-height:1em!important}}ion-loading.custom-loading[_ngcontent-%COMP%]   .loading-wrapper[_ngcontent-%COMP%]{background:#ffffff url(loading.663f11007405c21b.gif) no-repeat center;--height: 30%;--width: 100%;border-radius:.8rem;--backdrop-opacity: .8;backdrop-opacity:.8}.input[_ngcontent-%COMP%]{display:flex;flex-direction:column;gap:12px;--border-radius: 8px 8px !important}.input[_ngcontent-%COMP%]   ion-label[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:16px;color:var(--neutral-700)}.input[_ngcontent-%COMP%]   ion-input[_ngcontent-%COMP%]{color:var(--neutral-800);--padding-start: 16px;--padding-end: 16px;--padding-bottom: 12px;--padding-top: 12px;--border-radius: 8px 8px !important;max-height:50px}.input[_ngcontent-%COMP%]   ion-input[_ngcontent-%COMP%]   #unidadMedida[_ngcontent-%COMP%]{color:var(--neutral-300)}.input[_ngcontent-%COMP%]   .error[_ngcontent-%COMP%]{color:var(--error-200);font-size:.8rem;display:flex;align-items:center;font-family:var(--font-body);gap:6px}.input[_ngcontent-%COMP%]   .error[_ngcontent-%COMP%]   ion-icon[_ngcontent-%COMP%]{stroke:var(--error-200);color:var(--error-200)}.input[_ngcontent-%COMP%]   .custom-datetime[_ngcontent-%COMP%]{border:1px solid var(--neutral-300);border-radius:8px;padding:0;--background: var(--primary-50)}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]{text-align:center!important;padding-top:92px!important;min-height:209px}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2[_ngcontent-%COMP%]{color:var(--neutral-900)}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%]{font-family:var(--font-display);font-size:24px;font-weight:700;line-height:29px}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-sub-title[_ngcontent-%COMP%]{padding-top:8px;font-family:var(--font-body);font-size:16px;font-weight:400;line-height:19px;white-space:pre-line}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]{height:48px!important;width:48px!important;border-radius:50%!important;padding:0!important;position:absolute;top:28px;left:50%;transform:translate(-50%);background-position:center;background-repeat:no-repeat}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]{display:flex;justify-content:center;padding-left:24px;padding-right:24px;padding-bottom:24px}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]{margin:0!important;width:100%;height:44px;background:var(--primary-700);border-radius:8px;color:var(--neutral-50);text-align:center!important;font-family:var(--font-body);font-size:16px;text-transform:none}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]   span[_ngcontent-%COMP%], .custom-alert-error[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]   span[_ngcontent-%COMP%], .custom-alert-delete[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]   span[_ngcontent-%COMP%], .custom-alert-choice[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]   span[_ngcontent-%COMP%]{justify-content:center!important}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]{background-image:url(check.b9ca3bbca5f05228.svg);background-color:var(--primary-700)}ion-alert.custom-alert-success[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]{background:var(--primary-700)!important}ion-alert.custom-alert-error[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]{background-image:url(info.e4a438de3d6813c2.svg);background-color:var(--error-200)}ion-alert.custom-alert-error[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]{background:var(--error-200)}ion-alert.custom-alert-loading[_ngcontent-%COMP%]   .alert-wrapper[_ngcontent-%COMP%]{opacity:.8!important}ion-alert.custom-alert-loading[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]{height:0px;padding:0}ion-alert.custom-alert-loading[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]{height:0px;padding:0}ion-alert.custom-alert-loading[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]{min-height:264px!important;display:flex;justify-content:center;align-items:center}ion-alert.custom-alert-loading[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]   ion-spinner[_ngcontent-%COMP%]{width:64px;height:64px;color:var(--primary-600)}ion-alert.custom-alert-delete[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]{background-image:url(trash-2.eca3403c623b06a9.svg);background-color:var(--error-200)}ion-alert.custom-alert-delete[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]{justify-content:space-between}ion-alert.custom-alert-delete[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]{max-width:130px;background:var(--error-200)}ion-alert.custom-alert-delete[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button-role-cancel[_ngcontent-%COMP%]{color:var(--error-200);border:1px solid var(--error-200);background:transparent}ion-alert.custom-alert-choice[_ngcontent-%COMP%]   .alert-head[_ngcontent-%COMP%]   h2.alert-title[_ngcontent-%COMP%]{color:var(--primary-700)!important}ion-alert.custom-alert-choice[_ngcontent-%COMP%]   .alert-message[_ngcontent-%COMP%]{background-image:url(pill.dc46005d498c253f.svg);background-color:var(--primary-700)}ion-alert.custom-alert-choice[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]{justify-content:space-between}ion-alert.custom-alert-choice[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button[_ngcontent-%COMP%]{max-width:130px;color:var(--neutral-50);background:var(--primary-700);font-size:12px!important}ion-alert.custom-alert-choice[_ngcontent-%COMP%]   .alert-button-group[_ngcontent-%COMP%]   button.alert-button-role-cancel[_ngcontent-%COMP%]{color:var(--primary-700);border:1px solid var(--primary-700);background:transparent}ion-popover.custom-select-popover[_ngcontent-%COMP%]   .item-radio-checked[_ngcontent-%COMP%]{--background: var(--primary-200);--background-focused: var(--primary-50);--background-hover: var(--primary-50)}ion-action-sheet.custom-select-action-sheet[_ngcontent-%COMP%]   .action-sheet-group[_ngcontent-%COMP%]{border-radius:16px 16px 0 0;padding-top:32px!important}ion-action-sheet.custom-select-action-sheet[_ngcontent-%COMP%]   .action-sheet-group-cancel[_ngcontent-%COMP%]{border-radius:0;padding-top:0!important;padding-bottom:32px!important}ion-action-sheet.custom-select-action-sheet[_ngcontent-%COMP%]   .action-sheet-cancel[_ngcontent-%COMP%]{color:var(--primary-700)!important}ion-popover.custom-select-popover[_ngcontent-%COMP%]::part(content){border-radius:8px}ion-modal.custom-sheet-modal[_ngcontent-%COMP%]{--height: auto;--border-radius: 16px 16px 0 0}@media (prefers-color-scheme: dark){body[_ngcontent-%COMP%]{--ion-color-primary: #428cff;--ion-color-primary-rgb: 66,140,255;--ion-color-primary-contrast: #ffffff;--ion-color-primary-contrast-rgb: 255,255,255;--ion-color-primary-shade: #3a7be0;--ion-color-primary-tint: #5598ff;--ion-color-secondary: #50c8ff;--ion-color-secondary-rgb: 80,200,255;--ion-color-secondary-contrast: #ffffff;--ion-color-secondary-contrast-rgb: 255,255,255;--ion-color-secondary-shade: #46b0e0;--ion-color-secondary-tint: #62ceff;--ion-color-tertiary: #6a64ff;--ion-color-tertiary-rgb: 106,100,255;--ion-color-tertiary-contrast: #ffffff;--ion-color-tertiary-contrast-rgb: 255,255,255;--ion-color-tertiary-shade: #5d58e0;--ion-color-tertiary-tint: #7974ff;--ion-color-success: var(--primary-600);--ion-color-success-rgb: 47,223,117;--ion-color-success-contrast: #000000;--ion-color-success-contrast-rgb: 0,0,0;--ion-color-success-shade: #29c467;--ion-color-success-tint: #44e283;--ion-color-warning: #ffd534;--ion-color-warning-rgb: 255,213,52;--ion-color-warning-contrast: #000000;--ion-color-warning-contrast-rgb: 0,0,0;--ion-color-warning-shade: #e0bb2e;--ion-color-warning-tint: #ffd948;--ion-color-danger: var(--error-200);--ion-color-danger-rgb: var(--error-200);--ion-color-danger-contrast: var(--error-200);--ion-color-danger-contrast-rgb: var(--error-200);--ion-color-danger-shade: var(--error-200);--ion-color-danger-tint: var(--error-200);--ion-color-dark: #f4f5f8;--ion-color-dark-rgb: 244,245,248;--ion-color-dark-contrast: #000000;--ion-color-dark-contrast-rgb: 0,0,0;--ion-color-dark-shade: #d7d8da;--ion-color-dark-tint: #f5f6f9;--ion-color-medium: #989aa2;--ion-color-medium-rgb: 152,154,162;--ion-color-medium-contrast: #000000;--ion-color-medium-contrast-rgb: 0,0,0;--ion-color-medium-shade: #86888f;--ion-color-medium-tint: #a2a4ab;--ion-color-light: #222428;--ion-color-light-rgb: 34,36,40;--ion-color-light-contrast: #ffffff;--ion-color-light-contrast-rgb: 255,255,255;--ion-color-light-shade: #1e2023;--ion-color-light-tint: #383a3e}}ion-header[_ngcontent-%COMP%]{border-radius:0;background:var(--primary-50)}ion-content[_ngcontent-%COMP%]{--background: var(--primary-50)}.contenedor-header[_ngcontent-%COMP%]{background:var(--primary-50);height:20vh;width:100%}.contenedor-header[_ngcontent-%COMP%]   ion-toolbar[_ngcontent-%COMP%]{height:100%;width:100%;--background: url(header_light.508685377072668e.svg);max-height:153px;max-width:390px;display:flex;flex-direction:column;justify-content:flex-end;padding-bottom:20px}.contenedor-header[_ngcontent-%COMP%]   ion-toolbar[_ngcontent-%COMP%]   ion-button[_ngcontent-%COMP%]{font-family:var(--font-body);font-weight:400;font-size:18px;color:var(--primary-700)}.contenedor-principal[_ngcontent-%COMP%]{margin:3vh 32px 40vh;height:80vh}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]{height:4vh}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]   ion-text[_ngcontent-%COMP%]{text-align:start}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]   ion-text[_ngcontent-%COMP%]   h3[_ngcontent-%COMP%]{margin:0;color:var(--primary-700);font-family:var(--font-display);font-size:24px;font-weight:700;line-height:32px}.texto-seccion[_ngcontent-%COMP%]{font-weight:700;color:var(--primary-600)}ion-item[_ngcontent-%COMP%]{--background: var(--neutral-100)}.correoConfirmado[_ngcontent-%COMP%]   p[_ngcontent-%COMP%]{text-align:center;color:red}.correoConfirmado[_ngcontent-%COMP%]   ion-button[_ngcontent-%COMP%]{--background: var(--primary-700);--color: var(--neutral-50)}.padecimiento-nuevo[_ngcontent-%COMP%]{padding-top:2em}.seccion[_ngcontent-%COMP%]{padding-bottom:1em}.btn-enviar[_ngcontent-%COMP%]{--background: var(--primary-700);--color: var(--neutral-50);--border-radius: 10px}.btn-agregar-eliminar[_ngcontent-%COMP%]{position:relative;font-size:1em;--ripple-color: var(--primary-300)}.btn-agregar-eliminar[_ngcontent-%COMP%]   #circulo[_ngcontent-%COMP%]{color:var(--neutral-400)}.btn-agregar-eliminar[_ngcontent-%COMP%]   #icono[_ngcontent-%COMP%]{position:absolute;color:var(--neutral-50)}']}),s})()}}]);