"use strict";(self.webpackChunkapp=self.webpackChunkapp||[]).push([[9540],{9540:(K,_,u)=>{u.r(_),u.d(_,{InfoDomicilioComponent:()=>Y});var d=u(5861),f=u(6895),g=u(433),c=u(9149),M=u(2583),x=u(1135),P=u(8505),C=u(429),v=u(4384),o=u(1571),Z=u(6558),p=u(529);let U=(()=>{var e;class a{constructor(i){this.http=i,this.dataUrl="pais/"}consultarTodosParaSelector(){return this.http.get(this.dataUrl+"consultarTodosParaSelector")}}return(e=a).\u0275fac=function(i){return new(i||e)(o.LFG(p.eN))},e.\u0275prov=o.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),a})(),b=(()=>{var e;class a{constructor(i){this.http=i,this.dataUrl="estado/"}consultarPorPaisParaSelector(i){return this.http.get(this.dataUrl+`selector/pais/${i}`)}}return(e=a).\u0275fac=function(i){return new(i||e)(o.LFG(p.eN))},e.\u0275prov=o.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),a})(),I=(()=>{var e;class a{constructor(i){this.http=i,this.dataUrl="municipio/"}consultarPorEstadoParaSelector(i){return this.http.get(this.dataUrl+`selector/estado/${i}`)}}return(e=a).\u0275fac=function(i){return new(i||e)(o.LFG(p.eN))},e.\u0275prov=o.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),a})(),T=(()=>{var e;class a{constructor(i){this.http=i,this.dataUrl="localidad/"}consultarPorEstado(i){return this.http.get(this.dataUrl+`consultarPorEstado/${i}`)}}return(e=a).\u0275fac=function(i){return new(i||e)(o.LFG(p.eN))},e.\u0275prov=o.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),a})(),y=(()=>{var e;class a{constructor(i){this.http=i,this.dataUrl="colonia/"}consultarPorCodigoParaSelector(i){return this.http.get(this.dataUrl+`consultarPorCodigoParaSelector/${i}`)}}return(e=a).\u0275fac=function(i){return new(i||e)(o.LFG(p.eN))},e.\u0275prov=o.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),a})(),S=(()=>{var e;class a{constructor(i){this.http=i,this.dataUrl="codigoPostal/"}consultarPorCodigoPostal(i){return this.http.get(this.dataUrl+`consultarPorCodigoPostal/${i}`)}}return(e=a).\u0275fac=function(i){return new(i||e)(o.LFG(p.eN))},e.\u0275prov=o.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),a})();const D=["formulario"];function A(e,a){if(1&e&&(o.TgZ(0,"ion-select-option",47),o._uU(1),o.qZA()),2&e){const r=a.$implicit;o.Q6J("value",r.idPais),o.xp6(1),o.hij(" ",r.nombre," ")}}function E(e,a){1&e&&(o.TgZ(0,"div",48),o._UZ(1,"ion-icon",49),o._uU(2," El Pais es requerido "),o.qZA())}function L(e,a){if(1&e&&(o.TgZ(0,"ion-select-option",47),o._uU(1),o.qZA()),2&e){const r=a.$implicit;o.Q6J("value",r.idEstado),o.xp6(1),o.hij(" ",r.nombre," ")}}function J(e,a){1&e&&(o.TgZ(0,"div",48),o._UZ(1,"ion-icon",49),o._uU(2," El estado es requerido "),o.qZA())}function q(e,a){if(1&e&&(o.TgZ(0,"ion-select-option",47),o._uU(1),o.qZA()),2&e){const r=a.$implicit;o.Q6J("value",r.idMunicipio),o.xp6(1),o.hij(" ",r.nombre," ")}}function O(e,a){if(1&e&&(o.TgZ(0,"ion-select-option",47),o._uU(1),o.qZA()),2&e){const r=a.$implicit;o.Q6J("value",r.idLocalidad),o.xp6(1),o.hij(" ",r.nombre," ")}}function N(e,a){if(1&e&&(o.TgZ(0,"ion-select-option",47),o._uU(1),o.qZA()),2&e){const r=a.$implicit;o.Q6J("value",r.idColonia),o.xp6(1),o.hij(" ",r.nombre," ")}}function Q(e,a){1&e&&(o.TgZ(0,"div",48),o._UZ(1,"ion-icon",49),o._uU(2," La colonia es requerida "),o.qZA())}const m=function(){return{cssClass:"custom-select-action-sheet"}},h=function(e){return{"input-disabled":e}};function F(e,a){if(1&e){const r=o.EpF();o.TgZ(0,"div",9)(1,"form",10,11),o.NdJ("ngSubmit",function(){o.CHM(r);const n=o.MAs(2),t=o.oxw();return o.KtG(t.enviarFormulario(n))}),o.TgZ(3,"div",12)(4,"ion-grid",13)(5,"ion-row")(6,"ion-col",14)(7,"div",15)(8,"ion-label"),o._uU(9,"Pa\xeds"),o.qZA(),o.TgZ(10,"ion-item",16)(11,"ion-select",17,18),o.NdJ("ionChange",function(){o.CHM(r);const n=o.oxw();return o.KtG(n.onChangePais())})("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.idPais=n)}),o.YNc(13,A,2,2,"ion-select-option",19),o.qZA()(),o.YNc(14,E,3,0,"div",20),o.qZA()(),o.TgZ(15,"ion-col",21)(16,"div",15)(17,"ion-label"),o._uU(18,"C\xf3digo Postal"),o.qZA(),o.TgZ(19,"ion-item",22)(20,"ion-input",23,24),o.NdJ("ionChange",function(){o.CHM(r);const n=o.oxw();return o.KtG(n.onChangeCodigoPostal())})("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.codigoPostal=n)}),o.qZA()()()()(),o.TgZ(22,"ion-row")(23,"ion-col",25)(24,"div",15)(25,"ion-label"),o._uU(26,"Estado"),o.qZA(),o.TgZ(27,"ion-item",26)(28,"ion-select",27,28),o.NdJ("ionChange",function(){o.CHM(r);const n=o.oxw();return o.KtG(n.onChangeEstado())})("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.idEstado=n)}),o.YNc(30,L,2,2,"ion-select-option",19),o.qZA()(),o.YNc(31,J,3,0,"div",20),o.qZA()()(),o.TgZ(32,"ion-row")(33,"ion-col",14)(34,"div",15)(35,"ion-label"),o._uU(36,"Municipio"),o.qZA(),o.TgZ(37,"ion-item",26)(38,"ion-select",29,30),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.idMunicipio=n)}),o.YNc(40,q,2,2,"ion-select-option",19),o.qZA()()()(),o.TgZ(41,"ion-col",21)(42,"div",15)(43,"ion-label"),o._uU(44,"Localidad"),o.qZA(),o.TgZ(45,"ion-item",26)(46,"ion-select",31,32),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.idLocalidad=n)}),o.YNc(48,O,2,2,"ion-select-option",19),o.qZA()()()()(),o.TgZ(49,"ion-row")(50,"ion-col",25)(51,"div",15)(52,"ion-label"),o._uU(53,"Colonia"),o.qZA(),o.TgZ(54,"ion-item",26)(55,"ion-select",33,34),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.idColonia=n)}),o.YNc(57,N,2,2,"ion-select-option",19),o.qZA()(),o.YNc(58,Q,3,0,"div",20),o.qZA()()(),o.TgZ(59,"ion-row")(60,"ion-col",25)(61,"div",15)(62,"ion-label"),o._uU(63,"Calle"),o.qZA(),o.TgZ(64,"ion-item",35)(65,"ion-input",36,37),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.calle=n)}),o.qZA()()()()(),o.TgZ(67,"ion-row")(68,"ion-col",14)(69,"div",15)(70,"ion-label"),o._uU(71,"N\xfam. exterior"),o.qZA(),o.TgZ(72,"ion-item",35)(73,"ion-input",38,39),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.numeroExterior=n)}),o.qZA()()()(),o.TgZ(75,"ion-col",40)(76,"div",15)(77,"ion-label"),o._uU(78,"N\xfam. Interior"),o.qZA(),o.TgZ(79,"ion-item",35)(80,"ion-input",41,42),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.numeroInterior=n)}),o.qZA()()()()(),o.TgZ(82,"ion-row")(83,"ion-col",25)(84,"div",15)(85,"ion-label"),o._uU(86,"Entre calles"),o.qZA(),o.TgZ(87,"ion-item",35)(88,"ion-input",43,44),o.NdJ("ngModelChange",function(n){const s=o.CHM(r).ngIf;return o.KtG(s.entreCalles=n)}),o.qZA()()()()()()(),o.TgZ(90,"div",45)(91,"ion-button",46),o._uU(92," Guardar "),o.qZA()()()()}if(2&e){const r=a.ngIf,i=o.MAs(2),n=o.MAs(12),t=o.MAs(29),s=o.MAs(56),l=o.oxw();o.xp6(11),o.Q6J("interfaceOptions",o.DdM(36,m))("ngModel",r.idPais),o.xp6(2),o.Q6J("ngForOf",l.paisList),o.xp6(1),o.Q6J("ngIf",(null==n.errors?null:n.errors.required)&&(n.dirty||n.touched)),o.xp6(5),o.Q6J("ngClass",o.VKq(37,h,l.codigoPostalDisabled())),o.xp6(1),o.Q6J("maxlength",l.esPaisExtranjero?null:5)("minlength",l.esPaisExtranjero?null:5)("disabled",l.codigoPostalDisabled())("ngModel",r.codigoPostal),o.xp6(7),o.Q6J("ngClass",o.VKq(39,h,l.estadoDisabled())),o.xp6(1),o.Q6J("interfaceOptions",o.DdM(41,m))("disabled",l.estadoDisabled())("ngModel",r.idEstado),o.xp6(2),o.Q6J("ngForOf",l.estadoList),o.xp6(1),o.Q6J("ngIf",(null==t.errors?null:t.errors.required)&&(t.dirty||t.touched)),o.xp6(6),o.Q6J("ngClass",o.VKq(42,h,l.municipioDisabled())),o.xp6(1),o.Q6J("interfaceOptions",o.DdM(44,m))("disabled",l.municipioDisabled())("ngModel",r.idMunicipio),o.xp6(2),o.Q6J("ngForOf",l.municipioList),o.xp6(5),o.Q6J("ngClass",o.VKq(45,h,l.localidadDisabled())),o.xp6(1),o.Q6J("interfaceOptions",o.DdM(47,m))("disabled",l.localidadDisabled())("ngModel",r.idLocalidad),o.xp6(2),o.Q6J("ngForOf",l.localidadList),o.xp6(6),o.Q6J("ngClass",o.VKq(48,h,l.coloniaDisabled())),o.xp6(1),o.Q6J("interfaceOptions",o.DdM(50,m))("disabled",l.coloniaDisabled())("ngModel",r.idColonia),o.xp6(2),o.Q6J("ngForOf",l.coloniaList),o.xp6(1),o.Q6J("ngIf",(null==s.errors?null:s.errors.required)&&(s.dirty||s.touched)),o.xp6(7),o.Q6J("ngModel",r.calle),o.xp6(8),o.Q6J("ngModel",r.numeroExterior),o.xp6(7),o.Q6J("ngModel",r.numeroInterior),o.xp6(8),o.Q6J("ngModel",r.entreCalles),o.xp6(3),o.Q6J("disabled",l.submiting||i.invalid||!i.dirty)}}let Y=(()=>{var e;class a{constructor(i,n,t,s,l,G,w,z){this.usuarioService=i,this.paisService=n,this.estadoService=t,this.municipioService=s,this.localidadService=l,this.coloniaService=G,this.codigoPostalService=w,this.alertController=z,this.submiting=!1,this.esPaisExtranjero=!1,this.paisList=[],this.estadoList=[],this.municipioList=[],this.localidadList=[],this.coloniaList=[],this.cargandoSubject=new x.X(!1),this.cargando$=this.cargandoSubject.asObservable(),(0,M.a)({"chevron-down":"assets/img/svg/chevron-down.svg"})}ngOnInit(){var i=this;return(0,d.Z)(function*(){yield i.consultarPaises(),i.obtenerUsuario(),i.cargando$.subscribe(n=>{n?i.presentLoading():i.dismissLoading()})})()}onExit(){return!this.formulario.dirty||this.presentAlertSalir()}presentLoading(){var i=this;return(0,d.Z)(function*(){return i.loading=yield i.alertController.create({cssClass:"custom-alert-loading"}),yield i.loading.present()})()}dismissLoading(){var i=this;return(0,d.Z)(function*(){i.loading&&(yield i.loading.dismiss(),i.loading=null)})()}obtenerUsuario(){var i=this;return(0,d.Z)(function*(){i.informacionUsuario$=i.usuarioService.consultarInformacionDomicilio().pipe((0,P.b)(function(){var n=(0,d.Z)(function*(t){i.infoUsuario=t,yield Promise.all([i.consultarEstados(t.idPais),i.consultarMunicipios(t.idEstado),i.consultarLocalidades(t.idEstado),i.consultarColonias(t.codigoPostal)])});return function(t){return n.apply(this,arguments)}}()))})()}enviarFormulario(i){var n=this;return(0,d.Z)(function*(){if(i.invalid)return n.submiting=!1,C.CW(i),void n.presentAlertError("Campos requeridos","Debe completar todos los campos obligatorios");n.submiting=!0,n.cargandoSubject.next(!0),n.actualizarInformacionUsuario(n.infoUsuario)})()}actualizarInformacionUsuario(i){this.usuarioService.actualizarInformacionDomicilio(i).subscribe({next:()=>{},error:()=>{this.submiting=!1,this.cargandoSubject.next(!1)},complete:()=>{this.presentAlertSuccess("Informaci\xf3n actualizada","La informaci\xf3n se actualiz\xf3 correctamente"),this.submiting=!1,this.cargandoSubject.next(!1),this.formulario.form.markAsPristine()}})}consultarPaises(){var i=this;return(0,d.Z)(function*(){const n=yield i.paisService.consultarTodosParaSelector().toPromise();i.paisList=null!=n?n:[]})()}consultarEstados(i){var n=this;return(0,d.Z)(function*(){if(i){const t=i>0?yield n.estadoService.consultarPorPaisParaSelector(i).toPromise():[];n.estadoList=null!=t?t:[]}})()}consultarMunicipios(i){var n=this;return(0,d.Z)(function*(){if(i){const t=i>0?yield n.municipioService.consultarPorEstadoParaSelector(i).toPromise():[];n.municipioList=null!=t?t:[]}})()}consultarLocalidades(i){var n=this;return(0,d.Z)(function*(){if(i){const t=i>0?yield n.localidadService.consultarPorEstado(i).toPromise():[];n.localidadList=null!=t?t:[]}})()}consultarColonias(i){var n=this;return(0,d.Z)(function*(){const t=i&&5===i.length?yield n.coloniaService.consultarPorCodigoParaSelector(i).toPromise():[];n.coloniaList=null!=t?t:[]})()}onChangeCodigoPostal(){var i=this;return(0,d.Z)(function*(){var n;i.infoUsuario.idColonia=0;const t=null===(n=i.infoUsuario)||void 0===n?void 0:n.codigoPostal;5===t.length&&(yield i.asignarValoresDeCodigoPostal(t))})()}asignarValoresDeCodigoPostal(i){var n=this;return(0,d.Z)(function*(){const t=yield n.codigoPostalService.consultarPorCodigoPostal(i).toPromise().then(l=>l&&l.length>0?l[0]:null);if(!t)return;n.infoUsuario.idPais=t.idPais,n.infoUsuario.idEstado=t.idEstado,n.infoUsuario.idMunicipio=t.idMunicipio,n.infoUsuario.idLocalidad=null,yield Promise.all([n.consultarEstados(t.idPais),n.consultarMunicipios(t.idEstado),n.consultarLocalidades(t.idEstado)]),yield n.consultarColonias(i);const s=n.coloniaList.find(l=>C.q1(l.nombre,t.colonia))||n.coloniaList[0];s&&(n.infoUsuario.idColonia=s.idColonia)})()}onChangePais(){var i=this;return(0,d.Z)(function*(){i.esPaisExtranjero=i.infoUsuario.idPais!==i.idPaisMexico,i.infoUsuario.idEstado=null,i.infoUsuario.idMunicipio=null,i.infoUsuario.idLocalidad=null,i.infoUsuario.idColonia=null,i.infoUsuario.codigoPostal="",yield Promise.all([i.consultarEstados(i.infoUsuario.idPais),i.consultarMunicipios(i.infoUsuario.idEstado),i.consultarLocalidades(i.infoUsuario.idEstado)])})()}onChangeEstado(){var i=this;return(0,d.Z)(function*(){i.infoUsuario.idMunicipio=null,i.infoUsuario.idLocalidad=null,i.infoUsuario.idColonia=null,yield Promise.all([i.consultarMunicipios(i.infoUsuario.idEstado),i.consultarLocalidades(i.infoUsuario.idEstado)])})()}presentAlertError(i,n){var t=this;return(0,d.Z)(function*(){yield(yield t.alertController.create({header:i,subHeader:n,buttons:["Ok"],cssClass:"custom-alert color-error icon-info"})).present()})()}presentAlertSuccess(i,n){var t=this;return(0,d.Z)(function*(){yield(yield t.alertController.create({header:i,subHeader:n,buttons:["Ok"],cssClass:"custom-alert color-primary icon-check"})).present()})()}presentAlertSalir(){var i=this;return(0,d.Z)(function*(){const n=yield i.alertController.create({header:"\xbfEst\xe1 seguro que desea salir?",subHeader:"Perder\xe1 los cambios que no haya guardado",cssClass:"custom-alert color-error icon-info two-buttons",backdropDismiss:!1,buttons:[{text:"Cancelar",role:"cancel",handler:()=>!1},{text:"Salir",handler:()=>!0}]});return yield n.present(),"cancel"!==(yield n.onDidDismiss()).role})()}codigoPostalDisabled(){return null==this.infoUsuario.idPais}estadoDisabled(){return null==this.infoUsuario.idPais}municipioDisabled(){return!(null!=this.infoUsuario.idPais&&null!=this.infoUsuario.idEstado)}localidadDisabled(){return!(null!=this.infoUsuario.idPais&&null!=this.infoUsuario.idEstado)}coloniaDisabled(){return!(null!=this.infoUsuario.idPais&&null!=this.infoUsuario.idEstado&&null!=this.infoUsuario.idMunicipio&&null!=this.infoUsuario.codigoPostal&&""!=this.infoUsuario.codigoPostal)}}return(e=a).\u0275fac=function(i){return new(i||e)(o.Y36(Z.i),o.Y36(U),o.Y36(b),o.Y36(I),o.Y36(T),o.Y36(y),o.Y36(S),o.Y36(c.Br))},e.\u0275cmp=o.Xpm({type:e,selectors:[["app-info-domicilio"]],viewQuery:function(i,n){if(1&i&&o.Gf(D,5),2&i){let t;o.iGM(t=o.CRH())&&(n.formulario=t.first)}},standalone:!0,features:[o.jDz],decls:16,vars:3,consts:[[1,"ion-no-border"],[1,"contenedor-header-light"],["slot","start",1,"start"],["routerLink","inicio-perfil"],["slot","start","name","chevron-left"],[1,"primary"],[1,"contenedor-principal"],[1,"titulo"],["class","formulario",4,"ngIf"],[1,"formulario"],["novalidate","novalidate",3,"ngSubmit"],["formulario","ngForm"],[1,"contenido-formulario"],[1,"inputs","ion-no-padding"],["size","6",1,"padding-derecho"],[1,"input"],["lines","none",1,"select-input"],["id","pais","name","paisName","interface","action-sheet","cancelText","Cancelar","placeholder","Seleccionar","toggleIcon","chevron-down","required","",3,"interfaceOptions","ngModel","ionChange","ngModelChange"],["pais","ngModel"],[3,"value",4,"ngFor","ngForOf"],["class","input-text-error",4,"ngIf"],["size","6",1,"padding-izquierdo"],["lines","none",1,"general-input",3,"ngClass"],["id","codigoPostal","name","codigoPostal","type","text","inputmode","numeric","placeholder","C\xf3digo Postal",3,"maxlength","minlength","disabled","ngModel","ionChange","ngModelChange"],["codigoPostal","ngModel"],["size","12"],["lines","none",1,"select-input",3,"ngClass"],["id","estado","name","estadoName","interface","action-sheet","cancelText","Cancelar","placeholder","Seleccionar","toggleIcon","chevron-down","required","",3,"interfaceOptions","disabled","ngModel","ionChange","ngModelChange"],["estado","ngModel"],["id","municipio","name","municipioName","interface","action-sheet","cancelText","Cancelar","placeholder","Seleccionar","toggleIcon","chevron-down",3,"interfaceOptions","disabled","ngModel","ngModelChange"],["municipio","ngModel"],["id","localidad","name","localidadName","interface","action-sheet","cancelText","Cancelar","placeholder","Seleccionar","toggleIcon","chevron-down",3,"interfaceOptions","disabled","ngModel","ngModelChange"],["localidad","ngModel"],["id","colonia","name","coloniaName","interface","action-sheet","cancelText","Cancelar","placeholder","Seleccionar","toggleIcon","chevron-down",3,"interfaceOptions","disabled","ngModel","ngModelChange"],["colonia","ngModel"],["lines","none",1,"general-input"],["id","calle","name","calle","type","text","maxlength","50","autocomplete","off","placeholder","Calle",3,"ngModel","ngModelChange"],["calle","ngModel"],["id","numExterior","name","numExterior","type","text","maxlength","6","autocomplete","off","placeholder","N\xfamero Exterior",3,"ngModel","ngModelChange"],["numExterior","ngModel"],["size","6",1,"paddng-izquierdo"],["id","numInterior","name","numInterior","type","text","maxlength","6","autocomplete","off","placeholder","N\xfamero Interior",3,"ngModel","ngModelChange"],["numInterior","ngModel"],["id","entreCalles","name","entreCalles","type","text","maxlength","50","autocomplete","off","placeholder","Entre calles",3,"ngModel","ngModelChange"],["entreCalles","ngModel"],[1,"botones","botones-column"],["type","submit",1,"dark",3,"disabled"],[3,"value"],[1,"input-text-error"],["slot","start","name","info"]],template:function(i,n){1&i&&(o.TgZ(0,"ion-header",0)(1,"div",1)(2,"ion-toolbar")(3,"ion-buttons",2)(4,"ion-button",3),o._UZ(5,"ion-icon",4),o.TgZ(6,"ion-label"),o._uU(7,"Atr\xe1s"),o.qZA()()()()()(),o.TgZ(8,"ion-content",5)(9,"div",6)(10,"div",7)(11,"ion-text")(12,"h3"),o._uU(13,"Domicilio"),o.qZA()()(),o.YNc(14,F,93,51,"div",8),o.ALo(15,"async"),o.qZA()()),2&i&&(o.xp6(14),o.Q6J("ngIf",o.lcZ(15,1,n.informacionUsuario$)))},dependencies:[f.ez,f.mk,f.sg,f.O5,f.Ov,c.Pc,c.YG,c.Sm,c.wI,c.W2,c.jY,c.Gu,c.gu,c.pK,c.Ie,c.Q$,c.Nd,c.t9,c.n0,c.yW,c.sr,c.QI,c.j9,c.YI,g.u5,g._Y,g.JJ,g.JL,g.Q7,g.wO,g.nD,g.On,g.F,v.Bz,v.rH],styles:["ion-content[_ngcontent-%COMP%]{--keyboard-offset: 0 !important}.contenedor-principal[_ngcontent-%COMP%]{background:var(--primary-50)}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]{height:8vh;padding:16px 32px 0}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]   ion-text[_ngcontent-%COMP%]{text-align:start}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]   ion-text[_ngcontent-%COMP%]   h3[_ngcontent-%COMP%]{margin:0;color:var(--primary-700);font-family:var(--font-display);font-size:24px;font-weight:700;line-height:32px}.contenedor-principal[_ngcontent-%COMP%]   .formulario[_ngcontent-%COMP%]   .botones[_ngcontent-%COMP%]{height:15vh}"]}),a})()}}]);