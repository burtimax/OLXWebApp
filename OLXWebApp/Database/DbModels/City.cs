using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace OLXWebApp.Database.DbModels
{
    public class City
    {
        //int Id(PK)
        //int RegionId
        //string Name
        //string Municipality

        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name{ get; set; }
        public string Municipality{ get; set; }


    }
}
