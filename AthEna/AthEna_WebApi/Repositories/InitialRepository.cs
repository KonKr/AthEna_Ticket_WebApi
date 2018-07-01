using AthEna_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public abstract class InitialRepository
    {
        //Initialize a repostiory where the db context
        //is defined. Every repository which has to use
        //the database can inherit from this abstract class.
         
        public AthEnaDBContext db;
        public InitialRepository()
        {
            db = new AthEnaDBContext();
        }       

    }
}
