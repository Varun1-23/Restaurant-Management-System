using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }
        public long StateId { get; set; }
        public virtual StateDto State { get; set; }
        public long DistrictId { get; set; }
        public virtual DistrictDto District { get; set; }
        public long LocationId { get; set; }
        public virtual LocationDto Location { get; set; }
        public bool IsActive { get; set; }
    }
}
