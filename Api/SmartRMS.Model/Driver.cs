using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class Driver
    {
        public long Id { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string MobileNo { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Photo { get; set; }

        public DateTime IdExpiry { get; set; }
        [StringLength(200)]
        public string IdPhoto { get; set; }

        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
    }
}
