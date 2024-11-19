/**
 * 
 */
var arrayNumbers = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];
var arrayLetters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'i', 'z', ' '];
/*Functions for validations inputs field*/

function fieldValid(field) {
    $(".alert").alert('close');
    if (requiredValid(field) == false) return false;
    if (typeDecimalValid(field) == false) return false;	
    if (typeNumberValid(field) == false) return false;	   
    if (typeEmailValid(field) == false) return false;
    if (typeCurpValid(field) == false) return false;
    if (typeNameValid(field) == false) return false;
    if (typeGenerationValid(field) == false) return false;
	return true;
}

function requiredValid(field) {
	var value = $.trim($(field).val());
	$(field).val(value);
	var required = $(field).data('required'); 
    if (required && required == true && value.length == 0) {
        ShowNotification('warning', $(field).data('message'), 'Dato requerido.', true, 'toast-top-right', field);
		return false;
	}
	return true;
}

function typeNumberValid(field) {
    var value = $.trim($(field).val());
    var type = $(field).data('type');
    if (type && type == 'number') {
        if (isOnlyNumbers(value) == false || value == '0') {
            ShowNotification('warning', $(field).data('message'), 'Formato incorrecto.', true, 'toast-top-right', field);
            return false;
        }
    }
    return true;
}

function typeDecimalValid(field) {
    var value = $.trim($(field).val());    
    var type = $(field).data('type');
    if (type && type == 'decimal') {
        value = trimDecimalValue(value);
        $(field).val(value);
        if (isDecimalNumbers(value) == false) {
            ShowNotification('warning', $(field).data('message'), 'Formato incorrecto.', true, 'toast-top-right', field);
            return false;
        }
        if (parseFloat(value) == 0) {
            ShowNotification('warning', $(field).data('message'), 'Formato incorrecto.', true, 'toast-top-right', field);
            $(field).val('0');
            return false;
        }
    }    
    return true;
}

function trimDecimalValue(value) {
    if (value) {
        if (value.charAt(0) == '$') value = value.substr(1, value.length);
        for (var index = 0; index < value.length; index++) {
            var c = value.charAt(index);
            if (c != '0') {
                if (c != ',') {
                    return value.substr(index, value.length);
                }
            }
        }
    }    
    return value;
}

function typeEmailValid(field) {
    var value = $.trim($(field).val());
    var type = $(field).data('type');
    if (type && type == 'email') {
        if (value.length > 0 && isEmailValid(value) == false) {
            ShowNotification('warning', $(field).data('message'), 'Formato incorrecto.', true, 'toast-top-right', field);
            return false;
        }
    }

    return true;
}

function typeCurpValid(field) {
    var value = $.trim($(field).val());
    var type = $(field).data('type');
    if (type && type == 'curp') {
        if (value.length > 0 && isCurpValid(value) == false) {
            ShowNotification('warning', $(field).data('message'), 'Formato incorrecto.', true, 'toast-top-right', field);
            return false;
        }
    }

    return true;
}

function typeNameValid(field) {
    var value = $.trim($(field).val());
    var type = $(field).data('type');
    if (type && type == 'personName') {
        if (value.length > 0 && isPersonNameValid(value) == false) {
            ShowNotification('warning', $(field).data('message'), 'Acento no permitido.', true, 'toast-top-right', field);
            return false;
        }
    }

    return true;
}

function typeGenerationValid(field) {
    var value = $.trim($(field).val());
    var type = $(field).data('type');
    if (type && type == 'generation') {
        if (value.length > 0 && isGenerationValid(value) == false) {
            ShowNotification('warning', $(field).data('message'), 'Formato incorrecto.', true, 'toast-top-right', field);
            return false;
        }
    }

    return true;
}

function isOnlyNumbers(value) {
	for(var index= 0; index < value.length; index++) {
		if(!arrayNumbers.includes(value.charAt(index))) {
			return false;
		}
	}
	return true;
}

function isOnlyLetters(value) {
	for(var index= 0; index < value.length; index++) {
		if(!arrayLetters.includes(value.charAt(index))) {
			return false;
		}
	}
	return true;
}

function isOnlyNumbersOrLetters(value) {
	for(var index= 0; index < value.length; index++) {
		if(!arrayNumbers.includes(value.charAt(index)) && !arrayLetters.includes(value.charAt(index))) {
			return false;
		}
	}
	return true;
}

function isDecimalNumbers(value) {
    var re = /(\d{1,})|((\d+(\,|\.))+\d{2,})$/;
    return re.test(value);
}

function isEmailValid(value) {
    var re = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
	return re.test(value);
}

function isCurpValid(value) {
    //var re = /^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$/i;
    var re = /^[A-Z]{4}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$/i;

    return re.test(value);
}

function isPersonNameValid(value) {
    var re = /^[a-zA-Z\u00f1\u00d1]+(\s*[a-zA-Z\u00f1\u00d1]*)*[a-zA-Z\u00f1\u00d1]+$/g;
    return re.test(value);
}

function isGenerationValid(value) {
    var re = /^[0-9]{4}(-[0-9]{4})?$/g;
    return re.test(value);
}
/*--------------------------------------------*/

/*Functions for events input onkeypress, onkeyup,...*/

function isNumber(event) {
	var key =  event.which || event.keyCode;
	return (key >= 48 && key <= 57);
}

function isLetter(event) {
	var key =  event.which || event.keyCode;
	return ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || isSpacerOrEnter(key));
}

function isAlphaNumeric(event) {
	var key =  event.which || event.keyCode;
	return (isNumber(event) || isLetter(event));
}

function isSpacerOrEnter(key) {
	 return (key==32 || key==13);
}

function isValidLength(text, maxLenght) {
	return text.length <= maxLenght;
}

/*--------------------------------------------*/

/*Functions pluging form validations and reset*/
$.fn.formvalidate = function () {
    var fieldsValids = true;
	var inputComponents = $(this).find('[data-toggle="validation"]');	
	$.each(inputComponents, function(index, field) {		
        if (fieldValid(field) == false) {
            fieldsValids = false;
            return false;
		}
	});
    return fieldsValids;
}