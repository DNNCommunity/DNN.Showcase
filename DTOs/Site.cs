using System;
using System.Collections.Generic;

namespace Dnn.Showcase
{
    public class SiteDTO
    {
        // initialization
        public SiteDTO()
        {
        }

        // public properties
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public string thumbnail { get; set; }
        public int user_id { get; set; }
        public DateTime created_date { get; set; }
        public List<SiteCategoryDTO> site_categories { get; set; }
    }

}