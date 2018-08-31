function test(){
    //Initiate call...
    $('#getContactsWithCards_TBody').append('<tr><td colspan="5"><br><center><h1><i class="fas fa-spinner fa-spin"></i></h1></td></tr>');
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "http://athenaticket.ddns.net/api/ContactWithCard",
        "method": "GET",
        "headers": {
          "Content-Type": "application/json",
          "Cache-Control": "no-cache",
          "Postman-Token": "8931b777-7999-473d-ad2f-9d6352292007"
        }
      }
      
      $.ajax(settings).done(function (response) {
        //after ajax is complete, deal with the result data...
        $('#getContactsWithCards_TBody').empty();
        response.forEach(element => {
            $('#getContactsWithCards_TBody').append('<tr><td>' + element.contact_FirstName + '</td><td>' + element.contact_LastName + '</td><td>' + element.contact_IdCardNum + '</td><td>' + element.card_RegisteredOn + '</td><td>' + element.card_ChargeExpiresOn + '</td></tr>');
        });
      });
}