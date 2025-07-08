using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }
        public long CategoryId { get; set; }
        public virtual CategoryDto Category { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float OfferPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
