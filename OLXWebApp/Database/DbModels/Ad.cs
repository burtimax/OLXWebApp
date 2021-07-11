using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLXWebApp.Database.DbModels
{
    public class Ad
    {
        //int Id(PK)
        //int OwnerGroupAdsId(FK GroupAds)
        //string Title
        //string Description
        //int PhotoCount(кол-во фотографий)
        //string Photos(список идшников через запятую)
        //int CityId(FK City)
        //string Status(stopped, active, deleted)
        //datetime CreateTime

        public int Id { get; set; }
        public int OwnerGroupAdsId { get; set; }
        public string Title { get; set; }
        public string Descroption { get; set; }
        public string Photos { get; set; }
        public int PhotoCount { get; set; }
        public int CityId { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
