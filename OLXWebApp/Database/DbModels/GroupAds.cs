using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLXWebApp.Database.DbModels
{
    public class GroupAds
    {
        //int Id(PK)
        //string Name
        //string Photos(строка с идшниками фотографий которые принадлежат группе объявлений)
        ////string Cities (строка со списком городов, где можно будет публиковать объявления)
        //datetime CreateTime
        //string Status(stopped, active, deleted)
        //int OwnerOLXAccountId(FK OXLAccountId)

        public int Id { get; set; }
        public string Name { get; set; }
        public string Cities { get; set; }
        public string Status { get; set; } //(stopped, active, deleted)
        public int OwnerOLXAccountId { get; set; }
        public DateTime CreateTime { get; set; }

	}
}
