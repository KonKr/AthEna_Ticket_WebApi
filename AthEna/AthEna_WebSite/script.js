//Declare config variables...
var apiHostUrl = "http://athenacard-api.ddns.net";

function getContactsWithCardsAPI(){
    //Initiate call...
    $('#getContactsWithCards_TBody').empty();
    $('#getContactsWithCards_TBody').append('<tr><td colspan="20"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/ContactWithCard",
        "method": "GET",
        "headers": {
          "Content-Type": "application/json",
          "Cache-Control": "no-cache",
        }
      }
      
      $.ajax(settings).done(function (response) {
        //after ajax is complete, deal with the result data...
        $('#getContactsWithCards_TBody').empty();
        response.forEach(element => {
            var registeredDateToPrint = dateFormat(new Date(element.card_RegisteredOn), "dd/mm/yy, h:MM:ss TT" );
            var expirationDateToPrint = dateFormat(new Date(element.card_ChargeExpiresOn), "dd/mm/yy, h:MM:ss TT" );
            $('#getContactsWithCards_TBody').append('<tr><td>' + element.contact_FirstName + '</td><td>' + element.contact_LastName + '</td><td>' + element.contact_IdCardNum + '</td><td>' + element.card_Guid + '</td><td>' + registeredDateToPrint + '</td><td>' + expirationDateToPrint + '</td></tr>');
        });
      })
      .fail(function(jqXHR, status){
        if (jqXHR.status == 400) {
          alert("Bad Request. Please review your data.");
        }else if(jqXHR.status == 401){
            alert("Unauthorized request. Please review your credentials.");
        }else{
          alert("An error occured.");
        }    
      });
}

function createNewContactWithCard(FirstName, LastName, IdCardNum, SocialSecurityNum, event){
  //To prevent reloading in submitting form...
  event.preventDefault();

  //Initiate call...
  var settings = {
    "async": true,
    "crossDomain": true,
    "url": apiHostUrl + "/api/ContactWithCard",
    "method": "POST",
    "headers": {
      "Content-Type": "application/json",
      "Cache-Control": "no-cache",
    },
    "processData": false,
    "data": "{\n\t\"FirstName\":\"" + FirstName + "\",\n\t\"LastName\":\"" + LastName + "\",\n\t\"IdCardNum\":\"" + IdCardNum +"\",\n\t\"SocialSecurityNum\":" + SocialSecurityNum + "\n}"
  }
  
  //If create new attempt is sucessfull...
  $.ajax(settings)
  .done(function (response) {
    alert("New Contact was created successfully and its associated Card. ContactId is \"" + response.newContactId + "\" and associated Card is \"" + response.newCardId +"\"");
  })
  .fail(function(jqXHR, status){
    if (jqXHR.status == 400) {
      alert("Bad Request. Please review your data.");
    }else if(jqXHR.status == 401){
        alert("Unauthorized request. Please review your credentials.");
    }else{
      alert("An error occured.");
    }    
  });
  
}

function rechargeCard(CardId, ChargeEuros, event){
  event.preventDefault();

  var settings = {
    "async": true,
    "crossDomain": true,
    "url": apiHostUrl + "/api/Card/Recharge",
    "method": "POST",
    "headers": {
      "Content-Type": "application/json",
      "Cache-Control": "no-cache",
    },
    "processData": false,
    "data": "{\n\t\"CardId\":\"" + CardId + "\",\n\t\"ChargeEuros\":" + ChargeEuros + "\n}"
  }
  
  $.ajax(settings)
  .done(function (response) {
    alert("Card was successfully recharged. New exparation date was set as " + response.chargeExpirationDate);
  })
  .fail(function(jqXHR, status){
    if (jqXHR.status == 400) {
      alert("Bad Request. Please review your data.");
    }else if(jqXHR.status == 401){
        alert("Unauthorized request. Please review your credentials.");
    }else{
      alert("An error occured.");
    }    
  }); 

}

function validateCardOnMetro(CardId, ValidatingAtStationId, ValidationOnEmbarkation, event) {
    event.preventDefault();

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/Card/Validate/Metro",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Cache-Control": "no-cache",
        },
        "processData": false,
        "data": "{\n    \"ValidatingCardId\": \"" + CardId + "\",\n    \"ValidatingAtStationId\":\"" + ValidatingAtStationId + "\",\n    \"ValidationOnEmbarkation\":" + ValidationOnEmbarkation +"\n}"
    }

    $.ajax(settings)
        .done(function (response) {
            alert (response);
        })
        .fail(function (jqXHR, status) {
            if (jqXHR.status == 400) {
                alert("Bad Request. Please review your data.");
            }else if(jqXHR.status == 401){
                alert("Unauthorized request. Please review your credentials.");
            } else {
                alert("An error occured.");
            }
        });

}

