// Funciones para la actualización de la celda de Importe y que se restringa el uso de
// carácteres no númericos

function getCharCodeFromEvent(event:any) {
    event = event || window.event;
    return (typeof event.which === 'undefined') ? event.keyCode : event.which;
    }
    
    function isCharNumeric(charStr:any) {
    return !!/\d|\./.test(charStr);
    }
    
    function isKeyPressedNumeric(event:any) {
    var charCode = getCharCodeFromEvent(event);
    var charStr = String.fromCharCode(charCode);
    return isCharNumeric(charStr);
    }
    
    export function NumericCellEditor() {
    }
    
    NumericCellEditor.prototype.init = function (params:any) {
    this.eInput = document.createElement('input');
    this.eInput.style.cssText = 'border: none; text-align: center; width: 100%; background-color: none;';
    
    if (isCharNumeric(params.charPress)) {
      this.eInput.value = params.charPress;
    } else {
      if (params.value !== undefined && params.value !== null) {
        this.eInput.value = params.value;
      }
    }
    
    var that = this;
    this.eInput.addEventListener('keypress', (event:any) => {
      if (!isKeyPressedNumeric(event)) {
        that.eInput.focus();
        if (event.preventDefault) {
          event.preventDefault();
        }
      } else if (that.isKeyPressedNavigation(event)) {
        event.stopPropagation();
      }
    });
    
    var charPressIsNotANumber = params.charPress && ('1234567890.'.indexOf(params.charPress) < 0);
    this.cancelBeforeStart = charPressIsNotANumber;
    };
    
    NumericCellEditor.prototype.isKeyPressedNavigation = function (event:any) {
    return event.keyCode === 39
      || event.keyCode === 37;
    };
    
    
    NumericCellEditor.prototype.getGui = function () {
    return this.eInput;
    };
    
    NumericCellEditor.prototype.afterGuiAttached = function () {
    this.eInput.focus();
    };
    
    NumericCellEditor.prototype.isCancelBeforeStart = function () {
    return this.cancelBeforeStart;
    };
    
    NumericCellEditor.prototype.isCancelAfterEnd = function () {
    var value = this.getValue();
    return value.indexOf('007') >= 0;
    };
    
    NumericCellEditor.prototype.getValue = function () {
    return this.eInput.value;
    };
    
    NumericCellEditor.prototype.destroy = function () {
    };