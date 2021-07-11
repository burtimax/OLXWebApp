using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLXWebApp.Database.DbModels
{
    public class AdAttributes
    {

        //    int Id(PK)
        //    int OwnerAdId(FK Ad)
        //    string AttributeName
        //    string AttributeValue

        public int Id { get; set; }
        public int OwnerAdId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

    }
}
