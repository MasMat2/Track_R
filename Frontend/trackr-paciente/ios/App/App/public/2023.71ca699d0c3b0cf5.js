"use strict";(self.webpackChunkapp=self.webpackChunkapp||[]).push([[2023],{2023:(C,p,c)=>{c.r(p),c.d(p,{MisDoctoresPage:()=>E});var l=c(5861),t=c(12),u=c(6895),d=c(2583),f=c(1297),h=c(433),g=c(1135),o=c(1571),_=c(9134),P=c(1814);function v(e,a){1&e&&o._UZ(0,"ion-icon",15)}function M(e,a){if(1&e){const i=o.EpF();o.TgZ(0,"ion-item",13),o.NdJ("click",function(){const s=o.CHM(i).$implicit,m=o.oxw();return o.KtG(m.seleccionDoctor(s))}),o.TgZ(1,"ion-label")(2,"h3"),o._uU(3),o.qZA(),o.TgZ(4,"p"),o._uU(5),o.qZA()(),o.YNc(6,v,1,0,"ion-icon",14),o.qZA()}if(2&e){const i=a.$implicit,n=o.oxw();o.xp6(3),o.Oqu(i.nombre),o.xp6(2),o.Oqu(i.ambito),o.xp6(1),o.Q6J("ngIf",(null==n.currentDoctor?null:n.currentDoctor.idUsuarioDoctor)===i.idUsuarioDoctor)}}let y=(()=>{var e;class a{constructor(n,r,s){this.doctoresService=n,this.alertController=r,this.modarCtrl=s,this.cargandoSubject=new g.X(!1),this.cargando$=this.cargandoSubject.asObservable(),(0,d.a)({close:f.xvD,checkmark:f.d29})}ngOnInit(){this.cargando$.subscribe(n=>{n?this.presentLoading():this.dismissLoading()})}ionViewWillEnter(){this.consultarSelector()}presentLoading(){var n=this;return(0,l.Z)(function*(){return n.loading=yield n.alertController.create({cssClass:"custom-alert-loading"}),yield n.loading.present()})()}dismissLoading(){var n=this;return(0,l.Z)(function*(){n.loading&&(yield n.loading.dismiss(),n.loading=null)})()}seleccionDoctor(n){this.currentDoctor=n}agregar(){if(this.cargandoSubject.next(!0),null==this.currentDoctor)return void this.presentAlert("Seleccione un doctor");const r=this.doctoresService.agregar(this.currentDoctor).subscribe({next:()=>{this.consultarSelector()},error:()=>{this.cargandoSubject.next(!1)},complete:()=>{this.cargandoSubject.next(!1),this.presentAlertSuccess(),r.unsubscribe()}})}consultarSelector(){this.doctoresService.consultarSelector().subscribe(n=>{this.doctoresSelector=n})}presentAlert(n){var r=this;return(0,l.Z)(function*(){yield(yield r.alertController.create({header:"Mis Doctores",subHeader:n,buttons:["OK"],cssClass:"custom-alert color-error icon-info"})).present()})()}cerrarModal(){this.modarCtrl.dismiss()}presentAlertSuccess(){var n=this;return(0,l.Z)(function*(){yield(yield n.alertController.create({header:"Doctor Asignado",subHeader:"El doctor ha sido asignado exitosamente.",buttons:[{text:"De acuerdo",role:"confirm",handler:()=>{n.cerrarModal()}}],cssClass:"custom-alert color-primary icon-check"})).present()})()}}return(e=a).\u0275fac=function(n){return new(n||e)(o.Y36(_.q),o.Y36(t.Br),o.Y36(P.IN))},e.\u0275cmp=o.Xpm({type:e,selectors:[["app-doctores-formulario"]],standalone:!0,features:[o.jDz],decls:19,vars:1,consts:[[1,"ion-no-border"],[1,"contenedor-header"],["slot","start"],[3,"click"],["slot","icon-only","name","close"],["fullscreen","",1,"neutral"],[1,"contenedor-principal"],[1,"lista-doctores"],["button","","lines","none",3,"click",4,"ngFor","ngForOf"],[1,"contenedor-footer"],[1,"gradiente"],[1,"botones"],[1,"guardar",3,"click"],["button","","lines","none",3,"click"],["name","checkmark",4,"ngIf"],["name","checkmark"]],template:function(n,r){1&n&&(o.TgZ(0,"ion-header",0)(1,"div",1)(2,"ion-toolbar")(3,"ion-buttons",2)(4,"ion-button",3),o.NdJ("click",function(){return r.cerrarModal()}),o._UZ(5,"ion-icon",4),o.qZA()(),o.TgZ(6,"ion-title"),o._uU(7," Seleccionar doctor/auxiliar "),o.qZA()()()(),o.TgZ(8,"ion-content",5)(9,"div",6)(10,"div",7)(11,"ion-list"),o.YNc(12,M,7,3,"ion-item",8),o.qZA()()()(),o.TgZ(13,"ion-footer",0)(14,"div",9),o._UZ(15,"div",10),o.TgZ(16,"div",11)(17,"ion-button",12),o.NdJ("click",function(){return r.agregar()}),o._uU(18,"Guardar"),o.qZA()()()()),2&n&&(o.xp6(12),o.Q6J("ngForOf",r.doctoresSelector))},dependencies:[t.Pc,t.YG,t.Sm,t.W2,t.fr,t.Gu,t.gu,t.Ie,t.Q$,t.q_,t.wd,t.sr,h.u5,u.ez,u.sg,u.O5],styles:['[_ngcontent-%COMP%]:root{--color-primario: #9BD8AC;--color-secundario: #9ED1FE;--color-terceario: #671E75;--color-fuente-secundario: #7F7F7F;--background-primario: #0278E4;--background-secundario: #11B2A9;--background-terceario: #671E75;--ion-color-primary: #9BD8AC;--ion-color-primary-rgb: 155, 216, 172;--ion-color-primary-contrast: #000000;--ion-color-primary-contrast-rgb: 0, 0, 0;--ion-color-primary-shade: #6CC686;--ion-color-primary-tint: #B6E2C2;--ion-color-secondary: #9ED1FE;--ion-color-secondary-rgb: 158, 209, 254;--ion-color-secondary-contrast: #000000;--ion-color-secondary-contrast-rgb: 0, 0, 0;--ion-color-secondary-shade: #86C6FE;--ion-color-secondary-tint: #C2E2FE;--ion-color-tertiary: #671E75;--ion-color-tertiary-rgb: 103, 30, 117;--ion-color-tertiary-contrast: #ffffff;--ion-color-tertiary-contrast-rgb: 255, 255, 255;--ion-color-tertiary-shade: #471551;--ion-color-tertiary-tint: #802692;--ion-color-success: #2dd36f;--ion-color-success-rgb: 45, 211, 111;--ion-color-success-contrast: #ffffff;--ion-color-success-contrast-rgb: 255, 255, 255;--ion-color-success-shade: #28ba62;--ion-color-success-tint: #42d77d;--ion-color-warning: #ffc409;--ion-color-warning-rgb: 255, 196, 9;--ion-color-warning-contrast: #000000;--ion-color-warning-contrast-rgb: 0, 0, 0;--ion-color-warning-shade: #e0ac08;--ion-color-warning-tint: #ffca22;--ion-color-danger: #eb445a;--ion-color-danger-rgb: 235, 68, 90;--ion-color-danger-contrast: #ffffff;--ion-color-danger-contrast-rgb: 255, 255, 255;--ion-color-danger-shade: #cf3c4f;--ion-color-danger-tint: #ed576b;--ion-color-dark: #222428;--ion-color-dark-rgb: 34, 36, 40;--ion-color-dark-contrast: #ffffff;--ion-color-dark-contrast-rgb: 255, 255, 255;--ion-color-dark-shade: #1e2023;--ion-color-dark-tint: #383a3e;--ion-color-medium: #92949c;--ion-color-medium-rgb: 146, 148, 156;--ion-color-medium-contrast: #ffffff;--ion-color-medium-contrast-rgb: 255, 255, 255;--ion-color-medium-shade: #808289;--ion-color-medium-tint: #9d9fa6;--ion-color-light: #f4f5f8;--ion-color-light-rgb: 244, 245, 248;--ion-color-light-contrast: #000000;--ion-color-light-contrast-rgb: 0, 0, 0;--ion-color-light-shade: #d7d8da;--ion-color-light-tint: #f5f6f9;--accent-color-green: #9bd8ac;--primary-50: #F3F4FA;--primary-100: #EAEBF5;--primary-200: #D8D9ED;--primary-300: #C0C2E1;--primary-400: #A8A6D3;--primary-500: #958FC5;--primary-600: #8377B4;--primary-700: #695e93;--primary-800: #5C547F;--primary-900: #4D4867;--primary-950: #2D2A3C;--neutral-50: #FFFFFF;--neutral-100: #F4F4F4;--neutral-200: #DCDCDC;--neutral-300: #BDBDBD;--neutral-400: #989898;--neutral-500: #7C7C7C;--neutral-600: #656565;--neutral-700: #525252;--neutral-800: #464646;--neutral-900: #292929;--neutral-950: #000000;--error-100: #F9DEDC;--error-200: #B3261E;--error-300: #410E0B;--font-display: "Gayathri", sans-serif;--font-body: "Inter", sans-serif}[_ngcontent-%COMP%]:root   .ion-color-morado-custom[_ngcontent-%COMP%]{--ion-color-base: var(--primary-700);--ion-color-base-rgb: var(--primary-700);--ion-color-contrast: var(--neutral-50);--ion-color-contrast-rgb: var(--neutral-50);--ion-color-shade: var(--primary-950);--ion-color-tint: var(--primary-300)}@media (prefers-color-scheme: dark){body[_ngcontent-%COMP%]{--ion-color-primary: #428cff;--ion-color-primary-rgb: 66,140,255;--ion-color-primary-contrast: #ffffff;--ion-color-primary-contrast-rgb: 255,255,255;--ion-color-primary-shade: #3a7be0;--ion-color-primary-tint: #5598ff;--ion-color-secondary: #50c8ff;--ion-color-secondary-rgb: 80,200,255;--ion-color-secondary-contrast: #ffffff;--ion-color-secondary-contrast-rgb: 255,255,255;--ion-color-secondary-shade: #46b0e0;--ion-color-secondary-tint: #62ceff;--ion-color-tertiary: #6a64ff;--ion-color-tertiary-rgb: 106,100,255;--ion-color-tertiary-contrast: #ffffff;--ion-color-tertiary-contrast-rgb: 255,255,255;--ion-color-tertiary-shade: #5d58e0;--ion-color-tertiary-tint: #7974ff;--ion-color-success: var(--primary-600);--ion-color-success-rgb: 47,223,117;--ion-color-success-contrast: #000000;--ion-color-success-contrast-rgb: 0,0,0;--ion-color-success-shade: #29c467;--ion-color-success-tint: #44e283;--ion-color-warning: #ffd534;--ion-color-warning-rgb: 255,213,52;--ion-color-warning-contrast: #000000;--ion-color-warning-contrast-rgb: 0,0,0;--ion-color-warning-shade: #e0bb2e;--ion-color-warning-tint: #ffd948;--ion-color-danger: var(--error-200);--ion-color-danger-rgb: var(--error-200);--ion-color-danger-contrast: var(--error-200);--ion-color-danger-contrast-rgb: var(--error-200);--ion-color-danger-shade: var(--error-200);--ion-color-danger-tint: var(--error-200);--ion-color-dark: #f4f5f8;--ion-color-dark-rgb: 244,245,248;--ion-color-dark-contrast: #000000;--ion-color-dark-contrast-rgb: 0,0,0;--ion-color-dark-shade: #d7d8da;--ion-color-dark-tint: #f5f6f9;--ion-color-medium: #989aa2;--ion-color-medium-rgb: 152,154,162;--ion-color-medium-contrast: #000000;--ion-color-medium-contrast-rgb: 0,0,0;--ion-color-medium-shade: #86888f;--ion-color-medium-tint: #a2a4ab;--ion-color-light: #222428;--ion-color-light-rgb: 34,36,40;--ion-color-light-contrast: #ffffff;--ion-color-light-contrast-rgb: 255,255,255;--ion-color-light-shade: #1e2023;--ion-color-light-tint: #383a3e}}ion-content[_ngcontent-%COMP%]{--background: var(--neutral-50)}ion-footer[_ngcontent-%COMP%]{--background: var(--neutral-50)}.contenedor-header[_ngcontent-%COMP%]{height:10vh;background:var(--neutral-50);display:flex;align-items:flex-end;justify-content:flex-end}.contenedor-header[_ngcontent-%COMP%]   ion-toolbar[_ngcontent-%COMP%]{background:var(--neutral-50);--border-width: 0 0 2px 0 !important;--border-color: var(--primary-50)}.contenedor-header[_ngcontent-%COMP%]   ion-toolbar[_ngcontent-%COMP%]   ion-title[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:18px;color:var(--neutral-900)}.contenedor-principal[_ngcontent-%COMP%]{padding:0 32px 0 16px;background:var(--neutral-50)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-label[_ngcontent-%COMP%]   h3[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:16px;color:var(--neutral-900)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-label[_ngcontent-%COMP%]   p[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:16px;color:var(--neutral-400)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-icon[_ngcontent-%COMP%]{color:var(--primary-700)}.contenedor-principal[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]{background:var(--neutral-50)}.contenedor-footer[_ngcontent-%COMP%]{height:25vh;width:100%}.contenedor-footer[_ngcontent-%COMP%]   .gradiente[_ngcontent-%COMP%]{width:100%;height:40%;background:linear-gradient(rgba(243,244,250,0),rgb(243,244,250))}.contenedor-footer[_ngcontent-%COMP%]   .botones[_ngcontent-%COMP%]{background:rgb(243,244,250);height:60%;width:100%;display:flex;flex-direction:column;justify-content:flex-end;padding:0 32px 36px}.contenedor-footer[_ngcontent-%COMP%]   .botones[_ngcontent-%COMP%]   ion-button[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:16px;width:100%;max-width:600px;height:44px;--border-radius: 8px;color:var(--neutral-50);--background: var(--primary-700)}.contenedor-footer[_ngcontent-%COMP%]   .botones[_ngcontent-%COMP%]   .btn-login[_ngcontent-%COMP%]{--background: var(--primary-700)}.contenedor-footer[_ngcontent-%COMP%]   .botones[_ngcontent-%COMP%]   .btn-login.button-disabled[_ngcontent-%COMP%]{opacity:1;--background: var(--primary-300)}']}),a})();var b=c(4384),O=c(4328),D=c(1481);function x(e,a){if(1&e){const i=o.EpF();o.TgZ(0,"div",13)(1,"ion-text")(2,"p"),o._uU(3,"Nada Por aqu\xed. "),o._UZ(4,"br"),o._uU(5," A\xfan no tienes doctores asignados"),o.qZA()(),o.TgZ(6,"ion-button",14),o.NdJ("click",function(){o.CHM(i);const r=o.oxw();return o.KtG(r.AgregarDoctor())}),o._UZ(7,"ion-icon",15),o._uU(8," A\xf1adir doctor "),o.qZA()()}}function Z(e,a){if(1&e){const i=o.EpF();o.TgZ(0,"ion-item",18)(1,"ion-label")(2,"h3"),o._uU(3),o.qZA(),o.TgZ(4,"p"),o._uU(5),o.qZA(),o.TgZ(6,"p"),o._uU(7),o.qZA()(),o.TgZ(8,"ion-button",19),o.NdJ("click",function(){const s=o.CHM(i).$implicit,m=o.oxw(2);return o.KtG(m.presentarAlertaEliminar(s))}),o._UZ(9,"ion-icon",20),o.qZA()()}if(2&e){const i=a.$implicit;o.xp6(3),o.Oqu(i.nombre),o.xp6(2),o.Oqu(i.ambito),o.xp6(2),o.Oqu(i.hospital)}}function k(e,a){if(1&e&&(o.TgZ(0,"div",16)(1,"ion-list"),o.YNc(2,Z,10,3,"ion-item",17),o.qZA()()),2&e){const i=o.oxw();o.xp6(2),o.Q6J("ngForOf",i.misDoctores)}}let E=(()=>{var e;class a{constructor(n,r,s,m,F){this.doctoresService=n,this.alertController=r,this.archivoService=s,this.sanitizer=m,this.modalCtrl=F,(0,d.a)({chevronBack:f.Mny,add:f.IHx,trashOutline:f.gtu})}ionViewWillEnter(){this.consultarDoctores()}consultarDoctores(){this.doctoresService.consultarExpediente().subscribe(n=>{this.misDoctores=n})}eliminarDoctor(n){this.doctoresService.eliminar(n).subscribe({next:()=>{this.consultarDoctores()},error:()=>{},complete:()=>{this.presentarAlertaEliminadoExitosamente()}})}presentarAlertaEliminar(n){var r=this;return(0,l.Z)(function*(){yield(yield r.alertController.create({header:"\xbfSeguro que deseas eliminar este elemento?",subHeader:"No podr\xe1s recuperarlo",cssClass:"custom-alert color-error icon-trash two-buttons",buttons:[{text:"No, regresar",role:"cancel"},{text:"S\xed, eliminar",role:"confirm",handler:()=>{r.eliminarDoctor(n)}}]})).present()})()}presentarAlertaEliminadoExitosamente(){var n=this;return(0,l.Z)(function*(){yield(yield n.alertController.create({header:"Elemento eliminado exitosamente",buttons:[{text:"De acuerdo",role:"confirm"}],cssClass:"custom-alert color-primary icon-check"})).present()})()}AgregarDoctor(){var n=this;return(0,l.Z)(function*(){const r=yield n.modalCtrl.create({component:y});r.onWillDismiss().then(()=>{n.consultarDoctores()}),yield r.present()})()}listaDoctoresVacia(){var n;return(null===(n=this.misDoctores)||void 0===n?void 0:n.length)<=0}}return(e=a).\u0275fac=function(n){return new(n||e)(o.Y36(_.q),o.Y36(t.Br),o.Y36(O.g),o.Y36(D.H7),o.Y36(t.IN))},e.\u0275cmp=o.Xpm({type:e,selectors:[["app-mis-doctores"]],standalone:!0,features:[o.jDz],decls:19,vars:2,consts:[[1,"ion-no-border"],[1,"contenedor-header-light"],["slot","start",1,"start"],["routerLink","inicio-perfil"],["slot","start","name","chevron-back"],["slot","end",1,"end"],[1,"icon-only",3,"click"],["name","add"],[1,"primary"],[1,"contenedor-principal"],[1,"titulo"],["class","lista-vacia",4,"ngIf"],["class","lista-doctores",4,"ngIf"],[1,"lista-vacia"],["fill","outline",3,"click"],["slot","start","name","add"],[1,"lista-doctores"],["lines","none",4,"ngFor","ngForOf"],["lines","none"],["slot","end","fill","clear",3,"click"],["slot","icon-only","name","trash-outline"]],template:function(n,r){1&n&&(o.TgZ(0,"ion-header",0)(1,"div",1)(2,"ion-toolbar")(3,"ion-buttons",2)(4,"ion-button",3),o._UZ(5,"ion-icon",4),o.TgZ(6,"ion-label"),o._uU(7,"Atr\xe1s"),o.qZA()()(),o.TgZ(8,"ion-buttons",5)(9,"ion-button",6),o.NdJ("click",function(){return r.AgregarDoctor()}),o._UZ(10,"ion-icon",7),o.qZA()()()()(),o.TgZ(11,"ion-content",8)(12,"div",9)(13,"div",10)(14,"ion-text")(15,"h3"),o._uU(16,"Mis doctores"),o.qZA()()(),o.YNc(17,x,9,0,"div",11),o.YNc(18,k,3,1,"div",12),o.qZA()()),2&n&&(o.xp6(17),o.Q6J("ngIf",r.listaDoctoresVacia()),o.xp6(1),o.Q6J("ngIf",!r.listaDoctoresVacia()))},dependencies:[t.Pc,t.YG,t.Sm,t.W2,t.Gu,t.gu,t.Ie,t.Q$,t.q_,t.yW,t.sr,t.YI,u.ez,u.sg,u.O5,h.u5,b.Bz,b.rH],styles:["ion-content[_ngcontent-%COMP%]{--background: var(--primary-50)}.contenedor-principal[_ngcontent-%COMP%]{background:var(--primary-50);height:80vh}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]{height:8vh;padding:16px 32px 0}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]   ion-text[_ngcontent-%COMP%]{text-align:start}.contenedor-principal[_ngcontent-%COMP%]   .titulo[_ngcontent-%COMP%]   ion-text[_ngcontent-%COMP%]   h3[_ngcontent-%COMP%]{margin:0;color:var(--primary-700);font-family:var(--font-display);font-size:24px;font-weight:700;line-height:32px}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]{height:72vh}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]{background:var(--primary-50)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]{--border-width: 0 0 2px 0;--border-color: var(--primary-50)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-label[_ngcontent-%COMP%]{padding-left:16px}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-label[_ngcontent-%COMP%]   h3[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:16px;color:var(--neutral-900)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-label[_ngcontent-%COMP%]   p[_ngcontent-%COMP%]{font-family:var(--font-body);font-size:14px;color:var(--neutral-600)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-button[_ngcontent-%COMP%]{padding-right:8.8px!important;margin-right:0!important;--ripple-color: var(--error-200)}.contenedor-principal[_ngcontent-%COMP%]   .lista-doctores[_ngcontent-%COMP%]   ion-list[_ngcontent-%COMP%]   ion-item[_ngcontent-%COMP%]   ion-button[_ngcontent-%COMP%]   ion-icon[_ngcontent-%COMP%]{color:var(--error-200)}"]}),a})()},9134:(C,p,c)=>{c.d(p,{q:()=>u});var l=c(1571),t=c(529);let u=(()=>{var d;class f{constructor(g){this.http=g,this.url="expedienteDoctor/"}consultarExpediente(){return this.http.get(this.url)}consultarExpedienteConImagenes(){return this.http.get(this.url+"conImagenes")}consultarSelector(){return this.http.get(this.url+"selector")}consultarPorUsuarioParaSelector(){return this.http.get(this.url+"selectorPorUsuario")}eliminar(g){return this.http.delete(this.url+"eliminarDoctorTrackr",{body:g})}agregar(g){return this.http.post(this.url,g)}}return(d=f).\u0275fac=function(g){return new(g||d)(l.LFG(t.eN))},d.\u0275prov=l.Yz7({token:d,factory:d.\u0275fac,providedIn:"root"}),f})()}}]);