function validateCardOnBus(RouteId, ValidatingCardId, ValidatingAtBusId, event) {
    event.preventDefault();

    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/Card/Validate/Bus",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Cache-Control": "no-cache",
        },
        "processData": false,
        "data": "{\n\t\"RouteId\":\"" + RouteId + "\",\n\t\"ValidatingCardId\":\"" + ValidatingCardId + "\",\n\t\"ValidatingAtBusId\":\"" + ValidatingAtBusId +"\"\n}"
    }

    $.ajax(settings)
        .done(function (response) {
            alert(response);
        })
        .fail(function (jqXHR, status) {
            if (jqXHR.status == 400) {
                alert("Bad Request. Please review your data.");
            }else if(jqXHR.status == 401){
                alert("Unauthorized request. Please review your credentials.");
            } else {
                alert("An error occured.");
            }
        });

}

function getVehicles() {
    $('#getVehicles_TBody').empty();
    $('#getVehicles_TBody').append('<tr><td colspan="20"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/Vehicles",
        "method": "GET",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Basic a2Fwb2lvczpxd2VydHkxMjM0NTYhQCMkJV4=",
            "Cache-Control": "no-cache",
        }
    }

    $.ajax(settings).done(function (response) {
        //after ajax is complete, deal with the result data...
        $('#getVehicles_TBody').empty();
        response.forEach(element => {
            $('#getVehicles_TBody').append('<tr><td>' + element.licensePlate + '</td><td>' + element.vehicleId + '</td></tr>');
        });
    })
    .fail(function(jqXHR, status){
        if (jqXHR.status == 400) {
          alert("Bad Request. Please review your data.");
        }else if(jqXHR.status == 401){
            alert("Unauthorized request. Please review your credentials.");
        }else{
          alert("An error occured.");
        }    
      });
}

function createNewVehicle(Username, Password, LicensePlate, event) {    
    //To prevent reloading in submitting form...
    event.preventDefault();

    var usp = [Username, Password];
    var auth = usp.join(":");
    var encodedString = btoa(auth);

    //Initiate call...
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/Vehicles",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Basic " + encodedString + "",
            "Cache-Control": "no-cache",
        },
        "processData": false,
        "data": "{\n\t\"LicensePlate\":\"" + LicensePlate +"\"\n}"
    }

    //If create new attempt is sucessfull...
    $.ajax(settings)
        .done(function (response) {
            alert("New Vehicle was created successfully. VehicleId is \"" + response + "\"");
        })
        .fail(function (jqXHR, status) {
            if (jqXHR.status == 400) {
                alert("Bad Request. Please review your data.");
            }else if(jqXHR.status == 401){
                alert("Unauthorized request. Please review your credentials.");
            } else {
                alert("An error occured.");
            }
        });

}

function getRoutes() {
    $('#getRoutes_TBody').empty();
    $('#getRoutes_TBody').append('<tr><td colspan="20"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/Routes",
        "method": "GET",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Basic a2Fwb2lvczpxd2VydHkxMjM0NTYhQCMkJV4=",
            "Cache-Control": "no-cache",
        }
    }

    $.ajax(settings).done(function (response) {
        //after ajax is complete, deal with the result data...
        $('#getRoutes_TBody').empty();
        response.forEach(element => {
            $('#getRoutes_TBody').append('<tr><td>' + element.routeId + '</td><td>' + element.routeCodeNum + '</td><td>' + element.routeStartPoint + '</td><td>' + element.routeEndPoint + '</td></tr>');
        });
    })
    .fail(function(jqXHR, status){
        if (jqXHR.status == 400) {
          alert("Bad Request. Please review your data.");
        }else if(jqXHR.status == 401){
            alert("Unauthorized request. Please review your credentials.");
        }else{
          alert("An error occured.");
        }    
      });
}

