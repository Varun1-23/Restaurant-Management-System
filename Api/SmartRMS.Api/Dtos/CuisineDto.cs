using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartRMS.api
{
    public class CuisineDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float NoOfPeople { get; set; }
        public bool IsActive { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }
    }
}