$.validator.unobtrusive.adapters.add('comparedate', ['propertytested', 'allowequaldates', 'isdatebefore'], function (options) {
    options.rules['comparedate'] = options.params;
    options.messages['comparedate'] = options.message;
});

$.validator.unobtrusive.adapters.add("comparedatemultiple", ["propertytested", "allowequaldates", "isdatebefore"], function (options) {
    options.rules['comparedatemultiple'] = {
        propertytested: options.params.propertytested,
        allowequaldates: options.params.allowequaldates,
        isdatebefore: options.params.isdatebefore,
        errorMsgs: options.message
    };
    options.messages['comparedatemultiple'] = "";
});

$.validator.addMethod("comparedate", compareDate);
$.validator.addMethod("comparedatemultiple", compareDateMultiple);

function compareDate(value, element, params) {
    var parts = element.name.split(".");
    var prefix = "";
    if (parts.length > 1) prefix = parts[0] + ".";
    var startdatevalue = $('input[name="' + prefix + params.propertytested + '"]').val();

    if (!value || !startdatevalue) return true;
    
    if (startdatevalue.indexOf(' ',0) > 0) {
    
        startdatevalue = startdatevalue.substring(0,startdatevalue.indexOf(' ',0));
    } 
    if (value.indexOf(' ',0) > 0) {
    
        value = value.substring(0,value.indexOf(' ',0));
    }
    //Convert to uk dates
    var sdateukarray = startdatevalue.split('/');
    if (sdateukarray.length != 3) return true;
    var sdatevalueukdate = new Date(sdateukarray[2], parseInt(sdateukarray[1], 10) - 1, sdateukarray[0]);

    var dateukarrayukdate = value.split('/');
    if (dateukarrayukdate.length != 3) return true;
    
    var valuedateuk = new Date(dateukarrayukdate[2], parseInt(dateukarrayukdate[1], 10) - 1, dateukarrayukdate[0]);
    
    //THE TEST
    if (params.isdatebefore == "True") {
        return (params.allowequaldates == "True") ? sdatevalueukdate >= valuedateuk : sdatevalueukdate > valuedateuk;
    }
    else {
        return (params.allowequaldates == "True") ? sdatevalueukdate <= valuedateuk : sdatevalueukdate < valuedateuk;
    }
};

function compareDateMultiple(value, element, params) {
    var propertytested = params.propertytested.split('!');
    var allowequaldates = params.allowequaldates.split('!');
    var isdatebefore = params.isdatebefore.split('!');
    var msgs = params.errorMsgs.split('!');
    var errMsg = "";

    var retVal = true;
    $.each(propertytested, function (index, val) {
        var myParams = { "propertytested": val, "allowequaldates": allowequaldates[index], "isdatebefore": isdatebefore[index]

        };
        retVal = compareDate(value, element, myParams);
        if (retVal == false) {
            errMsg = msgs[index];
            return false;
        }
    });
    if (retVal == false) {
        var evalStr = "this.settings.messages[\"" + $(element).attr("name") +
                     "\"].comparedatemultiple='" + errMsg + "'";

        eval(evalStr);
    }
    return retVal;
};
