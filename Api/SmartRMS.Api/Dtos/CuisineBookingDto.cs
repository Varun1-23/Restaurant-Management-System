using SmartRMS.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class CuisineBookingDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }

        public bool IsActive { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }
        public long CuisineId { get; set; }
        public virtual CuisineDto Cuisine { get; set; }
        public long CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }
    }
}