function createNewRoute(Username, Password, RouteCodeNum, RouteEndPoint, RouteStartPoint, event) {
    //To prevent reloading in submitting form...
    event.preventDefault();

    var usp = [Username, Password];
    var auth = usp.join(":");
    var encodedString = btoa(auth);

    //Initiate call...
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/Routes",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Basic " + encodedString + "",
            "Cache-Control": "no-cache",
        },
        "processData": false,
        "data": "{\n\t\"RouteCodeNum\":\"" + RouteCodeNum + "\",\n\t\"RouteEndPoint\":\"" + RouteEndPoint + "\",\n\t\"RouteStartPoint\":\"" + RouteStartPoint +"\"\n}"
    }

    //If create new attempt is sucessfull...
    $.ajax(settings)
        .done(function (response) {
            alert("New Route was created successfully. RouteId is \"" + response + "\"");
        })
        .fail(function (jqXHR, status) {
            if (jqXHR.status == 400) {
                alert("Bad Request. Please review your data.");
            }else if(jqXHR.status == 401){
                alert("Unauthorized request. Please review your credentials.");
            } else {
                alert("An error occured.");
            }
        });

}

function getMetroStations() {
    $('#getMetroStations_TBody').empty();
    $('#getMetroStations_TBody').append('<tr><td colspan="20"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/MetroStations",
        "method": "GET",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Basic a2Fwb2lvczpxd2VydHkxMjM0NTYhQCMkJV4=",
            "Cache-Control": "no-cache",
        }
    }

    $.ajax(settings).done(function (response) {
        //after ajax is complete, deal with the result data...
        $('#getMetroStations_TBody').empty();
        response.forEach(element => {
            $('#getMetroStations_TBody').append('<tr><td>' + element.stationId + '</td><td>' + element.stationName + '</td><td>' + element.isOnLine + '</td><td>' + element.isAlsoOnLine + '</td></tr>');
        });
    })
    .fail(function(jqXHR, status){
        if (jqXHR.status == 400) {
          alert("Bad Request. Please review your data.");
        }else if(jqXHR.status == 401){
            alert("Unauthorized request. Please review your credentials.");
        }else{
          alert("An error occured.");
        }    
      });
}


function createNewMetroStation(Username, Password, StationName, IsOnLine, event) {
    //To prevent reloading in submitting form...
    event.preventDefault();

    var usp = [Username, Password];
    var auth = usp.join(":");
    var encodedString = btoa(auth);

    //Initiate call...
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": apiHostUrl + "/api/MetroStations",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "Authorization": "Basic " + encodedString + "",
            "Cache-Control": "no-cache",
        },
        "processData": false,
        "data": "{\n\t\"IsOnLine\":\"" + IsOnLine + "\",\n\t\"StationName\":\"" + StationName + "\"\n}"
    }

    //If create new attempt is sucessfull...
    $.ajax(settings)
        .done(function (response) {
            alert("New Station was created successfully. StationId is \"" + response + "\"");
        })
        .fail(function (jqXHR, status) {
            if (jqXHR.status == 400) {
                alert("Bad Request. Please review your data.");
            }else if(jqXHR.status == 401){
                alert("Unauthorized request. Please review your credentials.");
            }else {
                alert("An error occured.");
            }
        });

}



function getValidationHistory() {

    var DD = $('input[name=DDays]').val();
    if (DD == 0) {
        alert("Please insert a value of the Days Depth you would like to inspect the History and try again. ")
    } else {

        $('#getValidationHistory_TBody').empty();
        $('#getValidationHistory_TBody').append('<tr><td colspan="20"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": apiHostUrl + "/api/ValidationHistory/DaysDepth/" + DD + "/",
            "method": "GET",
            "headers": {
                "Content-Type": "application/json",
                "Authorization": "Basic a2Fwb2lvczpxd2VydHkxMjM0NTYhQCMkJV4=",
                "Cache-Control": "no-cache",
            }
        }

        $.ajax(settings).done(function (response) {
            //after ajax is complete, deal with the result data...
            $('#getValidationHistory_TBody').empty();
            response.forEach(element => {
                $('#getValidationHistory_TBody').append('<tr><td>' + element.vactivityId + '</td><td>' + element.validatedOn + '</td><td>' + element.validatedAt + '</td><td>' + element.stationId + '</td><td>' + element.busId + '</td><td>' + element.routeId + '</td><td>' + element.cardId + '</td><td>' + element.bus + '</td><td>' + element.card + '</td><td>' + element.route + '</td><td>' + element.station + '</td></tr>');
            });
        })
        .fail(function(jqXHR, status){
        if (jqXHR.status == 400) {
          alert("Bad Request. Please review your data.");
        }else if(jqXHR.status == 401){
            alert("Unauthorized request. Please review your credentials.");
        }else{
          alert("An error occured.");
        }    
      });
    }

}