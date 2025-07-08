using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.api
{
    public class ReviewDto
    {
        public long Id { get; set; }

        public float Rating { get; set; }
        public string Description { get; set; }

        public long RestaurantId { get; set; }
        public long CustomerId { get; set; }
        public long BookingId { get; set; }

        public bool IsActive { get; set; }

        public virtual RestaurantDto Restaurant { get; set; }
        public virtual BookingDto Booking { get; set; }
        public virtual CustomerDto Customer { get; set; }
    }
}