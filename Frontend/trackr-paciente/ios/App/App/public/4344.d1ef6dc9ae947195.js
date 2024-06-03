"use strict";(self.webpackChunkapp=self.webpackChunkapp||[]).push([[4344],{9160:(oe,F,f)=>{f.d(F,{L:()=>Z});var _=f(5861),c=f(9856),R=f(5079),v=f(1651),k=f(162),T=f(1135),D=f(9300),K=f(5698),q=f(7414),V=f(262),Y=f(2340),N=f(1571),J=f(2644),z=f(3369);let Z=(()=>{var P;class E{constructor(u,g){this.ChatPersonaService=u,this.authService=g,this.connectionStatus=new T.X(c.A.Disconnected),this.chatSubject=new T.X([]),this.chat$=this.chatSubject.asObservable(),this.endpoint="hub/chat",this.iniciarConexion()}iniciarConexion(){var u=this;return(0,_.Z)(function*(){const g=yield u.authService.obtenerToken();if(!g)return;const p=`${Y.N.urlBackend}${u.endpoint}`,G={accessTokenFactory:()=>g,transport:R.n.LongPolling};u.connection=(new v.s).configureLogging(k.i.Debug).withUrl(p,G).build(),u.connection.on("NuevoChat",(x,s)=>u.onNuevoChat(x,s)),u.connection.on("NuevaConexion",x=>u.onNuevaConexion(x)),u.connection.on("CargarChats",x=>u.onCargarChats(x)),u.connectionStatus.next(c.A.Connecting),yield u.connection.start()})()}detenerConexion(){var u=this;return(0,_.Z)(function*(){u.connectionStatus.next(c.A.Disconnecting),yield u.connection.stop(),u.chatSubject.next([]),u.connectionStatus.next(c.A.Disconnected)})()}obtenerNotificaciones(){return this.chatSubject.value}onNuevoChat(u,g){var p=this;return(0,_.Z)(function*(){u.fecha=new Date,p.chatSubject.value.push(u)})()}onNuevaConexion(u){for(const g of u)g.fecha=new Date(g.fecha);this.chatSubject.next(u)}onCargarChats(u){this.chatSubject.next(u)}ensureConnection(){var u=this;return(0,_.Z)(function*(){if(u.connection.state!==c.A.Connected){if(u.connection.state===c.A.Disconnected||u.connection.state===c.A.Disconnecting)throw new Error("No se ha iniciado la conexion con el Hub de Notificaciones");(u.connection.state===c.A.Connecting||u.connection.state===c.A.Reconnecting)&&u.connectionStatus.asObservable().pipe((0,D.h)(p=>p===c.A.Connected),(0,K.q)(1),(0,q.V)(1e4),(0,V.K)(()=>{throw new Error("No se pudo establecer la conexi\xf3n con el Hub de Notificaciones")}))}})()}agregarChat(u,g){var p=this;return(0,_.Z)(function*(){yield p.ensureConnection(),yield p.connection.invoke("NuevoChat",u,g)})()}abandonarChat(u){let g=this.chatSubject.value;g=g.filter(p=>p.idChat!=u),this.chatSubject.next(g)}}return(P=E).\u0275fac=function(u){return new(u||P)(N.LFG(J.R),N.LFG(z.e))},P.\u0275prov=N.Yz7({token:P,factory:P.\u0275fac,providedIn:"root"}),E})()},2644:(oe,F,f)=>{f.d(F,{R:()=>R});var _=f(1571),c=f(529);let R=(()=>{var v;class k{constructor(D){this.http=D,this.dataUrl="ChatPersona/"}agregarPersonas(D){return this.http.post(this.dataUrl,D)}obtenerIdUsuario(){return this.http.get(`${this.dataUrl}IdUsuario`)}obtenerIdPacientesPadecimiento(D){return this.http.get(`${this.dataUrl}Padecimiento/${D}`)}}return(v=k).\u0275fac=function(D){return new(D||v)(_.LFG(c.eN))},v.\u0275prov=_.Yz7({token:v,factory:v.\u0275fac,providedIn:"root"}),k})()},3051:(oe,F,f)=>{function _(a){return(_="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t})(a)}function c(a,t){if(t.length<a)throw new TypeError(a+" argument"+(a>1?"s":"")+" required, but only "+t.length+" present")}function v(a){c(1,arguments);var t=Object.prototype.toString.call(a);return a instanceof Date||"object"===_(a)&&"[object Date]"===t?new Date(a.getTime()):"number"==typeof a||"[object Number]"===t?new Date(a):(("string"==typeof a||"[object String]"===t)&&typeof console<"u"&&(console.warn("Starting with v2.0.0-beta.1 date-fns doesn't accept strings as date arguments. Please use `parseISO` to parse strings. See: https://github.com/date-fns/date-fns/blob/master/docs/upgradeGuide.md#string-arguments"),console.warn((new Error).stack)),new Date(NaN))}function T(a){if(null===a||!0===a||!1===a)return NaN;var t=Number(a);return isNaN(t)?t:t<0?Math.ceil(t):Math.floor(t)}f.d(F,{Z:()=>ht});var q=864e5;function Y(a){c(1,arguments);var e=v(a),n=e.getUTCDay(),r=(n<1?7:0)+n-1;return e.setUTCDate(e.getUTCDate()-r),e.setUTCHours(0,0,0,0),e}function N(a){c(1,arguments);var t=v(a),e=t.getUTCFullYear(),n=new Date(0);n.setUTCFullYear(e+1,0,4),n.setUTCHours(0,0,0,0);var r=Y(n),i=new Date(0);i.setUTCFullYear(e,0,4),i.setUTCHours(0,0,0,0);var o=Y(i);return t.getTime()>=r.getTime()?e+1:t.getTime()>=o.getTime()?e:e-1}var z=6048e5;var P={};function E(){return P}function u(a,t){var e,n,r,i,o,d,h,l;c(1,arguments);var w=E(),m=T(null!==(e=null!==(n=null!==(r=null!==(i=null==t?void 0:t.weekStartsOn)&&void 0!==i?i:null==t||null===(o=t.locale)||void 0===o||null===(d=o.options)||void 0===d?void 0:d.weekStartsOn)&&void 0!==r?r:w.weekStartsOn)&&void 0!==n?n:null===(h=w.locale)||void 0===h||null===(l=h.options)||void 0===l?void 0:l.weekStartsOn)&&void 0!==e?e:0);if(!(m>=0&&m<=6))throw new RangeError("weekStartsOn must be between 0 and 6 inclusively");var y=v(a),b=y.getUTCDay(),O=(b<m?7:0)+b-m;return y.setUTCDate(y.getUTCDate()-O),y.setUTCHours(0,0,0,0),y}function g(a,t){var e,n,r,i,o,d,h,l;c(1,arguments);var w=v(a),m=w.getUTCFullYear(),y=E(),b=T(null!==(e=null!==(n=null!==(r=null!==(i=null==t?void 0:t.firstWeekContainsDate)&&void 0!==i?i:null==t||null===(o=t.locale)||void 0===o||null===(d=o.options)||void 0===d?void 0:d.firstWeekContainsDate)&&void 0!==r?r:y.firstWeekContainsDate)&&void 0!==n?n:null===(h=y.locale)||void 0===h||null===(l=h.options)||void 0===l?void 0:l.firstWeekContainsDate)&&void 0!==e?e:1);if(!(b>=1&&b<=7))throw new RangeError("firstWeekContainsDate must be between 1 and 7 inclusively");var O=new Date(0);O.setUTCFullYear(m+1,0,b),O.setUTCHours(0,0,0,0);var $=u(O,t),U=new Date(0);U.setUTCFullYear(m,0,b),U.setUTCHours(0,0,0,0);var B=u(U,t);return w.getTime()>=$.getTime()?m+1:w.getTime()>=B.getTime()?m:m-1}var G=6048e5;function s(a,t){for(var e=a<0?"-":"",n=Math.abs(a).toString();n.length<t;)n="0"+n;return e+n}const M_y=function(t,e){var n=t.getUTCFullYear(),r=n>0?n:1-n;return s("yy"===e?r%100:r,e.length)},M_M=function(t,e){var n=t.getUTCMonth();return"M"===e?String(n+1):s(n+1,2)},M_d=function(t,e){return s(t.getUTCDate(),e.length)},M_h=function(t,e){return s(t.getUTCHours()%12||12,e.length)},M_H=function(t,e){return s(t.getUTCHours(),e.length)},M_m=function(t,e){return s(t.getUTCMinutes(),e.length)},M_s=function(t,e){return s(t.getUTCSeconds(),e.length)},M_S=function(t,e){var n=e.length,r=t.getUTCMilliseconds();return s(Math.floor(r*Math.pow(10,n-3)),e.length)};function se(a,t){var e=a>0?"-":"+",n=Math.abs(a),r=Math.floor(n/60),i=n%60;if(0===i)return e+String(r);var o=t||"";return e+String(r)+o+s(i,2)}function ce(a,t){return a%60==0?(a>0?"-":"+")+s(Math.abs(a)/60,2):W(a,t)}function W(a,t){var e=t||"",n=a>0?"-":"+",r=Math.abs(a);return n+s(Math.floor(r/60),2)+e+s(r%60,2)}const ve={G:function(t,e,n){var r=t.getUTCFullYear()>0?1:0;switch(e){case"G":case"GG":case"GGG":return n.era(r,{width:"abbreviated"});case"GGGGG":return n.era(r,{width:"narrow"});default:return n.era(r,{width:"wide"})}},y:function(t,e,n){if("yo"===e){var r=t.getUTCFullYear();return n.ordinalNumber(r>0?r:1-r,{unit:"year"})}return M_y(t,e)},Y:function(t,e,n,r){var i=g(t,r),o=i>0?i:1-i;return"YY"===e?s(o%100,2):"Yo"===e?n.ordinalNumber(o,{unit:"year"}):s(o,e.length)},R:function(t,e){return s(N(t),e.length)},u:function(t,e){return s(t.getUTCFullYear(),e.length)},Q:function(t,e,n){var r=Math.ceil((t.getUTCMonth()+1)/3);switch(e){case"Q":return String(r);case"QQ":return s(r,2);case"Qo":return n.ordinalNumber(r,{unit:"quarter"});case"QQQ":return n.quarter(r,{width:"abbreviated",context:"formatting"});case"QQQQQ":return n.quarter(r,{width:"narrow",context:"formatting"});default:return n.quarter(r,{width:"wide",context:"formatting"})}},q:function(t,e,n){var r=Math.ceil((t.getUTCMonth()+1)/3);switch(e){case"q":return String(r);case"qq":return s(r,2);case"qo":return n.ordinalNumber(r,{unit:"quarter"});case"qqq":return n.quarter(r,{width:"abbreviated",context:"standalone"});case"qqqqq":return n.quarter(r,{width:"narrow",context:"standalone"});default:return n.quarter(r,{width:"wide",context:"standalone"})}},M:function(t,e,n){var r=t.getUTCMonth();switch(e){case"M":case"MM":return M_M(t,e);case"Mo":return n.ordinalNumber(r+1,{unit:"month"});case"MMM":return n.month(r,{width:"abbreviated",context:"formatting"});case"MMMMM":return n.month(r,{width:"narrow",context:"formatting"});default:return n.month(r,{width:"wide",context:"formatting"})}},L:function(t,e,n){var r=t.getUTCMonth();switch(e){case"L":return String(r+1);case"LL":return s(r+1,2);case"Lo":return n.ordinalNumber(r+1,{unit:"month"});case"LLL":return n.month(r,{width:"abbreviated",context:"standalone"});case"LLLLL":return n.month(r,{width:"narrow",context:"standalone"});default:return n.month(r,{width:"wide",context:"standalone"})}},w:function(t,e,n,r){var i=function x(a,t){c(1,arguments);var e=v(a),n=u(e,t).getTime()-function p(a,t){var e,n,r,i,o,d,h,l;c(1,arguments);var w=E(),m=T(null!==(e=null!==(n=null!==(r=null!==(i=null==t?void 0:t.firstWeekContainsDate)&&void 0!==i?i:null==t||null===(o=t.locale)||void 0===o||null===(d=o.options)||void 0===d?void 0:d.firstWeekContainsDate)&&void 0!==r?r:w.firstWeekContainsDate)&&void 0!==n?n:null===(h=w.locale)||void 0===h||null===(l=h.options)||void 0===l?void 0:l.firstWeekContainsDate)&&void 0!==e?e:1),y=g(a,t),b=new Date(0);return b.setUTCFullYear(y,0,m),b.setUTCHours(0,0,0,0),u(b,t)}(e,t).getTime();return Math.round(n/G)+1}(t,r);return"wo"===e?n.ordinalNumber(i,{unit:"week"}):s(i,e.length)},I:function(t,e,n){var r=function Z(a){c(1,arguments);var t=v(a),e=Y(t).getTime()-function J(a){c(1,arguments);var t=N(a),e=new Date(0);return e.setUTCFullYear(t,0,4),e.setUTCHours(0,0,0,0),Y(e)}(t).getTime();return Math.round(e/z)+1}(t);return"Io"===e?n.ordinalNumber(r,{unit:"week"}):s(r,e.length)},d:function(t,e,n){return"do"===e?n.ordinalNumber(t.getUTCDate(),{unit:"date"}):M_d(t,e)},D:function(t,e,n){var r=function V(a){c(1,arguments);var t=v(a),e=t.getTime();t.setUTCMonth(0,1),t.setUTCHours(0,0,0,0);var n=t.getTime();return Math.floor((e-n)/q)+1}(t);return"Do"===e?n.ordinalNumber(r,{unit:"dayOfYear"}):s(r,e.length)},E:function(t,e,n){var r=t.getUTCDay();switch(e){case"E":case"EE":case"EEE":return n.day(r,{width:"abbreviated",context:"formatting"});case"EEEEE":return n.day(r,{width:"narrow",context:"formatting"});case"EEEEEE":return n.day(r,{width:"short",context:"formatting"});default:return n.day(r,{width:"wide",context:"formatting"})}},e:function(t,e,n,r){var i=t.getUTCDay(),o=(i-r.weekStartsOn+8)%7||7;switch(e){case"e":return String(o);case"ee":return s(o,2);case"eo":return n.ordinalNumber(o,{unit:"day"});case"eee":return n.day(i,{width:"abbreviated",context:"formatting"});case"eeeee":return n.day(i,{width:"narrow",context:"formatting"});case"eeeeee":return n.day(i,{width:"short",context:"formatting"});default:return n.day(i,{width:"wide",context:"formatting"})}},c:function(t,e,n,r){var i=t.getUTCDay(),o=(i-r.weekStartsOn+8)%7||7;switch(e){case"c":return String(o);case"cc":return s(o,e.length);case"co":return n.ordinalNumber(o,{unit:"day"});case"ccc":return n.day(i,{width:"abbreviated",context:"standalone"});case"ccccc":return n.day(i,{width:"narrow",context:"standalone"});case"cccccc":return n.day(i,{width:"short",context:"standalone"});default:return n.day(i,{width:"wide",context:"standalone"})}},i:function(t,e,n){var r=t.getUTCDay(),i=0===r?7:r;switch(e){case"i":return String(i);case"ii":return s(i,e.length);case"io":return n.ordinalNumber(i,{unit:"day"});case"iii":return n.day(r,{width:"abbreviated",context:"formatting"});case"iiiii":return n.day(r,{width:"narrow",context:"formatting"});case"iiiiii":return n.day(r,{width:"short",context:"formatting"});default:return n.day(r,{width:"wide",context:"formatting"})}},a:function(t,e,n){var i=t.getUTCHours()/12>=1?"pm":"am";switch(e){case"a":case"aa":return n.dayPeriod(i,{width:"abbreviated",context:"formatting"});case"aaa":return n.dayPeriod(i,{width:"abbreviated",context:"formatting"}).toLowerCase();case"aaaaa":return n.dayPeriod(i,{width:"narrow",context:"formatting"});default:return n.dayPeriod(i,{width:"wide",context:"formatting"})}},b:function(t,e,n){var i,r=t.getUTCHours();switch(i=12===r?"noon":0===r?"midnight":r/12>=1?"pm":"am",e){case"b":case"bb":return n.dayPeriod(i,{width:"abbreviated",context:"formatting"});case"bbb":return n.dayPeriod(i,{width:"abbreviated",context:"formatting"}).toLowerCase();case"bbbbb":return n.dayPeriod(i,{width:"narrow",context:"formatting"});default:return n.dayPeriod(i,{width:"wide",context:"formatting"})}},B:function(t,e,n){var i,r=t.getUTCHours();switch(i=r>=17?"evening":r>=12?"afternoon":r>=4?"morning":"night",e){case"B":case"BB":case"BBB":return n.dayPeriod(i,{width:"abbreviated",context:"formatting"});case"BBBBB":return n.dayPeriod(i,{width:"narrow",context:"formatting"});default:return n.dayPeriod(i,{width:"wide",context:"formatting"})}},h:function(t,e,n){if("ho"===e){var r=t.getUTCHours()%12;return 0===r&&(r=12),n.ordinalNumber(r,{unit:"hour"})}return M_h(t,e)},H:function(t,e,n){return"Ho"===e?n.ordinalNumber(t.getUTCHours(),{unit:"hour"}):M_H(t,e)},K:function(t,e,n){var r=t.getUTCHours()%12;return"Ko"===e?n.ordinalNumber(r,{unit:"hour"}):s(r,e.length)},k:function(t,e,n){var r=t.getUTCHours();return 0===r&&(r=24),"ko"===e?n.ordinalNumber(r,{unit:"hour"}):s(r,e.length)},m:function(t,e,n){return"mo"===e?n.ordinalNumber(t.getUTCMinutes(),{unit:"minute"}):M_m(t,e)},s:function(t,e,n){return"so"===e?n.ordinalNumber(t.getUTCSeconds(),{unit:"second"}):M_s(t,e)},S:function(t,e){return M_S(t,e)},X:function(t,e,n,r){var o=(r._originalDate||t).getTimezoneOffset();if(0===o)return"Z";switch(e){case"X":return ce(o);case"XXXX":case"XX":return W(o);default:return W(o,":")}},x:function(t,e,n,r){var o=(r._originalDate||t).getTimezoneOffset();switch(e){case"x":return ce(o);case"xxxx":case"xx":return W(o);default:return W(o,":")}},O:function(t,e,n,r){var o=(r._originalDate||t).getTimezoneOffset();switch(e){case"O":case"OO":case"OOO":return"GMT"+se(o,":");default:return"GMT"+W(o,":")}},z:function(t,e,n,r){var o=(r._originalDate||t).getTimezoneOffset();switch(e){case"z":case"zz":case"zzz":return"GMT"+se(o,":");default:return"GMT"+W(o,":")}},t:function(t,e,n,r){return s(Math.floor((r._originalDate||t).getTime()/1e3),e.length)},T:function(t,e,n,r){return s((r._originalDate||t).getTime(),e.length)}};var de=function(t,e){switch(t){case"P":return e.date({width:"short"});case"PP":return e.date({width:"medium"});case"PPP":return e.date({width:"long"});default:return e.date({width:"full"})}},le=function(t,e){switch(t){case"p":return e.time({width:"short"});case"pp":return e.time({width:"medium"});case"ppp":return e.time({width:"long"});default:return e.time({width:"full"})}};const be={p:le,P:function(t,e){var o,n=t.match(/(P+)(p+)?/)||[],r=n[1],i=n[2];if(!i)return de(t,e);switch(r){case"P":o=e.dateTime({width:"short"});break;case"PP":o=e.dateTime({width:"medium"});break;case"PPP":o=e.dateTime({width:"long"});break;default:o=e.dateTime({width:"full"})}return o.replace("{{date}}",de(r,e)).replace("{{time}}",le(i,e))}};var _e=["D","DD"],Ce=["YY","YYYY"];function fe(a,t,e){if("YYYY"===a)throw new RangeError("Use `yyyy` instead of `YYYY` (in `".concat(t,"`) for formatting years to the input `").concat(e,"`; see: https://github.com/date-fns/date-fns/blob/master/docs/unicodeTokens.md"));if("YY"===a)throw new RangeError("Use `yy` instead of `YY` (in `".concat(t,"`) for formatting years to the input `").concat(e,"`; see: https://github.com/date-fns/date-fns/blob/master/docs/unicodeTokens.md"));if("D"===a)throw new RangeError("Use `d` instead of `D` (in `".concat(t,"`) for formatting days of the month to the input `").concat(e,"`; see: https://github.com/date-fns/date-fns/blob/master/docs/unicodeTokens.md"));if("DD"===a)throw new RangeError("Use `dd` instead of `DD` (in `".concat(t,"`) for formatting days of the month to the input `").concat(e,"`; see: https://github.com/date-fns/date-fns/blob/master/docs/unicodeTokens.md"))}var De={lessThanXSeconds:{one:"less than a second",other:"less than {{count}} seconds"},xSeconds:{one:"1 second",other:"{{count}} seconds"},halfAMinute:"half a minute",lessThanXMinutes:{one:"less than a minute",other:"less than {{count}} minutes"},xMinutes:{one:"1 minute",other:"{{count}} minutes"},aboutXHours:{one:"about 1 hour",other:"about {{count}} hours"},xHours:{one:"1 hour",other:"{{count}} hours"},xDays:{one:"1 day",other:"{{count}} days"},aboutXWeeks:{one:"about 1 week",other:"about {{count}} weeks"},xWeeks:{one:"1 week",other:"{{count}} weeks"},aboutXMonths:{one:"about 1 month",other:"about {{count}} months"},xMonths:{one:"1 month",other:"{{count}} months"},aboutXYears:{one:"about 1 year",other:"about {{count}} years"},xYears:{one:"1 year",other:"{{count}} years"},overXYears:{one:"over 1 year",other:"over {{count}} years"},almostXYears:{one:"almost 1 year",other:"almost {{count}} years"}};function ee(a){return function(){var t=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},e=t.width?String(t.width):a.defaultWidth;return a.formats[e]||a.formats[a.defaultWidth]}}var xe={date:ee({formats:{full:"EEEE, MMMM do, y",long:"MMMM do, y",medium:"MMM d, y",short:"MM/dd/yyyy"},defaultWidth:"full"}),time:ee({formats:{full:"h:mm:ss a zzzz",long:"h:mm:ss a z",medium:"h:mm:ss a",short:"h:mm a"},defaultWidth:"full"}),dateTime:ee({formats:{full:"{{date}} 'at' {{time}}",long:"{{date}} 'at' {{time}}",medium:"{{date}}, {{time}}",short:"{{date}}, {{time}}"},defaultWidth:"full"})},Ue={lastWeek:"'last' eeee 'at' p",yesterday:"'yesterday at' p",today:"'today at' p",tomorrow:"'tomorrow at' p",nextWeek:"eeee 'at' p",other:"P"};function A(a){return function(t,e){var r;if("formatting"===(null!=e&&e.context?String(e.context):"standalone")&&a.formattingValues){var i=a.defaultFormattingWidth||a.defaultWidth,o=null!=e&&e.width?String(e.width):i;r=a.formattingValues[o]||a.formattingValues[i]}else{var d=a.defaultWidth,h=null!=e&&e.width?String(e.width):a.defaultWidth;r=a.values[h]||a.values[d]}return r[a.argumentCallback?a.argumentCallback(t):t]}}function I(a){return function(t){var e=arguments.length>1&&void 0!==arguments[1]?arguments[1]:{},n=e.width,i=t.match(n&&a.matchPatterns[n]||a.matchPatterns[a.defaultMatchWidth]);if(!i)return null;var l,o=i[0],d=n&&a.parsePatterns[n]||a.parsePatterns[a.defaultParseWidth],h=Array.isArray(d)?function Ge(a,t){for(var e=0;e<a.length;e++)if(t(a[e]))return e}(d,function(m){return m.test(o)}):function He(a,t){for(var e in a)if(a.hasOwnProperty(e)&&t(a[e]))return e}(d,function(m){return m.test(o)});return l=a.valueCallback?a.valueCallback(h):h,{value:l=e.valueCallback?e.valueCallback(l):l,rest:t.slice(o.length)}}}const ut={code:"en-US",formatDistance:function(t,e,n){var r,i=De[t];return r="string"==typeof i?i:1===e?i.one:i.other.replace("{{count}}",e.toString()),null!=n&&n.addSuffix?n.comparison&&n.comparison>0?"in "+r:r+" ago":r},formatLong:xe,formatRelative:function(t,e,n,r){return Ue[t]},localize:{ordinalNumber:function(t,e){var n=Number(t),r=n%100;if(r>20||r<10)switch(r%10){case 1:return n+"st";case 2:return n+"nd";case 3:return n+"rd"}return n+"th"},era:A({values:{narrow:["B","A"],abbreviated:["BC","AD"],wide:["Before Christ","Anno Domini"]},defaultWidth:"wide"}),quarter:A({values:{narrow:["1","2","3","4"],abbreviated:["Q1","Q2","Q3","Q4"],wide:["1st quarter","2nd quarter","3rd quarter","4th quarter"]},defaultWidth:"wide",argumentCallback:function(t){return t-1}}),month:A({values:{narrow:["J","F","M","A","M","J","J","A","S","O","N","D"],abbreviated:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"],wide:["January","February","March","April","May","June","July","August","September","October","November","December"]},defaultWidth:"wide"}),day:A({values:{narrow:["S","M","T","W","T","F","S"],short:["Su","Mo","Tu","We","Th","Fr","Sa"],abbreviated:["Sun","Mon","Tue","Wed","Thu","Fri","Sat"],wide:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"]},defaultWidth:"wide"}),dayPeriod:A({values:{narrow:{am:"a",pm:"p",midnight:"mi",noon:"n",morning:"morning",afternoon:"afternoon",evening:"evening",night:"night"},abbreviated:{am:"AM",pm:"PM",midnight:"midnight",noon:"noon",morning:"morning",afternoon:"afternoon",evening:"evening",night:"night"},wide:{am:"a.m.",pm:"p.m.",midnight:"midnight",noon:"noon",morning:"morning",afternoon:"afternoon",evening:"evening",night:"night"}},defaultWidth:"wide",formattingValues:{narrow:{am:"a",pm:"p",midnight:"mi",noon:"n",morning:"in the morning",afternoon:"in the afternoon",evening:"in the evening",night:"at night"},abbreviated:{am:"AM",pm:"PM",midnight:"midnight",noon:"noon",morning:"in the morning",afternoon:"in the afternoon",evening:"in the evening",night:"at night"},wide:{am:"a.m.",pm:"p.m.",midnight:"midnight",noon:"noon",morning:"in the morning",afternoon:"in the afternoon",evening:"in the evening",night:"at night"}},defaultFormattingWidth:"wide"})},match:{ordinalNumber:function Xe(a){return function(t){var e=arguments.length>1&&void 0!==arguments[1]?arguments[1]:{},n=t.match(a.matchPattern);if(!n)return null;var r=n[0],i=t.match(a.parsePattern);if(!i)return null;var o=a.valueCallback?a.valueCallback(i[0]):i[0];return{value:o=e.valueCallback?e.valueCallback(o):o,rest:t.slice(r.length)}}}({matchPattern:/^(\d+)(th|st|nd|rd)?/i,parsePattern:/\d+/i,valueCallback:function(t){return parseInt(t,10)}}),era:I({matchPatterns:{narrow:/^(b|a)/i,abbreviated:/^(b\.?\s?c\.?|b\.?\s?c\.?\s?e\.?|a\.?\s?d\.?|c\.?\s?e\.?)/i,wide:/^(before christ|before common era|anno domini|common era)/i},defaultMatchWidth:"wide",parsePatterns:{any:[/^b/i,/^(a|c)/i]},defaultParseWidth:"any"}),quarter:I({matchPatterns:{narrow:/^[1234]/i,abbreviated:/^q[1234]/i,wide:/^[1234](th|st|nd|rd)? quarter/i},defaultMatchWidth:"wide",parsePatterns:{any:[/1/i,/2/i,/3/i,/4/i]},defaultParseWidth:"any",valueCallback:function(t){return t+1}}),month:I({matchPatterns:{narrow:/^[jfmasond]/i,abbreviated:/^(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)/i,wide:/^(january|february|march|april|may|june|july|august|september|october|november|december)/i},defaultMatchWidth:"wide",parsePatterns:{narrow:[/^j/i,/^f/i,/^m/i,/^a/i,/^m/i,/^j/i,/^j/i,/^a/i,/^s/i,/^o/i,/^n/i,/^d/i],any:[/^ja/i,/^f/i,/^mar/i,/^ap/i,/^may/i,/^jun/i,/^jul/i,/^au/i,/^s/i,/^o/i,/^n/i,/^d/i]},defaultParseWidth:"any"}),day:I({matchPatterns:{narrow:/^[smtwf]/i,short:/^(su|mo|tu|we|th|fr|sa)/i,abbreviated:/^(sun|mon|tue|wed|thu|fri|sat)/i,wide:/^(sunday|monday|tuesday|wednesday|thursday|friday|saturday)/i},defaultMatchWidth:"wide",parsePatterns:{narrow:[/^s/i,/^m/i,/^t/i,/^w/i,/^t/i,/^f/i,/^s/i],any:[/^su/i,/^m/i,/^tu/i,/^w/i,/^th/i,/^f/i,/^sa/i]},defaultParseWidth:"any"}),dayPeriod:I({matchPatterns:{narrow:/^(a|p|mi|n|(in the|at) (morning|afternoon|evening|night))/i,any:/^([ap]\.?\s?m\.?|midnight|noon|(in the|at) (morning|afternoon|evening|night))/i},defaultMatchWidth:"any",parsePatterns:{any:{am:/^a/i,pm:/^p/i,midnight:/^mi/i,noon:/^no/i,morning:/morning/i,afternoon:/afternoon/i,evening:/evening/i,night:/night/i}},defaultParseWidth:"any"})},options:{weekStartsOn:0,firstWeekContainsDate:1}};var st=/[yYQqMLwIdDecihHKkms]o|(\w)\1*|''|'(''|[^'])+('|$)|./g,ct=/P+p+|P+|p+|''|'(''|[^'])+('|$)|./g,dt=/^'([^]*?)'?$/,lt=/''/g,ft=/[a-zA-Z]/;function ht(a,t,e){var n,r,i,o,d,h,l,w,m,y,b,O,$,U,B,te,ae,ne;c(2,arguments);var vt=String(t),j=E(),H=null!==(n=null!==(r=null==e?void 0:e.locale)&&void 0!==r?r:j.locale)&&void 0!==n?n:ut,re=T(null!==(i=null!==(o=null!==(d=null!==(h=null==e?void 0:e.firstWeekContainsDate)&&void 0!==h?h:null==e||null===(l=e.locale)||void 0===l||null===(w=l.options)||void 0===w?void 0:w.firstWeekContainsDate)&&void 0!==d?d:j.firstWeekContainsDate)&&void 0!==o?o:null===(m=j.locale)||void 0===m||null===(y=m.options)||void 0===y?void 0:y.firstWeekContainsDate)&&void 0!==i?i:1);if(!(re>=1&&re<=7))throw new RangeError("firstWeekContainsDate must be between 1 and 7 inclusively");var ie=T(null!==(b=null!==(O=null!==($=null!==(U=null==e?void 0:e.weekStartsOn)&&void 0!==U?U:null==e||null===(B=e.locale)||void 0===B||null===(te=B.options)||void 0===te?void 0:te.weekStartsOn)&&void 0!==$?$:j.weekStartsOn)&&void 0!==O?O:null===(ae=j.locale)||void 0===ae||null===(ne=ae.options)||void 0===ne?void 0:ne.weekStartsOn)&&void 0!==b?b:0);if(!(ie>=0&&ie<=6))throw new RangeError("weekStartsOn must be between 0 and 6 inclusively");if(!H.localize)throw new RangeError("locale must contain localize property");if(!H.formatLong)throw new RangeError("locale must contain formatLong property");var X=v(a);if(!function k(a){if(c(1,arguments),!function R(a){return c(1,arguments),a instanceof Date||"object"===_(a)&&"[object Date]"===Object.prototype.toString.call(a)}(a)&&"number"!=typeof a)return!1;var t=v(a);return!isNaN(Number(t))}(X))throw new RangeError("Invalid time value");var gt=function ye(a){var t=new Date(Date.UTC(a.getFullYear(),a.getMonth(),a.getDate(),a.getHours(),a.getMinutes(),a.getSeconds(),a.getMilliseconds()));return t.setUTCFullYear(a.getFullYear()),a.getTime()-t.getTime()}(X),wt=function K(a,t){return c(2,arguments),function D(a,t){c(2,arguments);var e=v(a).getTime(),n=T(t);return new Date(e+n)}(a,-T(t))}(X,gt),bt={firstWeekContainsDate:re,weekStartsOn:ie,locale:H,_originalDate:X},yt=vt.match(ct).map(function(C){var S=C[0];return"p"===S||"P"===S?(0,be[S])(C,H.formatLong):C}).join("").match(st).map(function(C){if("''"===C)return"'";var S=C[0];if("'"===S)return function mt(a){var t=a.match(dt);return t?t[1].replace(lt,"'"):a}(C);var Q=ve[S];if(Q)return!(null!=e&&e.useAdditionalWeekYearTokens)&&function pe(a){return-1!==Ce.indexOf(a)}(C)&&fe(C,t,String(a)),!(null!=e&&e.useAdditionalDayOfYearTokens)&&function Te(a){return-1!==_e.indexOf(a)}(C)&&fe(C,t,String(a)),Q(wt,C,H.localize,bt);if(S.match(ft))throw new RangeError("Format string contains an unescaped latin alphabet character `"+S+"`");return C}).join("");return yt}}}]);