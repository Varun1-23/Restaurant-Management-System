using SmartRMS.api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartRMS.Api.Dtos
{
    public class CustomerAddressDto
    {
        public long Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Pincode { get; set; }
        public string LocationName { get; set; }

        public long StateId { get; set; }
        public virtual StateDto State { get; set; }
        public long DistrictId { get; set; }
        public virtual DistrictDto District { get; set; }
        public long LocationId { get; set; }
        public virtual LocationDto Location { get; set; }
        public long CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }
    }
}