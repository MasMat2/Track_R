"use strict";(self.webpackChunkapp=self.webpackChunkapp||[]).push([[8501],{8501:(u,s,e)=>{e.r(s),e.d(s,{ScreenOrientationWeb:()=>a});var n=e(5861),o=e(7423),r=e(2671);class a extends o.Uw{constructor(){var t;super(),t=this,this.isSupported="orientation"in screen,this.handleOrientationChange=(0,n.Z)(function*(){const c={type:(yield t.getCurrentOrientation()).type};t.notifyListeners("screenOrientationChange",c)}),this.isSupported&&screen.orientation.addEventListener("change",this.handleOrientationChange)}lock(t){var i=this;return(0,n.Z)(function*(){i.isSupported||i.throwUnsupportedError(),yield screen.orientation.lock(t.type)})()}unlock(){var t=this;return(0,n.Z)(function*(){t.isSupported||t.throwUnsupportedError(),screen.orientation.unlock()})()}getCurrentOrientation(){var t=this;return(0,n.Z)(function*(){switch(t.isSupported||t.throwUnsupportedError(),screen.orientation.type){case"landscape-primary":return{type:r.F.LANDSCAPE_PRIMARY};case"landscape-secondary":return{type:r.F.LANDSCAPE_SECONDARY};case"portrait-secondary":return{type:r.F.PORTRAIT_SECONDARY};default:return{type:r.F.PORTRAIT_PRIMARY}}})()}throwUnsupportedError(){throw this.unavailable("Screen Orientation API not available in this browser.")}}}}]);