using SmartRMS.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class DriverDto
    {
        public long Id { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }

        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public long LocationId { get; set; }
        public virtual LocationDto Location { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime IdExpiry { get; set; }
        public string IdPhoto { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }

    }
}