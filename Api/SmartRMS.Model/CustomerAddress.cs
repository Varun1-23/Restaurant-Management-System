using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.Model
{
    public class CustomerAddress
    {
        public long Id { get; set; }
        [StringLength(250)]
        public string Address1 { get; set; }
        [StringLength(250)]
        public string Address2 { get; set; }
        [StringLength(100)]
        public string Pincode { get; set; }

        public long StateId { get; set; }
        public virtual State State { get; set; }
        public long DistrictId { get; set; }
        public virtual District District { get; set; }
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
