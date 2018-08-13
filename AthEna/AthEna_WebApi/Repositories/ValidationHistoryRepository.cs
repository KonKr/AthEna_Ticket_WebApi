using AthEna_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public class ValidationHistoryRepository : InitialRepository
    {
        public List<ValidationActivity> GetValidationActivityHistory_SpecificUser(int daysDepth, String idCardNum)
        {
            try
            {
                //First extract the contactId under given IdCardNum...
                var contactId = db.Contacts.Where(w => w.IdCardNum == idCardNum).Select(s => s.ContactId).FirstOrDefault();

                //Then extract the associated card id under given idCardNum...
                var cardId = db.Cards.Where(w => w.ContactId == contactId).Select(s => s.CardId).FirstOrDefault();

                var daysDepthWithMinus = daysDepth * -1;

                var validationActivityHistory = db.ValidationActivity.Where(w => w.CardId == cardId && w.ValidatedOn >= DateTime.Now.AddDays(daysDepthWithMinus)).ToList();
                return validationActivityHistory;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ValidationActivity> GetValidationActivityHistory(int daysDepth)
        {
            try
            {
                var daysDepthWithMinus = daysDepth * -1;
                var validationActivityHistory = db.ValidationActivity.Where(w => w.ValidatedOn >= DateTime.Now.AddDays(daysDepthWithMinus)).ToList();
                return validationActivityHistory;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
