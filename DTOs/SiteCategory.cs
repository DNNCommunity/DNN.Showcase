using System;

namespace Dnn.Showcase
{
    public class SiteCategoryDTO
    {
        // initialization
        public SiteCategoryDTO()
        {
        }

        // public properties
        public int id { get; set; }
        public int site_id { get; set; }
        public int category_id { get; set; }

        public string site_name { get; set; }
        public string category_name { get; set; }
    }

}