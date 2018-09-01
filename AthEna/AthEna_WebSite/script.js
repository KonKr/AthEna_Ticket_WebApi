//Declare config variables...
var apiHostUrl = "http://athenaticket.ddns.net";

function getContactsWithCardsAPI(){
    //Initiate call...
    $('#getContactsWithCards_TBody').empty();
    $('#getContactsWithCards_TBody').append('<tr><td colspan="5"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');
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
            $('#getContactsWithCards_TBody').append('<tr><td>' + element.contact_FirstName + '</td><td>' + element.contact_LastName + '</td><td>' + element.contact_IdCardNum + '</td><td>' + registeredDateToPrint + '</td><td>' + expirationDateToPrint + '</td></tr>');
        });
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
    }else{
      alert("An error occured.");
    }    
  }); 

}