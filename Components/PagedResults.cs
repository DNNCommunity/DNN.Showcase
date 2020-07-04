
using System.Collections.Generic;

namespace Dnn.Showcase
{
    public class PagedResults<T>
    {
        /// <summary>
        /// The page number this page represents. 
        /// </summary>
        public int page_number { get; set; }

        /// <summary> 
        /// The size of this page. 
        /// </summary> 
        public int page_size { get; set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        public int total_pages { get; set; }

        /// <summary> 
        /// The total number of items available. 
        /// </summary> 
        public int total_items { get; set; }

        /// <summary> 
        /// The items this page represents. 
        /// </summary> 
        public IEnumerable<T> items { get; set; }
    }
}