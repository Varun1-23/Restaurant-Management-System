using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class Product
    {
        public long Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Photo { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public float Price { get; set; }
        public float OfferPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
