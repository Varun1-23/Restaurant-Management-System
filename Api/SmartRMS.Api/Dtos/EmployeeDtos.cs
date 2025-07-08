using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }

        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
        public long LocationId { get; set; }
        public virtual LocationDto Location { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }

    }
}
