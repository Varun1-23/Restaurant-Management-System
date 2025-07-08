using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class Review
    {
        public long Id { get; set; }
  
        public float Rating { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
       
        public long RestaurantId { get; set; }
        public long CustomerId { get; set; }
        public long BookingId { get; set; }
      
        public bool IsActive { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
