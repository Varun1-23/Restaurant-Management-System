using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class RestaurantDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public long StateId { get; set; }
        public virtual StateDto State { get; set; }
        public long DistrictId { get; set; }
        public virtual DistrictDto District { get; set; }
        public long LocationId { get; set; }
        public virtual LocationDto Location { get; set; }
        public bool IsActive { get; set; }
        public string Cost { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public string License { get; set; }
        public bool IsApproved { get; set; }
        public string RejectionReason { get; set; }
    }
}
