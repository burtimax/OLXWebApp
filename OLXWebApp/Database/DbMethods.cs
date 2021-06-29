using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLXWebApp.Database.Context;
using OLXWebApp.Database.DbModels;

namespace OLXWebApp.Database
{
    public class DbMethods : IDbMethods
    {
        private ApplicationContext db;

        public DbMethods(ApplicationContext appContext)
        {
            this.db = appContext;
        }

        public void AddUser(User u)
        {
            throw new NotImplementedException();
        }
    }
}