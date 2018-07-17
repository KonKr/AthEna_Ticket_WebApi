using AthEna_WebApi.Models;
using AthEna_WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public class CardsRepository : InitialRepository
    {
        public dynamic RechargeCard(RechargeCard_ViewModel rechargeCardInfo)
        {
            try
            {
                //check the card's existance. If it DOES exist then take the record, otherwise return false...
                var cardRecordToUpdate = db.Cards.Where(w => w.CardId == rechargeCardInfo.CardId)
                                                 .FirstOrDefault();

                if (cardRecordToUpdate == null) //if no card record is found under given cardId return false...
                    return false;

                //retrieve the number of days to extend based on the amount of money given...
                var daysToExtend_TicketExpirationDate = Convert_EurosToDays(rechargeCardInfo.ChargeEuros);
                if (daysToExtend_TicketExpirationDate == 0)
                    return false;

                //update necessary fields...
                cardRecordToUpdate.LastRechargedOn = DateTime.Now;

                //if the expiration date is not 
                cardRecordToUpdate.ChargeExpiresOn = cardRecordToUpdate.ChargeExpiresOn > DateTime.Now ? cardRecordToUpdate.ChargeExpiresOn.AddDays(daysToExtend_TicketExpirationDate) : DateTime.Now.AddDays(daysToExtend_TicketExpirationDate);



                db.Cards.Update(cardRecordToUpdate);
                var savingResult = db.SaveChanges();

                if (savingResult != 0)//check if an error has occured...
                    return new CardChargeExpirationDate_ViewModel() { ChargeExpirationDate = cardRecordToUpdate.ChargeExpiresOn };
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ValidateCard_OnBus(ValidateCard_Bus_ViewModel validationInfo)
        {
            try
            {
                //create new validation entry...
                var validationEntryToAdd = new ValidationActivity()
                {
                    CardId = validationInfo.ValidatingCardId.Value,
                    BusId = validationInfo.ValidatingAtBusId,
                    RouteId = validationInfo.RouteId,
                    ValidatedOn = DateTime.Now,
                    ValidatedAt = null //validating card on a bus, there is no need to specify if it took place on embarkation or disembarkation... 
                };

                //add new entry...
                db.Add(validationEntryToAdd);
                var savingResult = db.SaveChanges();

                //return result...
                if (savingResult != 0)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic ValidateOnMetro(ValidateCard_Metro_ViewModel validationInfo)
        {
            try
            {
                //in case of disembarkation.. check if an embarkation validation has taken place before...
                if (validationInfo.ValidationOnEmbarkation == false)
                {
                    var validationActivity_LastRecord_For_SpecificCard = db.ValidationActivity.Take(100).Where(w => w.CardId == validationInfo.ValidatingCardId).OrderByDescending(o => o.ValidatedOn).FirstOrDefault();
                    if (!GetBoolForEmbarkation_Disembarkation(validationActivity_LastRecord_For_SpecificCard.ValidatedAt))
                        return 1; //return 1 in order to finaly return the generic error message...
                }

                //verify the card is still valid...
                var cardExpirationDate = db.Cards.Where(w => w.CardId == validationInfo.ValidatingCardId).Select(s => s.ChargeExpiresOn).FirstOrDefault();
                if (cardExpirationDate < DateTime.Now)
                    return false; //if the ticket has expired return appropriate error message...

                //create new validation entry...
                var validationEntryToAdd = new ValidationActivity()
                {
                    StationId = validationInfo.ValidatingAtStationId,
                    CardId = validationInfo.ValidatingCardId.Value,
                    ValidatedOn = DateTime.Now,
                    ValidatedAt = GetEnumForEmbarkation_Disembarkation(validationInfo.ValidationOnEmbarkation)
                };

                db.Add(validationEntryToAdd);
                var savingResult = db.SaveChanges();
                if (savingResult != 0) //check if an error has occured...
                    return cardExpirationDate;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Methods to use inside API's repository's methods...

        private int GetEnumForEmbarkation_Disembarkation(bool validationOnEmbarkation)
        {
            switch (validationOnEmbarkation)
            {
                case true:
                    return 1;
                case false:
                    return 0;
                default:
                    return 0;
            }
        }

        private bool GetBoolForEmbarkation_Disembarkation(int? validationOnEmbarkation)
        {
            switch (validationOnEmbarkation.Value)
            {
                case 1:
                    return true;
                case 0:
                    return false;
                default:
                    return false;
            }
        }

        public int Convert_EurosToDays(int Charge_Euros)
        {
            //check the amount of charge the use wishes to pay in order to recharge their card.
            //This method returns the number of the days the card will be valid. The ammounts are standards.
            try
            {
                switch (Charge_Euros)
                {
                    case 30:
                        return 30;
                    case 60:
                        return 60;
                    case 200:
                        return 365;
                    default:
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CardValidity_Model CheckCardsValidity(Guid? validatingCardId)
        {
            try
            {
                var cardExpirationDate = db.Cards.Where(w => w.CardId == validatingCardId).Select(s => s.ChargeExpiresOn).FirstOrDefault();
                return new CardValidity_Model
                {
                    Validity = cardExpirationDate >= DateTime.Now ? true : false,
                    ExpirationDate = cardExpirationDate
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
