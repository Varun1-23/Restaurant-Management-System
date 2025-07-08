using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class Restaurant
    {
        public long Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Photo { get; set; }
        [StringLength(100)]
        public string MobileNo { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public long StateId { get; set; }
        public virtual State State { get; set; }
        public long DistrictId { get; set; }
        public virtual District District { get; set; }
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string Cost { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(250)]
        public string License { get; set; }
        public bool IsApproved { get; set; }
        public string RejectionReason { get; set; }
    }
}
