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

        public List<CardOutgoingViewModel> GetAllCards()
        {
            try
            {
                var cardsList = db.Cards.Select(s => new CardOutgoingViewModel
                {
                    CardId = s.CardId,
                    ChargeExpiresOn = s.ChargeExpiresOn,
                    ContactId = s.ContactId,
                    lastRecharedOn = s.LastRechargedOn,
                    RegisteredOn = s.RegisteredOn,
                }).ToList();
                return cardsList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CardOutgoingViewModel GetCard(Guid cardId)
        {
            try
            {
                var cardRequested = db.Cards.Where(w => w.CardId == cardId).Select(s => new CardOutgoingViewModel
                {
                    CardId = s.CardId,
                    ChargeExpiresOn = s.ChargeExpiresOn,
                    ContactId = s.ContactId,
                    lastRecharedOn = s.LastRechargedOn,
                    RegisteredOn = s.RegisteredOn,
                }).FirstOrDefault();
                return cardRequested;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateNewCard(Card newCard)
        {
            try
            {
                var cardToAdd = new Card()//attempt to create a new object...
                {
                    CardId = Guid.NewGuid(),
                    ContactId = newCard.ContactId,
                    RegisteredOn = DateTime.Now,
                    LastRechargedOn = DateTime.Now,
                    ChargeExpiresOn = DateTime.Now,
                };

                db.Add(cardToAdd);
                var savingResult = db.SaveChanges();

                if (savingResult != 0)//check if an error has occured
                    return cardToAdd.ContactId;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public dynamic GetContactsWithCards()
        {
            try
            {
                var cons = db.Contacts.ToList();
                var cards = db.Cards.ToList();

                var joinedList = cons.Join(cards, a => a.ContactId, b => b.ContactId, (a, b) => new { a.ContactId, a.FirstName, a.LastName, b.CardId, b.ChargeExpiresOn }).ToList();
                return joinedList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic ValidateOnBus(ValidateCard_Bus_ViewModel validationInfo)
        {
            try
            {
                //verify the card is still valid...
                var cardExpirationDate = db.Cards.Where(w => w.CardId == validationInfo.ValidatingCardId).Select(s => s.ChargeExpiresOn).FirstOrDefault();
                if (cardExpirationDate < DateTime.Now)
                    return false; //if the ticket has expired return appropriate error message...

                //create new validation entry...
                var validationEntryToAdd = new ValidationActivity()
                {
                    BusId = validationInfo.ValidatingAtBusId,
                    CardId = validationInfo.ValidatingCardId.Value,
                    RouteId = validationInfo.RouteId,
                    ValidatedOn = DateTime.Now,
                    ValidatedAt = null //validating card on a bus, there is no need to specify if it took place on embarkation or disembarkation... 
                };

                db.Add(validationEntryToAdd);
                var savingResult = db.SaveChanges();
                if (savingResult != 0)//check if an error has occured...
                    return cardExpirationDate;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        


    }
}
