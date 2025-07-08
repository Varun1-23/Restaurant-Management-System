using SmartRMS.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class FilterDto
    {
        public long CustomerId { get; set; }
        public long RestaurantId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}