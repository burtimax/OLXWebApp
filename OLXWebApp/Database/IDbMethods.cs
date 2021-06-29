using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLXWebApp.Database.DbModels;

namespace OLXWebApp.Database
{
    public interface IDbMethods
    {
        void AddUser(User u);
    }
}
