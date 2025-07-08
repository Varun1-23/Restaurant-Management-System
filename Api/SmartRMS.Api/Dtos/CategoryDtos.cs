using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRMS.api
{
    public class CategoryDto
    {
        public long Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }       
        public string Photo { get; set; }
        public long RestaurantId { get; set; }
        public virtual RestaurantDto Restaurant { get; set; }
        public bool IsActive { get; set; }
    }
}
