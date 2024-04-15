"use strict";(self.webpackChunkapp=self.webpackChunkapp||[]).push([[8592],{76:(D,_,e)=>{e.d(_,{GW:()=>r,dk:()=>l,oK:()=>c});var c=(()=>{return(n=c||(c={})).Prompt="PROMPT",n.Camera="CAMERA",n.Photos="PHOTOS",c;var n})(),r=(()=>{return(n=r||(r={})).Rear="REAR",n.Front="FRONT",r;var n})(),l=(()=>{return(n=l||(l={})).Uri="uri",n.Base64="base64",n.DataUrl="dataUrl",l;var n})()},6388:(D,_,e)=>{e.d(_,{V1:()=>l,dk:()=>r.dk,oK:()=>r.oK});var c=e(7423),r=e(76);const l=(0,c.fo)("Camera",{web:()=>e.e(3954).then(e.bind(e,3954)).then(n=>new n.CameraWeb)})},2671:(D,_,e)=>{e.d(_,{F:()=>c});var c=(()=>{return(r=c||(c={})).LANDSCAPE="landscape",r.LANDSCAPE_PRIMARY="landscape-primary",r.LANDSCAPE_SECONDARY="landscape-secondary",r.PORTRAIT="portrait",r.PORTRAIT_PRIMARY="portrait-primary",r.PORTRAIT_SECONDARY="portrait-secondary",c;var r})()},8431:(D,_,e)=>{e.d(_,{c:()=>n});var c=e(408),r=e(1765),l=e(5067);const n=(s,a)=>{let t,i;const f=(u,p,C)=>{if(typeof document>"u")return;const y=document.elementFromPoint(u,p);y&&a(y)?y!==t&&(d(),h(y,C)):d()},h=(u,p)=>{t=u,i||(i=t);const C=t;(0,c.w)(()=>C.classList.add("ion-activated")),p()},d=(u=!1)=>{if(!t)return;const p=t;(0,c.w)(()=>p.classList.remove("ion-activated")),u&&i!==t&&t.click(),t=void 0};return(0,l.createGesture)({el:s,gestureName:"buttonActiveDrag",threshold:0,onStart:u=>f(u.currentX,u.currentY,r.a),onMove:u=>f(u.currentX,u.currentY,r.b),onEnd:()=>{d(!0),(0,r.h)(),i=void 0}})}},6319:(D,_,e)=>{e.d(_,{g:()=>r});var c=e(2972);const r=()=>{if(void 0!==c.w)return c.w.Capacitor}},2890:(D,_,e)=>{e.d(_,{c:()=>c,i:()=>r});const c=(l,n,s)=>"function"==typeof s?s(l,n):"string"==typeof s?l[s]===n[s]:Array.isArray(n)?n.includes(l):l===n,r=(l,n,s)=>void 0!==l&&(Array.isArray(l)?l.some(a=>c(a,n,s)):c(l,n,s))},5069:(D,_,e)=>{e.d(_,{g:()=>c});const c=(a,t,i,f,h)=>l(a[1],t[1],i[1],f[1],h).map(d=>r(a[0],t[0],i[0],f[0],d)),r=(a,t,i,f,h)=>h*(3*t*Math.pow(h-1,2)+h*(-3*i*h+3*i+f*h))-a*Math.pow(h-1,3),l=(a,t,i,f,h)=>s((f-=h)-3*(i-=h)+3*(t-=h)-(a-=h),3*i-6*t+3*a,3*t-3*a,a).filter(u=>u>=0&&u<=1),s=(a,t,i,f)=>{if(0===a)return((a,t,i)=>{const f=t*t-4*a*i;return f<0?[]:[(-t+Math.sqrt(f))/(2*a),(-t-Math.sqrt(f))/(2*a)]})(t,i,f);const h=(3*(i/=a)-(t/=a)*t)/3,d=(2*t*t*t-9*t*i+27*(f/=a))/27;if(0===h)return[Math.pow(-d,1/3)];if(0===d)return[Math.sqrt(-h),-Math.sqrt(-h)];const u=Math.pow(d/2,2)+Math.pow(h/3,3);if(0===u)return[Math.pow(d/2,.5)-t/3];if(u>0)return[Math.pow(-d/2+Math.sqrt(u),1/3)-Math.pow(d/2+Math.sqrt(u),1/3)-t/3];const p=Math.sqrt(Math.pow(-h/3,3)),C=Math.acos(-d/(2*Math.sqrt(Math.pow(-h/3,3)))),y=2*Math.pow(p,1/3);return[y*Math.cos(C/3)-t/3,y*Math.cos((C+2*Math.PI)/3)-t/3,y*Math.cos((C+4*Math.PI)/3)-t/3]}},6879:(D,_,e)=>{e.d(_,{i:()=>c});const c=r=>r&&""!==r.dir?"rtl"===r.dir.toLowerCase():"rtl"===(null==document?void 0:document.dir.toLowerCase())},6390:(D,_,e)=>{e.r(_),e.d(_,{startFocusVisible:()=>n});const c="ion-focused",l=["Tab","ArrowDown","Space","Escape"," ","Shift","Enter","ArrowLeft","ArrowRight","ArrowUp","Home","End"],n=s=>{let a=[],t=!0;const i=s?s.shadowRoot:document,f=s||document.body,h=M=>{a.forEach(E=>E.classList.remove(c)),M.forEach(E=>E.classList.add(c)),a=M},d=()=>{t=!1,h([])},u=M=>{t=l.includes(M.key),t||h([])},p=M=>{if(t&&void 0!==M.composedPath){const E=M.composedPath().filter(m=>!!m.classList&&m.classList.contains("ion-focusable"));h(E)}},C=()=>{i.activeElement===f&&h([])};return i.addEventListener("keydown",u),i.addEventListener("focusin",p),i.addEventListener("focusout",C),i.addEventListener("touchstart",d,{passive:!0}),i.addEventListener("mousedown",d),{destroy:()=>{i.removeEventListener("keydown",u),i.removeEventListener("focusin",p),i.removeEventListener("focusout",C),i.removeEventListener("touchstart",d),i.removeEventListener("mousedown",d)},setFocus:h}}},8134:(D,_,e)=>{e.d(_,{c:()=>r});var c=e(2961);const r=a=>{const t=a;let i;return{hasLegacyControl:()=>{if(void 0===i){const h=void 0!==t.label||l(t),d=t.hasAttribute("aria-label")||t.hasAttribute("aria-labelledby")&&null===t.shadowRoot,u=(0,c.h)(t);i=!0===t.legacy||!h&&!d&&null!==u}return i}}},l=a=>!!(n.includes(a.tagName)&&null!==a.querySelector('[slot="label"]')||s.includes(a.tagName)&&""!==a.textContent),n=["ION-INPUT","ION-TEXTAREA","ION-SELECT","ION-RANGE"],s=["ION-TOGGLE","ION-CHECKBOX","ION-RADIO"]},1765:(D,_,e)=>{e.d(_,{I:()=>r,a:()=>t,b:()=>i,c:()=>a,d:()=>h,h:()=>f});var c=e(6319),r=(()=>{return(d=r||(r={})).Heavy="HEAVY",d.Medium="MEDIUM",d.Light="LIGHT",r;var d})();const n={getEngine(){const d=window.TapticEngine;if(d)return d;const u=(0,c.g)();return null!=u&&u.isPluginAvailable("Haptics")?u.Plugins.Haptics:void 0},available(){if(!this.getEngine())return!1;const u=(0,c.g)();return"web"!==(null==u?void 0:u.getPlatform())||typeof navigator<"u"&&void 0!==navigator.vibrate},isCordova:()=>void 0!==window.TapticEngine,isCapacitor:()=>void 0!==(0,c.g)(),impact(d){const u=this.getEngine();if(!u)return;const p=this.isCapacitor()?d.style:d.style.toLowerCase();u.impact({style:p})},notification(d){const u=this.getEngine();if(!u)return;const p=this.isCapacitor()?d.type:d.type.toLowerCase();u.notification({type:p})},selection(){const d=this.isCapacitor()?r.Light:"light";this.impact({style:d})},selectionStart(){const d=this.getEngine();d&&(this.isCapacitor()?d.selectionStart():d.gestureSelectionStart())},selectionChanged(){const d=this.getEngine();d&&(this.isCapacitor()?d.selectionChanged():d.gestureSelectionChanged())},selectionEnd(){const d=this.getEngine();d&&(this.isCapacitor()?d.selectionEnd():d.gestureSelectionEnd())}},s=()=>n.available(),a=()=>{s()&&n.selection()},t=()=>{s()&&n.selectionStart()},i=()=>{s()&&n.selectionChanged()},f=()=>{s()&&n.selectionEnd()},h=d=>{s()&&n.impact(d)}},4253:(D,_,e)=>{e.d(_,{I:()=>a,a:()=>h,b:()=>s,c:()=>p,d:()=>y,f:()=>d,g:()=>f,i:()=>i,p:()=>C,r:()=>M,s:()=>u});var c=e(5861),r=e(2961),l=e(8909);const s="ion-content",a=".ion-content-scroll-host",t=`${s}, ${a}`,i=E=>"ION-CONTENT"===E.tagName,f=function(){var E=(0,c.Z)(function*(m){return i(m)?(yield new Promise(o=>(0,r.c)(m,o)),m.getScrollElement()):m});return function(o){return E.apply(this,arguments)}}(),h=E=>E.querySelector(a)||E.querySelector(t),d=E=>E.closest(t),u=(E,m)=>i(E)?E.scrollToTop(m):Promise.resolve(E.scrollTo({top:0,left:0,behavior:m>0?"smooth":"auto"})),p=(E,m,o,g)=>i(E)?E.scrollByPoint(m,o,g):Promise.resolve(E.scrollBy({top:o,left:m,behavior:g>0?"smooth":"auto"})),C=E=>(0,l.b)(E,s),y=E=>{if(i(E)){const o=E.scrollY;return E.scrollY=!1,o}return E.style.setProperty("overflow","hidden"),!0},M=(E,m)=>{i(E)?E.scrollY=m:E.style.removeProperty("overflow")}},5723:(D,_,e)=>{e.d(_,{a:()=>c,b:()=>p,c:()=>t,d:()=>C,e:()=>P,f:()=>a,g:()=>y,h:()=>l,i:()=>r,j:()=>g,k:()=>O,l:()=>i,m:()=>d,n:()=>M,o:()=>h,p:()=>s,q:()=>n,r:()=>o,s:()=>v,t:()=>u,u:()=>E,v:()=>m,w:()=>f});const c="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='square' stroke-miterlimit='10' stroke-width='48' d='M244 400L100 256l144-144M120 256h292' class='ionicon-fill-none'/></svg>",r="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' stroke-width='48' d='M112 268l144 144 144-144M256 392V100' class='ionicon-fill-none'/></svg>",l="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M368 64L144 256l224 192V64z'/></svg>",n="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M64 144l192 224 192-224H64z'/></svg>",s="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M448 368L256 144 64 368h384z'/></svg>",a="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' d='M416 128L192 384l-96-96' class='ionicon-fill-none ionicon-stroke-width'/></svg>",t="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' stroke-width='48' d='M328 112L184 256l144 144' class='ionicon-fill-none'/></svg>",i="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' stroke-width='48' d='M112 184l144 144 144-144' class='ionicon-fill-none'/></svg>",f="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M136 208l120-104 120 104M136 304l120 104 120-104' stroke-width='48' stroke-linecap='round' stroke-linejoin='round' class='ionicon-fill-none'/></svg>",h="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' stroke-width='48' d='M184 112l144 144-144 144' class='ionicon-fill-none'/></svg>",d="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' stroke-width='48' d='M184 112l144 144-144 144' class='ionicon-fill-none'/></svg>",u="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M289.94 256l95-95A24 24 0 00351 127l-95 95-95-95a24 24 0 00-34 34l95 95-95 95a24 24 0 1034 34l95-95 95 95a24 24 0 0034-34z'/></svg>",p="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M256 48C141.31 48 48 141.31 48 256s93.31 208 208 208 208-93.31 208-208S370.69 48 256 48zm75.31 260.69a16 16 0 11-22.62 22.62L256 278.63l-52.69 52.68a16 16 0 01-22.62-22.62L233.37 256l-52.68-52.69a16 16 0 0122.62-22.62L256 233.37l52.69-52.68a16 16 0 0122.62 22.62L278.63 256z'/></svg>",C="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M400 145.49L366.51 112 256 222.51 145.49 112 112 145.49 222.51 256 112 366.51 145.49 400 256 289.49 366.51 400 400 366.51 289.49 256 400 145.49z'/></svg>",y="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><circle cx='256' cy='256' r='192' stroke-linecap='round' stroke-linejoin='round' class='ionicon-fill-none ionicon-stroke-width'/></svg>",M="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><circle cx='256' cy='256' r='48'/><circle cx='416' cy='256' r='48'/><circle cx='96' cy='256' r='48'/></svg>",E="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-miterlimit='10' d='M80 160h352M80 256h352M80 352h352' class='ionicon-fill-none ionicon-stroke-width'/></svg>",m="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M64 384h384v-42.67H64zm0-106.67h384v-42.66H64zM64 128v42.67h384V128z'/></svg>",o="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' d='M400 256H112' class='ionicon-fill-none ionicon-stroke-width'/></svg>",g="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='round' stroke-linejoin='round' d='M96 256h320M96 176h320M96 336h320' class='ionicon-fill-none ionicon-stroke-width'/></svg>",O="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path stroke-linecap='square' stroke-linejoin='round' stroke-width='44' d='M118 304h276M118 208h276' class='ionicon-fill-none'/></svg>",v="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M221.09 64a157.09 157.09 0 10157.09 157.09A157.1 157.1 0 00221.09 64z' stroke-miterlimit='10' class='ionicon-fill-none ionicon-stroke-width'/><path stroke-linecap='round' stroke-miterlimit='10' d='M338.29 338.29L448 448' class='ionicon-fill-none ionicon-stroke-width'/></svg>",P="data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' class='ionicon' viewBox='0 0 512 512'><path d='M464 428L339.92 303.9a160.48 160.48 0 0030.72-94.58C370.64 120.37 298.27 48 209.32 48S48 120.37 48 209.32s72.37 161.32 161.32 161.32a160.48 160.48 0 0094.58-30.72L428 464zM209.32 319.69a110.38 110.38 0 11110.37-110.37 110.5 110.5 0 01-110.37 110.37z'/></svg>"},4063:(D,_,e)=>{e.d(_,{c:()=>n,g:()=>s});var c=e(2972),r=e(2961),l=e(8909);const n=(t,i,f)=>{let h,d;if(void 0!==c.w&&"MutationObserver"in c.w){const y=Array.isArray(i)?i:[i];h=new MutationObserver(M=>{for(const E of M)for(const m of E.addedNodes)if(m.nodeType===Node.ELEMENT_NODE&&y.includes(m.slot))return f(),void(0,r.r)(()=>u(m))}),h.observe(t,{childList:!0})}const u=y=>{var M;d&&(d.disconnect(),d=void 0),d=new MutationObserver(E=>{f();for(const m of E)for(const o of m.removedNodes)o.nodeType===Node.ELEMENT_NODE&&o.slot===i&&C()}),d.observe(null!==(M=y.parentElement)&&void 0!==M?M:y,{subtree:!0,childList:!0})},C=()=>{d&&(d.disconnect(),d=void 0)};return{destroy:()=>{h&&(h.disconnect(),h=void 0),C()}}},s=(t,i,f)=>{const h=null==t?0:t.toString().length,d=a(h,i);if(void 0===f)return d;try{return f(h,i)}catch(u){return(0,l.a)("Exception in provided `counterFormatter`.",u),d}},a=(t,i)=>`${t} / ${i}`},922:(D,_,e)=>{e.r(_),e.d(_,{KEYBOARD_DID_CLOSE:()=>s,KEYBOARD_DID_OPEN:()=>n,copyVisualViewport:()=>O,keyboardDidClose:()=>E,keyboardDidOpen:()=>y,keyboardDidResize:()=>M,resetKeyboardAssist:()=>h,setKeyboardClose:()=>C,setKeyboardOpen:()=>p,startKeyboardAssist:()=>d,trackViewportChanges:()=>g});var c=e(3037);e(6319),e(2972);const n="ionKeyboardDidShow",s="ionKeyboardDidHide";let t={},i={},f=!1;const h=()=>{t={},i={},f=!1},d=v=>{if(c.K.getEngine())u(v);else{if(!v.visualViewport)return;i=O(v.visualViewport),v.visualViewport.onresize=()=>{g(v),y()||M(v)?p(v):E(v)&&C(v)}}},u=v=>{v.addEventListener("keyboardDidShow",P=>p(v,P)),v.addEventListener("keyboardDidHide",()=>C(v))},p=(v,P)=>{m(v,P),f=!0},C=v=>{o(v),f=!1},y=()=>!f&&t.width===i.width&&(t.height-i.height)*i.scale>150,M=v=>f&&!E(v),E=v=>f&&i.height===v.innerHeight,m=(v,P)=>{const A=new CustomEvent(n,{detail:{keyboardHeight:P?P.keyboardHeight:v.innerHeight-i.height}});v.dispatchEvent(A)},o=v=>{const P=new CustomEvent(s);v.dispatchEvent(P)},g=v=>{t=Object.assign({},i),i=O(v.visualViewport)},O=v=>({width:Math.round(v.width),height:Math.round(v.height),offsetTop:v.offsetTop,offsetLeft:v.offsetLeft,pageTop:v.pageTop,pageLeft:v.pageLeft,scale:v.scale})},3037:(D,_,e)=>{e.d(_,{K:()=>n,a:()=>l});var c=e(6319),r=(()=>{return(s=r||(r={})).Unimplemented="UNIMPLEMENTED",s.Unavailable="UNAVAILABLE",r;var s})(),l=(()=>{return(s=l||(l={})).Body="body",s.Ionic="ionic",s.Native="native",s.None="none",l;var s})();const n={getEngine(){const s=(0,c.g)();if(null!=s&&s.isPluginAvailable("Keyboard"))return s.Plugins.Keyboard},getResizeMode(){const s=this.getEngine();return null!=s&&s.getResizeMode?s.getResizeMode().catch(a=>{if(a.code!==r.Unimplemented)throw a}):Promise.resolve(void 0)}}},2930:(D,_,e)=>{e.d(_,{c:()=>a});var c=e(5861),r=e(2972),l=e(3037);const n=t=>{if(void 0===r.d||t===l.a.None||void 0===t)return null;const i=r.d.querySelector("ion-app");return null!=i?i:r.d.body},s=t=>{const i=n(t);return null===i?0:i.clientHeight},a=function(){var t=(0,c.Z)(function*(i){let f,h,d,u;const p=function(){var m=(0,c.Z)(function*(){const o=yield l.K.getResizeMode(),g=void 0===o?void 0:o.mode;f=()=>{void 0===u&&(u=s(g)),d=!0,C(d,g)},h=()=>{d=!1,C(d,g)},null==r.w||r.w.addEventListener("keyboardWillShow",f),null==r.w||r.w.addEventListener("keyboardWillHide",h)});return function(){return m.apply(this,arguments)}}(),C=(m,o)=>{i&&i(m,y(o))},y=m=>{if(0===u||u===s(m))return;const o=n(m);return null!==o?new Promise(g=>{const v=new ResizeObserver(()=>{o.clientHeight===u&&(v.disconnect(),g())});v.observe(o)}):void 0};return yield p(),{init:p,destroy:()=>{null==r.w||r.w.removeEventListener("keyboardWillShow",f),null==r.w||r.w.removeEventListener("keyboardWillHide",h),f=h=void 0},isKeyboardVisible:()=>d}});return function(f){return t.apply(this,arguments)}}()},7389:(D,_,e)=>{e.d(_,{c:()=>r});var c=e(5861);const r=()=>{let l;return{lock:function(){var s=(0,c.Z)(function*(){const a=l;let t;return l=new Promise(i=>t=i),void 0!==a&&(yield a),t});return function(){return s.apply(this,arguments)}}()}}},2448:(D,_,e)=>{e.d(_,{c:()=>l});var c=e(2972),r=e(2961);const l=(n,s,a)=>{let t;const i=()=>!(void 0===s()||void 0!==n.label||null===a()),h=()=>{const u=s();if(void 0===u)return;if(!i())return void u.style.removeProperty("width");const p=a().scrollWidth;if(0===p&&null===u.offsetParent&&void 0!==c.w&&"IntersectionObserver"in c.w){if(void 0!==t)return;const C=t=new IntersectionObserver(y=>{1===y[0].intersectionRatio&&(h(),C.disconnect(),t=void 0)},{threshold:.01,root:n});C.observe(u)}else u.style.setProperty("width",.75*p+"px")};return{calculateNotchWidth:()=>{i()&&(0,r.r)(()=>{h()})},destroy:()=>{t&&(t.disconnect(),t=void 0)}}}},2677:(D,_,e)=>{e.d(_,{S:()=>r});const r={bubbles:{dur:1e3,circles:9,fn:(l,n,s)=>{const a=l*n/s-l+"ms",t=2*Math.PI*n/s;return{r:5,style:{top:32*Math.sin(t)+"%",left:32*Math.cos(t)+"%","animation-delay":a}}}},circles:{dur:1e3,circles:8,fn:(l,n,s)=>{const a=n/s,t=l*a-l+"ms",i=2*Math.PI*a;return{r:5,style:{top:32*Math.sin(i)+"%",left:32*Math.cos(i)+"%","animation-delay":t}}}},circular:{dur:1400,elmDuration:!0,circles:1,fn:()=>({r:20,cx:48,cy:48,fill:"none",viewBox:"24 24 48 48",transform:"translate(0,0)",style:{}})},crescent:{dur:750,circles:1,fn:()=>({r:26,style:{}})},dots:{dur:750,circles:3,fn:(l,n)=>({r:6,style:{left:32-32*n+"%","animation-delay":-110*n+"ms"}})},lines:{dur:1e3,lines:8,fn:(l,n,s)=>({y1:14,y2:26,style:{transform:`rotate(${360/s*n+(n<s/2?180:-180)}deg)`,"animation-delay":l*n/s-l+"ms"}})},"lines-small":{dur:1e3,lines:8,fn:(l,n,s)=>({y1:12,y2:20,style:{transform:`rotate(${360/s*n+(n<s/2?180:-180)}deg)`,"animation-delay":l*n/s-l+"ms"}})},"lines-sharp":{dur:1e3,lines:12,fn:(l,n,s)=>({y1:17,y2:29,style:{transform:`rotate(${30*n+(n<6?180:-180)}deg)`,"animation-delay":l*n/s-l+"ms"}})},"lines-sharp-small":{dur:1e3,lines:12,fn:(l,n,s)=>({y1:12,y2:20,style:{transform:`rotate(${30*n+(n<6?180:-180)}deg)`,"animation-delay":l*n/s-l+"ms"}})}}},2784:(D,_,e)=>{e.r(_),e.d(_,{createSwipeBackGesture:()=>s});var c=e(2961),r=e(6879),l=e(5067);e(2889);const s=(a,t,i,f,h)=>{const d=a.ownerDocument.defaultView;let u=(0,r.i)(a);const C=o=>u?-o.deltaX:o.deltaX;return(0,l.createGesture)({el:a,gestureName:"goback-swipe",gesturePriority:101,threshold:10,canStart:o=>(u=(0,r.i)(a),(o=>{const{startX:O}=o;return u?O>=d.innerWidth-50:O<=50})(o)&&t()),onStart:i,onMove:o=>{const O=C(o)/d.innerWidth;f(O)},onEnd:o=>{const g=C(o),O=d.innerWidth,v=g/O,P=(o=>u?-o.velocityX:o.velocityX)(o),A=P>=0&&(P>.2||g>O/2),L=(A?1-v:v)*O;let T=0;if(L>5){const S=L/Math.abs(P);T=Math.min(S,540)}h(A,v<=0?.01:(0,c.l)(0,v,.9999),T)}})}},2754:(D,_,e)=>{e.d(_,{w:()=>c});const c=(n,s,a)=>{if(typeof MutationObserver>"u")return;const t=new MutationObserver(i=>{a(r(i,s))});return t.observe(n,{childList:!0,subtree:!0}),t},r=(n,s)=>{let a;return n.forEach(t=>{for(let i=0;i<t.addedNodes.length;i++)a=l(t.addedNodes[i],s)||a}),a},l=(n,s)=>{if(1!==n.nodeType)return;const a=n;return(a.tagName===s.toUpperCase()?[a]:Array.from(a.querySelectorAll(s))).find(i=>i.value===a.value)}},9160:(D,_,e)=>{e.d(_,{L:()=>y});var c=e(5861),r=e(9856),l=e(5079),n=e(1651),s=e(162),a=e(1135),t=e(9300),i=e(5698),f=e(7414),h=e(262),d=e(2340),u=e(1571),p=e(2644),C=e(3369);let y=(()=>{var M;class E{constructor(o,g){this.ChatPersonaService=o,this.authService=g,this.connectionStatus=new a.X(r.A.Disconnected),this.chatSubject=new a.X([]),this.chat$=this.chatSubject.asObservable(),this.endpoint="hub/chat",this.iniciarConexion()}iniciarConexion(){var o=this;return(0,c.Z)(function*(){const g=yield o.authService.obtenerToken();if(!g)return;const O=`${d.N.urlBackend}${o.endpoint}`,v={accessTokenFactory:()=>g,transport:l.n.LongPolling};o.connection=(new n.s).configureLogging(s.i.Debug).withUrl(O,v).build(),o.connection.on("NuevoChat",(P,w)=>o.onNuevoChat(P,w)),o.connection.on("NuevaConexion",P=>o.onNuevaConexion(P)),o.connection.on("CargarChats",P=>o.onCargarChats(P)),o.connectionStatus.next(r.A.Connecting),yield o.connection.start()})()}detenerConexion(){var o=this;return(0,c.Z)(function*(){o.connectionStatus.next(r.A.Disconnecting),yield o.connection.stop(),o.chatSubject.next([]),o.connectionStatus.next(r.A.Disconnected)})()}obtenerNotificaciones(){return this.chatSubject.value}onNuevoChat(o,g){var O=this;return(0,c.Z)(function*(){o.fecha=new Date,O.chatSubject.value.push(o)})()}onNuevaConexion(o){for(const g of o)g.fecha=new Date(g.fecha);this.chatSubject.next(o)}onCargarChats(o){this.chatSubject.next(o)}ensureConnection(){var o=this;return(0,c.Z)(function*(){if(o.connection.state!==r.A.Connected){if(o.connection.state===r.A.Disconnected||o.connection.state===r.A.Disconnecting)throw new Error("No se ha iniciado la conexion con el Hub de Notificaciones");(o.connection.state===r.A.Connecting||o.connection.state===r.A.Reconnecting)&&o.connectionStatus.asObservable().pipe((0,t.h)(O=>O===r.A.Connected),(0,i.q)(1),(0,f.V)(1e4),(0,h.K)(()=>{throw new Error("No se pudo establecer la conexi\xf3n con el Hub de Notificaciones")}))}})()}agregarChat(o,g){var O=this;return(0,c.Z)(function*(){yield O.ensureConnection(),yield O.connection.invoke("NuevoChat",o,g)})()}abandonarChat(o){let g=this.chatSubject.value;g=g.filter(O=>O.idChat!=o),this.chatSubject.next(g)}}return(M=E).\u0275fac=function(o){return new(o||M)(u.LFG(p.R),u.LFG(C.e))},M.\u0275prov=u.Yz7({token:M,factory:M.\u0275fac,providedIn:"root"}),E})()},4273:(D,_,e)=>{e.d(_,{U:()=>y});var c=e(5861),r=e(9856),l=e(5079),n=e(1651),s=e(162),a=e(1135),t=e(9300),i=e(5698),f=e(7414),h=e(262),d=e(2340),u=e(1571),p=e(3369),C=e(9160);let y=(()=>{var M;class E{constructor(o,g){this.authService=o,this.chatHub=g,this.connectionStatus=new a.X(r.A.Disconnected),this.chatMensajeSubject=new a.X([]),this.chatMensaje$=this.chatMensajeSubject.asObservable(),this.endpoint="hub/chatMensaje",this.iniciarConexion()}iniciarConexion(){var o=this;return(0,c.Z)(function*(){const g=yield o.authService.obtenerToken();if(!g)return;const O=`${d.N.urlBackend}${o.endpoint}`,v={accessTokenFactory:()=>g,transport:l.n.LongPolling};o.connection=(new n.s).configureLogging(s.i.Debug).withUrl(O,v).build(),o.connection.on("NuevoMensaje",P=>o.onNuevoChatMensaje(P)),o.connection.on("NuevaConexion",P=>o.onNuevaConexion(P)),o.connection.on("AbandonarChat",P=>o.onAbandonarChat(P)),o.connectionStatus.next(r.A.Connecting),yield o.connection.start()})()}detenerConexion(){var o=this;return(0,c.Z)(function*(){o.connectionStatus.next(r.A.Disconnecting),yield o.connection.stop(),o.chatMensajeSubject.next([]),o.connectionStatus.next(r.A.Disconnected)})()}obtenerMensajes(){return this.chatMensajeSubject.value}onNuevoChatMensaje(o){const g=this.chatMensajeSubject.value;let O=g.find(v=>v.find(P=>P.idChat==o.idChat));if(O)O.push(o);else{let v=[];v.push(o),g.push(v)}this.chatMensajeSubject.next(g)}onNuevaConexion(o){this.chatMensajeSubject.next(o)}onAbandonarChat(o){let g=this.chatMensajeSubject.value;g.forEach((O,v)=>{O.some(P=>P.idChat==o)&&g.splice(v,1)}),this.chatMensajeSubject.next(g),this.chatHub.abandonarChat(o)}ensureConnection(){var o=this;return(0,c.Z)(function*(){if(o.connection.state!==r.A.Connected){if(o.connection.state===r.A.Disconnected||o.connection.state===r.A.Disconnecting)throw new Error("No se ha iniciado la conexion con el Hub de Notificaciones");(o.connection.state===r.A.Connecting||o.connection.state===r.A.Reconnecting)&&o.connectionStatus.asObservable().pipe((0,t.h)(O=>O===r.A.Connected),(0,i.q)(1),(0,f.V)(1e4),(0,h.K)(()=>{throw new Error("No se pudo establecer la conexi\xf3n con el Hub de Notificaciones")}))}})()}enviarMensaje(o){var g=this;return(0,c.Z)(function*(){yield g.ensureConnection(),yield g.connection.invoke("NuevoMensaje",o)})()}abandonarChat(o){var g=this;return(0,c.Z)(function*(){yield g.ensureConnection(),yield g.connection.invoke("AbandonarChat",o)})()}}return(M=E).\u0275fac=function(o){return new(o||M)(u.LFG(p.e),u.LFG(C.L))},M.\u0275prov=u.Yz7({token:M,factory:M.\u0275fac,providedIn:"root"}),E})()},2644:(D,_,e)=>{e.d(_,{R:()=>l});var c=e(1571),r=e(529);let l=(()=>{var n;class s{constructor(t){this.http=t,this.dataUrl="ChatPersona/"}agregarPersonas(t){return this.http.post(this.dataUrl,t)}obtenerIdUsuario(){return this.http.get(`${this.dataUrl}IdUsuario`)}obtenerIdPacientesPadecimiento(t){return this.http.get(`${this.dataUrl}Padecimiento/${t}`)}}return(n=s).\u0275fac=function(t){return new(t||n)(c.LFG(r.eN))},n.\u0275prov=c.Yz7({token:n,factory:n.\u0275fac,providedIn:"root"}),s})()},281:(D,_,e)=>{e.d(_,{$:()=>l});var c=e(1571),r=e(529);let l=(()=>{var n;class s{constructor(t){this.http=t,this.dataUrl="expedienteTratamiento/"}consultarTratamientos(){return this.http.get(this.dataUrl+"consultarTratamientos")}agregar(t){return this.http.post(this.dataUrl+"agregar",t)}selectorDeDoctor(){return this.http.get(this.dataUrl+"selectorDeDoctor")}selectorPadecimeintos(){return this.http.get(this.dataUrl+"selectorDePadecimiento")}}return(n=s).\u0275fac=function(t){return new(t||n)(c.LFG(r.eN))},n.\u0275prov=c.Yz7({token:n,factory:n.\u0275fac,providedIn:"root"}),s})()},226:(D,_,e)=>{e.d(_,{q:()=>l});var c=e(1571),r=e(529);let l=(()=>{var n;class s{constructor(t){this.http=t,this.url="expedienteDoctor/"}consultarExpediente(){return this.http.get(this.url)}consultarExpedienteConImagenes(){return this.http.get(this.url+"conImagenes")}consultarSelector(){return this.http.get(this.url+"selector")}eliminar(t){return this.http.delete(this.url,{body:t})}agregar(t){return this.http.post(this.url,t)}}return(n=s).\u0275fac=function(t){return new(t||n)(c.LFG(r.eN))},n.\u0275prov=c.Yz7({token:n,factory:n.\u0275fac,providedIn:"root"}),s})()},6558:(D,_,e)=>{e.d(_,{i:()=>l});var c=e(1571),r=e(529);let l=(()=>{var n;class s{constructor(t){this.http=t,this.url="usuario/"}agregar(t){return this.http.post(this.url+"agregar",t)}agregarTrackr(t){return this.http.post(this.url+"agregarTrackr",t)}consultarInformacionGeneral(){return this.http.get(this.url+"consultarInformacionGeneral")}actualizarInformacionGeneral(t){return this.http.put(this.url+"actualizarInformacionGeneral",t)}}return(n=s).\u0275fac=function(t){return new(t||n)(c.LFG(r.eN))},n.\u0275prov=c.Yz7({token:n,factory:n.\u0275fac,providedIn:"root"}),s})()},4328:(D,_,e)=>{e.d(_,{g:()=>l});var c=e(1571),r=e(529);let l=(()=>{var n;class s{constructor(t){this.http=t,this.dataUrl="archivo/"}obtenerUsuarioImagen(t){return this.http.get(this.dataUrl+`usuario/${t}`,{responseType:"blob"})}obtenerUsuarioEnSesionImagen(){return this.http.get(this.dataUrl+"usuarioEnSesionImagen",{responseType:"blob"})}}return(n=s).\u0275fac=function(t){return new(t||n)(c.LFG(r.eN))},n.\u0275prov=c.Yz7({token:n,factory:n.\u0275fac,providedIn:"root"}),s})()},5697:(D,_,e)=>{e.d(_,{a:()=>l});var c=e(1571),r=e(529);let l=(()=>{var n;class s{constructor(t){this.http=t,this.dataUrl="expedienteEstudio/"}consultarParaGrid(){return this.http.get(this.dataUrl+"grid")}agregarExpediente(t){return this.http.post(this.dataUrl,t)}consultar(t){return this.http.get(this.dataUrl+`consultar/${t}`)}eliminar(t){return this.http.delete(this.dataUrl+`${t}`)}}return(n=s).\u0275fac=function(t){return new(t||n)(c.LFG(r.eN))},n.\u0275prov=c.Yz7({token:n,factory:n.\u0275fac,providedIn:"root"}),s})()},4602:(D,_,e)=>{e.d(_,{H:()=>a});var c=e(5861),r=e(7423),l=e(2671);const n=(0,r.fo)("ScreenOrientation",{web:()=>e.e(8501).then(e.bind(e,8501)).then(t=>new t.ScreenOrientationWeb)});var s=e(1571);let a=(()=>{var t;class i{constructor(){}lockLandscape(){return(0,c.Z)(function*(){try{yield n.lock({type:l.F.LANDSCAPE})}catch(h){console.error("Error Screen orientation LANDSCAPE:",h)}})()}lockPortrait(){return(0,c.Z)(function*(){try{yield n.lock({type:l.F.PORTRAIT})}catch(h){console.error("Error Screen orientation PORTRAIT:",h)}})()}unlock(){return(0,c.Z)(function*(){try{yield n.unlock()}catch(h){console.error("Error al bloquear la orientaci\xf3n:",h)}})()}getCurrentOrientation(){return(0,c.Z)(function*(){try{return(yield n.getCurrentOrientation()).type}catch(h){return console.error("Error al obtener la orientaci\xf3n actual:",h),null}})()}}return(t=i).\u0275fac=function(h){return new(h||t)},t.\u0275prov=s.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),i})()},458:(D,_,e)=>{e.d(_,{Z:()=>s});var c=e(5861),r=e(6388);const n=(0,e(7423).fo)("FilePicker",{web:()=>e.e(3405).then(e.bind(e,3405)).then(a=>new a.FilePickerWeb)});class s{constructor(){var t=this;this.takePicture=(0,c.Z)(function*(){try{const i=yield r.V1.getPhoto({quality:90,allowEditing:!0,resultType:r.dk.DataUrl});if(t.imageSrc=i.dataUrl,t.imageSrc)return t.imageSrc;throw new Error("Error")}catch(i){throw console.error("Error al tomar la foto:",i),i}}),this.pickFiles=(0,c.Z)(function*(){const{files:i}=yield n.pickFiles({types:["image/png","image/jpeg","application/pdf","image/gif"],readData:!0});return i})}}}}]);