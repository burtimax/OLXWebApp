using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLXWebApp.Database.DbModels
{
    public class OLXAccount
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int UserOwnerId { get; set; }
    }
}
