using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class CuisineBooking
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(100)]
        public string Time { get; set; }
 
        public bool IsActive { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public long CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
