using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class LocationDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long StateId { get; set; }
        public virtual StateDto State { get; set; }
        public long DistrictId { get; set; }
        public virtual DistrictDto District { get; set; }
    }
}
